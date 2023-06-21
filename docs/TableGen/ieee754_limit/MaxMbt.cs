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
                    scale >>= 1;
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

            Console.WriteLine($"Maximum value before overflow = {hi2:x8}_{lo2:x8}");
            Console.WriteLine();
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
                        lo2 &= 0xFFFF_FFFF;
                    }
                    return hi2 > 0xFFFF_FFFF;
                } catch (OverflowException) {
                    return true;
                }
            }
        }
    }
}
