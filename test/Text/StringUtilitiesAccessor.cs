namespace RJCP.Core.Text
{
    using CodeQuality;

    public static class StringUtilitiesAccessor
    {
        private readonly static PrivateType StringUtilitiesType = new PrivateType(typeof(StringUtilities));

        public static int CountDigits(long value)
        {
            return (int)StringUtilitiesType.InvokeStatic(nameof(CountDigits), value);
        }

        public static int CountBitDigits(ulong value, int bitsPerDigit)
        {
            return (int)StringUtilitiesType.InvokeStatic(nameof(CountBitDigits), value, bitsPerDigit);
        }
    }
}
