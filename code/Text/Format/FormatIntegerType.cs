namespace RJCP.Core.Text.Format
{
    using System;
    using System.Globalization;
    using System.Text;

    internal sealed class FormatIntegerType : IFormatType
    {
        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            try {
                long value = 0;
                if (formatSpecifier.Length is null || formatSpecifier.Length.Equals("l")) {
                    value = GetLongInt(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("hh")) {
                    value = GetLongSByte(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("h")) {
                    value = GetLongShort(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("ll") || formatSpecifier.Length.Equals("z") ||
                    formatSpecifier.Length.Equals("t") || formatSpecifier.Length.Equals("j")) {
                    value = GetLong(values[currentArg]);
                } else {
                    value = GetLongInt(values[currentArg]);
                }
                currentArg++;
                LongToString(str, formatSpecifier, value);
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} to an integer", currentArg);
                throw new FormatException(message, e);
            }
        }

        private static int GetLongInt(object arg) { return unchecked((int)(GetLong(arg) & 0xFFFFFFFF)); }

        private static sbyte GetLongSByte(object arg) { return unchecked((sbyte)(GetLong(arg) & 0xFF)); }

        private static short GetLongShort(object arg) { return unchecked((short)(GetLong(arg) & 0xFFFF)); }

        private static long GetLongBool(bool value) { return value ? -1 : 0; }

        private static long GetLong(object value)
        {
            unchecked {
                if (value is int vInt) return vInt;
                if (value is long vLong) return vLong;
                if (value is bool vBool) return GetLongBool(vBool);
                if (value is short vShort) return vShort;
                if (value is char vChar) return vChar;
                if (value is sbyte vsByte) return vsByte;
                if (value is byte vByte) return vByte;
                if (value is uint vuInt) return vuInt;
                if (value is ulong vuLong) return (long)vuLong;
                if (value is ushort vuShort) return vuShort;
                if (value is string vString && vString.Length > 0) {
                    if (long.TryParse(vString, NumberStyles.Integer, CultureInfo.InvariantCulture, out long vsLong))
                        return vsLong;
                }
                throw new FormatException("Parameter doesn't map to an integer");
            }
        }

        private static void LongToString(StringBuilder str, FormatSpecifier formatSpecifier, long value)
        {
            int digits;
            if (value == 0 && formatSpecifier.Precision == 0) {
                digits = 0;
            } else {
                digits = Numbers.CountDigits(value);
            }

            int sign = 0;
            if (value < 0 || formatSpecifier.FormatFlags.Flag(FormatFlags.ShowSign)) {
                sign = value < 0 ? formatSpecifier.NumberFormatInfo.NegativeSign.Length : formatSpecifier.NumberFormatInfo.PositiveSign.Length;
            } else if (formatSpecifier.FormatFlags.Flag(FormatFlags.Blank)) {
                sign = 1;
            }

            int zeroes = 0;
            if (formatSpecifier.Precision >= 1) {
                zeroes = digits >= formatSpecifier.Precision ? 0 : formatSpecifier.Precision - digits;
            }

            int len = digits + zeroes + sign;
            int padding = 0;
            if (formatSpecifier.Width >= 1) {
                if (formatSpecifier.Precision < 0 && formatSpecifier.FormatFlags.Flag(FormatFlags.ZeroPad) && !formatSpecifier.FormatFlags.Flag(FormatFlags.LeftJustify)) {
                    zeroes = digits >= formatSpecifier.Width ? 0 : formatSpecifier.Width - digits - sign;
                } else {
                    padding = len > formatSpecifier.Width ? 0 : formatSpecifier.Width - len;
                }

                if (padding > 0 && !formatSpecifier.FormatFlags.Flag(FormatFlags.LeftJustify)) {
                    str.Append(' ', padding);
                }
            }

            if (value >= 0) {
                if (formatSpecifier.FormatFlags.Flag(FormatFlags.ShowSign)) {
                    str.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                } else if (formatSpecifier.FormatFlags.Flag(FormatFlags.Blank)) {
                    str.Append(' ');
                }
            } else {
                str.Append(formatSpecifier.NumberFormatInfo.NegativeSign);
            }

            if (zeroes > 0) str.Append('0', zeroes);

            if (value != 0 || formatSpecifier.Precision != 0) {
                char[] rawnum = new char[digits];
                long tValue = value;
                for (int i = digits; i > 0; --i) {
                    int digit = Math.Abs((int)(tValue % 10));
                    tValue /= 10;
                    rawnum[i - 1] = (char)(digit + '0');
                }
                str.Append(rawnum);
            }

            if (padding > 0 && formatSpecifier.FormatFlags.Flag(FormatFlags.LeftJustify)) {
                str.Append(' ', padding);
            }
        }
    }
}
