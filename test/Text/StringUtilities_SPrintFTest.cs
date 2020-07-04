namespace RJCP.Text
{
    using System;
    using NUnit.Framework;
    using static StringUtilities;

    [TestFixture(Category = "String.SPrintF")]
    public class StringUtilities_SPrintFTest
    {
        [Test]
        public void SPrintF_IntegerNormal()
        {
            string r = SPrintF("My Number: %d", 5);
            Assert.That(r, Is.EqualTo("My Number: 5"));
        }

        [Test]
        public void SPrintF_IntegerWidth()
        {
            string r = SPrintF("My Number: %10d", 5);
            Assert.That(r, Is.EqualTo("My Number:          5"));
        }

        [Test]
        public void SPrintF_IntegerWidthLeftAligned()
        {
            string r = SPrintF("My Number: %-10d", 5);
            Assert.That(r, Is.EqualTo("My Number: 5         "));
        }

        [Test]
        public void SPrintF_Percent()
        {
            string r = SPrintF("Processor Load %u%%", 6);
            Assert.That(r, Is.EqualTo("Processor Load 6%"));
        }

        [Test]
        public void SPrintF_Char()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%c", 65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%-2c", 'a'), Is.EqualTo("a "));
            Assert.That(SPrintF("%2c", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%+2c", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("% 2c", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%#2c", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%02c", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%-2c", 'a'), Is.EqualTo("a "));
            Assert.That(SPrintF("%+c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("% c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%#c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%0c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%-c", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%05c", 'a'), Is.EqualTo("    a"));

            // Because .NET is always wide char, the length modifier 'l' doesn't change anything
            Assert.That(SPrintF("%lc", 65), Is.EqualTo("A"));
            Assert.That(SPrintF("%lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%-2lc", 'a'), Is.EqualTo("a "));
            Assert.That(SPrintF("%2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%+2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("% 2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%#2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%02lc", 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%-2lc", 'a'), Is.EqualTo("a "));
            Assert.That(SPrintF("%+lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("% lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%#lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%0lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%-lc", 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%05lc", 'a'), Is.EqualTo("    a"));
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1905:Redundant casts should not be used", Justification = "Provided to make reading test case explicit")]
        public void SPrintF_CharTypeConversions()
        {
            Assert.That(SPrintF("%c", 'A'), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (byte)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (sbyte)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (short)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (ushort)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", 65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (uint)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (long)65), Is.EqualTo("A"));
            Assert.That(SPrintF("%c", (ulong)65), Is.EqualTo("A"));

            Assert.That(SPrintF("%c", (byte)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (sbyte)-1), Is.EqualTo("ÿ"));

            Assert.That(SPrintF("%c", (short)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (ushort)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (short)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (ushort)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", unchecked((short)0xAB30)), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (ushort)0xAB30), Is.EqualTo("ꬰ"));

            Assert.That(SPrintF("%c", (int)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (uint)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (int)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (uint)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (int)0xAB30), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (uint)0xAB30), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (int)0xff20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (uint)0xff20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", unchecked((int)0xffff20ac)), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (uint)0xffff20ac), Is.EqualTo("€"));

            Assert.That(SPrintF("%c", (long)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (ulong)255), Is.EqualTo("ÿ"));
            Assert.That(SPrintF("%c", (long)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (ulong)0x20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (long)0xAB30), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (ulong)0xAB30), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (long)0xffff20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", (ulong)0xffff20ac), Is.EqualTo("€"));
            Assert.That(SPrintF("%c", unchecked((long)0xffffffffffffAB30)), Is.EqualTo("ꬰ"));
            Assert.That(SPrintF("%c", (ulong)0xffffffffffffAB30), Is.EqualTo("ꬰ"));

            Assert.That(SPrintF("%c", (sbyte)-1), Is.EqualTo("ÿ"));    // -1 = 0xFF. The signed is type casted to unsigned with no bit modifications.
            Assert.That(SPrintF("%c", (short)-1280), Is.EqualTo("ﬀ")); // -1280 = 0xFB00. The signed is type casted to unsigned with no bit modifications.

            Assert.That(() => { _ = SPrintF("%c", true); }, Throws.TypeOf<FormatException>());
        }

        [Test]
        public void SPrintF_CharVarFieldWidth()
        {
            Assert.That(SPrintF("%*c", 0, 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%*c", 1, 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%*c", 2, 'a'), Is.EqualTo(" a"));
            Assert.That(SPrintF("%*c", 3, 'a'), Is.EqualTo("  a"));
            Assert.That(SPrintF("%*c", 4, 'a'), Is.EqualTo("   a"));
            Assert.That(SPrintF("%*c", 5, 'a'), Is.EqualTo("    a"));

            Assert.That(SPrintF("%*c", -1, 'a'), Is.EqualTo("a"));
            Assert.That(SPrintF("%*c", -2, 'a'), Is.EqualTo("a "));
            Assert.That(SPrintF("%*c", -3, 'a'), Is.EqualTo("a  "));
            Assert.That(SPrintF("%*c", -4, 'a'), Is.EqualTo("a   "));
            Assert.That(SPrintF("%*c", -5, 'a'), Is.EqualTo("a    "));
        }

        [Test]
        public void SPrintF_CharVarFieldWidthInvalid()
        {
            Assert.That(() => { SPrintF("%*c", "1", 'a'); }, Throws.TypeOf<FormatException>());
            Assert.That(() => { SPrintF("%*c", new object(), 'a'); }, Throws.TypeOf<FormatException>());
        }

        [Test]
        public void SPrintF_String()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%s", "foo"), Is.EqualTo("foo"));
            Assert.That(SPrintF("%-10s", "foo"), Is.EqualTo("foo       "));
            Assert.That(SPrintF("%10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%010s", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%+10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("% 10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%#10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%2s", "foobar"), Is.EqualTo("foobar"));
            Assert.That(SPrintF("%-2s", "foobar"), Is.EqualTo("foobar"));

            // Because .NET is always wide char, the length modifier 'l' doesn't change anything
            Assert.That(SPrintF("%ls", "foo"), Is.EqualTo("foo"));
            Assert.That(SPrintF("%-10ls", "foo"), Is.EqualTo("foo       "));
            Assert.That(SPrintF("%10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%010ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%+10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("% 10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%#10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(SPrintF("%2ls", "foobar"), Is.EqualTo("foobar"));
            Assert.That(SPrintF("%-2ls", "foobar"), Is.EqualTo("foobar"));
        }

        [Test]
        public void SPrintF_StringEmpty()
        {
            Assert.That(SPrintF("%s", string.Empty), Is.EqualTo(""));
        }

        [Test]
        public void SPrintF_StringNoParameters()
        {
            // This is tricky. You might thing you're passing a null string here, but instead
            // it's really a null parameter list.
            Assert.That(() => { SPrintF("%s", null); }, Throws.TypeOf<FormatException>());
        }

        [Test]
        public void SPrintF_StringNull()
        {
            // To pass a string of null, we need to construct the parameter list manually.
            Assert.That(SPrintF("%s", new object[] { null }), Is.EqualTo(""));

            string nulls = null;
            Assert.That(SPrintF("%s", nulls), Is.EqualTo(""));
        }

        [Test]
        public void SPrintF_Integer()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%d", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%2d", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%.1d", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%-.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%010d", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%-010d", 16384), Is.EqualTo("16384     "));
            Assert.That(SPrintF("%-+10d", 16384), Is.EqualTo("+16384    "));
            Assert.That(SPrintF("%+10d", 16384), Is.EqualTo("    +16384"));
            Assert.That(SPrintF("%04d", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%010.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%012.10d", 16384), Is.EqualTo("  0000016384"));
            Assert.That(SPrintF("%012.0d", 16384), Is.EqualTo("       16384"));
            Assert.That(SPrintF("%08.12d", 16384), Is.EqualTo("000000016384"));
            Assert.That(SPrintF("%010.12d", 16384), Is.EqualTo("000000016384"));
            Assert.That(SPrintF("% 10.10d", 16384), Is.EqualTo(" 0000016384"));
            Assert.That(SPrintF("%d", -49152), Is.EqualTo("-49152"));
            Assert.That(SPrintF("%2d", -49152), Is.EqualTo("-49152"));
            Assert.That(SPrintF("%.1d", -49152), Is.EqualTo("-49152"));
            Assert.That(SPrintF("%.10d", -49152), Is.EqualTo("-0000049152"));
            Assert.That(SPrintF("%05.10d", -49152), Is.EqualTo("-0000049152"));
            Assert.That(SPrintF("%12.10d", -49152), Is.EqualTo(" -0000049152"));
            Assert.That(SPrintF("% 12.10d", -49152), Is.EqualTo(" -0000049152"));
            Assert.That(SPrintF("%-0 15.10d", -49152), Is.EqualTo("-0000049152    "));
            Assert.That(SPrintF("%-015.10d", -49152), Is.EqualTo("-0000049152    "));
            Assert.That(SPrintF("%0 15.10d", -49152), Is.EqualTo("    -0000049152"));
            Assert.That(SPrintF("%015.10d", -49152), Is.EqualTo("    -0000049152"));
            Assert.That(SPrintF("%-0 15.2d", -49152), Is.EqualTo("-49152         "));
            Assert.That(SPrintF("%-015.2d", -49152), Is.EqualTo("-49152         "));
            Assert.That(SPrintF("%0 15.2d", -49152), Is.EqualTo("         -49152"));
            Assert.That(SPrintF("%015.2d", -49152), Is.EqualTo("         -49152"));
            Assert.That(SPrintF("%-0 15d", -49152), Is.EqualTo("-49152         "));
            Assert.That(SPrintF("%-015d", -49152), Is.EqualTo("-49152         "));
            Assert.That(SPrintF("%0 15d", -49152), Is.EqualTo("-00000000049152"));
            Assert.That(SPrintF("%015d", -49152), Is.EqualTo("-00000000049152"));
            Assert.That(SPrintF("% d", -32768), Is.EqualTo("-32768"));
            Assert.That(SPrintF("%+d", -32768), Is.EqualTo("-32768"));
            Assert.That(SPrintF("%#d", -32768), Is.EqualTo("-32768"));
            Assert.That(SPrintF("% d", 65536), Is.EqualTo(" 65536"));
            Assert.That(SPrintF("%+d", 65536), Is.EqualTo("+65536"));
            Assert.That(SPrintF("%#d", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("% +d", 65536), Is.EqualTo("+65536"));
            Assert.That(SPrintF("%+ d", 65536), Is.EqualTo("+65536"));
            Assert.That(SPrintF("% -d", 65536), Is.EqualTo(" 65536"));
            Assert.That(SPrintF("%- d", 65536), Is.EqualTo(" 65536"));
            Assert.That(SPrintF("%- 10d", 65536), Is.EqualTo(" 65536    "));
            Assert.That(SPrintF("%.0d", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%5.0d", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%05.0d", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%+.0d", 0), Is.EqualTo("+"));
            Assert.That(SPrintF("%-.0d", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% .0d", 0), Is.EqualTo(" "));
            Assert.That(SPrintF("%-+.0d", 0), Is.EqualTo("+"));
            Assert.That(SPrintF("% d", 0), Is.EqualTo(" 0"));
            Assert.That(SPrintF("%+d", 0), Is.EqualTo("+0"));
            Assert.That(SPrintF("%#d", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% +d", 0), Is.EqualTo("+0"));
            Assert.That(SPrintF("%+ d", 0), Is.EqualTo("+0"));
            Assert.That(SPrintF("% -d", 0), Is.EqualTo(" 0"));
            Assert.That(SPrintF("%- d", 0), Is.EqualTo(" 0"));
            Assert.That(SPrintF("%- 10d", 0), Is.EqualTo(" 0        "));
            Assert.That(SPrintF("%hhi", 0xFFE), Is.EqualTo("-2"));
            Assert.That(SPrintF("%hhi", 0x3FE), Is.EqualTo("-2"));
            Assert.That(SPrintF("%hhi", 0xF7F), Is.EqualTo("127"));
            Assert.That(SPrintF("%hhi", 0x37F), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", 0x8000000000000000UL), Is.EqualTo("-9223372036854775808"));
        }

        [TestCase("%d")]
        [TestCase("%ld")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1905:Redundant casts should not be used", Justification = "Provided to make reading test case explicit")]
        public void SPrintF_IntegerConversions(string specifier)
        {
            Assert.That(SPrintF(specifier, (byte)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (byte)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (sbyte)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (sbyte)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, (short)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (short)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (short)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (short)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, (ushort)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (ushort)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (ushort)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (ushort)65535), Is.EqualTo("65535"));

            Assert.That(SPrintF(specifier, (int)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (int)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (int)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (int)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (int)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, (uint)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (uint)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (uint)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (uint)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (uint)0xFFFFFFFF), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, (long)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (long)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (long)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (long)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (long)0xFFFFFFFF), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, (ulong)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (ulong)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (ulong)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (ulong)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (ulong)0xFFFFFFFF), Is.EqualTo("-1"));

            Assert.That(SPrintF(specifier, false), Is.EqualTo("0"));
            Assert.That(SPrintF(specifier, true), Is.EqualTo("-1"));
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1905:Redundant casts should not be used", Justification = "Provided to make reading test case explicit")]
        public void SPrintF_LongIntegerConversions()
        {
            Assert.That(SPrintF("%lld", (byte)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (byte)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (sbyte)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (sbyte)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF("%lld", (short)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (short)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (short)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (short)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF("%lld", (ushort)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (ushort)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (ushort)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (ushort)65535), Is.EqualTo("65535"));

            Assert.That(SPrintF("%lld", (int)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (int)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (int)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (int)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%lld", (int)-1), Is.EqualTo("-1"));

            Assert.That(SPrintF("%lld", (uint)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (uint)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (uint)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (uint)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%lld", (uint)0xFFFFFFFF), Is.EqualTo("4294967295"));

            Assert.That(SPrintF("%lld", (long)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (long)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (long)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (long)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%lld", (long)0xFFFFFFFF), Is.EqualTo("4294967295"));
            Assert.That(SPrintF("%lld", unchecked((long)0xFFFFFFFFFFFFFFFF)), Is.EqualTo("-1"));

            Assert.That(SPrintF("%lld", (ulong)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%lld", (ulong)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%lld", (ulong)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%lld", (ulong)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%lld", (ulong)0xFFFFFFFF), Is.EqualTo("4294967295"));
            Assert.That(SPrintF("%lld", (ulong)0xFFFFFFFFFFFFFFFF), Is.EqualTo("-1"));

            Assert.That(SPrintF("%lld", false), Is.EqualTo("0"));
            Assert.That(SPrintF("%lld", true), Is.EqualTo("-1"));
        }

        [Test]
        public void SPrintF_UnsignedInteger()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%u", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%2u", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%.1u", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%010u", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%-010u", 16384), Is.EqualTo("16384     "));
            Assert.That(SPrintF("%04u", 16384), Is.EqualTo("16384"));
            Assert.That(SPrintF("%010.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("% 10.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(SPrintF("%u", -49152), Is.EqualTo("4294918144"));
            Assert.That(SPrintF("%2u", -49152), Is.EqualTo("4294918144"));
            Assert.That(SPrintF("%.1u", -49152), Is.EqualTo("4294918144"));
            Assert.That(SPrintF("%.10u", -49152), Is.EqualTo("4294918144"));
            Assert.That(SPrintF("%05.10u", -49152), Is.EqualTo("4294918144"));
            Assert.That(SPrintF("%12.10u", -49152), Is.EqualTo("  4294918144"));
            Assert.That(SPrintF("% 12.10u", -49152), Is.EqualTo("  4294918144"));
            Assert.That(SPrintF("%-0 15.10u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%-015.10u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%0 15.10u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(SPrintF("%015.10u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(SPrintF("%-0 15.2u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%-015.2u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%0 15.2u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(SPrintF("%015.2u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(SPrintF("%-0 15u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%-015u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(SPrintF("%0 15u", -49152), Is.EqualTo("000004294918144"));
            Assert.That(SPrintF("%015u", -49152), Is.EqualTo("000004294918144"));
            Assert.That(SPrintF("% u", -32768), Is.EqualTo("4294934528"));
            Assert.That(SPrintF("%+u", -32768), Is.EqualTo("4294934528"));
            Assert.That(SPrintF("%#u", -32768), Is.EqualTo("4294934528"));
            Assert.That(SPrintF("% u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("%+u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("%#u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("% +u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("%+ u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("% -u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("%- u", 65536), Is.EqualTo("65536"));
            Assert.That(SPrintF("%- 10u", 65536), Is.EqualTo("65536     "));
            Assert.That(SPrintF("%.0u", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%5.0u", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%05.0u", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%+.0u", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-.0u", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% .0u", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-+.0u", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%#u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% +u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+ u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% -u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- u", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- 10u", 0), Is.EqualTo("0         "));
            Assert.That(SPrintF("%hhu", 0xFFE), Is.EqualTo("254"));
            Assert.That(SPrintF("%hhu", 0x3FE), Is.EqualTo("254"));
            Assert.That(SPrintF("%hhu", 0xF7F), Is.EqualTo("127"));
            Assert.That(SPrintF("%hhu", 0x37F), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", 0x8000000000000000UL), Is.EqualTo("9223372036854775808"));
        }

        [TestCase("%u")]
        [TestCase("%lu")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1905:Redundant casts should not be used", Justification = "Provided to make reading test case explicit")]
        public void SPrintF_UnsignedIntegerConversions(string specifier)
        {
            Assert.That(SPrintF(specifier, (byte)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (byte)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (sbyte)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (sbyte)-1), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, (short)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (short)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (short)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (short)-1), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, (ushort)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (ushort)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (ushort)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (ushort)65535), Is.EqualTo("65535"));

            Assert.That(SPrintF(specifier, (int)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (int)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (int)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (int)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (int)-1), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, (uint)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (uint)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (uint)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (uint)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (uint)0xFFFFFFFF), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, (long)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (long)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (long)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (long)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (long)0xFFFFFFFF), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, (ulong)127), Is.EqualTo("127"));
            Assert.That(SPrintF(specifier, (ulong)255), Is.EqualTo("255"));
            Assert.That(SPrintF(specifier, (ulong)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF(specifier, (ulong)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF(specifier, (ulong)0xFFFFFFFF), Is.EqualTo("4294967295"));

            Assert.That(SPrintF(specifier, false), Is.EqualTo("0"));
            Assert.That(SPrintF(specifier, true), Is.EqualTo("4294967295"));
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1905:Redundant casts should not be used", Justification = "Provided to make reading test case explicit")]
        public void SPrintF_LongUnsignedIntegerConversions()
        {
            Assert.That(SPrintF("%llu", (byte)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (byte)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (sbyte)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (sbyte)-1), Is.EqualTo("8446744073709551615"));

            Assert.That(SPrintF("%llu", (short)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (short)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (short)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (short)-1), Is.EqualTo("8446744073709551615"));

            Assert.That(SPrintF("%llu", (ushort)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (ushort)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (ushort)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (ushort)65535), Is.EqualTo("65535"));

            Assert.That(SPrintF("%llu", (int)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (int)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (int)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (int)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%llu", (int)-1), Is.EqualTo("8446744073709551615"));

            Assert.That(SPrintF("%llu", (uint)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (uint)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (uint)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (uint)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%llu", (uint)0xFFFFFFFF), Is.EqualTo("4294967295"));

            Assert.That(SPrintF("%llu", (long)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (long)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (long)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (long)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%llu", (long)0xFFFFFFFF), Is.EqualTo("4294967295"));
            Assert.That(SPrintF("%llu", unchecked((long)0xFFFFFFFFFFFFFFFF)), Is.EqualTo("8446744073709551615"));

            Assert.That(SPrintF("%llu", (ulong)127), Is.EqualTo("127"));
            Assert.That(SPrintF("%llu", (ulong)255), Is.EqualTo("255"));
            Assert.That(SPrintF("%llu", (ulong)32767), Is.EqualTo("32767"));
            Assert.That(SPrintF("%llu", (ulong)131072), Is.EqualTo("131072"));
            Assert.That(SPrintF("%llu", (ulong)0xFFFFFFFF), Is.EqualTo("4294967295"));
            Assert.That(SPrintF("%llu", (ulong)0xFFFFFFFFFFFFFFFF), Is.EqualTo("8446744073709551615"));

            Assert.That(SPrintF("%llu", false), Is.EqualTo("0"));
            Assert.That(SPrintF("%llu", true), Is.EqualTo("8446744073709551615"));
        }

        [Test]
        public void SPrintF_Hexadecimal()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%x", 16384), Is.EqualTo("4000"));
            Assert.That(SPrintF("%2x", 16384), Is.EqualTo("4000"));
            Assert.That(SPrintF("%.1x", 16384), Is.EqualTo("4000"));
            Assert.That(SPrintF("%.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(SPrintF("%010x", 16384), Is.EqualTo("0000004000"));
            Assert.That(SPrintF("%-010x", 16384), Is.EqualTo("4000      "));
            Assert.That(SPrintF("%04x", 16384), Is.EqualTo("4000"));
            Assert.That(SPrintF("%010.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(SPrintF("% 10.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(SPrintF("%x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(SPrintF("%2x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(SPrintF("%.1x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(SPrintF("%.10x", -49152), Is.EqualTo("00ffff4000"));
            Assert.That(SPrintF("%05.10x", -49152), Is.EqualTo("00ffff4000"));
            Assert.That(SPrintF("%12.10x", -49152), Is.EqualTo("  00ffff4000"));
            Assert.That(SPrintF("% 12.10x", -49152), Is.EqualTo("  00ffff4000"));
            Assert.That(SPrintF("%-0 15.10x", -49152), Is.EqualTo("00ffff4000     "));
            Assert.That(SPrintF("%-015.10x", -49152), Is.EqualTo("00ffff4000     "));
            Assert.That(SPrintF("%0 15.10x", -49152), Is.EqualTo("     00ffff4000"));
            Assert.That(SPrintF("%015.10x", -49152), Is.EqualTo("     00ffff4000"));
            Assert.That(SPrintF("%-0 15.2x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(SPrintF("%-015.2x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(SPrintF("%0 15.2x", -49152), Is.EqualTo("       ffff4000"));
            Assert.That(SPrintF("%015.2x", -49152), Is.EqualTo("       ffff4000"));
            Assert.That(SPrintF("%-0 15x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(SPrintF("%-015x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(SPrintF("%0 15x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(SPrintF("%015x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(SPrintF("% x", -32768), Is.EqualTo("ffff8000"));
            Assert.That(SPrintF("%+x", -32768), Is.EqualTo("ffff8000"));
            Assert.That(SPrintF("%#x", -32768), Is.EqualTo("0xffff8000"));
            Assert.That(SPrintF("% x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("%+x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("%#x", 65536), Is.EqualTo("0x10000"));
            Assert.That(SPrintF("% +x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("%+ x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("% -x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("%- x", 65536), Is.EqualTo("10000"));
            Assert.That(SPrintF("%- 10x", 65536), Is.EqualTo("10000     "));
            Assert.That(SPrintF("%.0x", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%5.0x", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%05.0x", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%+.0x", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-.0x", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% .0x", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-+.0x", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%#x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% +x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+ x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% -x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- x", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- 10x", 0), Is.EqualTo("0         "));
            Assert.That(SPrintF("%hhx", 0xFFE), Is.EqualTo("fe"));
            Assert.That(SPrintF("%hhx", 0x3FE), Is.EqualTo("fe"));
            Assert.That(SPrintF("%hhx", 0xF7F), Is.EqualTo("7f"));
            Assert.That(SPrintF("%hhx", 0x37F), Is.EqualTo("7f"));
            Assert.That(SPrintF("%llx", 0x8000000000000000UL), Is.EqualTo("8000000000000000"));
        }

        [Test]
        public void SPrintF_Octal()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%o", 16384), Is.EqualTo("40000"));
            Assert.That(SPrintF("%2o", 16384), Is.EqualTo("40000"));
            Assert.That(SPrintF("%.1o", 16384), Is.EqualTo("40000"));
            Assert.That(SPrintF("%.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(SPrintF("%010o", 16384), Is.EqualTo("0000040000"));
            Assert.That(SPrintF("%-010o", 16384), Is.EqualTo("40000     "));
            Assert.That(SPrintF("%04o", 16384), Is.EqualTo("40000"));
            Assert.That(SPrintF("%010.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(SPrintF("% 10.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(SPrintF("%o", -49152), Is.EqualTo("37777640000"));
            Assert.That(SPrintF("%2o", -49152), Is.EqualTo("37777640000"));
            Assert.That(SPrintF("%.1o", -49152), Is.EqualTo("37777640000"));
            Assert.That(SPrintF("%.10o", -49152), Is.EqualTo("37777640000"));
            Assert.That(SPrintF("%05.10o", -49152), Is.EqualTo("37777640000"));
            Assert.That(SPrintF("%12.10o", -49152), Is.EqualTo(" 37777640000"));
            Assert.That(SPrintF("% 12.10o", -49152), Is.EqualTo(" 37777640000"));
            Assert.That(SPrintF("%-0 15.10o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%-015.10o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%0 15.10o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(SPrintF("%015.10o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(SPrintF("%-0 15.2o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%-015.2o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%0 15.2o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(SPrintF("%015.2o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(SPrintF("%-0 15o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%-015o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(SPrintF("%0 15o", -49152), Is.EqualTo("000037777640000"));
            Assert.That(SPrintF("%015x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(SPrintF("% o", -32768), Is.EqualTo("37777700000"));
            Assert.That(SPrintF("%+o", -32768), Is.EqualTo("37777700000"));
            Assert.That(SPrintF("%#o", -32768), Is.EqualTo("037777700000"));
            Assert.That(SPrintF("% o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("%+o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("%#o", 65536), Is.EqualTo("0200000"));
            Assert.That(SPrintF("% +o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("%+ o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("% -o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("%- o", 65536), Is.EqualTo("200000"));
            Assert.That(SPrintF("%- 10o", 65536), Is.EqualTo("200000    "));
            Assert.That(SPrintF("%.0o", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%5.0o", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%05.0o", 0), Is.EqualTo("     "));
            Assert.That(SPrintF("%+.0o", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-.0o", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% .0o", 0), Is.EqualTo(""));
            Assert.That(SPrintF("%-+.0o", 0), Is.EqualTo(""));
            Assert.That(SPrintF("% o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%#o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% +o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%+ o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("% -o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- o", 0), Is.EqualTo("0"));
            Assert.That(SPrintF("%- 10o", 0), Is.EqualTo("0         "));
            Assert.That(SPrintF("%hho", 0xFFE), Is.EqualTo("376"));
            Assert.That(SPrintF("%hho", 0x3FE), Is.EqualTo("376"));
            Assert.That(SPrintF("%hho", 0xF7F), Is.EqualTo("177"));
            Assert.That(SPrintF("%hho", 0x37F), Is.EqualTo("177"));
            Assert.That(SPrintF("%llo", 0x8000000000000000UL), Is.EqualTo("1000000000000000000000"));
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out",
            Justification = "Commented code was generated, and after review is obviously wrong (a GCC 'feature' still within C standards)")]
        public void SPrintF_FixedDouble()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%f", 10), Is.EqualTo("10.000000"));
            Assert.That(SPrintF("%f", 123456.789), Is.EqualTo("123456.789000"));
            Assert.That(SPrintF("%.2f", 123456.789), Is.EqualTo("123456.79"));
            Assert.That(SPrintF("%f", 0.00000000010), Is.EqualTo("0.000000"));
            Assert.That(SPrintF("%f", 100000000000.0), Is.EqualTo("100000000000.000000"));
            Assert.That(SPrintF("%12f", 10), Is.EqualTo("   10.000000"));
            Assert.That(SPrintF("%12.0f", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%#12.0f", 10), Is.EqualTo("         10."));
            Assert.That(SPrintF("%#.0f", 10), Is.EqualTo("10."));
            Assert.That(SPrintF("%12.1f", 10), Is.EqualTo("        10.0"));
            Assert.That(SPrintF("%12.8f", 10), Is.EqualTo(" 10.00000000"));
            Assert.That(SPrintF("%12.10f", 10), Is.EqualTo("10.0000000000"));
            Assert.That(SPrintF("%13.10f", 10), Is.EqualTo("10.0000000000"));
            Assert.That(SPrintF("%14.10f", 10), Is.EqualTo(" 10.0000000000"));
            Assert.That(SPrintF("%f", -10), Is.EqualTo("-10.000000"));
            Assert.That(SPrintF("%12f", -10), Is.EqualTo("  -10.000000"));
            Assert.That(SPrintF("%12.0f", -10), Is.EqualTo("         -10"));
            Assert.That(SPrintF("%12.1f", -10), Is.EqualTo("       -10.0"));
            Assert.That(SPrintF("%12.10f", -10), Is.EqualTo("-10.0000000000"));
            Assert.That(SPrintF("% f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("% 2f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("% 8f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("% 9f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("% 10f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("% 12f", 10), Is.EqualTo("   10.000000"));
            Assert.That(SPrintF("%- f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("%- 2f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("%- 8f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("%- 9f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("%- 10f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(SPrintF("%- 12f", 10), Is.EqualTo(" 10.000000  "));
            Assert.That(SPrintF("%+f", 10), Is.EqualTo("+10.000000"));
            Assert.That(SPrintF("%-f", 10), Is.EqualTo("10.000000"));
            Assert.That(SPrintF("%#f", 10), Is.EqualTo("10.000000"));
            Assert.That(SPrintF("%0f", 10), Is.EqualTo("10.000000"));
            Assert.That(SPrintF("%012f", 10), Is.EqualTo("00010.000000"));
            Assert.That(SPrintF("%012f", -10), Is.EqualTo("-0010.000000"));
            Assert.That(SPrintF("%-012f", -10), Is.EqualTo("-10.000000  "));
            Assert.That(SPrintF("%05f", -10), Is.EqualTo("-10.000000"));
            //Assert.That(SPrintF("%f", 1e100), Is.EqualTo("10000000000000000159028911097599180468360810000000000000000000000000000000000000000000000000000000000.000000"));
            Assert.That(SPrintF("%f", 1e100), Is.EqualTo("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.000000"));
            Assert.That(SPrintF("%f", 1e-100), Is.EqualTo("0.000000"));
            Assert.That(SPrintF("%f", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%F", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("%10.2f", double.NaN), Is.EqualTo("       nan"));
            Assert.That(SPrintF("%10.2F", double.NaN), Is.EqualTo("       NAN"));
            Assert.That(SPrintF("%#f", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%#F", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("% f", double.NaN), Is.EqualTo(" nan"));
            Assert.That(SPrintF("% F", double.NaN), Is.EqualTo(" NAN"));
            Assert.That(SPrintF("%+f", double.NaN), Is.EqualTo("+nan"));
            Assert.That(SPrintF("%+F", double.NaN), Is.EqualTo("+NAN"));
            Assert.That(SPrintF("%-10f", double.NaN), Is.EqualTo("nan       "));
            Assert.That(SPrintF("%-10F", double.NaN), Is.EqualTo("NAN       "));
            Assert.That(SPrintF("%010f", double.NaN), Is.EqualTo("       nan"));
            Assert.That(SPrintF("%010F", double.NaN), Is.EqualTo("       NAN"));
            Assert.That(SPrintF("%f", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%F", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%10.2f", double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(SPrintF("%10.2F", double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(SPrintF("% f", double.PositiveInfinity), Is.EqualTo(" inf"));
            Assert.That(SPrintF("% F", double.PositiveInfinity), Is.EqualTo(" INF"));
            Assert.That(SPrintF("%+f", double.PositiveInfinity), Is.EqualTo("+inf"));
            Assert.That(SPrintF("%+F", double.PositiveInfinity), Is.EqualTo("+INF"));
            Assert.That(SPrintF("%#f", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%#F", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%-10f", double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(SPrintF("%-10F", double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(SPrintF("%010f", double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(SPrintF("%010F", double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(SPrintF("%f", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%F", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%10.2f", double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(SPrintF("%10.2F", double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(SPrintF("%#f", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%#F", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%-10f", double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(SPrintF("%-10F", double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        [Test]
        public void SPrintF_ExponentDouble()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%12e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%E", 10), Is.EqualTo("1.000000E+01"));
            Assert.That(SPrintF("%12E", 10), Is.EqualTo("1.000000E+01"));
            Assert.That(SPrintF("%e", 123456.789), Is.EqualTo("1.234568e+05"));
            Assert.That(SPrintF("%.2e", 123456.789), Is.EqualTo("1.23e+05"));
            Assert.That(SPrintF("%e", 0.00000000010), Is.EqualTo("1.000000e-10"));
            Assert.That(SPrintF("%e", 100000000000.0), Is.EqualTo("1.000000e+11"));
            Assert.That(SPrintF("%e", 1e101), Is.EqualTo("1.000000e+101"));
            Assert.That(SPrintF("%e", 1e-101), Is.EqualTo("1.000000e-101"));
            Assert.That(SPrintF("%e", 1e100), Is.EqualTo("1.000000e+100"));
            Assert.That(SPrintF("%e", 1e-100), Is.EqualTo("1.000000e-100"));
            Assert.That(SPrintF("%e", 1e99), Is.EqualTo("1.000000e+99"));
            Assert.That(SPrintF("%e", 1e-99), Is.EqualTo("1.000000e-99"));
            Assert.That(SPrintF("%12.0e", 10), Is.EqualTo("       1e+01"));
            Assert.That(SPrintF("%#12.0e", 10), Is.EqualTo("      1.e+01"));
            Assert.That(SPrintF("%12.1e", 10), Is.EqualTo("     1.0e+01"));
            Assert.That(SPrintF("%12.8e", 10), Is.EqualTo("1.00000000e+01"));
            Assert.That(SPrintF("%12.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(SPrintF("%13.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(SPrintF("%14.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(SPrintF("%e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(SPrintF("%12e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(SPrintF("%12.0e", -10), Is.EqualTo("      -1e+01"));
            Assert.That(SPrintF("%12.1e", -10), Is.EqualTo("    -1.0e+01"));
            Assert.That(SPrintF("%12.10e", -10), Is.EqualTo("-1.0000000000e+01"));
            Assert.That(SPrintF("% e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("% 2E", 10), Is.EqualTo(" 1.000000E+01"));
            Assert.That(SPrintF("% 8E", 10), Is.EqualTo(" 1.000000E+01"));
            Assert.That(SPrintF("% 9e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("% 10e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("% 12e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- 2e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- 8e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- 9e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- 10e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%- 12e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(SPrintF("%+e", 10), Is.EqualTo("+1.000000e+01"));
            Assert.That(SPrintF("%-e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%#e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%0e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%012e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(SPrintF("%012e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(SPrintF("%-012e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(SPrintF("%015e", 10), Is.EqualTo("0001.000000e+01"));
            Assert.That(SPrintF("%015e", -10), Is.EqualTo("-001.000000e+01"));
            Assert.That(SPrintF("%-015e", -10), Is.EqualTo("-1.000000e+01  "));
            Assert.That(SPrintF("%05e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(SPrintF("%e", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%E", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("%10.2e", double.NaN), Is.EqualTo("       nan"));
            Assert.That(SPrintF("%10.2E", double.NaN), Is.EqualTo("       NAN"));
            Assert.That(SPrintF("%#e", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%#E", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("%-10e", double.NaN), Is.EqualTo("nan       "));
            Assert.That(SPrintF("%-10E", double.NaN), Is.EqualTo("NAN       "));
            Assert.That(SPrintF("%e", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%E", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%10.2e", double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(SPrintF("%10.2E", double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(SPrintF("%#e", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%#E", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%-10e", double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(SPrintF("%-10E", double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(SPrintF("%e", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%E", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%10.2e", double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(SPrintF("%10.2E", double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(SPrintF("%#e", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%#E", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%-10e", double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(SPrintF("%-10E", double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        [Test]
        public void SPrintF_GeneralDouble()
        {
            // Obtained using Ubuntu 18.04 x64 GCC 7.4.0
            Assert.That(SPrintF("%g", 0.000001), Is.EqualTo("1e-06"));
            Assert.That(SPrintF("%g", 0.00001), Is.EqualTo("1e-05"));
            Assert.That(SPrintF("%g", 0.0001), Is.EqualTo("0.0001"));
            Assert.That(SPrintF("%g", 0.001), Is.EqualTo("0.001"));
            Assert.That(SPrintF("%g", 0.01), Is.EqualTo("0.01"));
            Assert.That(SPrintF("%g", 0.1), Is.EqualTo("0.1"));
            Assert.That(SPrintF("%g", 1), Is.EqualTo("1"));
            Assert.That(SPrintF("%g", 10), Is.EqualTo("10"));
            Assert.That(SPrintF("%g", 100), Is.EqualTo("100"));
            Assert.That(SPrintF("%g", 1000), Is.EqualTo("1000"));
            Assert.That(SPrintF("%g", 10000), Is.EqualTo("10000"));
            Assert.That(SPrintF("%g", 100000), Is.EqualTo("100000"));
            Assert.That(SPrintF("%g", 1000000), Is.EqualTo("1e+06"));
            Assert.That(SPrintF("%g", 10000000), Is.EqualTo("1e+07"));
            Assert.That(SPrintF("%g", 0.00000000010), Is.EqualTo("1e-10"));
            Assert.That(SPrintF("%g", 100000000000.0), Is.EqualTo("1e+11"));
            Assert.That(SPrintF("%.0g", 10), Is.EqualTo("1e+01"));
            Assert.That(SPrintF("%.0G", 10), Is.EqualTo("1E+01"));
            Assert.That(SPrintF("%g", 31.41), Is.EqualTo("31.41"));
            Assert.That(SPrintF("%.0g", 31.41), Is.EqualTo("3e+01"));
            Assert.That(SPrintF("%.0G", 31.41), Is.EqualTo("3E+01"));
            Assert.That(SPrintF("%.2g", 31.41), Is.EqualTo("31"));
            Assert.That(SPrintF("%.2G", 31.41), Is.EqualTo("31"));
            Assert.That(SPrintF("%.3g", 31.4159), Is.EqualTo("31.4"));
            Assert.That(SPrintF("%.3G", 31.4159), Is.EqualTo("31.4"));
            Assert.That(SPrintF("%.3g", 3.14159), Is.EqualTo("3.14"));
            Assert.That(SPrintF("%.3G", 3.14159), Is.EqualTo("3.14"));
            Assert.That(SPrintF("%g", 123456.789), Is.EqualTo("123457"));
            Assert.That(SPrintF("%.2g", 123456.789), Is.EqualTo("1.2e+05"));
            Assert.That(SPrintF("%g", 1e101), Is.EqualTo("1e+101"));
            Assert.That(SPrintF("%g", 1e-101), Is.EqualTo("1e-101"));
            Assert.That(SPrintF("%g", 1e100), Is.EqualTo("1e+100"));
            Assert.That(SPrintF("%g", 1e-100), Is.EqualTo("1e-100"));
            Assert.That(SPrintF("%g", 1e99), Is.EqualTo("1e+99"));
            Assert.That(SPrintF("%g", 1e-99), Is.EqualTo("1e-99"));
            Assert.That(SPrintF("%12g", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%12.0g", 10), Is.EqualTo("       1e+01"));
            Assert.That(SPrintF("%G", 10), Is.EqualTo("10"));
            Assert.That(SPrintF("%12G", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%12.0G", 10), Is.EqualTo("       1E+01"));
            Assert.That(SPrintF("%#12.0g", 10), Is.EqualTo("      1.e+01"));
            Assert.That(SPrintF("%12.1g", 10), Is.EqualTo("       1e+01"));
            Assert.That(SPrintF("%12.8g", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%12.10g", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%13.10g", 10), Is.EqualTo("           10"));
            Assert.That(SPrintF("%14.10g", 10), Is.EqualTo("            10"));
            Assert.That(SPrintF("%g", -10), Is.EqualTo("-10"));
            Assert.That(SPrintF("%12g", -10), Is.EqualTo("         -10"));
            Assert.That(SPrintF("%12.0g", -10), Is.EqualTo("      -1e+01"));
            Assert.That(SPrintF("%12.1g", -10), Is.EqualTo("      -1e+01"));
            Assert.That(SPrintF("%12.10g", -10), Is.EqualTo("         -10"));
            Assert.That(SPrintF("% g", 10), Is.EqualTo(" 10"));
            Assert.That(SPrintF("% 2G", 10), Is.EqualTo(" 10"));
            Assert.That(SPrintF("% 8G", 10), Is.EqualTo("      10"));
            Assert.That(SPrintF("% 9g", 10), Is.EqualTo("       10"));
            Assert.That(SPrintF("% 10g", 10), Is.EqualTo("        10"));
            Assert.That(SPrintF("% 12g", 10), Is.EqualTo("          10"));
            Assert.That(SPrintF("%- g", 10), Is.EqualTo(" 10"));
            Assert.That(SPrintF("%- 2g", 10), Is.EqualTo(" 10"));
            Assert.That(SPrintF("%- 8g", 10), Is.EqualTo(" 10     "));
            Assert.That(SPrintF("%- 9g", 10), Is.EqualTo(" 10      "));
            Assert.That(SPrintF("%- 10g", 10), Is.EqualTo(" 10       "));
            Assert.That(SPrintF("%- 12g", 10), Is.EqualTo(" 10         "));
            Assert.That(SPrintF("%+g", 10), Is.EqualTo("+10"));
            Assert.That(SPrintF("%-g", 10), Is.EqualTo("10"));
            Assert.That(SPrintF("%#g", 10), Is.EqualTo("10.0000"));
            Assert.That(SPrintF("%#g", 1000.0), Is.EqualTo("1000.00"));
            Assert.That(SPrintF("%#g", 10000.0), Is.EqualTo("10000.0"));
            Assert.That(SPrintF("%#g", 100000.0), Is.EqualTo("100000."));
            Assert.That(SPrintF("%#g", 1000000.0), Is.EqualTo("1.00000e+06"));
            Assert.That(SPrintF("%#g", 12000000.0), Is.EqualTo("1.20000e+07"));
            Assert.That(SPrintF("%0g", 10), Is.EqualTo("10"));
            Assert.That(SPrintF("%012g", 10), Is.EqualTo("000000000010"));
            Assert.That(SPrintF("%012g", -10), Is.EqualTo("-00000000010"));
            Assert.That(SPrintF("%-012g", -10), Is.EqualTo("-10         "));
            Assert.That(SPrintF("%05g", -10), Is.EqualTo("-0010"));
            Assert.That(SPrintF("%g", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%G", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("%10.2g", double.NaN), Is.EqualTo("       nan"));
            Assert.That(SPrintF("%10.2G", double.NaN), Is.EqualTo("       NAN"));
            Assert.That(SPrintF("%#g", double.NaN), Is.EqualTo("nan"));
            Assert.That(SPrintF("%#G", double.NaN), Is.EqualTo("NAN"));
            Assert.That(SPrintF("%-10g", double.NaN), Is.EqualTo("nan       "));
            Assert.That(SPrintF("%-10G", double.NaN), Is.EqualTo("NAN       "));
            Assert.That(SPrintF("%g", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%G", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%10.2g", double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(SPrintF("%10.2G", double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(SPrintF("%#g", double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(SPrintF("%#G", double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(SPrintF("%-10g", double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(SPrintF("%-10G", double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(SPrintF("%g", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%G", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%10.2g", double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(SPrintF("%10.2G", double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(SPrintF("%#g", double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(SPrintF("%#G", double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(SPrintF("%-10g", double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(SPrintF("%-10G", double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        [Test]
        public void SPrintF_CountDigitsLong()
        {
            Assert.That(StringUtilitiesAccessor.CountDigits(-1), Is.EqualTo(1));
            Assert.That(StringUtilitiesAccessor.CountDigits(0), Is.EqualTo(1));
            Assert.That(StringUtilitiesAccessor.CountDigits(1), Is.EqualTo(1));

            long value = 1;
            int digits = 2;
            while (digits <= 19) {
                value *= 10;
                Assert.That(StringUtilitiesAccessor.CountDigits(value - 1), Is.EqualTo(digits - 1));
                Assert.That(StringUtilitiesAccessor.CountDigits(value), Is.EqualTo(digits));
                Assert.That(StringUtilitiesAccessor.CountDigits(value + 1), Is.EqualTo(digits));
                digits++;
            }

            value = -1;
            digits = 2;
            while (digits <= 19) {
                value *= 10;
                Assert.That(StringUtilitiesAccessor.CountDigits(value - 1), Is.EqualTo(digits));
                Assert.That(StringUtilitiesAccessor.CountDigits(value), Is.EqualTo(digits));
                Assert.That(StringUtilitiesAccessor.CountDigits(value + 1), Is.EqualTo(digits - 1));
                digits++;
            }
        }

        [Test]
        public void SPrintF_CountBitDigitsHex()
        {
            Assert.That(StringUtilitiesAccessor.CountBitDigits(0, 4), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 16) {
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                digits++;
            }
        }

        [Test]
        public void SPrintF_CountBitDigitsOctal()
        {
            Assert.That(StringUtilitiesAccessor.CountBitDigits(0, 3), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 22) {
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(StringUtilitiesAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                digits++;
            }
        }

        [Test]
        public void SPrintF_InvalidSpecifier()
        {
            Assert.That(SPrintF("handlerTransmitThread: 0x%02 , 0x%02", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%02 , 0x%02"));
            Assert.That(SPrintF("handlerTransmitThread: 0x%02 , 0x%0", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%02 , 0x%0"));
            Assert.That(SPrintF("handlerTransmitThread: 0x%02 , 0x%", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%02 , 0x%"));
            Assert.That(SPrintF("handlerTransmitThread: 0x%02 , 0x", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%02 , 0x"));
            Assert.That(SPrintF("handlerTransmitThread: 0x%0 , 0x%02", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%0 , 0x%02"));
            Assert.That(SPrintF("handlerTransmitThread: 0x% , 0x%02", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x% , 0x%02"));
            Assert.That(SPrintF("handlerTransmitThread: 0x , 0x%02", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x , 0x%02"));
            Assert.That(SPrintF("handlerTransmitThread: 0x%02, 0x%02", 0, 2176), Is.EqualTo("handlerTransmitThread: 0x%02, 0x%02"));
        }

        [TestCase("%c", "x", "x")]
        [TestCase("%d", "0", "0")]
        [TestCase("%d", "-1", "-1")]
        [TestCase("%u", "10", "10")]
        [TestCase("%x", "10", "a")]
        [TestCase("%f", "3.14", "3.140000")]
        [TestCase("%6.3f", "3.14", " 3.140")]
        public void SPrintF_StringTypeConversion(string format, string value, string expected)
        {
            Assert.That(SPrintF(format, value), Is.EqualTo(expected));
        }
    }
}
