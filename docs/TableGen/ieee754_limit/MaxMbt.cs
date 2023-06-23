namespace IEEE754Limit
{
    using System;

    internal static class MaxMbt
    {
        public static void Run()
        {
            Console.WriteLine("Calculating the maximum value of MBT");
            Console.WriteLine("such that the value 'mm' does not overflow.");
            Console.WriteLine();

            const long DoubleBitsMantissaMask = 0xfffffffffffff;

            long m = 0xFFFFF_FFFFFFFF;
            m = (m + DoubleBitsMantissaMask + 1) * 10;

            ulong lo = (uint)m;
            ulong hi = (ulong)m >> 32;
            ulong lo2 = 0xFFFF_FFFF;
            ulong hi2 = 0x8000_0000;

            ulong scale = 0x0100_0000;
            ulong olo2 = lo2;
            ulong ohi2 = hi2;
            while (true) {
                bool v = CheckOverflow(lo, hi, lo2, hi2, out ulong mm, out long res);
                if (v) {
                    scale >>= 1;
                    lo2 = olo2;
                    hi2 = ohi2;
                } else {
                    Console.WriteLine($"{hi:x8}_{lo:x8} * {hi2:x8}_{lo2:x8} => res={res:x} mm={mm:x}");
                    olo2 = lo2;
                    ohi2 = hi2;
                }

                do {
                    v = Add(scale, ref hi2);
                    if (v) {
                        scale >>= 1;
                        hi2 = ohi2;
                    }
                } while (v);

                if (scale == 0) break;
            }

            if ((lo2 & 0xFFFF_FFFF) != 0) {
                hi2++;
                lo2 = 0;
            }
            Console.WriteLine($"Maximum value before overflow = {hi2:x8}_{lo2:x8}");
            Console.WriteLine();
        }

        public static bool CheckOverflow(ulong lo, ulong hi, ulong lo2, ulong hi2, out ulong mm, out long res)
        {
            const long SeventeenDigitsThreshold = 10_000_000_000_000_000;
            checked {
                try {
                    mm = hi * lo2 + lo * hi2 + ((lo * lo2) >> 32);
                    res = (long)(hi * hi2 + (mm >> 32));
                    while (res < SeventeenDigitsThreshold) {
                        mm = (mm & uint.MaxValue) * 10;
                        res = res * 10 + (long)(mm >> 32);
                    }
                    if ((mm & 0x80000000) != 0) res++;
                    return false;
                } catch (OverflowException) {
                    mm = 0; res = 0;
                    return true;
                }
            }
        }

        static bool Add(ulong scale, ref ulong hi2)
        {
            hi2 += scale;
            return hi2 > 0xFFFF_FFFF;
        }
    }
}
