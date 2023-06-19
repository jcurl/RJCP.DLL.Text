namespace RJCP.Core.Text.Format
{
    using System;
    using System.Text;

    internal sealed class FormatStringType : IFormatType
    {
        public void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values)
        {
            try {
                string s = (string)values[currentArg] ?? string.Empty;
                currentArg++;
                if (formatSpecifier.Width == -1) formatSpecifier.Width = 0;
                if (formatSpecifier.Width > s.Length) {
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        str.Append(s);
                        str.Append(' ', formatSpecifier.Width - s.Length);
                    } else {
                        str.Append(' ', formatSpecifier.Width - s.Length);
                        str.Append(s);
                    }
                    return;
                }
                str.Append(s);
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} to a string", currentArg);
                throw new FormatException(message, e);
            }
        }
    }
}
