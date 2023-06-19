namespace RJCP.Core.Text.Format
{
    using System.Text;

    internal interface IFormatType
    {
        void Convert(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, object[] values);
    }
}
