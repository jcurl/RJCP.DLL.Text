namespace RJCP.Core.Text.Format
{
    using System;
    using System.Globalization;

    internal sealed class FormatSpecifier
    {
        public FormatSpecifier()
        {
            Width = -1;
            Precision = -1;
            NumberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
        }

        public int ArgumentLength { get; set; }

        public FormatFlags FormatFlags { get; set; }

        public bool WidthAsArg { get; set; }

        public int Width { get; set; }

        public bool PrecisionAsArg { get; set; }

        public int Precision { get; set; }

        public string Length { get; set; }

        public char Specifier { get; set; }

        public NumberFormatInfo NumberFormatInfo { get; set; }

        public static FormatSpecifier Parse(string format, ref int position)
        {
            if (format[position] != '%') return null;

            FormatSpecifier formatSpecifier = new();
            int newPosition = position + 1;

            try {
                ParseFormatSpecifierFlag(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierWidth(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierPrecision(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierLength(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierSpecifier(format, ref newPosition, formatSpecifier);
            } catch (FormatException) {
                position = newPosition;
                return null;
            }

            formatSpecifier.ArgumentLength = newPosition - position;
            position = newPosition;
            return formatSpecifier;
        }

        private static void ParseFormatSpecifierFlag(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            while (position < format.Length) {
                char c = format[position];
                switch (c) {
                case '0':
                    formatSpecifier.FormatFlags |= FormatFlags.ZeroPad;
                    break;
                case ' ':
                    formatSpecifier.FormatFlags |= FormatFlags.Blank;
                    break;
                case '-':
                    formatSpecifier.FormatFlags |= FormatFlags.LeftJustify;
                    break;
                case '+':
                    formatSpecifier.FormatFlags |= FormatFlags.ShowSign;
                    break;
                case '#':
                    formatSpecifier.FormatFlags |= FormatFlags.Alternative;
                    break;
                default:
                    return;
                }
                position++;
            }
            throw new FormatException("Incomplete format specifier");
        }

        private static void ParseFormatSpecifierWidth(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (format[position] == '*') {
                formatSpecifier.WidthAsArg = true;
                position++;
            } else {
                formatSpecifier.Width = ParseInt(format, ref position);
            }
        }

        private static void ParseFormatSpecifierPrecision(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (position >= format.Length) throw new FormatException("Incomplete format specifier");
            if (format[position] != '.') return;
            position++;

            if (format[position] == '*') {
                formatSpecifier.PrecisionAsArg = true;
                position++;
            } else {
                formatSpecifier.Precision = ParseInt(format, ref position);
            }
        }

        private static void ParseFormatSpecifierLength(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            int startPosition = position;
            while (position < format.Length) {
                char c = format[position];
                if (c is 'l' or 'h' or 'j' or 'z' or 't' or 'L') {
                    position++;
                } else {
                    if (startPosition == position) return;
                    formatSpecifier.Length = format.Substring(startPosition, position - startPosition);
                    if (!IsAnyOf(formatSpecifier.Length, "hh", "h", "l", "ll", "j", "z", "t", "L"))
                        throw new FormatException("Invalid Length specifier");
                    return;
                }
            }
            throw new FormatException("Incomplete format specifier");
        }

        private const string FormatSpecifiers = "diouxXfFeEgGaAcspn%";

        private static void ParseFormatSpecifierSpecifier(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (position >= format.Length) throw new FormatException("Incomplete format specifier");
            char c = format[position];

            if (!FormatSpecifiers.Contains(c.ToString())) throw new FormatException("Invalid specifier");
            position++;
            formatSpecifier.Specifier = c;
        }

        private static int ParseInt(string format, ref int position)
        {
            int endPosition = position;
            bool parsing = true;
            while (parsing && endPosition < format.Length) {
                char c = format[endPosition];
                if (c is < '0' or > '9') {
                    parsing = false;
                } else {
                    endPosition++;
                }
            }

            // We expect this to be not the end of the format string.
            if (parsing) {
                position = endPosition;
                throw new FormatException("Incomplete format specifier");
            }

            if (endPosition == position) return -1;
            try {
                int value = int.Parse(format.Substring(position, endPosition - position).ToString());
                position = endPosition;
                return value;
            } catch (OverflowException e) {
                throw new FormatException("Overflow in precision specifier", e);
            }
        }

        private static bool IsAnyOf(string check, params string[] values)
        {
            foreach (string value in values) {
                if (check.Equals(value)) return true;
            }
            return false;
        }
    }
}
