namespace RJCP.Core.Text.Format
{
    using CodeQuality;

    public static class NumbersAccessor
    {
        private const string AssemblyName = "RJCP.Core.Text";
        private const string TypeName = "RJCP.Core.Text.Format.Numbers";
        private static readonly PrivateType AccType = new PrivateType(AssemblyName, TypeName);

        public static int CountDigits(long value)
        {
            return (int)AccType.InvokeStatic(nameof(CountDigits), value);
        }

        public static int CountBitDigits(ulong value, int bitsPerDigit)
        {
            return (int)AccType.InvokeStatic(nameof(CountBitDigits), value, bitsPerDigit);
        }
    }
}
