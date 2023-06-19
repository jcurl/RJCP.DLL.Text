namespace RJCP.Core.Text.Format
{
    using System;
    using System.Text;

    internal sealed class FormatCharType : IFormatType
    {
        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            try {
                int c = GetChar(values[currentArg]);
                currentArg++;
                if (formatSpecifier.Width == -1) formatSpecifier.Width = 1;
                if (formatSpecifier.Width > 1) {
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        str.Append((char)c);
                        str.Append(' ', formatSpecifier.Width - 1);
                    } else {
                        str.Append(' ', formatSpecifier.Width - 1);
                        str.Append((char)c);
                    }
                    return;
                }
                unchecked {
                    // If the 'int' input doesn't fit within a char, then coerce it and chop of extra bits as done in C.
                    str.Append((char)c);
                }
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} type {1} to a char",
                    currentArg + 1, values[currentArg].GetType());
                throw new FormatException(message, e);
            }
        }

        private static int GetChar(object value)
        {
            unchecked {
                if (value is char vChar) return vChar;
                if (value is sbyte vsByte) return (byte)vsByte;
                if (value is byte vByte) return vByte;
                if (value is short vShort) return vShort;
                if (value is ushort vuShort) return (short)vuShort;
                if (value is int vInt) return (short)vInt;
                if (value is uint vuInt) return (short)vuInt;
                if (value is long vLong) return (short)vLong;
                if (value is ulong vuLong) return (short)vuLong;
                if (value is string vString && vString.Length > 0) return vString[0];
                throw new FormatException("Parameter doesn't map to an integer");
            }
        }
    }
}
