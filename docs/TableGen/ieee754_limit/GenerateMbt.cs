#define DYNAMIC_OVERFLOW

namespace IEEE754Limit
{
    using System;
    using System.Text;

    internal static class GenerateMbt
    {
        private static readonly ulong[] mbt = new ulong[2047];
        private static readonly int[] et = new int[2047];

        public static void Run()
        {
            // Generate non-fractional multipliers
            int e = 1023;
            mbt[e] = 4_096_000_000_000_000_000;
            et[e] = -15;

            Console.WriteLine("Calculating generated tables for double precision");
            Console.WriteLine();

            CalculatePositiveExp(e);
            CalculateNegativeExp(e);
            Console.WriteLine();

            DumpTable(mbt, "Formatter_MantissaBitsTable", 3);
            DumpTable(et, "Formatter_TensExponentTable", 12);

#if DYNAMIC_OVERFLOW
            Console.WriteLine($"Max MBT value without Overflow: {max:x}");
#endif
        }

        private static void CalculatePositiveExp(int e)
        {
            double rem = 0;
            Console.Write($"E={e}; MBT[E]={mbt[e],20:D}; ET[E]={et[e]}     \r");
            for (int i = e + 1; i < mbt.Length; i++) {
                et[i] = et[i - 1];
                checked {
                    bool overflow = false;
                    try {
                        mbt[i] = mbt[i - 1] * 2;
                        if (CheckOverflow(mbt[i])) {
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

                Console.Write($"E={i}; MBT[E]={mbt[i],20:D}; ET[E]={et[i]}; remainder={rem}     \r");
            }
        }

        private static void CalculateNegativeExp(int e)
        {
            double rem = 0;
            Console.Write($"E={e}; MBT[E]={mbt[e],20:D}; ET[E]={et[e]}     \r");
            for (int i = e - 1; i > 0; i--) {
                et[i] = et[i + 1];
                checked {
                    mbt[i] = mbt[i + 1] / 2;
                    rem = (rem + mbt[i + 1] % 2) / 2;
                    try {
                        ulong newMbt = mbt[i] * 10;
                        if (!CheckOverflow(newMbt)) {
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
                Console.Write($"E={i}; MBT[E]={mbt[i],20:D}; ET[E]={et[i]}; remainder={rem}     \r");
            }
            Console.WriteLine("");
        }

        private static ulong max = 0;

        private static bool CheckOverflow(ulong mbt)
        {
#if !DYNAMIC_OVERFLOW
            //const ulong MaxOverflow = 0xFF000000_00000000;
            const ulong MaxOverflow = 0xFC000000_00000000;
            return mbt >= MaxOverflow;
#else
            const ulong hi = 0x00000000_013fffff;  // Maximum mantissa value to try and get overflow
            const ulong lo = 0x00000000_fffffff6;
            ulong hi2 = mbt >> 32;
            ulong lo2 = mbt & 0xFFFFFFFF;
            bool v = MaxMbt.CheckOverflow(lo, hi, lo2, hi2, out _, out _);
            if (!v) {
                if (max < mbt) max = mbt;
            }
            return v;
#endif
        }

        private const int CommentOffset = 90;

        private static void DumpTable<T>(T[] array, string nameof, int group)
        {
            StringBuilder sb = new();

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
            Console.WriteLine("        };\n");
        }
    }
}
