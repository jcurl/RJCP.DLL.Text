namespace RJCP.Core.Text
{
    using System;
    using System.Text;
    using Format;

    /// <summary>
    /// Collection of string utilities
    /// </summary>
    public static partial class StringUtilities
    {
        private static readonly FormatType FormatTypes = new();

        /// <summary>
        /// Format a string based on the C-Standard.
        /// </summary>
        /// <returns>A formatted string</returns>
        /// <param name="format">The formatting string as per the C style <c>printf</c> function family.</param>
        /// <param name="values">The list of objects as given in the format string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> may not be <see langword="null"/>.</exception>
        /// <exception cref="FormatException">There was a fatal error parsing the string and/or the parameters.</exception>
        /// <remarks>
        /// <para>This method takes a <paramref name="format"/> specifier and converts it to a string based on the
        /// values provided. See http://www.cplusplus.com/reference/cstdio/printf or http://en.cppreference.com/w/cpp/io/c/fprintf.
        /// The string specifiers are of the format <c>%[flags][width][.precision][length]specifier</c>.</para>
        /// <para>Results are compatible with GCC 7.4.0 on Ubuntu 18.04 compiled under x64.</para>
        /// <para>The specifiers supported are:</para>
        /// <list type="bullet">
        ///   <item><c>d</c> or <c>i</c>: signed decimal integer.
        ///     <list type="bullet">
        ///       <item>The length modifiers <c>hh</c>, <c>h</c>, <c>l</c>, <c>ll</c>, <c>j</c>, <c>t</c>, <c>z</c> are supported.</item>
        ///       <item><c>hh</c> is truncated to 8-bit and treated as a <c>int8_t</c>.</item>
        ///       <item><c>h</c> is truncated to 16-bit and treated as a <c>int16_t</c>.</item>
        ///       <item><c>l</c> and no length modifier are truncated to 32-bit and treated as a <c>int32_t</c>.</item>
        ///       <item><c>ll</c>, <c>z</c>, <c>t</c> and <c>j</c> are treated as a signed 64-bit <c>int64_t</c> type. If an
        ///       unsigned value is given, it is typecast to a signed value so that large positive values will then be printed
        ///       as a negative value.</item>
        ///       <item>The length modifier <c>j</c> is not supported, but treated as a 64-bit as this is the maximum
        ///       bit size that .NET supports.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>u</c>: unsigned decimal integer.
        ///     <list type="bullet">
        ///       <item>The length modifiers <c>hh</c>, <c>h</c>, <c>l</c>, <c>ll</c>, <c>j</c>, <c>t</c>, <c>z</c> are supported.</item>
        ///       <item><c>hh</c> is truncated to 8-bit and treated as a <c>uint8_t</c>.</item>
        ///       <item><c>h</c> is truncated to 16-bit and treated as a <c>uint16_t</c>.</item>
        ///       <item><c>l</c> and no length modifier are truncated to 32-bit and treated as a <c>uint32_t</c>.</item>
        ///       <item><c>ll</c>, <c>z</c>, <c>t</c> and <c>j</c> are treated as a signed 64-bit <c>uint64_t</c> type.</item>
        ///       <item>The length modifier <c>j</c> is not supported, but treated as a 64-bit as this is the maximum
        ///       bit size that .NET supports.</item>
        ///       <item>The flags <i>space</i> and <c>+</c> are ignored.</item>
        ///       <item>All negative numbers are typecast to their bit equivalent unsigned value.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>o</c>: octal.
        ///     <list type="bullet">
        ///       <item>The length modifiers <c>hh</c>, <c>h</c>, <c>l</c>, <c>ll</c>, <c>j</c>, <c>t</c>, <c>z</c> are supported.</item>
        ///       <item><c>hh</c> is truncated to 8-bit and treated as a <c>uint8_t</c>.</item>
        ///       <item><c>h</c> is truncated to 16-bit and treated as a <c>uint16_t</c>.</item>
        ///       <item><c>l</c> and no length modifier are truncated to 32-bit and treated as a <c>uint32_t</c>.</item>
        ///       <item><c>ll</c>, <c>z</c>, <c>t</c> and <c>j</c> are treated as a signed 64-bit <c>uint64_t</c> type.</item>
        ///       <item>The length modifier <c>j</c> is not supported on GCC 4.8.3, but treated as a 64-bit as this is the maximum
        ///       bit size that .NET supports.</item>
        ///       <item>The flags <i>space</i> and <c>+</c> are ignored.</item>
        ///       <item>All negative numbers are typecast to their bit equivalent unsigned value.</item>
        ///       <item>The alternative flag <c>#</c> can be used to add a <c>0</c> at the front, but only if the value itself isn't already
        ///       zero.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>x</c>: unsigned hexadecimal integer.</item>
        ///   <item><c>X</c>: unsigned hexadecimal integer (upper case).
        ///     <list type="bullet">
        ///       <item>The length modifiers <c>hh</c>, <c>h</c>, <c>l</c>, <c>ll</c>, <c>j</c>, <c>t</c>, <c>z</c> are supported.</item>
        ///       <item><c>hh</c> is truncated to 8-bit and treated as a <c>uint8_t</c>.</item>
        ///       <item><c>h</c> is truncated to 16-bit and treated as a <c>uint16_t</c>.</item>
        ///       <item><c>l</c> and no length modifier are truncated to 32-bit and treated as a <c>uint32_t</c>.</item>
        ///       <item><c>ll</c>, <c>z</c>, <c>t</c> and <c>j</c> are treated as a signed 64-bit <c>uint64_t</c> type.</item>
        ///       <item>The length modifier <c>j</c> is not supported on GCC 4.8.3, but treated as a 64-bit as this is the maximum
        ///       bit size that .NET supports.</item>
        ///       <item>The flags <i>space</i> and <c>+</c> are ignored.</item>
        ///       <item>All negative numbers are typecast to their bit equivalent unsigned value.</item>
        ///       <item>The alternative flag <c>#</c> can be used to add a <c>0x</c> or <c>0X</c> at the front, but only if the value
        ///       itself isn't already zero.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>f</c> or <c>F</c>: decimal floating point with a fixed point. Implementation is based on Mono.
        ///   </item>
        ///   <item><c>e</c> or <c>E</c>: scientific notation.
        ///   </item>
        ///   <item><c>g</c> or <c>G</c>: Use the shortest notation <c>f</c> or <c>e</c>.
        ///     <list type="bullet">
        ///       <item>For the g conversion style conversion with style e or f will be performed.</item>
        ///       <item>For the G conversion style conversion with style E or F will be performed.</item>
        ///       <item>Let P equal the precision if nonzero, 6 if the precision is not specified, or 1 if the precision is ​0​.
        ///       Then, if a conversion with style E would have an exponent of X:</item>
        ///       <item>if P > X ≥ −4, the conversion is with style f or F and precision P − 1 − X.</item>
        ///       <item>otherwise, the conversion is with style e or E and precision P − 1. </item>
        ///       <item>Unless alternative representation is requested the trailing zeros are removed, also the decimal
        ///       point character is removed if no fractional part is left.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>c</c>: character.
        ///     <list type="bullet">
        ///       <item>Not specifying a length modifier has the same behavior is specifying <c>l</c>. All other length modifiers
        ///       are ignored.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>s</c>: string.
        ///     <list type="bullet">
        ///     <item>Not specifying a length modifier has the same behavior is specifying <c>l</c>. All other length modifiers
        ///     are ignored.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>p</c>: pointer.
        ///     <list type="bullet">
        ///       <item>Currently not implemented and throws an exception.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>n</c>: number of characters written so far.
        ///     <list type="bullet">
        ///       <item>Currently not implemented and throws an exception.</item>
        ///     </list>
        ///   </item>
        ///   <item><c>%</c>: print a <c>%</c>.</item>
        /// </list>
        /// <para>The specifiers that are in the C-Standard that are not supported are:</para>
        /// <list type="bullet">
        ///   <item><c>a</c> or <c>A</c>: hexadecimal floating point.</item>
        /// </list>
        /// <para>The flags supported are:</para>
        /// <list type="bullet">
        ///   <item><c>-</c>: Left-justify within the given field width; Right justification is the default (see width sub-specifier).</item>
        ///   <item><c>+</c>: Forces to precede the result with a plus or minus sign (+ or -) even for positive numbers. By default,
        ///   only negative numbers are preceded with a - sign.</item>
        ///   <item>(space): If no sign is going to be written, a blank space is inserted before the value.</item>
        ///   <item><c>#</c>: Used with o, x or X specifiers the value is precede with 0, 0x or 0X respectively for values different than zero.
        ///   Used with a, A, e, E, f, F, g or G it forces the written output to contain a decimal point even if no more digits follow.
        ///   By default, if no digits follow, no decimal point is written.</item>
        ///   <item><c>0</c>: Left-pads the number with zeros (0) instead of spaces when padding is specified (see width sub-specifier).</item>
        /// </list>
        /// <para>The SPrintF library is slower than the standard .NET implementation. That means, you should only use this method
        /// where required (e.g. where input strings are in a C format) and not use this method where the .NET method
        /// <c>string.Format()</c> can be used.</para>
        /// <para>On an Intel i7-4930K 3.4GHz processor, the following measurements (of 100,000 cycles) were obtained
        /// (Release build, .NET 4.5 Windows 7 x64). Values should not be treated as absolute, as this depends on the Operating
        /// System, CPU and .NET version installed.</para>
        /// <list type="bullet">
        /// <item>Integer %d: SPrintF = ~39ms. string.Format = ~17ms.</item>
        /// <item>Integer %u: SPrintF = ~29ms. string.Format = ~17ms.</item>
        /// <item>IEEE-754 %f: 123456.789. SPrintF (double/float) = ~42ms/45ms. string.Format = ~31ms/33ms.</item>
        /// <item>IEEE-754 %e: 123456.789. SPrintF (double/float) = ~41ms/47ms. string.Format = ~33ms/35ms.</item>
        /// <item>IEEE-754 %g: 123456.789. SPrintF (double/float) = ~45ms/48ms. string.Format = ~32ms/33ms.</item>
        /// </list>
        /// </remarks>
        public static string SPrintF(string format, params object[] values)
        {
            ThrowHelper.ThrowIfNull(format);

            StringBuilder sb = new();

            int charPos = 0;
            int nextCharPos = 0;
            int currentArg = 0;
            while (charPos < format.Length) {
                charPos = nextCharPos;
                nextCharPos = GetNextFormatChar(format, charPos);
                if (nextCharPos == -1) {
                    // No more special characters seen.
#if NETFRAMEWORK
                    sb.Append(format.Substring(charPos));
#else
                    sb.Append(format.AsSpan(charPos));
#endif
                    return sb.ToString();
                }
                if (nextCharPos > charPos) {
#if NETFRAMEWORK
                    sb.Append(format.Substring(charPos, nextCharPos - charPos));
#else
                    sb.Append(format.AsSpan(charPos, nextCharPos - charPos));
#endif
                    charPos = nextCharPos;
                }

                FormatSpecifier formatSpecifier = FormatSpecifier.Parse(format, ref nextCharPos);
                if (formatSpecifier is null) {
                    // The format specifier is invalid, so copy it verbatim.
#if NETFRAMEWORK
                    sb.Append(format.Substring(charPos, nextCharPos - charPos));
#else
                    sb.Append(format.AsSpan(charPos, nextCharPos - charPos));
#endif
                    continue;
                }

                // Read the input parameters and convert it.
                FormatTypes.Convert(sb, formatSpecifier, ref currentArg, values);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Looks through a format string and returns the position of the next interesting character.
        /// </summary>
        /// <param name="format">The format specifier to look through.</param>
        /// <param name="position">The position in the format specifier to start looking from.</param>
        /// <returns>A position in the format specifier for the next interesting character, otherwise -1 if none found.</returns>
        private static int GetNextFormatChar(string format, int position)
        {
            if (position >= format.Length) return -1;
            return format.IndexOf('%', position);
        }
    }
}
