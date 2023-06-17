# SPrintF Test Generator

This (small) C++ program is used to automatically generate `sprintf` test cases
to compare against the current libc implementation.

## Building

To build the test program on a Unix like system with GNU compiler toolchain:

```sh
make
./sprintf
```

## Output

The output to the console generates template functions that can be copied to the
C# project. The template functions are NUnit test cases that compare the C#
implementation of `SPrintF` to the libc implementation of `sprintf`.

## Expected Differences

While the general output is expected to be the same (especially the formatting
strings) between the system `sprintf` and the C# `SPrintF` implementstion, small
differences can be expected in the final precision.

Output may depend also on the specific implementation that is being used for the
test case generation. The LLVM implementation may be different to the Gnu
Compiler Collection, which may be different to other vendors also. Changes in
the version of the compiler toolchain may result in minor changes.

These are to be evaluated when running the test cases in C#.
