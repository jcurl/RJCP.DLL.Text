namespace RJCP.Core.Text.Format
{
    using System;

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
}
