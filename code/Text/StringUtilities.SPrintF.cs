// GCC doesn't show blanks, plus symbol when displaying unsigned integers. Define this if that's what you want.
//#undef SHOW_ULONG_SIGN

namespace RJCP.Core.Text
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Collection of string utilities
    /// </summary>
    public static partial class StringUtilities
    {
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
            if (format == null) throw new ArgumentNullException(nameof(format));

            StringBuilder sb = new StringBuilder();

            int charPos = 0;
            int nextCharPos = 0;
            int currentArg = 0;
            while (charPos < format.Length) {
                charPos = nextCharPos;
                nextCharPos = PrintFGetNextSpecialChar(format, charPos);
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

                FormatSpecifier formatSpecifier = ParseFormatSpecifier(format, ref nextCharPos);
                if (formatSpecifier == null) {
                    // The format specifier is invalid, so copy it verbatim.
#if NETFRAMEWORK
                    sb.Append(format.Substring(charPos, nextCharPos - charPos));
#else
                    sb.Append(format.AsSpan(charPos, nextCharPos - charPos));
#endif
                    continue;
                }

                // Read the input parameters and convert it.
                ConvertFormatSpecifier(sb, formatSpecifier, ref currentArg, values);
            }
            return sb.ToString();
        }

        #region SPrintF implementation
        #region Parse Format String %[+- #0]<w>.<p><l><specifier>
        /// <summary>
        /// Looks through a format string and returns the position of the next interesting character.
        /// </summary>
        /// <param name="format">The format specifier to look through.</param>
        /// <param name="position">The position in the format specifier to start looking from.</param>
        /// <returns>A position in the format specifier for the next interesting character, otherwise -1 if none found.</returns>
        private static int PrintFGetNextSpecialChar(string format, int position)
        {
            if (position >= format.Length) return -1;
            return format.IndexOf('%', position);
        }

        [Flags]
        private enum FormatFlags
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

        private sealed class FormatSpecifier
        {
            public FormatSpecifier()
            {
                Width = -1;
                Precision = -1;
                NumberFormatInfo = CultureInfo.InvariantCulture.NumberFormat;
            }

            public int ArgumentLength { get; set; }
            public FormatFlags FormatFlags { get; set; }
            public bool WidthAsArg { get; set; }
            public int Width { get; set; }
            public bool PrecisionAsArg { get; set; }
            public int Precision { get; set; }
            public string Length { get; set; }
            public char Specifier { get; set; }

            public NumberFormatInfo NumberFormatInfo { get; set; }
        }

        private static FormatSpecifier ParseFormatSpecifier(string format, ref int position)
        {
            if (format[position] != '%') return null;

            FormatSpecifier formatSpecifier = new FormatSpecifier();
            int newPosition = position + 1;

            try {
                ParseFormatSpecifierFlag(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierWidth(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierPrecision(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierLength(format, ref newPosition, formatSpecifier);
                ParseFormatSpecifierSpecifier(format, ref newPosition, formatSpecifier);
            } catch (FormatException) {
                position = newPosition;
                return null;
            }

            formatSpecifier.ArgumentLength = newPosition - position;
            position = newPosition;
            return formatSpecifier;
        }

        private static void ParseFormatSpecifierFlag(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            while (position < format.Length) {
                char c = format[position];
                switch (c) {
                case '0':
                    formatSpecifier.FormatFlags |= FormatFlags.ZeroPad;
                    break;
                case ' ':
                    formatSpecifier.FormatFlags |= FormatFlags.Blank;
                    break;
                case '-':
                    formatSpecifier.FormatFlags |= FormatFlags.LeftJustify;
                    break;
                case '+':
                    formatSpecifier.FormatFlags |= FormatFlags.ShowSign;
                    break;
                case '#':
                    formatSpecifier.FormatFlags |= FormatFlags.Alternative;
                    break;
                default:
                    return;
                }
                position++;
            }
            throw new FormatException("Incomplete format specifier");
        }

        private static void ParseFormatSpecifierWidth(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (format[position] == '*') {
                formatSpecifier.WidthAsArg = true;
                position++;
            } else {
                formatSpecifier.Width = ParseInt(format, ref position);
            }
        }

        private static void ParseFormatSpecifierPrecision(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (position >= format.Length) throw new FormatException("Incomplete format specifier");
            if (format[position] != '.') return;
            position++;

            if (format[position] == '*') {
                formatSpecifier.PrecisionAsArg = true;
                position++;
            } else {
                formatSpecifier.Precision = ParseInt(format, ref position);
            }
        }

        private static int ParseInt(string format, ref int position)
        {
            int endPosition = position;
            bool parsing = true;
            while (parsing && endPosition < format.Length) {
                char c = format[endPosition];
                if (c < '0' || c > '9') {
                    parsing = false;
                } else {
                    endPosition++;
                }
            }

            // We expect this to be not the end of the format string.
            if (parsing) {
                position = endPosition;
                throw new FormatException("Incomplete format specifier");
            }

            if (endPosition == position) return -1;
            try {
                int value = int.Parse(format.Substring(position, endPosition - position).ToString());
                position = endPosition;
                return value;
            } catch (OverflowException e) {
                throw new FormatException("Overflow in precision specifier", e);
            }
        }

        private static void ParseFormatSpecifierLength(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            int startPosition = position;
            while (position < format.Length) {
                char c = format[position];
                if (c == 'l' || c == 'h' || c == 'j' || c == 'z' || c == 't' || c == 'L') {
                    position++;
                } else {
                    if (startPosition == position) return;
                    formatSpecifier.Length = format.Substring(startPosition, position - startPosition);
                    if (!formatSpecifier.Length.IsAnyOf("hh", "h", "l", "ll", "j", "z", "t", "L"))
                        throw new FormatException("Invalid Length specifier");
                    return;
                }
            }
            throw new FormatException("Incomplete format specifier");
        }

        private static bool IsAnyOf(this string check, params string[] values)
        {
            foreach (string value in values) {
                if (check.Equals(value)) return true;
            }
            return false;
        }

        private const string FormatSpecifiers = "diouxXfFeEgGaAcspn%";

        private static void ParseFormatSpecifierSpecifier(string format, ref int position, FormatSpecifier formatSpecifier)
        {
            if (position >= format.Length) throw new FormatException("Incomplete format specifier");
            char c = format[position];

            if (!FormatSpecifiers.Contains(c.ToString())) throw new FormatException("Invalid specifier");
            position++;
            formatSpecifier.Specifier = c;
        }
        #endregion

        #region Format String Conversion Dispatch
        private static void ConvertFormatSpecifier(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
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
                ConvertFormatSpecifierInteger(str, formatSpecifier, ref currentArg, values);
                return;
            case 'o':
            case 'u':
            case 'x':
            case 'X':
                ConvertFormatSpecifierUnsignedInteger(str, formatSpecifier, ref currentArg, values);
                return;
            case 'f':
            case 'F':
                ConvertFormatSpecifierFloatMono(str, formatSpecifier, ref currentArg, values);
                return;
            case 'e':
            case 'E':
                ConvertFormatSpecifierFloatMono(str, formatSpecifier, ref currentArg, values);
                return;
            case 'g':
            case 'G':
                ConvertFormatSpecifierFloatMono(str, formatSpecifier, ref currentArg, values);
                return;
            case 'a':
            case 'A':
                break;
            case 'c':
                ConvertFormatSpecifierChar(str, formatSpecifier, ref currentArg, values);
                return;
            case 's':
                ConvertFormatSpecifierString(str, formatSpecifier, ref currentArg, values);
                return;
            case 'p':
                break;
            case 'n':
                break;
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Format Specifier Char (%c)
        private static void ConvertFormatSpecifierChar(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
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
        #endregion

        #region Format Specifier String (%s)
        private static void ConvertFormatSpecifierString(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
        {
            try {
                string s = (string)values[currentArg];
                if (s == null) s = string.Empty;
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
        #endregion

        #region Format Specifier Integer (%i, %d)
        private static void ConvertFormatSpecifierInteger(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
        {
            try {
                long value = 0;
                if (formatSpecifier.Length == null || formatSpecifier.Length.Equals("l")) {
                    value = GetLongInt(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("hh")) {
                    value = GetLongSByte(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("h")) {
                    value = GetLongShort(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("ll") || formatSpecifier.Length.Equals("z") ||
                    formatSpecifier.Length.Equals("t") || formatSpecifier.Length.Equals("j")) {
                    value = GetLong(values[currentArg]);
                } else {
                    value = GetLongInt(values[currentArg]);
                }
                currentArg++;
                LongToString(str, formatSpecifier, value);
                return;
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} to an integer", currentArg);
                throw new FormatException(message, e);
            }
        }

        private static int GetLongInt(object arg) { return unchecked((int)(GetLong(arg) & 0xFFFFFFFF)); }

        private static sbyte GetLongSByte(object arg) { return unchecked((sbyte)(GetLong(arg) & 0xFF)); }

        private static short GetLongShort(object arg) { return unchecked((short)(GetLong(arg) & 0xFFFF)); }

        private static long GetLongBool(bool value) { return value ? -1 : 0; }

        private static long GetLong(object value)
        {
            unchecked {
                if (value is int vInt) return vInt;
                if (value is long vLong) return vLong;
                if (value is bool vBool) return GetLongBool(vBool);
                if (value is short vShort) return vShort;
                if (value is char vChar) return vChar;
                if (value is sbyte vsByte) return vsByte;
                if (value is byte vByte) return vByte;
                if (value is uint vuInt) return vuInt;
                if (value is ulong vuLong) return (long)vuLong;
                if (value is ushort vuShort) return vuShort;
                if (value is string vString && vString.Length > 0) {
                    if (long.TryParse(vString, NumberStyles.Integer, CultureInfo.InvariantCulture, out long vsLong))
                        return vsLong;
                }
                throw new FormatException("Parameter doesn't map to an integer");
            }
        }

        private static readonly long[] LongPowerOfTenNegative = new long[] {
            -10,                                 //  2
            -100,                                //  3
            -1000,                               //  4
            -10000,                              //  5
            -100000,                             //  6
            -1000000,                            //  7
            -10000000,                           //  8
            -100000000,                          //  9
            -1000000000,                         // 10
            -10000000000,                        // 11
            -100000000000,                       // 12
            -1000000000000,                      // 13
            -10000000000000,                     // 14
            -100000000000000,                    // 15
            -1000000000000000,                   // 16
            -10000000000000000,                  // 17
            -100000000000000000,                 // 18
            -1000000000000000000,                // 19 digits
        };

        private static readonly long[] LongPowerOfTenPositive = new long[] {
            10,                                  //  2
            100,                                 //  3
            1000,                                //  4
            10000,                               //  5
            100000,                              //  6
            1000000,                             //  7
            10000000,                            //  8
            100000000,                           //  9
            1000000000,                          // 10
            10000000000,                         // 11
            100000000000,                        // 12
            1000000000000,                       // 13
            10000000000000,                      // 14
            100000000000000,                     // 15
            1000000000000000,                    // 16
            10000000000000000,                   // 17
            100000000000000000,                  // 18
            1000000000000000000,                 // 19 digits (long)
        };

        private static readonly ulong[] ULongPowerOfTenPositive = new ulong[] {
            10,                                  //  2
            100,                                 //  3
            1000,                                //  4
            10000,                               //  5
            100000,                              //  6
            1000000,                             //  7
            10000000,                            //  8
            100000000,                           //  9
            1000000000,                          // 10
            10000000000,                         // 11
            100000000000,                        // 12
            1000000000000,                       // 13
            10000000000000,                      // 14
            100000000000000,                     // 15
            1000000000000000,                    // 16
            10000000000000000,                   // 17
            100000000000000000,                  // 18
            1000000000000000000,                 // 19 digits (long)
            10000000000000000000                 // 20 digits (ulong)
        };

        private static int CountDigits(long value)
        {
            // Bisect through the arrays looking for the length of the number in digits
            if (value >= 0) {
                int lower = 0; int upper = LongPowerOfTenPositive.Length - 1;
                while (true) {
                    int mid = (lower + upper) >> 1;
                    if (value < LongPowerOfTenPositive[mid]) {
                        if (mid == 0) return 1;
                        upper = mid - 1;
                    } else if (mid < LongPowerOfTenPositive.Length - 1 && value >= LongPowerOfTenPositive[mid + 1]) {
                        lower = mid + 1;
                    } else {
                        return mid + 2;
                    }
                }
            } else {
                int lower = 0; int upper = LongPowerOfTenNegative.Length - 1;
                while (true) {
                    int mid = (lower + upper) >> 1;
                    if (value > LongPowerOfTenNegative[mid]) {
                        if (mid == 0) return 1;
                        upper = mid - 1;
                    } else if (mid < LongPowerOfTenNegative.Length - 1 && value <= LongPowerOfTenNegative[mid + 1]) {
                        lower = mid + 1;
                    } else {
                        return mid + 2;
                    }
                }
            }
        }

        private static int CountDigits(ulong value)
        {
            // Bisect through the arrays looking for the length of the number in digits
            int lower = 0; int upper = LongPowerOfTenPositive.Length - 1;
            while (true) {
                int mid = (lower + upper) >> 1;
                if (value < ULongPowerOfTenPositive[mid]) {
                    if (mid == 0) return 1;
                    upper = mid - 1;
                } else if (mid < LongPowerOfTenPositive.Length - 1 && value >= ULongPowerOfTenPositive[mid + 1]) {
                    lower = mid + 1;
                } else {
                    return mid + 2;
                }
            }
        }

        private static int CountBitDigits(ulong value, int bitsPerDigit)
        {
            if (value == 0) return 1;  // zero also needs one character
            int firstBitPos = 64;
            if (value <= 0x00000000FFFFFFFF) { firstBitPos -= 32; value <<= 32; }
            if (value <= 0x0000FFFFFFFFFFFF) { firstBitPos -= 16; value <<= 16; }
            if (value <= 0x00FFFFFFFFFFFFFF) { firstBitPos -= 8; value <<= 8; }
            if (value <= 0x0FFFFFFFFFFFFFFF) { firstBitPos -= 4; value <<= 4; }
            if (value <= 0x3FFFFFFFFFFFFFFF) { firstBitPos -= 2; value <<= 2; }
            if (value <= 0x7FFFFFFFFFFFFFFF) { firstBitPos -= 1; }

            return firstBitPos / bitsPerDigit + (firstBitPos % bitsPerDigit == 0 ? 0 : 1);
        }

        private static void LongToString(StringBuilder str, FormatSpecifier formatSpecifier, long value)
        {
            int digits;
            if (value == 0 && formatSpecifier.Precision == 0) {
                digits = 0;
            } else {
                digits = CountDigits(value);
            }

            int sign = 0;
            if (value < 0 || formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                sign = value < 0 ? formatSpecifier.NumberFormatInfo.NegativeSign.Length : formatSpecifier.NumberFormatInfo.PositiveSign.Length;
            } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                sign = 1;
            }

            int zeroes = 0;
            if (formatSpecifier.Precision >= 1) {
                zeroes = digits >= formatSpecifier.Precision ? 0 : formatSpecifier.Precision - digits;
            }

            int len = digits + zeroes + sign;
            int padding = 0;
            if (formatSpecifier.Width >= 1) {
                if (formatSpecifier.Precision < 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    zeroes = digits >= formatSpecifier.Width ? 0 : formatSpecifier.Width - digits - sign;
                } else {
                    padding = len > formatSpecifier.Width ? 0 : formatSpecifier.Width - len;
                }

                if (padding > 0 && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    str.Append(' ', padding);
                }
            }

            if (value >= 0) {
                if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    str.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    str.Append(' ');
                }
            } else {
                str.Append(formatSpecifier.NumberFormatInfo.NegativeSign);
            }

            if (zeroes > 0) str.Append('0', zeroes);

            if (value != 0 || formatSpecifier.Precision != 0) {
                char[] rawnum = new char[digits];
                long tValue = value;
                for (int i = digits; i > 0; --i) {
                    int digit = Math.Abs((int)(tValue % 10));
                    tValue /= 10;
                    rawnum[i - 1] = (char)(digit + '0');
                }
                str.Append(rawnum);
            }

            if (padding > 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                str.Append(' ', padding);
            }
        }
        #endregion

        #region Format Specifier Unsigned Integer (%u, %x, %o)
        private static void ConvertFormatSpecifierUnsignedInteger(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
        {
            try {
                ulong value = 0;
                if (formatSpecifier.Length == null || formatSpecifier.Length.Equals("l")) {
                    value = GetULongUInt(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("hh")) {
                    value = GetULongByte(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("h")) {
                    value = GetULongUShort(values[currentArg]);
                } else if (formatSpecifier.Length.Equals("ll") || formatSpecifier.Length.Equals("z") ||
                    formatSpecifier.Length.Equals("t") || formatSpecifier.Length.Equals("j")) {
                    value = GetULong(values[currentArg]);
                } else {
                    value = GetULongUInt(values[currentArg]);
                }
                currentArg++;
                UlongToString(str, formatSpecifier, value);
            } catch (InvalidCastException e) {
                string message = string.Format("Couldn't convert argument {0} to an integer", currentArg);
                throw new FormatException(message, e);
            }
        }

        private static uint GetULongUInt(object arg) { return unchecked((uint)(GetULong(arg) & 0xFFFFFFFF)); }

        private static byte GetULongByte(object arg) { return unchecked((byte)(GetULong(arg) & 0xFF)); }

        private static ushort GetULongUShort(object arg) { return unchecked((ushort)(GetULong(arg) & 0xFFFF)); }

        private static ulong GetULongBool(bool value) { return value ? unchecked((ulong)-1) : 0; }

        private static ulong GetULong(object value)
        {
            unchecked {
                if (value is int vInt) return (ulong)vInt;
                if (value is long vLong) return (ulong)vLong;
                if (value is bool vBool) return GetULongBool(vBool);
                if (value is short vShort) return (ulong)vShort;
                if (value is char vChar) return vChar;
                if (value is sbyte vsByte) return (ulong)vsByte;
                if (value is byte vByte) return vByte;
                if (value is uint vuInt) return vuInt;
                if (value is ulong vuLong) return vuLong;
                if (value is ushort vuShort) return vuShort;
                if (value is string vString && vString.Length > 0) {
                    if (ulong.TryParse(vString, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong vsuLong))
                        return vsuLong;
                }
                throw new FormatException("Parameter doesn't map to an unsigned integer");
            }
        }

        private static readonly char[] BaseDigitsLower = new[] {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        private static readonly char[] BaseDigitsUpper = new[] {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        private static void UlongToString(StringBuilder str, FormatSpecifier formatSpecifier, ulong value)
        {
            int alternative = 0;
            ulong baseNumber;
            char[] baseDigits;
            int sign = 0;
            int digits = 0;

            switch (formatSpecifier.Specifier) {
            case 'x':
                baseNumber = 16;
                baseDigits = BaseDigitsLower;
                // Sign and Blank are ignored
                if (value != 0) {
                    // Strange, that GCC only displays 0x if the value isn't zero.
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 2;
                    digits = CountBitDigits(value, 4);
                } else if (formatSpecifier.Precision != 0) {
                    digits = CountBitDigits(value, 4);
                }
                break;
            case 'X':
                baseNumber = 16;
                baseDigits = BaseDigitsUpper;
                // Sign and Blank are ignored
                if (value != 0) {
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 2;
                    digits = CountBitDigits(value, 4);
                } else if (formatSpecifier.Precision != 0) {
                    digits = CountBitDigits(value, 4);
                }
                break;
            case 'o':
                baseNumber = 8;
                baseDigits = BaseDigitsLower;
                // Sign and Blank are ignored
                if (value != 0) {
                    // Strange, that GCC only displays 0x if the value isn't zero.
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) alternative = 1;
                    digits = CountBitDigits(value, 3);
                } else if (formatSpecifier.Precision != 0) {
                    digits = CountBitDigits(value, 3);
                }
                break;
            case 'u':
                baseNumber = 10;
                baseDigits = BaseDigitsLower;
#if SHOW_ULONG_SIGN
                // In GCC, these options appear to be ignored, and so is commented out.
                if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    sign = formatSpecifier.NumberFormatInfo.PositiveSign.Length;
                } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    sign = 1;
                }
#endif
                if (value != 0 || formatSpecifier.Precision != 0) {
                    // Int64.MaxValue and UInt64.MaxValue have the same number of digits
                    digits = CountDigits(value);
                }
                break;
            default:
                throw new FormatException("Unknown Specifier converting to unsigned integer");
            }

            int zeroes = 0;
            if (formatSpecifier.Precision >= 1) {
                zeroes = digits >= formatSpecifier.Precision ? 0 : formatSpecifier.Precision - digits;
            }

            int len = alternative + zeroes + digits + sign;
            int padding = 0;
            if (formatSpecifier.Width >= 1) {
                if (formatSpecifier.Precision < 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    zeroes = digits >= formatSpecifier.Width ? 0 : formatSpecifier.Width - digits - sign;
                } else {
                    padding = len > formatSpecifier.Width ? 0 : formatSpecifier.Width - len;
                }

                if (padding > 0 && !formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                    str.Append(' ', padding);
                }
            }

#if SHOW_ULONG_SIGN
            // In GCC, these options appear to be ignored, and so is commented out.
            if (sign > 0) {
                if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    str.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    str.Append(' ');
                }
            }
#endif
            if (alternative > 0) {
                switch (formatSpecifier.Specifier) {
                case 'x': str.Append("0x"); break;
                case 'X': str.Append("0X"); break;
                case 'o': str.Append('0'); break;
                }
            }
            if (zeroes > 0) str.Append('0', zeroes);

            if (digits > 0) {
                char[] rawnum = new char[digits];
                ulong tValue = value;
                for (int i = digits; i > 0; --i) {
                    int digit = (int)(tValue % baseNumber);
                    tValue /= baseNumber;
                    rawnum[i - 1] = baseDigits[digit];
                }
                str.Append(rawnum);
            }

            if (padding > 0 && formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                str.Append(' ', padding);
            }
        }
        #endregion

        #region Format Specifier Float (%f, %e, %g) - Based on Mono
        // The code was then adapted to be pure C# and to fit in with existing code (so we don't
        // have a complete one-to-one, back porting changes from Mono to here is a manual task).
        //
        // Refer to the following files in the Mono source tree
        // * mcs/class/corlib/System/NumberFormatter.cs
        //   - master/48e112d652335da23dd1b4d5f3227e54b74d0814, commit date 07/Aug/2014 as of 08/Jan/2015
        //
        // Licensing from Mono as of 01/Jan/2015:
        // * LICENSE (master/758246c2aa9f40f2859e78def0c4d7118b767366, commit date 12/Dec/2013 as of 08/Jan/2015)
        //
        // ** mcs/class
        // The class libraries developed by the Mono team are licensed
        // under the MIT X11 terms.
        //
        // While I've tried to keep the methods as similar as possible, diff tools aren't able to compare
        // this file with NumberFormatter.cs, thus requiring a manual diff.
        private sealed class NumberFormatter
        {
            #region Static Fields
            const int DefaultExpPrecision = 6;
            const int HundredMillion = 100000000;
            const long SeventeenDigitsThreshold = 10000000000000000;

            const int DoubleBitsExponentShift = 52;
            const int DoubleBitsExponentMask = 0x7ff;
            const long DoubleBitsMantissaMask = 0xfffffffffffff;

            const int SingleDefPrecision = 7;
            const int DoubleDefPrecision = 15;

            private static long GetTenPowerOf(int i)
            {
                if (i == 0) return 1;
                return LongPowerOfTenPositive[i - 1];
            }
            #endregion Static Fields

            #region Fields
            private readonly StringBuilder _sbuf;

            private bool _NaN;
            private bool _infinity;
            private bool _specifierIsUpper;
            private bool _positive;
            private char _specifier;
            private int _precision;
            private int _defPrecision;

            private FormatSpecifier _formatSpecifier;

            private int _digitsLen;
            private int _offset; // Represent the first digit offset.
            private int _decPointPos;

            // The following fields are a hexadecimal representation of the digits.
            // For instance _val = 0x234 represents the digits '2', '3', '4'.
            private uint _val1; // Digits 0 - 7.
            private uint _val2; // Digits 8 - 15.
            private uint _val3; // Digits 16 - 23.
            private uint _val4; // Digits 23 - 31. Only needed for decimals.
            #endregion Fields

            #region Constructor Helpers
            // Translate an unsigned long to hexadecimal digits.
            private void InitDecHexDigits(ulong value)
            {
                if (value >= HundredMillion) {
                    long div1 = (long)(value / HundredMillion);
                    value -= HundredMillion * (ulong)div1;
                    if (div1 >= HundredMillion) {
                        int div2 = (int)(div1 / HundredMillion);
                        div1 -= div2 * (long)HundredMillion;
                        _val3 = ToDecHex(div2);
                    }
                    if (div1 != 0)
                        _val2 = ToDecHex((int)(div1));
                }
                if (value != 0)
                    _val1 = ToDecHex((int)value);
            }

            // Helper to translate an int in the range 0 .. 9999 to its
            // Hexadecimal digits representation.
            private static uint FastToDecHex(int val)
            {
                if (val < 100)
                    return (uint)Formatter_DecHexDigits[val];

                // Uses 2^19 (524288) to compute val / 100 for val < 10000.
                int v = (val * 5243) >> 19;
                return (uint)((Formatter_DecHexDigits[v] << 8) | Formatter_DecHexDigits[val - v * 100]);
            }

            // Helper to translate an int in the range 0 .. 99999999 to its
            // Hexadecimal digits representation.
            private static uint ToDecHex(int val)
            {
                uint res = 0;
                if (val >= 10000) {
                    int v = val / 10000;
                    val -= v * 10000;
                    res = FastToDecHex(v) << 16;
                }
                return res | FastToDecHex(val);
            }

            // Helper to count number of hexadecimal digits in a number.
            private static int FastDecHexLen(int val)
            {
                if (val < 0x100)
                    if (val < 0x10)
                        return 1;
                    else
                        return 2;
                else if (val < 0x1000)
                    return 3;
                else
                    return 4;
            }

            private static int DecHexLen(uint val)
            {
                if (val < 0x10000)
                    return FastDecHexLen((int)val);
                return 4 + FastDecHexLen((int)(val >> 16));
            }

            // Count number of hexadecimal digits stored in _val1 .. _val4.
            private int DecHexLen()
            {
                if (_val4 != 0)
                    return DecHexLen(_val4) + 24;
                else if (_val3 != 0)
                    return DecHexLen(_val3) + 16;
                else if (_val2 != 0)
                    return DecHexLen(_val2) + 8;
                else if (_val1 != 0)
                    return DecHexLen(_val1);
                else
                    return 0;
            }

            // Helper to count the 10th scale (number of digits) in a number
            private static int ScaleOrder(long hi)
            {
                return CountDigits(hi);
            }

            // Compute the initial precision for rounding a floating number
            // according to the used format.
            private int InitialFloatingPrecision()
            {
                if (_precision < _defPrecision)
                    return _defPrecision;
                if (_specifier == 'G')
                    return Math.Min(_defPrecision + 2, _precision);
                if (_specifier == 'E')
                    return Math.Min(_defPrecision + 2, _precision + 1);
                return _defPrecision;
            }
            #endregion Constructor Helpers

            #region Constructors
            // Parse the given format and initialize the following fields:
            //   _isCustomFormat, _specifierIsUpper, _specifier & _precision.
            public NumberFormatter(StringBuilder sb, FormatSpecifier format)
            {
                _sbuf = sb;
                Init(format);
            }

            private void Init(FormatSpecifier format)
            {
                _val1 = _val2 = _val3 = _val4 = 0;
                _offset = 0;
                _NaN = _infinity = false;
                _specifierIsUpper = false;
                _precision = format.Precision;

                if (format.Specifier >= 'A' && format.Specifier <= 'Z') {
                    _specifierIsUpper = true;
                    _specifier = format.Specifier;
                } else {
                    _specifier = (char)(format.Specifier - 'a' + 'A');
                }

                _formatSpecifier = format;
            }

            public void Init(double value, int defPrecision)
            {
                _defPrecision = defPrecision;

                // We assume the number is a 64-bit IEEE 754 compatible floating point digit.
                //   Bit 63    = sign (s)
                //   Bit 62-52 = exponent (e)
                //   Bit 51- 0 = mantissa (m)
                // float = (s == 0 ? 1 : -1) * 1.m * 2 ^ (e-1023)  (normal; e != 0; e != 2047)
                // float = (s == 0 ? 1 : -1) * 0.m * 2 ^ -1023     (subnormal; e == 0)
                long bits = BitConverter.DoubleToInt64Bits(value);
                _positive = bits >= 0;
                bits &= Int64.MaxValue;
                if (bits == 0) {
                    _decPointPos = 1;
                    _digitsLen = 0;
                    return;
                }

                int e = (int)(bits >> DoubleBitsExponentShift);
                long m = bits & DoubleBitsMantissaMask;  // m is the lower 52 bits
                if (e == DoubleBitsExponentMask) {    // If (e == 0x7FF)
                    _NaN = m != 0;                    //  m != 0 => NaN
                    _infinity = m == 0;               //  m == 0 => Inf (see also sign)
                    return;
                }

                int expAdjust = 0;
                if (e == 0) {
                    // This is a so-called "subnormal" floating point number. We lose precision
                    // so that we can have even smaller numbers.

                    // We need 'm' to be large enough so we won't lose precision.
                    e = 1;
                    int scale = ScaleOrder(m);
                    if (scale < DoubleDefPrecision) {
                        expAdjust = scale - DoubleDefPrecision;
                        m *= GetTenPowerOf(-expAdjust);
                    }
                } else {
                    m = (m + DoubleBitsMantissaMask + 1) * 10;
                    expAdjust = -1;
                }

                // The following converts a floating point (base 2) to two new
                // components:
                // * res: An integer value (of maximum precision that can be held within a long)
                // * expAdjust: Exponent (base 10)

                // multiply the mantissa by 10 ^ N
                unchecked {
                    ulong lo = (uint)m;              // m can be 52-bits, keep the lower 32 bits, as hi contains the rest.
                    ulong hi = (ulong)m >> 32;
                    ulong lo2 = Formatter_MantissaBitsTable[e];
                    ulong hi2 = lo2 >> 32;
                    lo2 = (uint)lo2;
                    ulong mm = hi * lo2 + lo * hi2 + ((lo * lo2) >> 32);
                    long res = (long)(hi * hi2 + (mm >> 32));
                    while (res < SeventeenDigitsThreshold) {
                        mm = (mm & uint.MaxValue) * 10;
                        res = res * 10 + (long)(mm >> 32);
                        expAdjust--;
                    }
                    if ((mm & 0x80000000) != 0) res++;

                    int order = DoubleDefPrecision + 2;
                    _decPointPos = Formatter_TensExponentTable[e] + expAdjust + order;

                    // Rescale 'res' to the initial precision (15-17 for doubles).
                    int initialPrecision = InitialFloatingPrecision();
                    if (order > initialPrecision) {
                        long val = GetTenPowerOf(order - initialPrecision);
                        res = (res + (val >> 1)) / val;
                        order = initialPrecision;
                    }
                    if (res >= GetTenPowerOf(order)) {
                        order++;
                        _decPointPos++;
                    }

                    InitDecHexDigits((ulong)res);
                    _offset = CountTrailingZeros();
                    _digitsLen = order - _offset;
                }
            }
            #endregion Constructors

            #region Helper properties
            private int IntegerDigits
            {
                get { return _decPointPos > 0 ? _decPointPos : 1; }
            }
            #endregion Helper properties

            #region Round
            private void RoundPos(int pos)
            {
                RoundBits(_digitsLen - pos);
            }

            private bool RoundDecimal(int decimals)
            {
                return RoundBits(_digitsLen - _decPointPos - decimals);
            }

            private bool RoundBits(int shift)
            {
                if (shift <= 0)
                    return false;

                if (shift > _digitsLen) {
                    _digitsLen = 0;
                    _decPointPos = 1;
                    _val1 = _val2 = _val3 = _val4 = 0;
                    _positive = true;
                    return false;
                }
                shift += _offset;
                _digitsLen += _offset;
                while (shift > 8) {
                    _val1 = _val2;
                    _val2 = _val3;
                    _val3 = _val4;
                    _val4 = 0;
                    _digitsLen -= 8;
                    shift -= 8;
                }
                shift = (shift - 1) << 2;
                uint v = _val1 >> shift;
                uint rem16 = v & 0xf;
                _val1 = (v ^ rem16) << shift;
                bool res = false;
                if (rem16 >= 0x5) {
                    _val1 |= 0x99999999 >> (28 - shift);
                    AddOneToDecHex();
                    int newlen = DecHexLen();
                    res = newlen != _digitsLen;
                    _decPointPos = _decPointPos + newlen - _digitsLen;
                    _digitsLen = newlen;
                }
                RemoveTrailingZeros();
                return res;
            }

            private void RemoveTrailingZeros()
            {
                _offset = CountTrailingZeros();
                _digitsLen -= _offset;
                if (_digitsLen == 0) {
                    _offset = 0;
                    _decPointPos = 1;
                    _positive = true;
                }
            }

            private void AddOneToDecHex()
            {
                if (_val1 == 0x99999999) {
                    _val1 = 0;
                    if (_val2 == 0x99999999) {
                        _val2 = 0;
                        if (_val3 == 0x99999999) {
                            _val3 = 0;
                            _val4 = AddOneToDecHex(_val4);
                        } else
                            _val3 = AddOneToDecHex(_val3);
                    } else
                        _val2 = AddOneToDecHex(_val2);
                } else
                    _val1 = AddOneToDecHex(_val1);
            }

            // Assume val != 0x99999999
            private static uint AddOneToDecHex(uint val)
            {
                if ((val & 0xffff) == 0x9999)
                    if ((val & 0xffffff) == 0x999999)
                        if ((val & 0xfffffff) == 0x9999999)
                            return val + 0x06666667;
                        else
                            return val + 0x00666667;
                    else if ((val & 0xfffff) == 0x99999)
                        return val + 0x00066667;
                    else
                        return val + 0x00006667;
                else if ((val & 0xff) == 0x99)
                    if ((val & 0xfff) == 0x999)
                        return val + 0x00000667;
                    else
                        return val + 0x00000067;
                else if ((val & 0xf) == 0x9)
                    return val + 0x00000007;
                else
                    return val + 1;
            }

            private int CountTrailingZeros()
            {
                if (_val1 != 0)
                    return CountTrailingZeros(_val1);
                if (_val2 != 0)
                    return CountTrailingZeros(_val2) + 8;
                if (_val3 != 0)
                    return CountTrailingZeros(_val3) + 16;
                if (_val4 != 0)
                    return CountTrailingZeros(_val4) + 24;
                return _digitsLen;
            }

            private static int CountTrailingZeros(uint val)
            {
                if ((val & 0xffff) == 0)
                    if ((val & 0xffffff) == 0)
                        if ((val & 0xfffffff) == 0)
                            return 7;
                        else
                            return 6;
                    else if ((val & 0xfffff) == 0)
                        return 5;
                    else
                        return 4;
                else if ((val & 0xff) == 0)
                    if ((val & 0xfff) == 0)
                        return 3;
                    else
                        return 2;
                else if ((val & 0xf) == 0)
                    return 1;
                else
                    return 0;
            }
            #endregion Round

            #region public number formatting methods
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Provided for code consistency")]
            public static void FloatToString(StringBuilder str, FormatSpecifier format, float value, IFormatProvider fp)
            {
                NumberFormatter inst = new NumberFormatter(str, format);
                inst.Init(value, SingleDefPrecision);
                NumberFormatInfo nfi = format.NumberFormatInfo;
                if (inst.FormatInfNan(format, true)) return;
                inst.NumberToString(format, nfi);
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Provided for code consistency")]
            public static void DoubleToString(StringBuilder str, FormatSpecifier format, double value, IFormatProvider fp)
            {
                NumberFormatter inst = new NumberFormatter(str, format);
                inst.Init(value, DoubleDefPrecision);
                NumberFormatInfo nfi = format.NumberFormatInfo;
                if (inst.FormatInfNan(format, true)) return;
                inst.NumberToString(format, nfi);
            }

            private void NumberToString(FormatSpecifier format, NumberFormatInfo nfi)
            {
                switch (_specifier) {
                case 'E':
                    FormatExponential(_precision, nfi); break;
                case 'F':
                    FormatFixedPoint(_precision, nfi); break;
                case 'G':
                    FormatGeneral(_precision, nfi); break;
                default:
                    string message = string.Format("The specified format '{0}' is invalid", format.Specifier);
                    throw new FormatException(message);
                }
            }

            private void FormatFixedPoint(int precision, NumberFormatInfo nfi)
            {
                // The default precision for %f
                if (precision == -1) precision = DefaultExpPrecision;

                RoundDecimal(precision);

                // Calculate the number of characters we need in total
                int signLen = SignLength(nfi);
                int decSepLen = 0;
                if (precision > 0 || _formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    decSepLen = nfi.NumberDecimalSeparator.Length;
                }

                int bufLen = IntegerDigits + precision + decSepLen + signLen;
                // Take into account Blank, ShowSign, ZeroPad, Left/Right justify.
                if (bufLen < _formatSpecifier.Width) {
                    if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        // Right justify with zero's
                        if (signLen > 0) FormatSign(nfi);
                        _sbuf.Append('0', _formatSpecifier.Width - bufLen);
                        FormatFixedPointBasic(precision, nfi);
                    } else {
                        if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                            if (signLen > 0) FormatSign(nfi);
                            FormatFixedPointBasic(precision, nfi);
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                        } else {
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                            if (signLen > 0) FormatSign(nfi);
                            FormatFixedPointBasic(precision, nfi);
                        }
                    }
                } else {
                    if (signLen > 0) FormatSign(nfi);
                    FormatFixedPointBasic(precision, nfi);
                }
            }

            private void FormatFixedPointBasic(int precision, NumberFormatInfo nfi)
            {
                AppendIntegerString(IntegerDigits);

                if (precision > 0) {
                    _sbuf.Append(nfi.NumberDecimalSeparator);
                    AppendDecimalString(precision);
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    // Alternate just shows a '.' with no values after the decimal
                    _sbuf.Append(nfi.NumberDecimalSeparator);
                }
            }

            private void FormatGeneral(int precision, NumberFormatInfo nfi)
            {
                int initialPrecision = precision;
                if (precision == -1) {
                    precision = DefaultExpPrecision;
                } else {
                    if (precision == 0) precision = 1;
                }
                RoundPos(precision);

                int intDigits = _decPointPos;
                int digits = _digitsLen;
                int decDigits = digits - intDigits;

                if ((intDigits > precision || intDigits <= -4)) {
                    if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                        if (initialPrecision == -1) {
                            FormatExponential(precision - 1, nfi, 2);
                        } else {
                            FormatExponential(initialPrecision, nfi, 2);
                        }
                    } else {
                        FormatExponential(digits - 1, nfi, 2);
                    }
                    return;
                }

                if (decDigits < 0) decDigits = 0;
                if (intDigits < 0) intDigits = 0;

                int extraDigits = 0;
                bool dec = (decDigits > 0);
                if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    dec = true;
                    // Show the decimal point, even if it might not be present
                    if (decDigits == 0 && precision > intDigits) {
                        extraDigits = precision - intDigits - decDigits;
                    }
                }

                // Calculate the number of characters we need in total
                int signLen = SignLength(nfi);
                int bufLen = decDigits + extraDigits + (dec ? 1 : 0) +
                    (intDigits == 0 ? 1 : intDigits) +
                    signLen;

                if (bufLen < _formatSpecifier.Width) {
                    if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        // Right justify with zero's
                        if (signLen > 0) FormatSign(nfi);
                        _sbuf.Append('0', _formatSpecifier.Width - bufLen);
                        FormatGeneralBasic(nfi, digits, decDigits, intDigits, extraDigits);
                    } else {
                        if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                            if (signLen > 0) FormatSign(nfi);
                            FormatGeneralBasic(nfi, digits, decDigits, intDigits, extraDigits);
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                        } else {
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                            if (signLen > 0) FormatSign(nfi);
                            FormatGeneralBasic(nfi, digits, decDigits, intDigits, extraDigits);
                        }
                    }
                } else {
                    if (signLen > 0) FormatSign(nfi);
                    FormatGeneralBasic(nfi, digits, decDigits, intDigits, extraDigits);
                }
            }

            private void FormatGeneralBasic(NumberFormatInfo nfi, int digits, int decDigits, int intDigits, int extraDigits)
            {
                if (intDigits == 0)
                    _sbuf.Append('0');
                else
                    AppendDigits(digits - intDigits, digits);

                if (extraDigits > 0 || decDigits > 0 || _formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    _sbuf.Append(nfi.NumberDecimalSeparator);
                }

                if (decDigits > 0) AppendDigits(0, decDigits);
                if (extraDigits > 0) _sbuf.Append('0', extraDigits);
            }

            public void FormatExponential(int precision, NumberFormatInfo nfi)
            {
                if (precision == -1) precision = DefaultExpPrecision;

                RoundPos(precision + 1);
                FormatExponential(precision, nfi, 2);    // GCC shows 2 digits, MSVCRT shows 3.
            }

            private void FormatExponential(int precision, NumberFormatInfo nfi, int expDigits)
            {
                int decDigits = _decPointPos;
                int exponent = decDigits - 1;
                decDigits = _decPointPos = 1;

                // Calculate the number of characters we need in total
                int signLen = SignLength(nfi);
                int decSepLen = 0;
                if (precision > 0 || _formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    decSepLen = nfi.NumberDecimalSeparator.Length;
                }

                // 1[.[xxxx]]E[+-]xx
                int bufLen = 1 + precision + decSepLen + signLen + 2 + Math.Max(exponent, expDigits);
                if (bufLen < _formatSpecifier.Width) {
                    if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.ZeroPad) && !_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        // Right justify with zero's
                        if (signLen > 0) FormatSign(nfi);
                        _sbuf.Append('0', _formatSpecifier.Width - bufLen);
                        FormatExponentialBasic(precision, nfi, expDigits, exponent);
                    } else {
                        if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                            if (signLen > 0) FormatSign(nfi);
                            FormatExponentialBasic(precision, nfi, expDigits, exponent);
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                        } else {
                            _sbuf.Append(' ', _formatSpecifier.Width - bufLen);
                            if (signLen > 0) FormatSign(nfi);
                            FormatExponentialBasic(precision, nfi, expDigits, exponent);
                        }
                    }
                } else {
                    if (signLen > 0) FormatSign(nfi);
                    FormatExponentialBasic(precision, nfi, expDigits, exponent);
                }
            }

            private void FormatExponentialBasic(int precision, NumberFormatInfo nfi, int expDigits, int exponent)
            {
                AppendOneDigit(_digitsLen - 1);

                if (precision > 0) {
                    _sbuf.Append(nfi.NumberDecimalSeparator);
                    AppendDigits(_digitsLen - precision - 1, _digitsLen - _decPointPos);
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Alternative)) {
                    // Alternate just shows a '.' with no values after the decimal
                    _sbuf.Append(nfi.NumberDecimalSeparator);
                }

                AppendExponent(nfi, exponent, expDigits);
            }

            private int SignLength(NumberFormatInfo nfi)
            {
                if (!_positive) {
                    return nfi.NegativeSign.Length;
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    return 1;
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    return nfi.PositiveSign.Length;
                }
                return 0;
            }

            private void FormatSign(NumberFormatInfo nfi)
            {
                if (!_positive) {
                    _sbuf.Append(nfi.NegativeSign);
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                    _sbuf.Append(' ');
                } else if (_formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                    _sbuf.Append(nfi.PositiveSign);
                }
            }
            #endregion public number formatting methods

            #region Append helpers
            private void AppendIntegerString(int minLength)
            {
                if (_decPointPos <= 0) {
                    _sbuf.Append('0', minLength);
                    return;
                }

                if (_decPointPos < minLength)
                    _sbuf.Append('0', minLength - _decPointPos);

                AppendDigits(_digitsLen - _decPointPos, _digitsLen);
            }

            private void AppendDecimalString(int precision)
            {
                AppendDigits(_digitsLen - precision - _decPointPos, _digitsLen - _decPointPos);
            }

            // minDigits is in the range 1..3
            private void AppendExponent(NumberFormatInfo nfi, int exponent, int minDigits)
            {
                if (_specifierIsUpper || _specifier == 'R')
                    _sbuf.Append('E');
                else
                    _sbuf.Append('e');

                if (exponent >= 0)
                    _sbuf.Append(nfi.PositiveSign);
                else {
                    _sbuf.Append(nfi.NegativeSign);
                    exponent = -exponent;
                }

                if (exponent == 0)
                    _sbuf.Append('0', minDigits);
                else if (exponent < 10) {
                    _sbuf.Append('0', minDigits - 1);
                    _sbuf.Append((char)('0' | exponent));
                } else {
                    uint hexDigit = FastToDecHex(exponent);
                    if (exponent >= 100 || minDigits == 3)
                        _sbuf.Append((char)('0' | (hexDigit >> 8)));
                    _sbuf.Append((char)('0' | ((hexDigit >> 4) & 0xf)));
                    _sbuf.Append((char)('0' | (hexDigit & 0xf)));
                }
            }

            private void AppendOneDigit(int start)
            {
                start += _offset;
                uint v;
                if (start < 0)
                    v = 0;
                else if (start < 8)
                    v = _val1;
                else if (start < 16)
                    v = _val2;
                else if (start < 24)
                    v = _val3;
                else if (start < 32)
                    v = _val4;
                else
                    v = 0;
                v >>= (start & 0x7) << 2;
                _sbuf.Append((char)('0' | v & 0xf));
            }

            private void AppendDigits(int start, int end)
            {
                if (start >= end)
                    return;

                int i = end - start;
                char[] cbuf = new char[i];

                end += _offset;
                start += _offset;

                for (int next = start + 8 - (start & 0x7); ; start = next, next += 8) {
                    uint v;
                    if (next == 8)
                        v = _val1;
                    else if (next == 16)
                        v = _val2;
                    else if (next == 24)
                        v = _val3;
                    else if (next == 32)
                        v = _val4;
                    else
                        v = 0;
                    v >>= (start & 0x7) << 2;
                    if (next > end)
                        next = end;

                    cbuf[--i] = (char)('0' | v & 0xf);
                    switch (next - start) {
                    case 8:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 7;
                    case 7:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 6;
                    case 6:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 5;
                    case 5:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 4;
                    case 4:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 3;
                    case 3:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 2;
                    case 2:
                        cbuf[--i] = (char)('0' | (v >>= 4) & 0xf);
                        goto case 1;
                    case 1:
                        if (next == end) {
                            _sbuf.Append(cbuf);
                            return;
                        }
                        continue;
                    }
                }
            }
            #endregion Append helpers

            private bool FormatInfNan(FormatSpecifier formatSpecifier, bool useC)
            {
                if (_NaN) {
                    string nan = useC ?
                        (_specifierIsUpper ? "NAN" : "nan") :
                        (formatSpecifier.NumberFormatInfo.NaNSymbol);
                    int sign = 0;
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                        sign = 1;
                    } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                        sign = formatSpecifier.NumberFormatInfo.PositiveSign.Length;
                    }
                    int length = nan.Length + sign;
                    if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                        if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                            _sbuf.Append(' ');
                        } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                            _sbuf.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                        }
                        _sbuf.Append(nan);
                        if (length < formatSpecifier.Width) {
                            _sbuf.Append(' ', formatSpecifier.Width - length);
                        }
                    } else {
                        if (length < formatSpecifier.Width) {
                            _sbuf.Append(' ', formatSpecifier.Width - length);
                        }
                        if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                            _sbuf.Append(' ');
                        } else if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                            _sbuf.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                        }
                        _sbuf.Append(nan);
                    }
                    return true;
                }

                if (_infinity) {
                    string inf;
                    if (useC) {
                        inf = _specifierIsUpper ? "INF" : "inf";
                        int sign = 0;
                        if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                            sign = 1;
                        } else if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                            sign = formatSpecifier.NumberFormatInfo.PositiveSign.Length;
                        } else if (!_positive) {
                            sign = formatSpecifier.NumberFormatInfo.NegativeSign.Length;
                        }
                        int length = inf.Length + sign;
                        if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                            if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                                _sbuf.Append(' ');
                            } else if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                                _sbuf.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                            } else if (!_positive) {
                                _sbuf.Append(formatSpecifier.NumberFormatInfo.NegativeSign);
                            }
                            _sbuf.Append(inf);
                            if (length < formatSpecifier.Width) {
                                _sbuf.Append(' ', formatSpecifier.Width - length);
                            }
                        } else {
                            if (length < formatSpecifier.Width) {
                                _sbuf.Append(' ', formatSpecifier.Width - length);
                            }
                            if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.Blank)) {
                                _sbuf.Append(' ');
                            } else if (_positive && formatSpecifier.FormatFlags.HasFlag(FormatFlags.ShowSign)) {
                                _sbuf.Append(formatSpecifier.NumberFormatInfo.PositiveSign);
                            } else if (!_positive) {
                                _sbuf.Append(formatSpecifier.NumberFormatInfo.NegativeSign);
                            }
                            _sbuf.Append(inf);
                        }
                    } else {
                        inf = _positive ? formatSpecifier.NumberFormatInfo.PositiveInfinitySymbol : formatSpecifier.NumberFormatInfo.NegativeInfinitySymbol;
                        int length = inf.Length;
                        if (formatSpecifier.FormatFlags.HasFlag(FormatFlags.LeftJustify)) {
                            _sbuf.Append(inf);
                            if (length < formatSpecifier.Width) {
                                _sbuf.Append(' ', formatSpecifier.Width - length);
                            }
                        } else {
                            if (length < formatSpecifier.Width) {
                                _sbuf.Append(' ', formatSpecifier.Width - length);
                            }
                            _sbuf.Append(inf);
                        }
                    }
                    return true;
                }
                return false;
            }
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

        private static void ConvertFormatSpecifierFloatMono(StringBuilder str, FormatSpecifier formatSpecifier, ref int currentArg, params object[] values)
        {
            double value = GetDouble(values[currentArg]);

            if (values[currentArg] is double) {
                NumberFormatter.DoubleToString(str, formatSpecifier, value, formatSpecifier.NumberFormatInfo);
            } else {
                NumberFormatter.FloatToString(str, formatSpecifier, (float)value, formatSpecifier.NumberFormatInfo);
            }
            currentArg++;
        }

        #region Mono Constants
        // Taken from Mono sources
        // * mono/metatdata/number-formatter.h
        //   master/9af0b8a2224e2eb6a05f00607805a180b1c67a35 (commit date 27/05/2008, as of 08/Jan/2015)
        private static readonly ulong[] Formatter_MantissaBitsTable = new ulong[] {
            4556951262222748432, 9113902524445496865, 1822780504889099373,
            3645561009778198746, 7291122019556397492, 14582244039112794984,
            2916448807822558996, 5832897615645117993, 11665795231290235987,
            2333159046258047197, 4666318092516094394, 9332636185032188789,
            1866527237006437757, 3733054474012875515, 7466108948025751031,
            14932217896051502063, 2986443579210300412, 5972887158420600825,
            11945774316841201651, 2389154863368240330, 4778309726736480660,
            9556619453472961320, 1911323890694592264, 3822647781389184528,
            7645295562778369056, 15290591125556738113, 3058118225111347622,
            6116236450222695245, 12232472900445390490, 2446494580089078098,
            4892989160178156196, 9785978320356312392, 1957195664071262478,
            3914391328142524957, 7828782656285049914, 15657565312570099828,
            3131513062514019965, 6263026125028039931, 12526052250056079862,
            2505210450011215972, 5010420900022431944, 10020841800044863889,
            2004168360008972777, 4008336720017945555, 8016673440035891111,
            16033346880071782223, 3206669376014356444, 6413338752028712889,
            12826677504057425779, 2565335500811485155, 5130671001622970311,
            10261342003245940623, 2052268400649188124, 4104536801298376249,
            8209073602596752498, 16418147205193504997, 3283629441038700999,
            6567258882077401998, 13134517764154803997, 2626903552830960799,
            5253807105661921599, 10507614211323843198, 2101522842264768639,
            4203045684529537279, 8406091369059074558, 16812182738118149117,
            3362436547623629823, 6724873095247259646, 13449746190494519293,
            2689949238098903858, 5379898476197807717, 10759796952395615435,
            2151959390479123087, 4303918780958246174, 8607837561916492348,
            17215675123832984696, 3443135024766596939, 6886270049533193878,
            13772540099066387756, 2754508019813277551, 5509016039626555102,
            11018032079253110205, 2203606415850622041, 4407212831701244082,
            8814425663402488164, 17628851326804976328, 3525770265360995265,
            7051540530721990531, 14103081061443981063, 2820616212288796212,
            5641232424577592425, 11282464849155184850, 2256492969831036970,
            4512985939662073940, 9025971879324147880, 18051943758648295760,
            3610388751729659152, 7220777503459318304, 14441555006918636608,
            2888311001383727321, 5776622002767454643, 11553244005534909286,
            2310648801106981857, 4621297602213963714, 9242595204427927429,
            1848519040885585485, 3697038081771170971, 7394076163542341943,
            14788152327084683887, 2957630465416936777, 5915260930833873554,
            11830521861667747109, 2366104372333549421, 4732208744667098843,
            9464417489334197687, 1892883497866839537, 3785766995733679075,
            7571533991467358150, 15143067982934716300, 3028613596586943260,
            6057227193173886520, 12114454386347773040, 2422890877269554608,
            4845781754539109216, 9691563509078218432, 1938312701815643686,
            3876625403631287372, 7753250807262574745, 15506501614525149491,
            3101300322905029898, 6202600645810059796, 12405201291620119593,
            2481040258324023918, 4962080516648047837, 9924161033296095674,
            1984832206659219134, 3969664413318438269, 7939328826636876539,
            15878657653273753079, 3175731530654750615, 6351463061309501231,
            12702926122619002463, 2540585224523800492, 5081170449047600985,
            10162340898095201970, 2032468179619040394, 4064936359238080788,
            8129872718476161576, 16259745436952323153, 3251949087390464630,
            6503898174780929261, 13007796349561858522, 2601559269912371704,
            5203118539824743409, 10406237079649486818, 2081247415929897363,
            4162494831859794727, 8324989663719589454, 16649979327439178909,
            3329995865487835781, 6659991730975671563, 13319983461951343127,
            2663996692390268625, 5327993384780537250, 10655986769561074501,
            2131197353912214900, 4262394707824429800, 8524789415648859601,
            17049578831297719202, 3409915766259543840, 6819831532519087681,
            13639663065038175362, 2727932613007635072, 5455865226015270144,
            10911730452030540289, 2182346090406108057, 4364692180812216115,
            8729384361624432231, 17458768723248864463, 3491753744649772892,
            6983507489299545785, 13967014978599091570, 2793402995719818314,
            5586805991439636628, 11173611982879273256, 2234722396575854651,
            4469444793151709302, 8938889586303418605, 17877779172606837210,
            3575555834521367442, 7151111669042734884, 14302223338085469768,
            2860444667617093953, 5720889335234187907, 11441778670468375814,
            2288355734093675162, 4576711468187350325, 9153422936374700651,
            1830684587274940130, 3661369174549880260, 7322738349099760521,
            14645476698199521043, 2929095339639904208, 5858190679279808417,
            11716381358559616834, 2343276271711923366, 4686552543423846733,
            9373105086847693467, 1874621017369538693, 3749242034739077387,
            7498484069478154774, 14996968138956309548, 2999393627791261909,
            5998787255582523819, 11997574511165047638, 2399514902233009527,
            4799029804466019055, 9598059608932038110, 1919611921786407622,
            3839223843572815244, 7678447687145630488, 15356895374291260977,
            3071379074858252195, 6142758149716504390, 12285516299433008781,
            2457103259886601756, 4914206519773203512, 9828413039546407025,
            1965682607909281405, 3931365215818562810, 7862730431637125620,
            15725460863274251240, 3145092172654850248, 6290184345309700496,
            12580368690619400992, 2516073738123880198, 5032147476247760397,
            10064294952495520794, 2012858990499104158, 4025717980998208317,
            8051435961996416635, 16102871923992833270, 3220574384798566654,
            6441148769597133308, 12882297539194266616, 2576459507838853323,
            5152919015677706646, 10305838031355413293, 2061167606271082658,
            4122335212542165317, 8244670425084330634, 16489340850168661269,
            3297868170033732253, 6595736340067464507, 13191472680134929015,
            2638294536026985803, 5276589072053971606, 10553178144107943212,
            2110635628821588642, 4221271257643177284, 8442542515286354569,
            16885085030572709139, 3377017006114541827, 6754034012229083655,
            13508068024458167311, 2701613604891633462, 5403227209783266924,
            10806454419566533849, 2161290883913306769, 4322581767826613539,
            8645163535653227079, 17290327071306454158, 3458065414261290831,
            6916130828522581663, 13832261657045163327, 2766452331409032665,
            5532904662818065330, 11065809325636130661, 2213161865127226132,
            4426323730254452264, 8852647460508904529, 17705294921017809058,
            3541058984203561811, 7082117968407123623, 14164235936814247246,
            2832847187362849449, 5665694374725698898, 11331388749451397797,
            2266277749890279559, 4532555499780559119, 9065110999561118238,
            1813022199912223647, 3626044399824447295, 7252088799648894590,
            14504177599297789180, 2900835519859557836, 5801671039719115672,
            11603342079438231344, 2320668415887646268, 4641336831775292537,
            9282673663550585075, 1856534732710117015, 3713069465420234030,
            7426138930840468060, 14852277861680936121, 2970455572336187224,
            5940911144672374448, 11881822289344748896, 2376364457868949779,
            4752728915737899558, 9505457831475799117, 1901091566295159823,
            3802183132590319647, 7604366265180639294, 15208732530361278588,
            3041746506072255717, 6083493012144511435, 12166986024289022870,
            2433397204857804574, 4866794409715609148, 9733588819431218296,
            1946717763886243659, 3893435527772487318, 7786871055544974637,
            15573742111089949274, 3114748422217989854, 6229496844435979709,
            12458993688871959419, 2491798737774391883, 4983597475548783767,
            9967194951097567535, 1993438990219513507, 3986877980439027014,
            7973755960878054028, 15947511921756108056, 3189502384351221611,
            6379004768702443222, 12758009537404886445, 2551601907480977289,
            5103203814961954578, 10206407629923909156, 2041281525984781831,
            4082563051969563662, 8165126103939127325, 16330252207878254650,
            3266050441575650930, 6532100883151301860, 13064201766302603720,
            2612840353260520744, 5225680706521041488, 10451361413042082976,
            2090272282608416595, 4180544565216833190, 8361089130433666380,
            16722178260867332761, 3344435652173466552, 6688871304346933104,
            13377742608693866209, 2675548521738773241, 5351097043477546483,
            10702194086955092967, 2140438817391018593, 4280877634782037187,
            8561755269564074374, 17123510539128148748, 3424702107825629749,
            6849404215651259499, 13698808431302518998, 2739761686260503799,
            5479523372521007599, 10959046745042015198, 2191809349008403039,
            4383618698016806079, 8767237396033612159, 17534474792067224318,
            3506894958413444863, 7013789916826889727, 14027579833653779454,
            2805515966730755890, 5611031933461511781, 11222063866923023563,
            2244412773384604712, 4488825546769209425, 8977651093538418850,
            17955302187076837701, 3591060437415367540, 7182120874830735080,
            14364241749661470161, 2872848349932294032, 5745696699864588064,
            11491393399729176129, 2298278679945835225, 4596557359891670451,
            9193114719783340903, 1838622943956668180, 3677245887913336361,
            7354491775826672722, 14708983551653345445, 2941796710330669089,
            5883593420661338178, 11767186841322676356, 2353437368264535271,
            4706874736529070542, 9413749473058141084, 1882749894611628216,
            3765499789223256433, 7530999578446512867, 15061999156893025735,
            3012399831378605147, 6024799662757210294, 12049599325514420588,
            2409919865102884117, 4819839730205768235, 9639679460411536470,
            1927935892082307294, 3855871784164614588, 7711743568329229176,
            15423487136658458353, 3084697427331691670, 6169394854663383341,
            12338789709326766682, 2467757941865353336, 4935515883730706673,
            9871031767461413346, 1974206353492282669, 3948412706984565338,
            7896825413969130677, 15793650827938261354, 3158730165587652270,
            6317460331175304541, 12634920662350609083, 2526984132470121816,
            5053968264940243633, 10107936529880487266, 2021587305976097453,
            4043174611952194906, 8086349223904389813, 16172698447808779626,
            3234539689561755925, 6469079379123511850, 12938158758247023701,
            2587631751649404740, 5175263503298809480, 10350527006597618960,
            2070105401319523792, 4140210802639047584, 8280421605278095168,
            16560843210556190337, 3312168642111238067, 6624337284222476135,
            13248674568444952270, 2649734913688990454, 5299469827377980908,
            10598939654755961816, 2119787930951192363, 4239575861902384726,
            8479151723804769452, 16958303447609538905, 3391660689521907781,
            6783321379043815562, 13566642758087631124, 2713328551617526224,
            5426657103235052449, 10853314206470104899, 2170662841294020979,
            4341325682588041959, 8682651365176083919, 17365302730352167839,
            3473060546070433567, 6946121092140867135, 13892242184281734271,
            2778448436856346854, 5556896873712693708, 11113793747425387417,
            2222758749485077483, 4445517498970154966, 8891034997940309933,
            17782069995880619867, 3556413999176123973, 7112827998352247947,
            14225655996704495894, 2845131199340899178, 5690262398681798357,
            11380524797363596715, 2276104959472719343, 4552209918945438686,
            9104419837890877372, 1820883967578175474, 3641767935156350948,
            7283535870312701897, 14567071740625403795, 2913414348125080759,
            5826828696250161518, 11653657392500323036, 2330731478500064607,
            4661462957000129214, 9322925914000258429, 1864585182800051685,
            3729170365600103371, 7458340731200206743, 14916681462400413486,
            2983336292480082697, 5966672584960165394, 11933345169920330789,
            2386669033984066157, 4773338067968132315, 9546676135936264631,
            1909335227187252926, 3818670454374505852, 7637340908749011705,
            15274681817498023410, 3054936363499604682, 6109872726999209364,
            12219745453998418728, 2443949090799683745, 4887898181599367491,
            9775796363198734982, 1955159272639746996, 3910318545279493993,
            7820637090558987986, 15641274181117975972, 3128254836223595194,
            6256509672447190388, 12513019344894380777, 2502603868978876155,
            5005207737957752311, 10010415475915504622, 2002083095183100924,
            4004166190366201848, 8008332380732403697, 16016664761464807395,
            3203332952292961479, 6406665904585922958, 12813331809171845916,
            2562666361834369183, 5125332723668738366, 10250665447337476733,
            2050133089467495346, 4100266178934990693, 8200532357869981386,
            16401064715739962772, 3280212943147992554, 6560425886295985109,
            13120851772591970218, 2624170354518394043, 5248340709036788087,
            10496681418073576174, 2099336283614715234, 4198672567229430469,
            8397345134458860939, 16794690268917721879, 3358938053783544375,
            6717876107567088751, 13435752215134177503, 2687150443026835500,
            5374300886053671001, 10748601772107342002, 2149720354421468400,
            4299440708842936801, 8598881417685873602, 17197762835371747204,
            3439552567074349440, 6879105134148698881, 13758210268297397763,
            2751642053659479552, 5503284107318959105, 11006568214637918210,
            2201313642927583642, 4402627285855167284, 8805254571710334568,
            17610509143420669137, 3522101828684133827, 7044203657368267654,
            14088407314736535309, 2817681462947307061, 5635362925894614123,
            11270725851789228247, 2254145170357845649, 4508290340715691299,
            9016580681431382598, 18033161362862765196, 3606632272572553039,
            7213264545145106078, 14426529090290212157, 2885305818058042431,
            5770611636116084862, 11541223272232169725, 2308244654446433945,
            4616489308892867890, 9232978617785735780, 1846595723557147156,
            3693191447114294312, 7386382894228588624, 14772765788457177249,
            2954553157691435449, 5909106315382870899, 11818212630765741799,
            2363642526153148359, 4727285052306296719, 9454570104612593439,
            1890914020922518687, 3781828041845037375, 7563656083690074751,
            15127312167380149503, 3025462433476029900, 6050924866952059801,
            12101849733904119602, 2420369946780823920, 4840739893561647841,
            9681479787123295682, 1936295957424659136, 3872591914849318272,
            7745183829698636545, 15490367659397273091, 3098073531879454618,
            6196147063758909236, 12392294127517818473, 2478458825503563694,
            4956917651007127389, 9913835302014254778, 1982767060402850955,
            3965534120805701911, 7931068241611403822, 15862136483222807645,
            3172427296644561529, 6344854593289123058, 12689709186578246116,
            2537941837315649223, 5075883674631298446, 10151767349262596893,
            2030353469852519378, 4060706939705038757, 8121413879410077514,
            16242827758820155028, 3248565551764031005, 6497131103528062011,
            12994262207056124023, 2598852441411224804, 5197704882822449609,
            10395409765644899218, 2079081953128979843, 4158163906257959687,
            8316327812515919374, 16632655625031838749, 3326531125006367749,
            6653062250012735499, 13306124500025470999, 2661224900005094199,
            5322449800010188399, 10644899600020376799, 2128979920004075359,
            4257959840008150719, 8515919680016301439, 17031839360032602879,
            3406367872006520575, 6812735744013041151, 13625471488026082303,
            2725094297605216460, 5450188595210432921, 10900377190420865842,
            2180075438084173168, 4360150876168346337, 8720301752336692674,
            17440603504673385348, 3488120700934677069, 6976241401869354139,
            13952482803738708279, 2790496560747741655, 5580993121495483311,
            11161986242990966623, 2232397248598193324, 4464794497196386649,
            8929588994392773298, 17859177988785546597, 3571835597757109319,
            7143671195514218638, 14287342391028437277, 2857468478205687455,
            5714936956411374911, 11429873912822749822, 2285974782564549964,
            4571949565129099928, 9143899130258199857, 1828779826051639971,
            3657559652103279943, 7315119304206559886, 14630238608413119772,
            2926047721682623954, 5852095443365247908, 11704190886730495817,
            2340838177346099163, 4681676354692198327, 9363352709384396654,
            1872670541876879330, 3745341083753758661, 7490682167507517323,
            14981364335015034646, 2996272867003006929, 5992545734006013858,
            11985091468012027717, 2397018293602405543, 4794036587204811087,
            9588073174409622174, 1917614634881924434, 3835229269763848869,
            7670458539527697739, 15340917079055395478, 3068183415811079095,
            6136366831622158191, 12272733663244316382, 2454546732648863276,
            4909093465297726553, 9818186930595453106, 1963637386119090621,
            3927274772238181242, 7854549544476362484, 15709099088952724969,
            3141819817790544993, 6283639635581089987, 12567279271162179975,
            2513455854232435995, 5026911708464871990, 10053823416929743980,
            2010764683385948796, 4021529366771897592, 8043058733543795184,
            16086117467087590369, 3217223493417518073, 6434446986835036147,
            12868893973670072295, 2573778794734014459, 5147557589468028918,
            10295115178936057836, 2059023035787211567, 4118046071574423134,
            8236092143148846269, 16472184286297692538, 3294436857259538507,
            6588873714519077015, 13177747429038154030, 2635549485807630806,
            5271098971615261612, 10542197943230523224, 2108439588646104644,
            4216879177292209289, 8433758354584418579, 16867516709168837158,
            3373503341833767431, 6747006683667534863, 13494013367335069727,
            2698802673467013945, 5397605346934027890, 10795210693868055781,
            2159042138773611156, 4318084277547222312, 8636168555094444625,
            17272337110188889250, 3454467422037777850, 6908934844075555700,
            13817869688151111400, 2763573937630222280, 5527147875260444560,
            11054295750520889120, 2210859150104177824, 4421718300208355648,
            8843436600416711296, 17686873200833422592, 3537374640166684518,
            7074749280333369037, 14149498560666738074, 2829899712133347614,
            5659799424266695229, 11319598848533390459, 2263919769706678091,
            4527839539413356183, 9055679078826712367, 1811135815765342473,
            3622271631530684947, 7244543263061369894, 14489086526122739788,
            2897817305224547957, 5795634610449095915, 11591269220898191830,
            2318253844179638366, 4636507688359276732, 9273015376718553464,
            1854603075343710692, 3709206150687421385, 7418412301374842771,
            14836824602749685542, 2967364920549937108, 5934729841099874217,
            11869459682199748434, 2373891936439949686, 4747783872879899373,
            9495567745759798747, 1899113549151959749, 3798227098303919498,
            7596454196607838997, 15192908393215677995, 3038581678643135599,
            6077163357286271198, 12154326714572542396, 2430865342914508479,
            4861730685829016958, 9723461371658033917, 1944692274331606783,
            3889384548663213566, 7778769097326427133, 15557538194652854267,
            3111507638930570853, 6223015277861141707, 12446030555722283414,
            2489206111144456682, 4978412222288913365, 9956824444577826731,
            1991364888915565346, 3982729777831130692, 7965459555662261385,
            15930919111324522770, 3186183822264904554, 6372367644529809108,
            12744735289059618216, 2548947057811923643, 5097894115623847286,
            10195788231247694572, 2039157646249538914, 4078315292499077829,
            8156630584998155658, 16313261169996311316, 3262652233999262263,
            6525304467998524526, 13050608935997049053, 2610121787199409810,
            5220243574398819621, 10440487148797639242, 2088097429759527848,
            4176194859519055697, 8352389719038111394, 16704779438076222788,
            3340955887615244557, 6681911775230489115, 13363823550460978230,
            2672764710092195646, 5345529420184391292, 10691058840368782584,
            2138211768073756516, 4276423536147513033, 8552847072295026067,
            17105694144590052135, 3421138828918010427, 6842277657836020854,
            13684555315672041708, 2736911063134408341, 5473822126268816683,
            10947644252537633366, 2189528850507526673, 4379057701015053346,
            8758115402030106693, 17516230804060213386, 3503246160812042677,
            7006492321624085354, 14012984643248170709, 2802596928649634141,
            5605193857299268283, 11210387714598536567, 2242077542919707313,
            4484155085839414626, 8968310171678829253, 17936620343357658507,
            3587324068671531701, 7174648137343063403, 14349296274686126806,
            2869859254937225361, 5739718509874450722, 11479437019748901445,
            2295887403949780289, 4591774807899560578, 9183549615799121156,
            1836709923159824231, 3673419846319648462, 7346839692639296924,
            14693679385278593849, 2938735877055718769, 5877471754111437539,
            11754943508222875079, 2350988701644575015, 4701977403289150031,
            9403954806578300063, 1880790961315660012, 3761581922631320025,
            7523163845262640050, 15046327690525280101, 3009265538105056020,
            6018531076210112040, 12037062152420224081, 2407412430484044816,
            4814824860968089632, 9629649721936179265, 1925929944387235853,
            3851859888774471706, 7703719777548943412, 15407439555097886824,
            3081487911019577364, 6162975822039154729, 12325951644078309459,
            2465190328815661891, 4930380657631323783, 9860761315262647567,
            1972152263052529513, 3944304526105059027, 7888609052210118054,
            15777218104420236108, 3155443620884047221, 6310887241768094443,
            12621774483536188886, 2524354896707237777, 5048709793414475554,
            10097419586828951109, 2019483917365790221, 4038967834731580443,
            8077935669463160887, 16155871338926321774, 3231174267785264354,
            6462348535570528709, 12924697071141057419, 2584939414228211483,
            5169878828456422967, 10339757656912845935, 2067951531382569187,
            4135903062765138374, 8271806125530276748, 16543612251060553497,
            3308722450212110699, 6617444900424221398, 13234889800848442797,
            2646977960169688559, 5293955920339377119, 10587911840678754238,
            2117582368135750847, 4235164736271501695, 8470329472543003390,
            16940658945086006781, 3388131789017201356, 6776263578034402712,
            13552527156068805425, 2710505431213761085, 5421010862427522170,
            10842021724855044340, 2168404344971008868, 4336808689942017736,
            8673617379884035472, 17347234759768070944, 3469446951953614188,
            6938893903907228377, 13877787807814456755, 2775557561562891351,
            5551115123125782702, 11102230246251565404, 2220446049250313080,
            4440892098500626161, 8881784197001252323, 17763568394002504646,
            3552713678800500929, 7105427357601001858, 14210854715202003717,
            2842170943040400743, 5684341886080801486, 11368683772161602973,
            2273736754432320594, 4547473508864641189, 9094947017729282379,
            1818989403545856475, 3637978807091712951, 7275957614183425903,
            14551915228366851806, 2910383045673370361, 5820766091346740722,
            11641532182693481445, 2328306436538696289, 4656612873077392578,
            9313225746154785156, 1862645149230957031, 3725290298461914062,
            7450580596923828125, 14901161193847656250, 2980232238769531250,
            5960464477539062500, 11920928955078125000, 2384185791015625000,
            4768371582031250000, 9536743164062500000, 1907348632812500000,
            3814697265625000000, 7629394531250000000, 15258789062500000000,
            3051757812500000000, 6103515625000000000, 12207031250000000000,
            2441406250000000000, 4882812500000000000, 9765625000000000000,
            1953125000000000000, 3906250000000000000, 7812500000000000000,
            15625000000000000000, 3125000000000000000, 6250000000000000000,
            12500000000000000000, 2500000000000000000, 5000000000000000000,
            10000000000000000000, 2000000000000000000, 4000000000000000000,
            8000000000000000000, 16000000000000000000, 3200000000000000000,
            6400000000000000000, 12800000000000000000, 2560000000000000000,
            5120000000000000000, 10240000000000000000, 2048000000000000000,
            4096000000000000000, 8192000000000000000, 16384000000000000000,
            3276800000000000000, 6553600000000000000, 13107200000000000000,
            2621440000000000000, 5242880000000000000, 10485760000000000000,
            2097152000000000000, 4194304000000000000, 8388608000000000000,
            16777216000000000000, 3355443200000000000, 6710886400000000000,
            13421772800000000000, 2684354560000000000, 5368709120000000000,
            10737418240000000000, 2147483648000000000, 4294967296000000000,
            8589934592000000000, 17179869184000000000, 3435973836800000000,
            6871947673600000000, 13743895347200000000, 2748779069440000000,
            5497558138880000000, 10995116277760000000, 2199023255552000000,
            4398046511104000000, 8796093022208000000, 17592186044416000000,
            3518437208883200000, 7036874417766400000, 14073748835532800000,
            2814749767106560000, 5629499534213120000, 11258999068426240000,
            2251799813685248000, 4503599627370496000, 9007199254740992000,
            18014398509481984000, 3602879701896396800, 7205759403792793600,
            14411518807585587200, 2882303761517117440, 5764607523034234880,
            11529215046068469760, 2305843009213693952, 4611686018427387904,
            9223372036854775808, 1844674407370955161, 3689348814741910323,
            7378697629483820646, 14757395258967641292, 2951479051793528258,
            5902958103587056517, 11805916207174113034, 2361183241434822606,
            4722366482869645213, 9444732965739290427, 1888946593147858085,
            3777893186295716170, 7555786372591432341, 15111572745182864683,
            3022314549036572936, 6044629098073145873, 12089258196146291747,
            2417851639229258349, 4835703278458516698, 9671406556917033397,
            1934281311383406679, 3868562622766813359, 7737125245533626718,
            15474250491067253436, 3094850098213450687, 6189700196426901374,
            12379400392853802748, 2475880078570760549, 4951760157141521099,
            9903520314283042199, 1980704062856608439, 3961408125713216879,
            7922816251426433759, 15845632502852867518, 3169126500570573503,
            6338253001141147007, 12676506002282294014, 2535301200456458802,
            5070602400912917605, 10141204801825835211, 2028240960365167042,
            4056481920730334084, 8112963841460668169, 16225927682921336339,
            3245185536584267267, 6490371073168534535, 12980742146337069071,
            2596148429267413814, 5192296858534827628, 10384593717069655257,
            2076918743413931051, 4153837486827862102, 8307674973655724205,
            16615349947311448411, 3323069989462289682, 6646139978924579364,
            13292279957849158729, 2658455991569831745, 5316911983139663491,
            10633823966279326983, 2126764793255865396, 4253529586511730793,
            8507059173023461586, 17014118346046923173, 3402823669209384634,
            6805647338418769269, 13611294676837538538, 2722258935367507707,
            5444517870735015415, 10889035741470030830, 2177807148294006166,
            4355614296588012332, 8711228593176024664, 17422457186352049329,
            3484491437270409865, 6968982874540819731, 13937965749081639463,
            2787593149816327892, 5575186299632655785, 11150372599265311570,
            2230074519853062314, 4460149039706124628, 8920298079412249256,
            17840596158824498513, 3568119231764899702, 7136238463529799405,
            14272476927059598810, 2854495385411919762, 5708990770823839524,
            11417981541647679048, 2283596308329535809, 4567192616659071619,
            9134385233318143238, 1826877046663628647, 3653754093327257295,
            7307508186654514591, 14615016373309029182, 2923003274661805836,
            5846006549323611672, 11692013098647223345, 2338402619729444669,
            4676805239458889338, 9353610478917778676, 1870722095783555735,
            3741444191567111470, 7482888383134222941, 14965776766268445882,
            2993155353253689176, 5986310706507378352, 11972621413014756705,
            2394524282602951341, 4789048565205902682, 9578097130411805364,
            1915619426082361072, 3831238852164722145, 7662477704329444291,
            15324955408658888583, 3064991081731777716, 6129982163463555433,
            12259964326927110866, 2451992865385422173, 4903985730770844346,
            9807971461541688693, 1961594292308337738, 3923188584616675477,
            7846377169233350954, 15692754338466701909, 3138550867693340381,
            6277101735386680763, 12554203470773361527, 2510840694154672305,
            5021681388309344611, 10043362776618689222, 2008672555323737844,
            4017345110647475688, 8034690221294951377, 16069380442589902755,
            3213876088517980551, 6427752177035961102, 12855504354071922204,
            2571100870814384440, 5142201741628768881, 10284403483257537763,
            2056880696651507552, 4113761393303015105, 8227522786606030210,
            16455045573212060421, 3291009114642412084, 6582018229284824168,
            13164036458569648337, 2632807291713929667, 5265614583427859334,
            10531229166855718669, 2106245833371143733, 4212491666742287467,
            8424983333484574935, 16849966666969149871, 3369993333393829974,
            6739986666787659948, 13479973333575319897, 2695994666715063979,
            5391989333430127958, 10783978666860255917, 2156795733372051183,
            4313591466744102367, 8627182933488204734, 17254365866976409468,
            3450873173395281893, 6901746346790563787, 13803492693581127574,
            2760698538716225514, 5521397077432451029, 11042794154864902059,
            2208558830972980411, 4417117661945960823, 8834235323891921647,
            17668470647783843295, 3533694129556768659, 7067388259113537318,
            14134776518227074636, 2826955303645414927, 5653910607290829854,
            11307821214581659709, 2261564242916331941, 4523128485832663883,
            9046256971665327767, 18092513943330655534, 3618502788666131106,
            7237005577332262213, 14474011154664524427, 2894802230932904885,
            5789604461865809771, 11579208923731619542, 2315841784746323908,
            4631683569492647816, 9263367138985295633, 1852673427797059126,
            3705346855594118253, 7410693711188236507, 14821387422376473014,
            2964277484475294602, 5928554968950589205, 11857109937901178411,
            2371421987580235682, 4742843975160471364, 9485687950320942729,
            1897137590064188545, 3794275180128377091, 7588550360256754183,
            15177100720513508366, 3035420144102701673, 6070840288205403346,
            12141680576410806693, 2428336115282161338, 4856672230564322677,
            9713344461128645354, 1942668892225729070, 3885337784451458141,
            7770675568902916283, 15541351137805832567, 3108270227561166513,
            6216540455122333026, 12433080910244666053, 2486616182048933210,
            4973232364097866421, 9946464728195732843, 1989292945639146568,
            3978585891278293137, 7957171782556586274, 15914343565113172548,
            3182868713022634509, 6365737426045269019, 12731474852090538039,
            2546294970418107607, 5092589940836215215, 10185179881672430431,
            2037035976334486086, 4074071952668972172, 8148143905337944345,
            16296287810675888690, 3259257562135177738, 6518515124270355476,
            13037030248540710952, 2607406049708142190, 5214812099416284380,
            10429624198832568761, 2085924839766513752, 4171849679533027504,
            8343699359066055009, 16687398718132110018, 3337479743626422003,
            6674959487252844007, 13349918974505688014, 2669983794901137602,
            5339967589802275205, 10679935179604550411, 2135987035920910082,
            4271974071841820164, 8543948143683640329, 17087896287367280659,
            3417579257473456131, 6835158514946912263, 13670317029893824527,
            2734063405978764905, 5468126811957529810, 10936253623915059621,
            2187250724783011924, 4374501449566023848, 8749002899132047697,
            17498005798264095394, 3499601159652819078, 6999202319305638157,
            13998404638611276315, 2799680927722255263, 5599361855444510526,
            11198723710889021052, 2239744742177804210, 4479489484355608421,
            8958978968711216842, 17917957937422433684, 3583591587484486736,
            7167183174968973473, 14334366349937946947, 2866873269987589389,
            5733746539975178779, 11467493079950357558, 2293498615990071511,
            4586997231980143023, 9173994463960286046, 1834798892792057209,
            3669597785584114418, 7339195571168228837, 14678391142336457674,
            2935678228467291534, 5871356456934583069, 11742712913869166139,
            2348542582773833227, 4697085165547666455, 9394170331095332911,
            1878834066219066582, 3757668132438133164, 7515336264876266329,
            15030672529752532658, 3006134505950506531, 6012269011901013063,
            12024538023802026126, 2404907604760405225, 4809815209520810450,
            9619630419041620901, 1923926083808324180, 3847852167616648360,
            7695704335233296721, 15391408670466593442, 3078281734093318688,
            6156563468186637376, 12313126936373274753, 2462625387274654950,
            4925250774549309901, 9850501549098619803, 1970100309819723960,
            3940200619639447921, 7880401239278895842, 15760802478557791684,
            3152160495711558336, 6304320991423116673, 12608641982846233347,
            2521728396569246669, 5043456793138493339, 10086913586276986678,
            2017382717255397335, 4034765434510794671, 8069530869021589342,
            16139061738043178685, 3227812347608635737, 6455624695217271474,
            12911249390434542948, 2582249878086908589, 5164499756173817179,
            10328999512347634358, 2065799902469526871, 4131599804939053743,
            8263199609878107486, 16526399219756214973, 3305279843951242994,
            6610559687902485989, 13221119375804971979, 2644223875160994395,
            5288447750321988791, 10576895500643977583, 2115379100128795516,
            4230758200257591033, 8461516400515182066, 16923032801030364133,
            3384606560206072826, 6769213120412145653, 13538426240824291306,
            2707685248164858261, 5415370496329716522, 10830740992659433045,
            2166148198531886609, 4332296397063773218, 8664592794127546436,
            17329185588255092872, 3465837117651018574, 6931674235302037148,
            13863348470604074297, 2772669694120814859, 5545339388241629719,
            11090678776483259438, 2218135755296651887, 4436271510593303775,
            8872543021186607550, 17745086042373215101, 3549017208474643020,
            7098034416949286040, 14196068833898572081, 2839213766779714416,
            5678427533559428832, 11356855067118857664, 2271371013423771532,
            4542742026847543065, 9085484053695086131, 1817096810739017226,
            3634193621478034452, 7268387242956068905, 14536774485912137810,
            2907354897182427562, 5814709794364855124, 11629419588729710248,
            2325883917745942049, 4651767835491884099, 9303535670983768199,
            1860707134196753639, 3721414268393507279, 7442828536787014559,
            14885657073574029118, 2977131414714805823, 5954262829429611647,
            11908525658859223294, 2381705131771844658, 4763410263543689317,
            9526820527087378635, 1905364105417475727, 3810728210834951454,
            7621456421669902908, 15242912843339805817, 3048582568667961163,
            6097165137335922326, 12194330274671844653, 2438866054934368930,
            4877732109868737861, 9755464219737475723, 1951092843947495144,
            3902185687894990289, 7804371375789980578, 15608742751579961156,
            3121748550315992231, 6243497100631984462, 12486994201263968925,
            2497398840252793785, 4994797680505587570, 9989595361011175140,
            1997919072202235028, 3995838144404470056, 7991676288808940112,
            15983352577617880224, 3196670515523576044, 6393341031047152089,
            12786682062094304179, 2557336412418860835, 5114672824837721671,
            10229345649675443343, 2045869129935088668, 4091738259870177337,
            8183476519740354675, 16366953039480709350, 3273390607896141870,
            6546781215792283740, 13093562431584567480, 2618712486316913496,
            5237424972633826992, 10474849945267653984, 2094969989053530796,
            4189939978107061593, 8379879956214123187, 16759759912428246374,
            3351951982485649274, 6703903964971298549, 13407807929942597099,
            2681561585988519419, 5363123171977038839, 10726246343954077679,
            2145249268790815535, 4290498537581631071, 8580997075163262143,
            17161994150326524287, 3432398830065304857, 6864797660130609714,
            13729595320261219429, 2745919064052243885, 5491838128104487771,
            10983676256208975543, 2196735251241795108, 4393470502483590217,
            8786941004967180435, 17573882009934360870, 3514776401986872174,
            7029552803973744348, 14059105607947488696, 2811821121589497739,
            5623642243178995478, 11247284486357990957, 2249456897271598191,
            4498913794543196382, 8997827589086392765, 17995655178172785531,
            3599131035634557106, 7198262071269114212, 14396524142538228424,
            2879304828507645684, 5758609657015291369, 11517219314030582739,
            2303443862806116547, 4606887725612233095, 9213775451224466191,
            1842755090244893238, 3685510180489786476, 7371020360979572953,
            14742040721959145907, 2948408144391829181, 5896816288783658362,
            11793632577567316725, 2358726515513463345, 4717453031026926690,
            9434906062053853380, 1886981212410770676, 3773962424821541352,
            7547924849643082704, 15095849699286165408, 3019169939857233081,
            6038339879714466163, 12076679759428932327, 2415335951885786465,
            4830671903771572930, 9661343807543145861, 1932268761508629172,
            3864537523017258344, 7729075046034516689, 15458150092069033378,
            3091630018413806675, 6183260036827613351, 12366520073655226703,
            2473304014731045340, 4946608029462090681, 9893216058924181362,
            1978643211784836272, 3957286423569672544, 7914572847139345089,
            15829145694278690179, 3165829138855738035, 6331658277711476071,
            12663316555422952143, 2532663311084590428, 5065326622169180857,
            10130653244338361715, 2026130648867672343, 4052261297735344686,
            8104522595470689372, 16209045190941378744, 3241809038188275748,
            6483618076376551497, 12967236152753102995, 2593447230550620599,
            5186894461101241198, 10373788922202482396, 2074757784440496479,
            4149515568880992958, 8299031137761985917, 16598062275523971834,
            3319612455104794366, 6639224910209588733, 13278449820419177467,
            2655689964083835493, 5311379928167670986, 10622759856335341973,
            2124551971267068394, 4249103942534136789, 8498207885068273579,
            16996415770136547158, 3399283154027309431, 6798566308054618863,
            13597132616109237726, 2719426523221847545, 5438853046443695090,
            10877706092887390181, 2175541218577478036, 4351082437154956072,
            8702164874309912144, 17404329748619824289, 3480865949723964857,
            6961731899447929715, 13923463798895859431, 2784692759779171886,
            5569385519558343772, 11138771039116687545, 2227754207823337509,
            4455508415646675018, 8911016831293350036, 17822033662586700072,
            3564406732517340014, 7128813465034680029, 14257626930069360058,
            2851525386013872011, 5703050772027744023, 11406101544055488046,
            2281220308811097609, 4562440617622195218, 9124881235244390437,
            1824976247048878087, 3649952494097756174, 7299904988195512349,
            14599809976391024699, 2919961995278204939, 5839923990556409879,
            11679847981112819759, 2335969596222563951, 4671939192445127903,
            9343878384890255807, 1868775676978051161, 3737551353956102323,
            7475102707912204646, 14950205415824409292, 2990041083164881858,
            5980082166329763716, 11960164332659527433, 2392032866531905486,
            4784065733063810973, 9568131466127621947, 1913626293225524389,
            3827252586451048778, 7654505172902097557, 15309010345804195115,
            3061802069160839023, 6123604138321678046, 12247208276643356092,
            2449441655328671218, 4898883310657342436, 9797766621314684873,
            1959553324262936974, 3919106648525873949, 7838213297051747899,
            15676426594103495798, 3135285318820699159, 6270570637641398319,
            12541141275282796638, 2508228255056559327, 5016456510113118655,
            10032913020226237310, 2006582604045247462, 4013165208090494924,
            8026330416180989848, 16052660832361979697, 3210532166472395939,
            6421064332944791878, 12842128665889583757, 2568425733177916751,
            5136851466355833503, 10273702932711667006, 2054740586542333401,
            4109481173084666802, 8218962346169333605, 16437924692338667210,
            3287584938467733442, 6575169876935466884, 13150339753870933768,
            2630067950774186753, 5260135901548373507, 10520271803096747014,
            2104054360619349402, 4208108721238698805, 8416217442477397611,
            16832434884954795223, 3366486976990959044, 6732973953981918089,
            13465947907963836178, 2693189581592767235, 5386379163185534471,
            10772758326371068942, 2154551665274213788, 4309103330548427577,
            8618206661096855154, 17236413322193710308, 3447282664438742061,
            6894565328877484123, 13789130657754968246, 2757826131550993649,
            5515652263101987298, 11031304526203974597, 2206260905240794919,
            4412521810481589838, 8825043620963179677, 17650087241926359355,
            3530017448385271871, 7060034896770543742, 14120069793541087484,
            2824013958708217496, 5648027917416434993, 11296055834832869987,
            2259211166966573997, 4518422333933147995, 9036844667866295990,
            18073689335732591980, 3614737867146518396, 7229475734293036792,
            14458951468586073584, 2891790293717214716, 5783580587434429433,
            11567161174868858867, 2313432234973771773, 4626864469947543547,
            9253728939895087094, 1850745787979017418, 3701491575958034837,
            7402983151916069675, 14805966303832139350, 2961193260766427870,
            5922386521532855740, 11844773043065711480, 2368954608613142296,
            4737909217226284592, 9475818434452569184, 1895163686890513836,
            3790327373781027673, 7580654747562055347, 15161309495124110694,
            3032261899024822138, 6064523798049644277, 12129047596099288555,
            2425809519219857711, 4851619038439715422, 9703238076879430844,
            1940647615375886168, 3881295230751772337, 7762590461503544675,
            15525180923007089351, 3105036184601417870, 6210072369202835740,
            12420144738405671481, 2484028947681134296, 4968057895362268592,
            9936115790724537184, 1987223158144907436, 3974446316289814873,
            7948892632579629747, 15897785265159259495, 3179557053031851899,
            6359114106063703798, 12718228212127407596, 2543645642425481519,
            5087291284850963038, 10174582569701926077, 2034916513940385215,
            4069833027880770430, 8139666055761540861, 16279332111523081723,
            3255866422304616344, 6511732844609232689, 13023465689218465379,
            2604693137843693075, 5209386275687386151, 10418772551374772303,
            2083754510274954460, 4167509020549908921, 8335018041099817842,
            16670036082199635685, 3334007216439927137, 6668014432879854274,
            13336028865759708548, 2667205773151941709, 5334411546303883419,
            10668823092607766838, 2133764618521553367, 4267529237043106735,
            8535058474086213470, 17070116948172426941, 3414023389634485388,
            6828046779268970776, 13656093558537941553, 2731218711707588310,
            5462437423415176621, 10924874846830353242, 2184974969366070648,
            4369949938732141297, 8739899877464282594, 17479799754928565188,
            3495959950985713037, 6991919901971426075, 13983839803942852150,
            2796767960788570430, 5593535921577140860, 11187071843154281720,
            2237414368630856344, 4474828737261712688, 8949657474523425376,
            17899314949046850752, 3579862989809370150, 7159725979618740301,
            14319451959237480602, 2863890391847496120, 5727780783694992240,
            11455561567389984481, 2291112313477996896, 4582224626955993792,
            9164449253911987585, 1832889850782397517, 3665779701564795034,
            7331559403129590068, 14663118806259180136, 2932623761251836027,
            5865247522503672054, 11730495045007344109, 2346099009001468821,
            4692198018002937643, 9384396036005875287, 1876879207201175057,
            3753758414402350114, 7507516828804700229, 15015033657609400459,
            3003006731521880091, 6006013463043760183, 12012026926087520367,
            2402405385217504073, 4804810770435008147, 9609621540870016294,
            1921924308174003258, 3843848616348006517, 7687697232696013035,
            15375394465392026070, 3075078893078405214, 6150157786156810428,
            12300315572313620856, 2460063114462724171, 4920126228925448342,
            9840252457850896685, 1968050491570179337, 3936100983140358674,
            7872201966280717348, 15744403932561434696, 3148880786512286939,
            6297761573024573878, 12595523146049147757, 2519104629209829551,
            5038209258419659102, 10076418516839318205, 2015283703367863641,
            4030567406735727282, 8061134813471454564, 16122269626942909129,
            3224453925388581825, 6448907850777163651, 12897815701554327303,
            2579563140310865460, 5159126280621730921, 10318252561243461842,
            2063650512248692368, 4127301024497384737, 8254602048994769474,
            16509204097989538948, 3301840819597907789, 6603681639195815579,
            13207363278391631158, 2641472655678326231, 5282945311356652463,
            10565890622713304927, 2113178124542660985, 4226356249085321970,
            8452712498170643941, 16905424996341287883, 3381084999268257576,
            6762169998536515153, 13524339997073030306, 2704867999414606061,
            5409735998829212122, 10819471997658424245, 2163894399531684849,
            4327788799063369698, 8655577598126739396, 17311155196253478792,
            3462231039250695758, 6924462078501391516, 13848924157002783033,
            2769784831400556606, 5539569662801113213, 11079139325602226427,
            2215827865120445285, 4431655730240890570, 8863311460481781141,
            17726622920963562283, 3545324584192712456, 7090649168385424913,
            14181298336770849826, 2836259667354169965, 5672519334708339930,
            11345038669416679861, 2269007733883335972, 4538015467766671944,
            9076030935533343889, 18152061871066687778, 3630412374213337555,
            7260824748426675111, 14521649496853350222, 2904329899370670044,
            5808659798741340089, 11617319597482680178, 2323463919496536035,
            4646927838993072071, 9293855677986144142, 1858771135597228828,
            3717542271194457656, 7435084542388915313, 14870169084777830627,
            2974033816955566125, 5948067633911132251, 11896135267822264502,
            2379227053564452900, 4758454107128905800, 9516908214257811601,
            1903381642851562320, 3806763285703124640, 7613526571406249281,
            15227053142812498563, 3045410628562499712, 6090821257124999425,
            12181642514249998850, 2436328502849999770, 4872657005699999540,
            9745314011399999080, 1949062802279999816, 3898125604559999632,
            7796251209119999264, 15592502418239998528, 3118500483647999705,
            6237000967295999411, 12474001934591998822, 2494800386918399764,
            4989600773836799529, 9979201547673599058, 1995840309534719811,
            3991680619069439623, 7983361238138879246, 15966722476277758493,
            3193344495255551698, 6386688990511103397, 12773377981022206794,
            2554675596204441358, 5109351192408882717, 10218702384817765435,
            2043740476963553087, 4087480953927106174, 8174961907854212348,
            16349923815708424697, 3269984763141684939, 6539969526283369878,
            13079939052566739757, 2615987810513347951, 5231975621026695903,
            10463951242053391806, 2092790248410678361, 4185580496821356722,
            8371160993642713444, 16742321987285426889, 3348464397457085377,
            6696928794914170755, 13393857589828341511, 2678771517965668302,
            5357543035931336604, 10715086071862673209, 2143017214372534641,
            4286034428745069283, 8572068857490138567, 17144137714980277135,
            3428827542996055427, 6857655085992110854, 13715310171984221708,
            2743062034396844341, 5486124068793688683, 10972248137587377366,
            2194449627517475473, 4388899255034950946, 8777798510069901893,
            17555597020139803786, 3511119404027960757, 7022238808055921514,
            14044477616111843029, 2808895523222368605, 5617791046444737211,
            11235582092889474423, 2247116418577894884, 4494232837155789769,
            8988465674311579538, 17976931348623159077, 3595386269724631815,
            7190772539449263630, 14381545078898527261, 2876309015779705452,
            5752618031559410904, 11505236063118821809, 2301047212623764361,
            4602094425247528723, 9204188850495057447, 1840837770099011489,
            3681675540198022979, 7363351080396045958,
        };

        // Taken from Mono sources
        // * mono/metatdata/number-formatter.h
        //   master/9af0b8a2224e2eb6a05f00607805a180b1c67a35 (commit date 27/05/2008, as of 08/Jan/2015)
        private static readonly int[] Formatter_TensExponentTable = new[] {
            -323, -323, -322, -322, -322, -322, -321, -321, -321, -320, -320, -320,
            -319, -319, -319, -319, -318, -318, -318, -317, -317, -317, -316, -316,
            -316, -316, -315, -315, -315, -314, -314, -314, -313, -313, -313, -313,
            -312, -312, -312, -311, -311, -311, -310, -310, -310, -310, -309, -309,
            -309, -308, -308, -308, -307, -307, -307, -307, -306, -306, -306, -305,
            -305, -305, -304, -304, -304, -304, -303, -303, -303, -302, -302, -302,
            -301, -301, -301, -301, -300, -300, -300, -299, -299, -299, -298, -298,
            -298, -298, -297, -297, -297, -296, -296, -296, -295, -295, -295, -295,
            -294, -294, -294, -293, -293, -293, -292, -292, -292, -291, -291, -291,
            -291, -290, -290, -290, -289, -289, -289, -288, -288, -288, -288, -287,
            -287, -287, -286, -286, -286, -285, -285, -285, -285, -284, -284, -284,
            -283, -283, -283, -282, -282, -282, -282, -281, -281, -281, -280, -280,
            -280, -279, -279, -279, -279, -278, -278, -278, -277, -277, -277, -276,
            -276, -276, -276, -275, -275, -275, -274, -274, -274, -273, -273, -273,
            -273, -272, -272, -272, -271, -271, -271, -270, -270, -270, -270, -269,
            -269, -269, -268, -268, -268, -267, -267, -267, -267, -266, -266, -266,
            -265, -265, -265, -264, -264, -264, -263, -263, -263, -263, -262, -262,
            -262, -261, -261, -261, -260, -260, -260, -260, -259, -259, -259, -258,
            -258, -258, -257, -257, -257, -257, -256, -256, -256, -255, -255, -255,
            -254, -254, -254, -254, -253, -253, -253, -252, -252, -252, -251, -251,
            -251, -251, -250, -250, -250, -249, -249, -249, -248, -248, -248, -248,
            -247, -247, -247, -246, -246, -246, -245, -245, -245, -245, -244, -244,
            -244, -243, -243, -243, -242, -242, -242, -242, -241, -241, -241, -240,
            -240, -240, -239, -239, -239, -239, -238, -238, -238, -237, -237, -237,
            -236, -236, -236, -235, -235, -235, -235, -234, -234, -234, -233, -233,
            -233, -232, -232, -232, -232, -231, -231, -231, -230, -230, -230, -229,
            -229, -229, -229, -228, -228, -228, -227, -227, -227, -226, -226, -226,
            -226, -225, -225, -225, -224, -224, -224, -223, -223, -223, -223, -222,
            -222, -222, -221, -221, -221, -220, -220, -220, -220, -219, -219, -219,
            -218, -218, -218, -217, -217, -217, -217, -216, -216, -216, -215, -215,
            -215, -214, -214, -214, -214, -213, -213, -213, -212, -212, -212, -211,
            -211, -211, -211, -210, -210, -210, -209, -209, -209, -208, -208, -208,
            -208, -207, -207, -207, -206, -206, -206, -205, -205, -205, -204, -204,
            -204, -204, -203, -203, -203, -202, -202, -202, -201, -201, -201, -201,
            -200, -200, -200, -199, -199, -199, -198, -198, -198, -198, -197, -197,
            -197, -196, -196, -196, -195, -195, -195, -195, -194, -194, -194, -193,
            -193, -193, -192, -192, -192, -192, -191, -191, -191, -190, -190, -190,
            -189, -189, -189, -189, -188, -188, -188, -187, -187, -187, -186, -186,
            -186, -186, -185, -185, -185, -184, -184, -184, -183, -183, -183, -183,
            -182, -182, -182, -181, -181, -181, -180, -180, -180, -180, -179, -179,
            -179, -178, -178, -178, -177, -177, -177, -176, -176, -176, -176, -175,
            -175, -175, -174, -174, -174, -173, -173, -173, -173, -172, -172, -172,
            -171, -171, -171, -170, -170, -170, -170, -169, -169, -169, -168, -168,
            -168, -167, -167, -167, -167, -166, -166, -166, -165, -165, -165, -164,
            -164, -164, -164, -163, -163, -163, -162, -162, -162, -161, -161, -161,
            -161, -160, -160, -160, -159, -159, -159, -158, -158, -158, -158, -157,
            -157, -157, -156, -156, -156, -155, -155, -155, -155, -154, -154, -154,
            -153, -153, -153, -152, -152, -152, -152, -151, -151, -151, -150, -150,
            -150, -149, -149, -149, -149, -148, -148, -148, -147, -147, -147, -146,
            -146, -146, -145, -145, -145, -145, -144, -144, -144, -143, -143, -143,
            -142, -142, -142, -142, -141, -141, -141, -140, -140, -140, -139, -139,
            -139, -139, -138, -138, -138, -137, -137, -137, -136, -136, -136, -136,
            -135, -135, -135, -134, -134, -134, -133, -133, -133, -133, -132, -132,
            -132, -131, -131, -131, -130, -130, -130, -130, -129, -129, -129, -128,
            -128, -128, -127, -127, -127, -127, -126, -126, -126, -125, -125, -125,
            -124, -124, -124, -124, -123, -123, -123, -122, -122, -122, -121, -121,
            -121, -121, -120, -120, -120, -119, -119, -119, -118, -118, -118, -117,
            -117, -117, -117, -116, -116, -116, -115, -115, -115, -114, -114, -114,
            -114, -113, -113, -113, -112, -112, -112, -111, -111, -111, -111, -110,
            -110, -110, -109, -109, -109, -108, -108, -108, -108, -107, -107, -107,
            -106, -106, -106, -105, -105, -105, -105, -104, -104, -104, -103, -103,
            -103, -102, -102, -102, -102, -101, -101, -101, -100, -100, -100, -99,
            -99, -99, -99, -98, -98, -98, -97, -97, -97, -96, -96, -96,
            -96, -95, -95, -95, -94, -94, -94, -93, -93, -93, -93, -92,
            -92, -92, -91, -91, -91, -90, -90, -90, -89, -89, -89, -89,
            -88, -88, -88, -87, -87, -87, -86, -86, -86, -86, -85, -85,
            -85, -84, -84, -84, -83, -83, -83, -83, -82, -82, -82, -81,
            -81, -81, -80, -80, -80, -80, -79, -79, -79, -78, -78, -78,
            -77, -77, -77, -77, -76, -76, -76, -75, -75, -75, -74, -74,
            -74, -74, -73, -73, -73, -72, -72, -72, -71, -71, -71, -71,
            -70, -70, -70, -69, -69, -69, -68, -68, -68, -68, -67, -67,
            -67, -66, -66, -66, -65, -65, -65, -65, -64, -64, -64, -63,
            -63, -63, -62, -62, -62, -62, -61, -61, -61, -60, -60, -60,
            -59, -59, -59, -58, -58, -58, -58, -57, -57, -57, -56, -56,
            -56, -55, -55, -55, -55, -54, -54, -54, -53, -53, -53, -52,
            -52, -52, -52, -51, -51, -51, -50, -50, -50, -49, -49, -49,
            -49, -48, -48, -48, -47, -47, -47, -46, -46, -46, -46, -45,
            -45, -45, -44, -44, -44, -43, -43, -43, -43, -42, -42, -42,
            -41, -41, -41, -40, -40, -40, -40, -39, -39, -39, -38, -38,
            -38, -37, -37, -37, -37, -36, -36, -36, -35, -35, -35, -34,
            -34, -34, -34, -33, -33, -33, -32, -32, -32, -31, -31, -31,
            -30, -30, -30, -30, -29, -29, -29, -28, -28, -28, -27, -27,
            -27, -27, -26, -26, -26, -25, -25, -25, -24, -24, -24, -24,
            -23, -23, -23, -22, -22, -22, -21, -21, -21, -21, -20, -20,
            -20, -19, -19, -19, -18, -18, -18, -18, -17, -17, -17, -16,
            -16, -16, -15, -15, -15, -15, -14, -14, -14, -13, -13, -13,
            -12, -12, -12, -12, -11, -11, -11, -10, -10, -10, -9, -9,
            -9, -9, -8, -8, -8, -7, -7, -7, -6, -6, -6, -6,
            -5, -5, -5, -4, -4, -4, -3, -3, -3, -3, -2, -2,
            -2, -1, -1, -1, 0, 0, 0, 1, 1, 1, 1, 2,
            2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5,
            6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 9, 9,
            9, 10, 10, 10, 10, 11, 11, 11, 12, 12, 12, 13,
            13, 13, 13, 14, 14, 14, 15, 15, 15, 16, 16, 16,
            16, 17, 17, 17, 18, 18, 18, 19, 19, 19, 19, 20,
            20, 20, 21, 21, 21, 22, 22, 22, 22, 23, 23, 23,
            24, 24, 24, 25, 25, 25, 25, 26, 26, 26, 27, 27,
            27, 28, 28, 28, 29, 29, 29, 29, 30, 30, 30, 31,
            31, 31, 32, 32, 32, 32, 33, 33, 33, 34, 34, 34,
            35, 35, 35, 35, 36, 36, 36, 37, 37, 37, 38, 38,
            38, 38, 39, 39, 39, 40, 40, 40, 41, 41, 41, 41,
            42, 42, 42, 43, 43, 43, 44, 44, 44, 44, 45, 45,
            45, 46, 46, 46, 47, 47, 47, 47, 48, 48, 48, 49,
            49, 49, 50, 50, 50, 50, 51, 51, 51, 52, 52, 52,
            53, 53, 53, 53, 54, 54, 54, 55, 55, 55, 56, 56,
            56, 56, 57, 57, 57, 58, 58, 58, 59, 59, 59, 60,
            60, 60, 60, 61, 61, 61, 62, 62, 62, 63, 63, 63,
            63, 64, 64, 64, 65, 65, 65, 66, 66, 66, 66, 67,
            67, 67, 68, 68, 68, 69, 69, 69, 69, 70, 70, 70,
            71, 71, 71, 72, 72, 72, 72, 73, 73, 73, 74, 74,
            74, 75, 75, 75, 75, 76, 76, 76, 77, 77, 77, 78,
            78, 78, 78, 79, 79, 79, 80, 80, 80, 81, 81, 81,
            81, 82, 82, 82, 83, 83, 83, 84, 84, 84, 84, 85,
            85, 85, 86, 86, 86, 87, 87, 87, 88, 88, 88, 88,
            89, 89, 89, 90, 90, 90, 91, 91, 91, 91, 92, 92,
            92, 93, 93, 93, 94, 94, 94, 94, 95, 95, 95, 96,
            96, 96, 97, 97, 97, 97, 98, 98, 98, 99, 99, 99,
            100, 100, 100, 100, 101, 101, 101, 102, 102, 102, 103, 103,
            103, 103, 104, 104, 104, 105, 105, 105, 106, 106, 106, 106,
            107, 107, 107, 108, 108, 108, 109, 109, 109, 109, 110, 110,
            110, 111, 111, 111, 112, 112, 112, 112, 113, 113, 113, 114,
            114, 114, 115, 115, 115, 116, 116, 116, 116, 117, 117, 117,
            118, 118, 118, 119, 119, 119, 119, 120, 120, 120, 121, 121,
            121, 122, 122, 122, 122, 123, 123, 123, 124, 124, 124, 125,
            125, 125, 125, 126, 126, 126, 127, 127, 127, 128, 128, 128,
            128, 129, 129, 129, 130, 130, 130, 131, 131, 131, 131, 132,
            132, 132, 133, 133, 133, 134, 134, 134, 134, 135, 135, 135,
            136, 136, 136, 137, 137, 137, 137, 138, 138, 138, 139, 139,
            139, 140, 140, 140, 140, 141, 141, 141, 142, 142, 142, 143,
            143, 143, 143, 144, 144, 144, 145, 145, 145, 146, 146, 146,
            147, 147, 147, 147, 148, 148, 148, 149, 149, 149, 150, 150,
            150, 150, 151, 151, 151, 152, 152, 152, 153, 153, 153, 153,
            154, 154, 154, 155, 155, 155, 156, 156, 156, 156, 157, 157,
            157, 158, 158, 158, 159, 159, 159, 159, 160, 160, 160, 161,
            161, 161, 162, 162, 162, 162, 163, 163, 163, 164, 164, 164,
            165, 165, 165, 165, 166, 166, 166, 167, 167, 167, 168, 168,
            168, 168, 169, 169, 169, 170, 170, 170, 171, 171, 171, 171,
            172, 172, 172, 173, 173, 173, 174, 174, 174, 175, 175, 175,
            175, 176, 176, 176, 177, 177, 177, 178, 178, 178, 178, 179,
            179, 179, 180, 180, 180, 181, 181, 181, 181, 182, 182, 182,
            183, 183, 183, 184, 184, 184, 184, 185, 185, 185, 186, 186,
            186, 187, 187, 187, 187, 188, 188, 188, 189, 189, 189, 190,
            190, 190, 190, 191, 191, 191, 192, 192, 192, 193, 193, 193,
            193, 194, 194, 194, 195, 195, 195, 196, 196, 196, 196, 197,
            197, 197, 198, 198, 198, 199, 199, 199, 199, 200, 200, 200,
            201, 201, 201, 202, 202, 202, 202, 203, 203, 203, 204, 204,
            204, 205, 205, 205, 206, 206, 206, 206, 207, 207, 207, 208,
            208, 208, 209, 209, 209, 209, 210, 210, 210, 211, 211, 211,
            212, 212, 212, 212, 213, 213, 213, 214, 214, 214, 215, 215,
            215, 215, 216, 216, 216, 217, 217, 217, 218, 218, 218, 218,
            219, 219, 219, 220, 220, 220, 221, 221, 221, 221, 222, 222,
            222, 223, 223, 223, 224, 224, 224, 224, 225, 225, 225, 226,
            226, 226, 227, 227, 227, 227, 228, 228, 228, 229, 229, 229,
            230, 230, 230, 230, 231, 231, 231, 232, 232, 232, 233, 233,
            233, 234, 234, 234, 234, 235, 235, 235, 236, 236, 236, 237,
            237, 237, 237, 238, 238, 238, 239, 239, 239, 240, 240, 240,
            240, 241, 241, 241, 242, 242, 242, 243, 243, 243, 243, 244,
            244, 244, 245, 245, 245, 246, 246, 246, 246, 247, 247, 247,
            248, 248, 248, 249, 249, 249, 249, 250, 250, 250, 251, 251,
            251, 252, 252, 252, 252, 253, 253, 253, 254, 254, 254, 255,
            255, 255, 255, 256, 256, 256, 257, 257, 257, 258, 258, 258,
            258, 259, 259, 259, 260, 260, 260, 261, 261, 261, 261, 262,
            262, 262, 263, 263, 263, 264, 264, 264, 265, 265, 265, 265,
            266, 266, 266, 267, 267, 267, 268, 268, 268, 268, 269, 269,
            269, 270, 270, 270, 271, 271, 271, 271, 272, 272, 272, 273,
            273, 273, 274, 274, 274, 274, 275, 275, 275, 276, 276, 276,
            277, 277, 277, 277, 278, 278, 278, 279, 279, 279, 280, 280,
            280, 280, 281, 281, 281, 282, 282, 282, 283, 283, 283, 283,
            284, 284, 284, 285, 285, 285, 286, 286, 286, 286, 287, 287,
            287, 288, 288, 288, 289, 289, 289, 289, 290, 290, 290, 291,
            291, 291, 292, 292, 292, 293, 293, 293,
        };

        // DecHexDigits s a translation table from a decimal number to its
        // digits hexadecimal representation (e.g. DecHexDigits [34] = 0x34).
        private static readonly int[] Formatter_DecHexDigits = new int[] {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59,
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99,
        };
        #endregion
        #endregion
        #endregion
    }
}
