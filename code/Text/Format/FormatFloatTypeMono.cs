namespace RJCP.Core.Text.Format
{
    using System;
    using System.Globalization;
    using System.Text;

    internal sealed class FormatFloatTypeMono : IFormatType
    {
        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            double value = GetDouble(values[currentArg]);

            if (values[currentArg] is double) {
                DoubleToString(str, formatSpecifier, value);
            } else {
                FloatToString(str, formatSpecifier, (float)value);
            }
            currentArg++;
        }

        private static double GetDoubleBool(bool value) { return value ? -1 : 0; }

        private static double GetDouble(object value)
        {
            if (value is double vDouble) return vDouble;
            if (value is float vFloat) return vFloat;
            if (value is int vInt) return vInt;
            if (value is long vLong) return vLong;
            if (value is decimal vDec) return (double)vDec;
            if (value is bool vBool) return GetDoubleBool(vBool);
            if (value is short vShort) return vShort;
            if (value is byte vByte) return vByte;
            if (value is char vChar) return vChar;
            if (value is uint vuInt) return vuInt;
            if (value is ulong vuLong) return vuLong;
            if (value is ushort vuShort) return vuShort;
            if (value is sbyte vsByte) return vsByte;
            if (value is string vString && vString.Length > 0) {
                if (double.TryParse(vString, NumberStyles.Any, CultureInfo.InvariantCulture, out double vsDouble))
                    return vsDouble;
            }
            throw new FormatException("Parameter doesn't map to a double");
        }

        public static void FloatToString(StringBuilder str, FormatSpecifier format, float value)
        {
            DoubleFormatter inst = new DoubleFormatter(str, format);
            inst.Init(value, DoubleFormatter.SingleDefPrecision);
            if (inst.FormatInfNan(true)) return;
            inst.NumberToString();
        }

        public static void DoubleToString(StringBuilder str, FormatSpecifier format, double value)
        {
            DoubleFormatter inst = new DoubleFormatter(str, format);
            inst.Init(value, DoubleFormatter.DoubleDefPrecision);
            if (inst.FormatInfNan(true)) return;
            inst.NumberToString();
        }
    }
}
