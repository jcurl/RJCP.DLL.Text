// GCC doesn't show blanks, plus symbol when displaying unsigned integers. Define this if that's what you want.
//#undef SHOW_ULONG_SIGN

namespace RJCP.Core.Text.Format
{
    using System;
    using System.Globalization;
    using System.Text;

    internal sealed class FormatUnsignedIntegerType : IFormatType
    {
        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            try {
                ulong value = 0;
                if (formatSpecifier.Length == null || formatSpecifier.Length.Equals("l")) {
                    value = GetULongUInt(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("hh")) {
                    value = GetULongByte(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("h")) {
                    value = GetULongUShort(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("ll") || formatSpecifier.Length.Equals("z") ||
                    formatSpecifier.Length.Equals("t") || formatSpecifier.Length.Equals("j")) {
                    value = GetULong(values[currentArg]);
                } else {
                    value = GetULongUInt(values[currentArg]);
                }
                currentArg++;
                UlongToString(str, formatSpecifier, value);
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} to an integer", currentArg);
                throw new FormatException(message, e);
            }
        }

        private static uint GetULongUInt(object arg) { return unchecked((uint)(GetULong(arg) & 0xFFFFFFFF)); }

        private static byte GetULongByte(object arg) { return unchecked((byte)(GetULong(arg) & 0xFF)); }

        private static ushort GetULongUShort(object arg) { return unchecked((ushort)(GetULong(arg) & 0xFFFF)); }

        private static ulong GetULongBool(bool value) { return value ? unchecked((ulong)-1) : 0; }

        private static ulong GetULong(object value)
        {
            unchecked {
                if (value is int vInt) return (ulong)vInt;
                if (value is long vLong) return (ulong)vLong;
                if (value is bool vBool) return GetULongBool(vBool);
                if (value is short vShort) return (ulong)vShort;
                if (value is char vChar) return vChar;
                if (value is sbyte vsByte) return (ulong)vsByte;
                if (value is byte vByte) return vByte;
                if (value is uint vuInt) return vuInt;
                if (value is ulong vuLong) return vuLong;
                if (value is ushort vuShort) return vuShort;
                if (value is string vString && vString.Length > 0) {
                    if (ulong.TryParse(vString, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong vsuLong))
                        return vsuLong;
                }
                throw new FormatException("Parameter doesn't map to an unsigned integer");
            }
        }

        private static readonly char[] BaseDigitsLower = new[] {
                '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
            };

        private static readonly char[] BaseDigitsUpper = new[] {
                '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
            };

        private static void UlongToString(StringBuilder str, FormatSpecifier formatSpecifier, ulong value)
        {
            int alternative = 0;
            ulong baseNumber;
            char[] baseDigits;
            int sign = 0;
            int digits = 0;

            switch (formatSpecifier.Specifier) {
            case 'x':
                baseNumber = 16;
                baseDigits = BaseDigitsLower;
                // Sign and Blank are ignored
                if (value != 0) {
                    // Strange, that GCC only displays 0x if the value isn't zero.
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 2;
                    digits = Numbers.CountBitDigits(value, 4);
                } else if (formatSpecifier.Precision != 0) {
                    digits = Numbers.CountBitDigits(value, 4);
                }
                break;
            case 'X':
                baseNumber = 16;
                baseDigits = BaseDigitsUpper;
                // Sign and Blank are ignored
                if (value != 0) {
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 2;
                    digits = Numbers.CountBitDigits(value, 4);
                } else if (formatSpecifier.Precision != 0) {
                    digits = Numbers.CountBitDigits(value, 4);
                }
                break;
            case 'o':
                baseNumber = 8;
                baseDigits = BaseDigitsLower;
                // Sign and Blank are ignored
                if (value != 0) {
                    // Strange, that GCC only displays 0x if the value isn't zero.
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 1;
                    digits = Numbers.CountBitDigits(value, 3);
                } else if (formatSpecifier.Precision != 0) {
                    digits = Numbers.CountBitDigits(value, 3);
                }
                break;
            case 'u':
                baseNumber = 10;
                baseDigits = BaseDigitsLower;
#if SHOW_ULONG_SIGN
                // In GCC, these options appear to be ignored, and so is commented out.
                if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    sign = formatSpecifier.NumberFormatInfo.PositiveSign.Length;
                } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    sign = 1;
                }
#endif
                if (value != 0 || formatSpecifier.Precision != 0) {
                    // Int64.MaxValue and UInt64.MaxValue have the same number of digits
                    digits = Numbers.CountDigits(value);
                }
                break;
            default:
                throw new FormatException("Unknown Specifier converting to unsigned integer");
            }

            int zeroes = 0;
            if (formatSpecifier.Precision >= 1) {
                zeroes = digits >= formatSpecifier.Precision ? 0 : formatSpecifier.Precision - digits;
            }

            int len = alternative + zeroes + digits + sign;
            int padding = 0;
            if (formatSpecifier.Width >= 1) {
                if (formatSpecifier.Precision < 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    zeroes = digits >= formatSpecifier.Width ? 0 : formatSpecifier.Width - digits - sign;
                } else {
                    padding = len > formatSpecifier.Width ? 0 : formatSpecifier.Width - len;
                }

                if (padding > 0 && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    str.Append(' ', padding);
                }
            }

#if SHOW_ULONG_SIGN
            // In GCC, these options appear to be ignored, and so is commented out.
            if (sign > 0) {
                if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    str.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    str.Append(' ');
                }
            }
#endif
            if (alternative > 0) {
                switch (formatSpecifier.Specifier) {
                case 'x': str.Append("0x"); break;
                case 'X': str.Append("0X"); break;
                case 'o': str.Append('0'); break;
                }
            }
            if (zeroes > 0) str.Append('0', zeroes);

            if (digits > 0) {
                char[] rawnum = new char[digits];
                ulong tValue = value;
                for (int i = digits; i > 0; --i) {
                    int digit = (int)(tValue % baseNumber);
                    tValue /= baseNumber;
                    rawnum[i - 1] = baseDigits[digit];
                }
                str.Append(rawnum);
            }

            if (padding > 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                str.Append(' ', padding);
            }
        }
    }
}
