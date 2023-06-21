# Design Details for Formatting IEEE754 <!-- omit in toc -->

This document covers the implementation details of the method `DoubleToString`,
or more specifically, it's helper methods `NumberFormatter`. I wrote it as it
took me a few hours to figure it out, despite it being "relatively" simple.
There are still a surprising number of decisions that must be made for it to be
successful.

- [1. Source Origins](#1-source-origins)
- [2. IEEE 754 Standard](#2-ieee-754-standard)
  - [2.1. Double Precision Floating Point Numbers](#21-double-precision-floating-point-numbers)
  - [2.2. Summary of Precision](#22-summary-of-precision)
  - [2.3. Special Numbers](#23-special-numbers)
    - [2.3.1. Zero](#231-zero)
    - [2.3.2. Subnormal Numbers](#232-subnormal-numbers)
    - [2.3.3. Infinity and Not a Number](#233-infinity-and-not-a-number)
- [3. Breakdown of the DoubleFormatter.Convert function](#3-breakdown-of-the-doubleformatterconvert-function)
  - [3.1. Precision of Floats](#31-precision-of-floats)
  - [3.2. Breakdown from the Mantissa to an Integer with a Decimal Offset](#32-breakdown-from-the-mantissa-to-an-integer-with-a-decimal-offset)
  - [3.3. Handling of all Cases](#33-handling-of-all-cases)
    - [3.3.1. Zero](#331-zero)
    - [3.3.2. Inf and NaN](#332-inf-and-nan)
    - [3.3.3. Normal Form](#333-normal-form)
    - [3.3.4. Subnormal](#334-subnormal)
  - [3.4. Calculation of the value MBT](#34-calculation-of-the-value-mbt)
    - [3.4.1. Multiplication](#341-multiplication)
    - [3.4.2. Increasing Precision](#342-increasing-precision)
    - [3.4.3. Rounding Error](#343-rounding-error)
    - [3.4.4. Overflow for Multiplication](#344-overflow-for-multiplication)
    - [3.4.5. Solving for MBT and ET](#345-solving-for-mbt-and-et)
      - [3.4.5.1. Example, A Value of 1](#3451-example-a-value-of-1)
      - [3.4.5.2. Example, A Value of 2-ε](#3452-example-a-value-of-2-ε)
      - [3.4.5.3. Example, Higher Orders of 2](#3453-example-higher-orders-of-2)
      - [3.4.5.4. Example, Lower Orders of 2](#3454-example-lower-orders-of-2)
    - [3.4.6. Generating the Table MBT and ET](#346-generating-the-table-mbt-and-et)
      - [3.4.6.1. Iterative Approach](#3461-iterative-approach)
      - [3.4.6.2. Iterative Approach Details for Increasing Exponent](#3462-iterative-approach-details-for-increasing-exponent)
      - [3.4.6.3. Iterative Approach Details for Decreasing Exponent](#3463-iterative-approach-details-for-decreasing-exponent)
      - [3.4.6.4. Further Work](#3464-further-work)
    - [3.4.7. Subnormal Calculations from the Table](#347-subnormal-calculations-from-the-table)
- [4. Appendix](#4-appendix)
  - [4.1. Long Multiplication](#41-long-multiplication)
  - [4.2. Calculation of the Maximum Value of MBT](#42-calculation-of-the-maximum-value-of-mbt)
  - [4.3. Calculation of the Mantissa and Exponent Table](#43-calculation-of-the-mantissa-and-exponent-table)

## 1. Source Origins

The origin of the code is from the Mono repository

* [`mcs/class/corlib/System/NumberFormatter.cs`](https://github.com/mono/mono/blob/48e112d652335da23dd1b4d5f3227e54b74d0814/mcs/class/corlib/System/NumberFormatter.cs)
  * commit `48e112d652335da23dd1b4d5f3227e54b74d0814` commit date 07/Aug/2014
  * To date (11/Jun/2023) no other relevant changes have been made.

and the look up tables

* [`mono/metatdata/number-formatter.h`](https://github.com/mono/mono/blob/48e112d652335da23dd1b4d5f3227e54b74d0814/mono/metadata/number-formatter.h)
  * commit `9af0b8a2224e2eb6a05f00607805a180b1c67a35` commit date 27/May/2008
  * To date (11/Jun/2023) no other relevant changes have been made.

The main question is how does the function `DoubleFormatter.Convert(double
value, int defPrecision)` work.

Once this is satisfactorily answered, one can write similar algorithms for lower
precision floats (e.g. a 16-bit precision `HalfFloat`) if there is no mechanism
to convert from a `HalfFloat` to a `double`, or even higher precision floats by
using big integer arithmetic (the work is more arduous and slower but the
principle remains the same).

## 2. IEEE 754 Standard

### 2.1. Double Precision Floating Point Numbers

We recall the IEEE 754 standard has the following bit structure for a floating
point double precision (64-bit) number.

```text
  63    62 ...... 52   51 ................................................. 0
 +---+ +------------+ +------------------------------------------------------+
 | S | |  Exponent  | |                       Mantissa                       |
 +---| +------------+ +------------------------------------------------------+
```

The `S` sign bit is 1 bit, is either -1 (if set), or 1 (if clear).

The `E` exponent is 11 bits, with a bias of 1023.

The `m` mantissa is `M` = 52 bits.

The format for a floating point number is represented as

```text
value = S * (1 + m / 2^M) * 2 ^ (E - bias)                             (Eq. 1)
```

so that for a double:

```text
value = S * (1 + m / 2^52) * 2 ^ (E - 1023)
```

The goal of the algorithm is to represent this mathematical form as a human
readable number.

### 2.2. Summary of Precision

| Precision           | Exponent (bits) | Mantissa (bits) |
| ------------------- | --------------: | --------------: |
| Octuple (256-bit)   |              19 |             236 |
| Quadruple (128-bit) |              15 |             112 |
| Double (64-bit)     |              11 |              52 |
| Single (32-bit)     |               8 |              23 |
| Half (16-bit)       |               5 |              10 |

### 2.3. Special Numbers

#### 2.3.1. Zero

It is observed that it is possible to have +0 or -0 in IEEE 754 representation
when `E == 0` and `m == 0`.

#### 2.3.2. Subnormal Numbers

An exponent value `E == 0` implies subnormal floating point numbers, where a
loss of precision is accepted for smaller possible numbers in the same
representation. In this case, a subnormal number is considered as:

```text
value = (0 + m / 2^M) * 2 ^ (1 - bias)                                 (Eq. 1b)
```

#### 2.3.3. Infinity and Not a Number

If `E == 1*` (all bits are set, so 2047 for double, or 255 for single), then the
following conditions also apply:

* If `m = 0`, the number is +/- infinity
* If `m != 0`, the number is +/- NaN

## 3. Breakdown of the DoubleFormatter.Convert function

### 3.1. Precision of Floats

The default precision for a double precision number is _15_ for a double
precision float. This arises that the value `2^52 + (2^52 - 1)` has a value of
`0x1F_FFFF_FFFF_FFFF` which in decimal is 9,007,199,254,740,991, i.e. between 15
and 16 digits of precision.

The default precision for a single precision number is _7_ for a single
precision float. We see that `2^23 + (2^23 - 1)` has the value `0xFF_FFFF` which
in decimal is 16,777,215, i.e. between 7 and 8 digits of precision.

Note, the value we're calculating above is `1.m` where `m = 2^M-1`.

### 3.2. Breakdown from the Mantissa to an Integer with a Decimal Offset

The algorithm in the `Convert` function is essentially calculating only the
value:

```text
value = S * (1 + m / 2^52) * 2 ^ (E - 1023)
```

using a double precision (the upscaling of a float to a double results in no
loss of data and the time for the conversion is negligible that two
implementations are not required).

It is quite simply solving for:

```text
(1 + m / 2^52) * 2 ^ (E - 1023) = (2^52 + m) * MBT[E] * 10 ^ ET[E]     (Eq. 2)
```

The representation on the left is the IEEE 754 value. The representation on the
right is treating the mantissa `m` as an integer. We want to solve for what
`MBT[E]` and `ET[E]` should be, as these are implemented as lookup tables.

Thus, we're converting from a base 2 numeral system, to a base 10 numeral
system, which is easier for humans to read.

### 3.3. Handling of all Cases

#### 3.3.1. Zero

The first entrance into the function is handling the special case that the value
is zero (`E == M == 0`).

```csharp
_positive = bits >= 0;
bits &= long.MaxValue;
if (bits == 0) {
    _decPointPos = 1;
    _digitsLen = 0;
    return;
}
```

#### 3.3.2. Inf and NaN

This case handles when `E == 1*`, i.e. 0x7FF.

```csharp
int e = (int)(bits >> DoubleBitsExponentShift);
long m = bits & DoubleBitsMantissaMask;  // m is the lower 52 bits
if (e == DoubleBitsExponentMask) {       // If (e == 0x7FF)
    _NaN = m != 0;                       //  m != 0 => NaN
    _infinity = m == 0;                  //  m == 0 => Inf (see also sign)
    return;
}
```

#### 3.3.3. Normal Form

We see that in the case of normal numbers, we now calculate `m' = 1.m`, that is:

```text
m' = 2^52 + m
```

Note that the implementation from Mono multiplies `m'` by 10, and sets
`expAdjust` to be -1 to later shift the decimal point to the left by one.

```csharp
m = (m + DoubleBitsMantissaMask + 1) * 10;
expAdjust = -1;
```

I believe this is done to provide a small amount of extra precision when
multiplying two 64-bit values.

#### 3.3.4. Subnormal

A subnormal number must be multiplied by units of 10 (the units of 10 come about
as we're solving later for `ET[E]` on the right of Eq. 2) so that when later
multiplying to solve Eq. 2, we don't lose precision.

```csharp
if (e == 0) {
    // This is a so-called "subnormal" floating point number. We lose precision
    // so that we can have even smaller numbers.

    // We need 'm' to be large enough so we won't lose precision.
    e = 1;
    int scale = Numbers.CountDigits(m);
    if (scale < DoubleDefPrecision) {
        expAdjust = scale - DoubleDefPrecision;
        m *= GetTenPowerOf(-expAdjust);
    }
}
```

The function `Numbers.CountDigits` just counts how many digits (base 10) there
is in `m`, and we multiply the value to make as much use as the available bits
allow in a 64-bit long. The value `expAdjust` is decreased, to remember that the
value before solving was already scaled.

Here, `m' = 0.m`.

### 3.4. Calculation of the value MBT

We want to now solve for

```text
(1 + m / 2^52) * 2 ^ (E - 1023) = m' * MBT[E] * 10 ^ (ET[E]-n)     (Eq. 2)
```

The value of `n` is the adjustment scaling `expAdjust` earlier. Note that `m'`
and `MBT[E]` are both 64-bit values, and multiplying these results in a 128 bit
value.

Natively, .NET only has 64-bit integer types, so the result must be placed in a
64-bit long. The lower 64-bits shall be discarded. If we rewrite Eq. 2 for this
fact:

```text
                                       MBT[E]
(1 + m / 2^52) * 2 ^ (E - 1023) = m' * ------ * 10 ^ (ET[E]-n)     (Eq. 2b)
                                        2^64
```

#### 3.4.1. Multiplication

The multiplication is done by this code:

```csharp
ulong lo = (uint)m;
ulong hi = (ulong)m >> 32;
ulong lo2 = Formatter_MantissaBitsTable[e];
ulong hi2 = lo2 >> 32;
lo2 = (uint)lo2;
ulong mm = hi * lo2 + lo * hi2 + ((lo * lo2) >> 32);
long res = (long)(hi * hi2 + (mm >> 32));
```

If we remind ourselves of multiplication as in the Appendix, we see that `res`
is the upper 64 bit result bits 127-64. `mm` is the result of bits 95-32 (the
lower 32-bits are discarded seen in the shift of `lo*lo2`). The bits discarded
is precision which is not needed (see the discussion at the start on precision
based on the size of the mantissa).

We must be careful, as seen later, as the variable `mm` may result in an
arithmetic overflow.

#### 3.4.2. Increasing Precision

The algorithm will multiple the result `res` by 10 every time that there is less
than 17 decimal digits, pushing precision from `mm` up into `res`.

```csharp
while (res < SeventeenDigitsThreshold) {
    mm = (mm & uint.MaxValue) * 10;
    res = res * 10 + (long)(mm >> 32);
    expAdjust--;
}
```

Of course, with every multiplication by 10, we must be sure to shift the decimal
point by one to correct in the final result.

When multiplying `mm`, we discard the bits 95-64 as they're in the previous
calculation for `res`, which will be multiplied in the next separate step. From
`mm` bits 63-32 are multiplied and may contribute in further loops or the next
step for the rounding error.

#### 3.4.3. Rounding Error

if bit 63 of the result is set, then we increment the least significant digit in
`res` by one.

```csharp
if ((mm & 0x80000000) != 0) res++;
```

#### 3.4.4. Overflow for Multiplication

One must be careful that there could be an arithmetic overflow with the
multiplication and addition of the values of `mm` and `res`. Indeed, the commit
[9af0b8a](https://github.com/mono/mono/commit/9af0b8a2224e2eb6a05f00607805a180b1c67a35)
from Mono did make such a correction.

We know that the value of an adjusted `m'` is about 57 bits (52-bit mantissa, 1
bit for the prefix, then multiplied by 10). If we consider the maximum possible
value for `MBT[E]`, then:

```text
m' = 0x13F_FFFF_FFFF_FFF6
MBT = 0xFFFF_FFFF_FFFF_FFFF

mm = 0x013F_FFFF * 0xFFFF_FFFF +     | = 0x   013F_FFFE_FEC0_0001 +
     0xFFFF_FFFF * 0xFFFF_FFF6 +     |   0x   FFFF_FFF5_0000_000A +
     0xFFFF_FFFF * 0xFFFF_FFF6 >> 32 |   0x             FFFF_FFF5
                                     |      ---------------------
                                     |   0x 1 003F FFF4 FEC0 0000
```

It's obvious that the value `MBT` may not be `0xFFFF_FFFF_FFFF_FFFF` else an
overflow occurs. See the Appendix for a binary search algorithm that determines
what the maximum value of `MBT` may be, with the maximum value of `m' =
0x13F_FFFF_FFFF_FFF6`. It turns out, that `MBT` may not be more than
`FFFFFFFF_000008CC`.

Note that the value of `res` can never overflow so long as `m'` is only 56-bits
(the result is 57 bits, even with the addition of the 32-bit portion from `mm`).

When testing generating the table, it appears that the limit chosen by the
authors of commit
[9af0b8a](https://github.com/mono/mono/commit/9af0b8a2224e2eb6a05f00607805a180b1c67a35)
is `0xFC000000_00000000`.

This gives as an opportunity for slightly better precision without overflowing.

#### 3.4.5. Solving for MBT and ET

Now we get to the point, where we can ask what the values of `MBT[E]` and
`ET[E]` should be. We observe the divisor of 2^64. That is a result of
discarding the lower 64-bit of the multiplication (keeping only `res`),
effectively dividing the result by 2^64.

For each value of `E` we want to choose a value of `MBT[E]` to be as large as
possible, such that the multiplications above don't overflow. For a 64-bit
integer, this will result in a precision required for the final result of a
double precision floating point number.

##### 3.4.5.1. Example, A Value of 1

Let's take the simple example of a double precision of 1.

* Sign `S` is zero (positive)
* `E = 1023` for an exponent 2^0 (`e + bias`, i.e. `0 + bias`)
* `m` is zero

such that from Eq. 2b

```text
                                MBT[E]
1 = (1 + 0/2^52) * 2^0 = 2^52 * ------ * 10 ^ (ET[E]-n)
                                 2^64
```

To choose `MBT[E]` to be as large as possible to fit within 64-bit

```text
    MBT[E]
1 = ------ * 10 ^ (ET[E]-n)
     2^12
```

i.e. choose `MBT[1023]` as 4,096,000,000,000,000,000 (having 62-bits) and
`ET[E]` as -15. Note, that 2^12 = 4,096, so the answer is 1.

##### 3.4.5.2. Example, A Value of 2-ε

Note the value of ε implies the smallest unit of precision possible.

```text
      ┌    2^52-1┐                      MBT[E]
2-ε = │1 + ------│ * 2^0 = (2^53 - 1) * ------ * 10 ^ (ET[E]-n)
      └     2^52 ┘                       2^64
```

See that `m' = 2^52 + 2^52 - 1 = 2^53 - 1`. We shouldn't rearrange the equation
(we might be tempted that dividing any 64-bit value by 2^64 is zero), as this
will result in losing precision. So we must do the calculation by hand:

```text
                    1FFFFF FFFFFFFF
                  38D7EA4C 68000000
-------- -------- -------- --------
                  67FFFFFF 98000000
            CFFFF 98000000
         38D7EA4B C72815B4
   71AFD 10A815B4
-------- -------- -------- --------
   71AFD 498CFFFF C72815B3 98000000
```

i.e. `0x7_1AFD_498C_FFFF` is 1,999,999,999,999,999.

##### 3.4.5.3. Example, Higher Orders of 2

It should be obvious in the previous examples, that every time we increase the
value of E by 1, we should double the value of `MBT[E]` such that we keep the
most significant 64-bits and adjust the exponent accordingly.

##### 3.4.5.4. Example, Lower Orders of 2

We would, as `E` decrements, halve the value of `MBT[E]`. As we get to one, it
would be 0.5 (2^-1), 0.25 (2^-2), 0.125 (2^-3), etc. and the exponent would
adjust accordingly.

#### 3.4.6. Generating the Table MBT and ET

##### 3.4.6.1. Iterative Approach

We can now generate the table for all values of `E ∈ [1023,2046]`. Start with

* `E = 1023`, with `MBT[E] = 4,096,000,000,000,000,000` and `ET[E] = -15` as
  seen with the example for the value `d = 1`.
* Increment `E`
  * `ET[E] = ET[E-1]`
  * `MBT[E] = MBT[E-1] * 2`
  * If the doubling results in an overflow, divide by 10 and decrement `ET[E]`.

To generate the table for all values of `E ∈ [0, 1022]`

* Start with `E = 1023`
* Decrement `E`
  * `ET[E] = ET[E-1]`
  * `MBT[E] = MBT[E-1] / 2`
  * Multiply `MBT[E]` by 10 and increment `ET[E]`, unless the multiplication
    results in an overflow.

##### 3.4.6.2. Iterative Approach Details for Increasing Exponent

To avoid loss of precision with the divide by 10, higher precision than 64-bit
is required.

Let's take when the mantissa `m` is zero, so that `m' = 2^52`.

```text
                        MBT[E]
2 ^ (E - 1023) = 2^52 * ------ * 10 ^ (ET[E]-n)
                         2^64
```

Let's assume `n = 0` for the normal case (and that `m'` is not multiplied by 10):

```text
MBT[E] * 10 ^ ET[E] = 2 ^ (E - 1023 + 12)
```

The problem arises when the exponent is 52 such that the right hand side (RHS)
no longer fits within a 64-bit integer. The LHS is treated as a single entity,
we choose both tables to be the largest possible value that doesn't result in an
overflow when calculating `mm`.

|    e |    E |                   RHS |                   `MBT[E]` | `ET[E]` |
| ---: | ---: | --------------------: | -------------------------: | ------: |
|    0 | 1023 |                  4096 |  4,096,000,000,000,000,000 |     -15 |
|    1 | 1024 |                  8192 |  8,192,000,000,000,000,000 |     -15 |
|    2 | 1025 |                 16384 | 16,384,000,000,000,000,000 |     -15 |
|    3 | 1026 |                 32768 |  3,276,800,000,000,000,000 |     -14 |
|    4 | 1027 |                 65536 |  6,553,600,000,000,000,000 |     -14 |
|    5 | 1028 |                131072 | 13,107,200,000,000,000,000 |     -14 |
|    6 | 1029 |                262144 |  2,621,440,000,000,000,000 |     -13 |
|    7 | 1030 |                524288 |  5,242,880,000,000,000,000 |     -13 |
|    8 | 1031 |               1048576 | 10,485,760,000,000,000,000 |     -13 |
|    9 | 1032 |               2097152 |  2,097,512,000,000,000,000 |     -12 |
|   10 | 1033 |               4194304 |  4,194,304,000,000,000,000 |     -12 |
|   11 | 1034 |               8388608 |  8,388,608,000,000,000,000 |     -12 |
|   12 | 1035 |              16777216 | 16,777,216,000,000,000,000 |     -12 |
|   .. | .... |                   ... |                        ... |     ... |
|   48 | 1071 |   1152921504606846976 | 11,529,215,046,068,469,760 |      -1 |
|   49 | 1072 |   2305843009213693952 |  2,305,843,009,213,693,952 |       0 |
|   50 | 1073 |   4611686018427387904 |  4,611,686,018,427,387,904 |       0 |
|   51 | 1074 |   9223372036854775808 |  9,223,372,036,854,775,808 |       0 |
|   52 | 1075 |  18446744073709551616 |  1,844,674,407,370,955,161 |       1 |
|   53 | 1076 |  36893488147419103232 |  3,689,348,814,741,910,323 |       1 |
|   54 | 1077 |  73786976294838206464 |  7,378,697,629,483,820,646 |       1 |
|   55 | 1078 | 147573952589676412928 | 14,757,395,258,967,641,292 |       1 |
|   56 | 1079 | 295147905179352825856 |  2,951,479,051,793,528,258 |       2 |
|   56 | 1080 | 590295810358705651712 |  5,902,958,103,587,056,517 |       2 |

Precision loss is now observed with `E=1075`, so that if we simply double the
value at `E=1075` we'd naively get 3,689,348,814,741,910,322, which is
incorrect. Note that at this position:

```text
MBT[1075] * 10 ^ ET[1075] = 2 ^ 64
```

To correct for this, take the remainder when dividing by 10, so that on the next
doubling, double the remainder also, and if it is greater than 10, add one to
the value of `MBT`.

##### 3.4.6.3. Iterative Approach Details for Decreasing Exponent

Similar to above, we must take into account possible loss in precision due to
the iterative approach.

|    e |    E |                    RHS |                   `MBT[E]` | `ET[E]` |
| ---: | ---: | ---------------------: | -------------------------: | ------: |
|    0 | 1023 |                   4096 |  4,096,000,000,000,000,000 |     -15 |
|   -1 | 1022 |                   2048 |  2,048,000,000,000,000,000 |     -15 |
|   -2 | 1021 |                   1024 | 10,240,000,000,000,000,000 |     -16 |
|   -3 | 1020 |                    512 |  5,120,000,000,000,000,000 |     -16 |
|   .. | .... |                    ... |                        ... |     ... |
|      |  986 |                        |  2,980,232,238,769,531,250 |     -26 |
|      |  985 | 0.14901161193847656250 | 14,901,161,193,847,656,250 |     -27 |
|      |  984 |                        |  7,450,580,596,923,828,125 |     -27 |
|      |  983 |                        |  3,725,290,298,461,914,062 |     -27 |
|      |  982 |                        |  1,862,645,149,230,957,031 |     -27 |
|      |  981 |                        |  9,313,225,746,154,785,156 |     -28 |

Again, we must be careful about the loss of precision when halving a number,
e.g. from 982 to 981. When halving the number, if there is a remainder, we must
store it, such then when later multiplying the value by 10 to maximise the
amount of precision, the remainder is used to fill in the look up table.

##### 3.4.6.4. Further Work

Instead of using a double to maintain the extra precision, one could use integer
arithmetic for multiplication and division, discarding lower order bits as
needed. This would be needed if calculating tables for precisions greater than
64-bit.

#### 3.4.7. Subnormal Calculations from the Table

We notice for the conversion of subnormal values to decimal, the entry `MBT[1]`
is used. Let's compare the normal with the subnormal case.

* For the normal case when `E = 1` from Eq. 1:

  ```text
  value = 1 + m / 2^52 * 2 ^ (-1022)
  ```

  which in code is described as (with the multiplication by 10 increasing
  precision slightly, but can be effectively ignored):

  ```csharp
  m = (m + DoubleBitsMantissaMask + 1) * 10;
  expAdjust = -1;
  ```

* For the subnormal case when `E = 0` from Eq. 1b:

  ```text
  value = 0 + m / 2^52 * 2 ^ (-1022)
  ```

  which in code is described as (where the if statement is just adjusting the
  scale dynamically to maximise precision for later multiplication but can be
  effectively ignored):

  ```csharp
  e = 1;
  int scale = Numbers.CountDigits(m);
  if (scale < DoubleDefPrecision) {
      expAdjust = scale - DoubleDefPrecision;
      m *= Numbers.GetTenPowerOf(-expAdjust);
  }
  ```

The two are equivalent as if we use `MBT[1]` with the only difference being
`m'`. This means after the initial step we don't need to differentiate between
normal and subnormal numbers.

## 4. Appendix

### 4.1. Long Multiplication

Long multiplication of symbols of base `n` is calculated as

```text
        A   B
    x   C   D
  -----------
          D.B
+     D.A   0
+     C.B   0
+ C.A   0   0
-------------
C.A.n^2 + D.A.n + C.B.n + D.B
```

In school, the base is `n=10` and each A, B, C, D is a digit from 0-9. In 64-bit
multiplication, we must split digits into units of 32-bit symbols, and multiply
each, such that `n=2^32`.

So that if `MBT[E]` is split into `hi2` and `lo2`, and the value of `m'` is
split into `hi` and `lo`, each 32-bit, multiplication is:

```text
      hi  lo
     hi2 lo2
------------
(hi*hi2)<<64 + (lo2*hi + hi2*lo)<<32 + lo2*lo
```

We should also remember, that the result needs to be of size `n^2` (i.e. two
32-bit numbers will fit in a 64-bit result).

### 4.2. Calculation of the Maximum Value of MBT

This code can be found in the folder `TableGen`.

The following C# program (.NET 6.0) calculates and prints the largest possible
value of `MBT` that doesn't overflow when calculating `mm`.

```csharp
namespace IEEE754Limit;

class Program
{
    public static void Main(string[] args)
    {
        ulong lo = 0xffff_fff6;
        ulong hi = 0x13f_ffff;
        ulong lo2 = 0x0000_0000;
        ulong hi2 = 0x7fff_ffff;

        ulong scale = 0x100_0000_0000_0000;
        ulong olo2 = lo2;
        ulong ohi2 = hi2;
        while (true) {
            bool v = CheckOverflow(lo, hi, lo2, hi2, out ulong mm);
            if (v) {
                scale = scale >> 1;
                lo2 = olo2;
                hi2 = ohi2;
            } else {
                Console.WriteLine($"{hi:x8}_{lo:x8} * {hi2:x8}_{lo2:x8} = {mm:x}");
                olo2 = lo2;
                ohi2 = hi2;
            }

            do {
                v = Add(scale, ref lo2, ref hi2);
                if (v) {
                    scale >>= 1;
                    lo2 = olo2;
                    hi2 = ohi2;
                }
            } while (v);

            if (scale == 0) break;
        }
    }

    static bool CheckOverflow(ulong lo, ulong hi, ulong lo2, ulong hi2, out ulong mm)
    {
        checked {
            try {
                mm = hi * lo2 + lo * hi2 + ((lo * lo2) >> 32);
                return false;
            } catch (OverflowException) {
                mm = 0;
                return true;
            }
        }
    }

    static bool Add(ulong scale, ref ulong lo2, ref ulong hi2)
    {
        checked {
            try {
                lo2 += scale;
                if (lo2 > 0xFFFF_FFFF) {
                    hi2 += (lo2 >> 32);
                    lo2 = lo2 & 0xFFFF_FFFF;
                }
                return hi2 > 0xFFFF_FFFF;
            } catch (OverflowException) {
                return true;
            }
        }
    }
}
```

The last few lines of the output are:

```text
m'                  MBT                 mm
-----------------   -----------------   ----------------
013fffff_fffffff6 * feffffff_00000000 = fefffff50a00000a
013fffff_fffffff6 * ffffffff_00000000 = fffffff50000000a
013fffff_fffffff6 * ffffffff_00000800 = ffffffff00000009
013fffff_fffffff6 * ffffffff_00000880 = ffffffffa0000009
013fffff_fffffff6 * ffffffff_000008c0 = fffffffff0000009
013fffff_fffffff6 * ffffffff_000008c8 = fffffffffa000009
013fffff_fffffff6 * ffffffff_000008cc = ffffffffff000009
```

Remember, that `0x013F_FFFF_FFFF_FFF6` results from `10 * 0x1F_FFFF_FFFF_FFFF`,
as the algorithm multiplies by 10 and sets `expAdjust` before doing the
multiplications.

### 4.3. Calculation of the Mantissa and Exponent Table

This code can be found in the folder `TableGen`.

This section covers the code that can be used to reconstruct the original
tables. Note, the overflow is less than it could be.

```csharp
namespace IEEE754Limit
{
    using System.Text;

    internal static class GenerateMtb
    {
        //private const ulong MaxOverflow = 0xffffffff_000008cc;
        private const ulong MaxOverflow = 0xFC000000_00000000;
        private static readonly ulong[] mbt = new ulong[2047];
        private static readonly int[] et = new int[2047];

        public static void Run()
        {
            int e = 1023;
            mbt[e] = 4_096_000_000_000_000_000;
            et[e] = -15;

            CalculatePositiveExp(e);
            CalculateNegativeExp(e);
            DumpTable(mbt, "Formatter_MantissaBitsTable", 3);
            DumpTable(et, "Formatter_TensExponentTable", 12);
        }

        private static void CalculatePositiveExp(int e)
        {
            double rem = 0;
            for (int i = e + 1; i < mbt.Length; i++) {
                et[i] = et[i - 1];
                checked {
                    bool overflow = false;
                    try {
                        mbt[i] = mbt[i - 1] * 2;
                        if (mbt[i] >= MaxOverflow) {
                            overflow = true;
                        } else {
                            rem *= 2;
                            if (rem >= 10) {
                                mbt[i] += 1;
                                rem -= 10;
                            }
                        }
                    } catch (OverflowException) {
                        overflow = true;
                    }

                    if (overflow) {
                        // Multiply the previous value by 2, scale back factor of 10.
                        //  et is incremented by 1 to indicate we scaled back a factor of 10
                        et[i] = et[i - 1] + 1;
                        mbt[i] = mbt[i - 1] / 5;
                        rem = rem / 5 + mbt[i - 1] % 5 * 2;
                    }
                }
            }
        }

        private static void CalculateNegativeExp(int e)
        {
            double rem = 0;
            for (int i = e - 1; i > 0; i--) {
                et[i] = et[i + 1];
                checked {
                    mbt[i] = mbt[i + 1] / 2;
                    rem = (rem + mbt[i + 1] % 2) / 2;
                    try {
                        ulong newMbt = mbt[i] * 10;
                        if (newMbt < MaxOverflow) {
                            mbt[i] = newMbt;
                            rem *= 10;
                            if (rem >= 1) {
                                uint round = (uint)rem;
                                rem -= round;
                                mbt[i] += round;
                            }
                            et[i] -= 1;
                        }
                    } catch (OverflowException) {
                        /* We could multiply by 10 */
                    }
                }
            }
        }

        private const int CommentOffset = 90;

        private static void DumpTable<T>(T[] array, string nameof, int group)
        {
            StringBuilder sb = new StringBuilder();

            string typeName;
            if (typeof(T) == typeof(uint)) {
                typeName = "uint";
            } else if (typeof(T) == typeof(ulong)) {
                typeName = "ulong";
            } else if (typeof(T) == typeof(int)) {
                typeName = "int";
            } else {
                throw new ArgumentException("Unknown type T");
            }

            // Dump the table
            int spc;
            Console.WriteLine($"        private static readonly {typeName}[] {nameof} = new {typeName}[] {{");
            for (int i = 0; i < array.Length; i++) {
                if (i % group == 0) {
                    if (i != 0) {
                        spc = Math.Max(1, CommentOffset - sb.Length);
                        sb.Append(' ', spc).Append($"// E={i - group}");
                        Console.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                    sb.Append(' ', 12);
                }
                sb.Append($"{array[i]}");
                if (i != array.Length - 1) sb.Append(", ");
            }

            if (array.Length % group != 0) {
                int finalOffset = array.Length - array.Length % group;
                spc = Math.Max(1, CommentOffset - sb.Length);
                sb.Append(' ', spc).Append($"// E={finalOffset}");
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("\n        };");
        }
    }
}
```
