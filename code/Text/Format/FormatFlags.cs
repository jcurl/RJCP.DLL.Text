namespace RJCP.Core.Text.Format
{
    using System;

#if NETSTANDARD2_1
    using System.Runtime.CompilerServices;
#endif

    [Flags]
    internal enum FormatFlags
    {
        /// <summary>
        /// The <c>-</c> flag. The default is to right justify.
        /// </summary>
        LeftJustify = 1,

        /// <summary>
        /// The <c>+</c> flag. Forces a sign symbol to precede the result.
        /// </summary>
        ShowSign = 2,

        /// <summary>
        /// The <b>space</b> flag. If no sign is to be printed, precede with a blank space.
        /// </summary>
        Blank = 4,

        /// <summary>
        /// The <c>#</c> flag. Precedes non-zero hex/octal with <c>0x</c>, <c>0X</c> or <c>0</c>.
        /// Floating points are forced to have a <c>.</c>
        /// </summary>
        Alternative = 8,

        /// <summary>
        /// The <c>0</c> flag. Left pads the number with zero.
        /// </summary>
        ZeroPad = 16
    }

    /// <summary>
    /// Extensions for testing enumerations that is faster.
    /// </summary>
    internal static class FormatFlagsExtension
    {
        /// <summary>
        /// Checks if the value has the enumeration flag set, as in <see cref="Enum.HasFlag(Enum)"/>.
        /// </summary>
        /// <param name="value">The value that should be tested.</param>
        /// <param name="flag">The flag that should be tested for.</param>
        /// <returns>Is <see langword="true"/> if the flag is set, <see langword="false"/> otherwise.</returns>
#if NETSTANDARD2_1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool Flag(this FormatFlags value, FormatFlags flag)
        {
            if (((int)value & (int)flag) != 0) return true;
            return false;
        }
    }
}
