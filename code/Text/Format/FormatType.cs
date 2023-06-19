namespace RJCP.Core.Text.Format
{
    using System;
    using System.Text;

    internal sealed class FormatType : IFormatType
    {
        private static readonly IFormatType FormatInteger = new FormatIntegerType();
        private static readonly IFormatType FormatUnsignedInteger = new FormatUnsignedIntegerType();
        private static readonly IFormatType FormatChar = new FormatCharType();
        private static readonly IFormatType FormatString = new FormatStringType();
        private static readonly IFormatType FormatDouble = new FormatFloatTypeMono();

        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            if (formatSpecifier.WidthAsArg) {
                if (currentArg >= values.Length) throw new FormatException("Insufficient number of arguments in list");
                if (!(values[currentArg] is int)) {
                    string message = string.Format("Argument {0} must be an integer type when specifying the width", currentArg);
                    throw new FormatException(message);
                }
                formatSpecifier.Width = (int)values[currentArg];
                if (formatSpecifier.Width < 0) {
                    formatSpecifier.Width = -formatSpecifier.Width;
                    formatSpecifier.FormatFlags |= FormatFlags.LeftJustify;
                }
                currentArg++;
            }
            if (formatSpecifier.PrecisionAsArg) {
                if (currentArg >= values.Length) throw new FormatException("Insufficient number of arguments in list");
                if (!(values[currentArg] is int)) {
                    string message = string.Format("Argument {0} must be an integer type when specifying the precision", currentArg);
                    throw new FormatException(message);
                }
                formatSpecifier.Precision = (int)values[currentArg];
                if (formatSpecifier.Precision < 0) {
                    formatSpecifier.Precision = -1;
                    formatSpecifier.PrecisionAsArg = false;
                }
                currentArg++;
            }

            // Handle all specifiers that don't need an argument.
            switch (formatSpecifier.Specifier) {
            case '%':
                if (formatSpecifier.ArgumentLength != 2)
                    throw new FormatException("Literal character % doesn't support any formatting");
                str.Append('%');
                return;
            }

            // Handle all specifiers that need an argument.
            if (values == null || currentArg >= values.Length) throw new FormatException("Insufficient number of arguments in list");
            switch (formatSpecifier.Specifier) {
            case 'd':
            case 'i':
                FormatInteger.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'o':
            case 'u':
            case 'x':
            case 'X':
                FormatUnsignedInteger.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'f':
            case 'F':
                FormatDouble.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'e':
            case 'E':
                FormatDouble.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'g':
            case 'G':
                FormatDouble.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'a':
            case 'A':
                break;
            case 'c':
                FormatChar.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 's':
                FormatString.Convert(str, formatSpecifier, ref currentArg, values);
                return;
            case 'p':
                break;
            case 'n':
                break;
            }

            throw new NotImplementedException();
        }
    }
}
