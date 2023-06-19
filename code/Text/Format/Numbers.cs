namespace RJCP.Core.Text.Format
{
    internal static class Numbers
    {
        private static readonly long[] LongPowerOfTenNegative = new long[] {
            -10,                                 //  2
            -100,                                //  3
            -1000,                               //  4
            -10000,                              //  5
            -100000,                             //  6
            -1000000,                            //  7
            -10000000,                           //  8
            -100000000,                          //  9
            -1000000000,                         // 10
            -10000000000,                        // 11
            -100000000000,                       // 12
            -1000000000000,                      // 13
            -10000000000000,                     // 14
            -100000000000000,                    // 15
            -1000000000000000,                   // 16
            -10000000000000000,                  // 17
            -100000000000000000,                 // 18
            -1000000000000000000,                // 19 digits
        };

        private static readonly long[] LongPowerOfTenPositive = new long[] {
            10,                                  //  2
            100,                                 //  3
            1000,                                //  4
            10000,                               //  5
            100000,                              //  6
            1000000,                             //  7
            10000000,                            //  8
            100000000,                           //  9
            1000000000,                          // 10
            10000000000,                         // 11
            100000000000,                        // 12
            1000000000000,                       // 13
            10000000000000,                      // 14
            100000000000000,                     // 15
            1000000000000000,                    // 16
            10000000000000000,                   // 17
            100000000000000000,                  // 18
            1000000000000000000,                 // 19 digits (long)
        };

        private static readonly ulong[] ULongPowerOfTenPositive = new ulong[] {
            10,                                  //  2
            100,                                 //  3
            1000,                                //  4
            10000,                               //  5
            100000,                              //  6
            1000000,                             //  7
            10000000,                            //  8
            100000000,                           //  9
            1000000000,                          // 10
            10000000000,                         // 11
            100000000000,                        // 12
            1000000000000,                       // 13
            10000000000000,                      // 14
            100000000000000,                     // 15
            1000000000000000,                    // 16
            10000000000000000,                   // 17
            100000000000000000,                  // 18
            1000000000000000000,                 // 19 digits (long)
            10000000000000000000                 // 20 digits (ulong)
        };

        public static int CountDigits(long value)
        {
            // Bisect through the arrays looking for the length of the number in digits
            if (value >= 0) {
                int lower = 0; int upper = LongPowerOfTenPositive.Length - 1;
                while (true) {
                    int mid = (lower + upper) >> 1;
                    if (value < LongPowerOfTenPositive[mid]) {
                        if (mid == 0) return 1;
                        upper = mid - 1;
                    } else if (mid < LongPowerOfTenPositive.Length - 1 && value >= LongPowerOfTenPositive[mid + 1]) {
                        lower = mid + 1;
                    } else {
                        return mid + 2;
                    }
                }
            } else {
                int lower = 0; int upper = LongPowerOfTenNegative.Length - 1;
                while (true) {
                    int mid = (lower + upper) >> 1;
                    if (value > LongPowerOfTenNegative[mid]) {
                        if (mid == 0) return 1;
                        upper = mid - 1;
                    } else if (mid < LongPowerOfTenNegative.Length - 1 && value <= LongPowerOfTenNegative[mid + 1]) {
                        lower = mid + 1;
                    } else {
                        return mid + 2;
                    }
                }
            }
        }

        public static int CountDigits(ulong value)
        {
            // Bisect through the arrays looking for the length of the number in digits
            int lower = 0; int upper = LongPowerOfTenPositive.Length - 1;
            while (true) {
                int mid = (lower + upper) >> 1;
                if (value < ULongPowerOfTenPositive[mid]) {
                    if (mid == 0) return 1;
                    upper = mid - 1;
                } else if (mid < LongPowerOfTenPositive.Length - 1 && value >= ULongPowerOfTenPositive[mid + 1]) {
                    lower = mid + 1;
                } else {
                    return mid + 2;
                }
            }
        }

        public static int CountBitDigits(ulong value, int bitsPerDigit)
        {
            if (value == 0) return 1;  // zero also needs one character
            int firstBitPos = 64;
            if (value <= 0x00000000FFFFFFFF) { firstBitPos -= 32; value <<= 32; }
            if (value <= 0x0000FFFFFFFFFFFF) { firstBitPos -= 16; value <<= 16; }
            if (value <= 0x00FFFFFFFFFFFFFF) { firstBitPos -= 8; value <<= 8; }
            if (value <= 0x0FFFFFFFFFFFFFFF) { firstBitPos -= 4; value <<= 4; }
            if (value <= 0x3FFFFFFFFFFFFFFF) { firstBitPos -= 2; value <<= 2; }
            if (value <= 0x7FFFFFFFFFFFFFFF) { firstBitPos -= 1; }

            return firstBitPos / bitsPerDigit + (firstBitPos % bitsPerDigit == 0 ? 0 : 1);
        }

        public static long GetTenPowerOf(int i)
        {
            if (i == 0) return 1;
            return LongPowerOfTenPositive[i - 1];
        }
    }
}
