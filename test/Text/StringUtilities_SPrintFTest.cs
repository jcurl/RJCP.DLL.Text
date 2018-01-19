namespace RJCP.Text
{
    using System;
    using NUnit.Framework;

    [TestFixture(Category = "String.SPrintF")]
    public class StringUtilitiesSPrintFTest
    {
        [Test]
        public void SPrintF_IntegerNormal()
        {
            string r = StringUtilities.SPrintF("My Number: %d", 5);
            Assert.That(r, Is.EqualTo("My Number: 5"));
        }

        [Test]
        public void SPrintF_IntegerWidth()
        {
            string r = StringUtilities.SPrintF("My Number: %10d", 5);
            Assert.That(r, Is.EqualTo("My Number:          5"));
        }

        [Test]
        public void SPrintF_IntegerWidthLeftAligned()
        {
            string r = StringUtilities.SPrintF("My Number: %-10d", 5);
            Assert.That(r, Is.EqualTo("My Number: 5         "));
        }

        [Test]
        public void SPrintF_Percent()
        {
            string r = StringUtilities.SPrintF("Processor Load %u%%", 6);
            Assert.That(r, Is.EqualTo("Processor Load 6%"));
        }

        [Test]
        public void SPrintF_Char()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%c", 65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%-2c", 'a'), Is.EqualTo("a "));
            Assert.That(StringUtilities.SPrintF("%2c", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%+2c", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("% 2c", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%#2c", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%02c", 'a'), Is.EqualTo("0a"));
            Assert.That(StringUtilities.SPrintF("%-2c", 'a'), Is.EqualTo("a "));
            Assert.That(StringUtilities.SPrintF("%+c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("% c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%#c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%0c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%-c", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%05c", 'a'), Is.EqualTo("0000a"));

            // Because .NET is always wide char, the length modifier 'l' doesn't change anything
            Assert.That(StringUtilities.SPrintF("%lc", 65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%-2lc", 'a'), Is.EqualTo("a "));
            Assert.That(StringUtilities.SPrintF("%2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%+2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("% 2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%#2lc", 'a'), Is.EqualTo(" a"));
            Assert.That(StringUtilities.SPrintF("%02lc", 'a'), Is.EqualTo("0a"));
            Assert.That(StringUtilities.SPrintF("%-2lc", 'a'), Is.EqualTo("a "));
            Assert.That(StringUtilities.SPrintF("%+lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("% lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%#lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%0lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%-lc", 'a'), Is.EqualTo("a"));
            Assert.That(StringUtilities.SPrintF("%05c", 'a'), Is.EqualTo("0000a"));
        }

        [Test]
        public void SPrintF_CharConversions()
        {
            Assert.That(StringUtilities.SPrintF("%c", (char)'A'), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (byte)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (sbyte)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (short)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (ushort)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (int)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (uint)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (long)65), Is.EqualTo("A"));
            Assert.That(StringUtilities.SPrintF("%c", (ulong)65), Is.EqualTo("A"));

            Assert.That(StringUtilities.SPrintF("%c", (sbyte)-1), Is.EqualTo("ÿ"));    // -1 = 0xFF. The signed is type casted to unsigned with no bit modifications.
            Assert.That(StringUtilities.SPrintF("%c", (short)-1280), Is.EqualTo("ﬀ")); // -1280 = 0xFB00. The signed is type casted to unsigned with no bit modifications.
        }

        [Test]
        public void SPrintF_String()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%s", "foo"), Is.EqualTo("foo"));
            Assert.That(StringUtilities.SPrintF("%-10s", "foo"), Is.EqualTo("foo       "));
            Assert.That(StringUtilities.SPrintF("%10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%010s", "foo"), Is.EqualTo("0000000foo"));
            Assert.That(StringUtilities.SPrintF("%+10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("% 10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%#10s", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%2s", "foobar"), Is.EqualTo("foobar"));
            Assert.That(StringUtilities.SPrintF("%-2s", "foobar"), Is.EqualTo("foobar"));

            // Because .NET is always wide char, the length modifier 'l' doesn't change anything
            Assert.That(StringUtilities.SPrintF("%ls", "foo"), Is.EqualTo("foo"));
            Assert.That(StringUtilities.SPrintF("%-10ls", "foo"), Is.EqualTo("foo       "));
            Assert.That(StringUtilities.SPrintF("%10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%010ls", "foo"), Is.EqualTo("0000000foo"));
            Assert.That(StringUtilities.SPrintF("%+10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("% 10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%#10ls", "foo"), Is.EqualTo("       foo"));
            Assert.That(StringUtilities.SPrintF("%2ls", "foobar"), Is.EqualTo("foobar"));
            Assert.That(StringUtilities.SPrintF("%-2ls", "foobar"), Is.EqualTo("foobar"));
        }

        [Test]
        public void SPrintF_StringEmpty()
        {
            Assert.That(StringUtilities.SPrintF("%s", string.Empty), Is.EqualTo(""));
        }

        [Test]
        public void SPrintF_StringNoParameters()
        {
            // This is tricky. You might thing you're passing a null string here, but instead
            // it's really a null parameter list.
            Assert.That(() => { StringUtilities.SPrintF("%s", null); }, Throws.TypeOf<FormatException>());
        }

        [Test]
        public void SPrintF_StringNull()
        {
            // To pass a string of null, we need to construct the parameter list manually.
            Assert.That(StringUtilities.SPrintF("%s", new object[] { null }), Is.EqualTo(""));

            string nulls = null;
            Assert.That(StringUtilities.SPrintF("%s", nulls), Is.EqualTo(""));
        }

        [Test]
        public void SPrintF_Integer()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%d", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%2d", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%.1d", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%-.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%010d", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%-010d", 16384), Is.EqualTo("16384     "));
            Assert.That(StringUtilities.SPrintF("%-+10d", 16384), Is.EqualTo("+16384    "));
            Assert.That(StringUtilities.SPrintF("%+10d", 16384), Is.EqualTo("    +16384"));
            Assert.That(StringUtilities.SPrintF("%04d", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%010.10d", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%012.10d", 16384), Is.EqualTo("  0000016384"));
            Assert.That(StringUtilities.SPrintF("%012.0d", 16384), Is.EqualTo("       16384"));
            Assert.That(StringUtilities.SPrintF("%08.12d", 16384), Is.EqualTo("000000016384"));
            Assert.That(StringUtilities.SPrintF("%010.12d", 16384), Is.EqualTo("000000016384"));
            Assert.That(StringUtilities.SPrintF("% 10.10d", 16384), Is.EqualTo(" 0000016384"));
            Assert.That(StringUtilities.SPrintF("%d", -49152), Is.EqualTo("-49152"));
            Assert.That(StringUtilities.SPrintF("%2d", -49152), Is.EqualTo("-49152"));
            Assert.That(StringUtilities.SPrintF("%.1d", -49152), Is.EqualTo("-49152"));
            Assert.That(StringUtilities.SPrintF("%.10d", -49152), Is.EqualTo("-0000049152"));
            Assert.That(StringUtilities.SPrintF("%05.10d", -49152), Is.EqualTo("-0000049152"));
            Assert.That(StringUtilities.SPrintF("%12.10d", -49152), Is.EqualTo(" -0000049152"));
            Assert.That(StringUtilities.SPrintF("% 12.10d", -49152), Is.EqualTo(" -0000049152"));
            Assert.That(StringUtilities.SPrintF("%-0 15.10d", -49152), Is.EqualTo("-0000049152    "));
            Assert.That(StringUtilities.SPrintF("%-015.10d", -49152), Is.EqualTo("-0000049152    "));
            Assert.That(StringUtilities.SPrintF("%0 15.10d", -49152), Is.EqualTo("    -0000049152"));
            Assert.That(StringUtilities.SPrintF("%015.10d", -49152), Is.EqualTo("    -0000049152"));
            Assert.That(StringUtilities.SPrintF("%-0 15.2d", -49152), Is.EqualTo("-49152         "));
            Assert.That(StringUtilities.SPrintF("%-015.2d", -49152), Is.EqualTo("-49152         "));
            Assert.That(StringUtilities.SPrintF("%0 15.2d", -49152), Is.EqualTo("         -49152"));
            Assert.That(StringUtilities.SPrintF("%015.2d", -49152), Is.EqualTo("         -49152"));
            Assert.That(StringUtilities.SPrintF("%-0 15d", -49152), Is.EqualTo("-49152         "));
            Assert.That(StringUtilities.SPrintF("%-015d", -49152), Is.EqualTo("-49152         "));
            Assert.That(StringUtilities.SPrintF("%0 15d", -49152), Is.EqualTo("-00000000049152"));
            Assert.That(StringUtilities.SPrintF("%015d", -49152), Is.EqualTo("-00000000049152"));
            Assert.That(StringUtilities.SPrintF("% d", -32768), Is.EqualTo("-32768"));
            Assert.That(StringUtilities.SPrintF("%+d", -32768), Is.EqualTo("-32768"));
            Assert.That(StringUtilities.SPrintF("%#d", -32768), Is.EqualTo("-32768"));
            Assert.That(StringUtilities.SPrintF("% d", 65536), Is.EqualTo(" 65536"));
            Assert.That(StringUtilities.SPrintF("%+d", 65536), Is.EqualTo("+65536"));
            Assert.That(StringUtilities.SPrintF("%#d", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("% +d", 65536), Is.EqualTo("+65536"));
            Assert.That(StringUtilities.SPrintF("%+ d", 65536), Is.EqualTo("+65536"));
            Assert.That(StringUtilities.SPrintF("% -d", 65536), Is.EqualTo(" 65536"));
            Assert.That(StringUtilities.SPrintF("%- d", 65536), Is.EqualTo(" 65536"));
            Assert.That(StringUtilities.SPrintF("%- 10d", 65536), Is.EqualTo(" 65536    "));
            Assert.That(StringUtilities.SPrintF("%.0d", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%5.0d", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%05.0d", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%+.0d", 0), Is.EqualTo("+"));
            Assert.That(StringUtilities.SPrintF("%-.0d", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% .0d", 0), Is.EqualTo(" "));
            Assert.That(StringUtilities.SPrintF("%-+.0d", 0), Is.EqualTo("+"));
            Assert.That(StringUtilities.SPrintF("% d", 0), Is.EqualTo(" 0"));
            Assert.That(StringUtilities.SPrintF("%+d", 0), Is.EqualTo("+0"));
            Assert.That(StringUtilities.SPrintF("%#d", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% +d", 0), Is.EqualTo("+0"));
            Assert.That(StringUtilities.SPrintF("%+ d", 0), Is.EqualTo("+0"));
            Assert.That(StringUtilities.SPrintF("% -d", 0), Is.EqualTo(" 0"));
            Assert.That(StringUtilities.SPrintF("%- d", 0), Is.EqualTo(" 0"));
            Assert.That(StringUtilities.SPrintF("%- 10d", 0), Is.EqualTo(" 0        "));
            Assert.That(StringUtilities.SPrintF("%hhi", 0xFFE), Is.EqualTo("-2"));
            Assert.That(StringUtilities.SPrintF("%hhi", 0x3FE), Is.EqualTo("-2"));
            Assert.That(StringUtilities.SPrintF("%hhi", 0xF7F), Is.EqualTo("127"));
            Assert.That(StringUtilities.SPrintF("%hhi", 0x37F), Is.EqualTo("127"));
            Assert.That(StringUtilities.SPrintF("%lld", 0x8000000000000000UL), Is.EqualTo("-9223372036854775808"));
        }

        [Test]
        public void SPrintF_UnsignedInteger()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%u", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%2u", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%.1u", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%010u", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%-010u", 16384), Is.EqualTo("16384     "));
            Assert.That(StringUtilities.SPrintF("%04u", 16384), Is.EqualTo("16384"));
            Assert.That(StringUtilities.SPrintF("%010.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("% 10.10u", 16384), Is.EqualTo("0000016384"));
            Assert.That(StringUtilities.SPrintF("%u", -49152), Is.EqualTo("4294918144"));
            Assert.That(StringUtilities.SPrintF("%2u", -49152), Is.EqualTo("4294918144"));
            Assert.That(StringUtilities.SPrintF("%.1u", -49152), Is.EqualTo("4294918144"));
            Assert.That(StringUtilities.SPrintF("%.10u", -49152), Is.EqualTo("4294918144"));
            Assert.That(StringUtilities.SPrintF("%05.10u", -49152), Is.EqualTo("4294918144"));
            Assert.That(StringUtilities.SPrintF("%12.10u", -49152), Is.EqualTo("  4294918144"));
            Assert.That(StringUtilities.SPrintF("% 12.10u", -49152), Is.EqualTo("  4294918144"));
            Assert.That(StringUtilities.SPrintF("%-0 15.10u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%-015.10u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%0 15.10u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(StringUtilities.SPrintF("%015.10u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(StringUtilities.SPrintF("%-0 15.2u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%-015.2u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%0 15.2u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(StringUtilities.SPrintF("%015.2u", -49152), Is.EqualTo("     4294918144"));
            Assert.That(StringUtilities.SPrintF("%-0 15u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%-015u", -49152), Is.EqualTo("4294918144     "));
            Assert.That(StringUtilities.SPrintF("%0 15u", -49152), Is.EqualTo("000004294918144"));
            Assert.That(StringUtilities.SPrintF("%015u", -49152), Is.EqualTo("000004294918144"));
            Assert.That(StringUtilities.SPrintF("% u", -32768), Is.EqualTo("4294934528"));
            Assert.That(StringUtilities.SPrintF("%+u", -32768), Is.EqualTo("4294934528"));
            Assert.That(StringUtilities.SPrintF("%#u", -32768), Is.EqualTo("4294934528"));
            Assert.That(StringUtilities.SPrintF("% u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("%+u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("%#u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("% +u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("%+ u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("% -u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("%- u", 65536), Is.EqualTo("65536"));
            Assert.That(StringUtilities.SPrintF("%- 10u", 65536), Is.EqualTo("65536     "));
            Assert.That(StringUtilities.SPrintF("%.0u", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%5.0u", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%05.0u", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%+.0u", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-.0u", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% .0u", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-+.0u", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%#u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% +u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+ u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% -u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- u", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- 10u", 0), Is.EqualTo("0         "));
            Assert.That(StringUtilities.SPrintF("%hhu", 0xFFE), Is.EqualTo("254"));
            Assert.That(StringUtilities.SPrintF("%hhu", 0x3FE), Is.EqualTo("254"));
            Assert.That(StringUtilities.SPrintF("%hhu", 0xF7F), Is.EqualTo("127"));
            Assert.That(StringUtilities.SPrintF("%hhu", 0x37F), Is.EqualTo("127"));
            Assert.That(StringUtilities.SPrintF("%llu", 0x8000000000000000UL), Is.EqualTo("9223372036854775808"));
        }

        [Test]
        public void SPrintF_Hexadecimal()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%x", 16384), Is.EqualTo("4000"));
            Assert.That(StringUtilities.SPrintF("%2x", 16384), Is.EqualTo("4000"));
            Assert.That(StringUtilities.SPrintF("%.1x", 16384), Is.EqualTo("4000"));
            Assert.That(StringUtilities.SPrintF("%.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(StringUtilities.SPrintF("%010x", 16384), Is.EqualTo("0000004000"));
            Assert.That(StringUtilities.SPrintF("%-010x", 16384), Is.EqualTo("4000      "));
            Assert.That(StringUtilities.SPrintF("%04x", 16384), Is.EqualTo("4000"));
            Assert.That(StringUtilities.SPrintF("%010.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(StringUtilities.SPrintF("% 10.10x", 16384), Is.EqualTo("0000004000"));
            Assert.That(StringUtilities.SPrintF("%x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(StringUtilities.SPrintF("%2x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(StringUtilities.SPrintF("%.1x", -49152), Is.EqualTo("ffff4000"));
            Assert.That(StringUtilities.SPrintF("%.10x", -49152), Is.EqualTo("00ffff4000"));
            Assert.That(StringUtilities.SPrintF("%05.10x", -49152), Is.EqualTo("00ffff4000"));
            Assert.That(StringUtilities.SPrintF("%12.10x", -49152), Is.EqualTo("  00ffff4000"));
            Assert.That(StringUtilities.SPrintF("% 12.10x", -49152), Is.EqualTo("  00ffff4000"));
            Assert.That(StringUtilities.SPrintF("%-0 15.10x", -49152), Is.EqualTo("00ffff4000     "));
            Assert.That(StringUtilities.SPrintF("%-015.10x", -49152), Is.EqualTo("00ffff4000     "));
            Assert.That(StringUtilities.SPrintF("%0 15.10x", -49152), Is.EqualTo("     00ffff4000"));
            Assert.That(StringUtilities.SPrintF("%015.10x", -49152), Is.EqualTo("     00ffff4000"));
            Assert.That(StringUtilities.SPrintF("%-0 15.2x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(StringUtilities.SPrintF("%-015.2x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(StringUtilities.SPrintF("%0 15.2x", -49152), Is.EqualTo("       ffff4000"));
            Assert.That(StringUtilities.SPrintF("%015.2x", -49152), Is.EqualTo("       ffff4000"));
            Assert.That(StringUtilities.SPrintF("%-0 15x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(StringUtilities.SPrintF("%-015x", -49152), Is.EqualTo("ffff4000       "));
            Assert.That(StringUtilities.SPrintF("%0 15x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(StringUtilities.SPrintF("%015x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(StringUtilities.SPrintF("% x", -32768), Is.EqualTo("ffff8000"));
            Assert.That(StringUtilities.SPrintF("%+x", -32768), Is.EqualTo("ffff8000"));
            Assert.That(StringUtilities.SPrintF("%#x", -32768), Is.EqualTo("0xffff8000"));
            Assert.That(StringUtilities.SPrintF("% x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%+x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%#x", 65536), Is.EqualTo("0x10000"));
            Assert.That(StringUtilities.SPrintF("% +x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%+ x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("% -x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%- x", 65536), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%- 10x", 65536), Is.EqualTo("10000     "));
            Assert.That(StringUtilities.SPrintF("%.0x", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%5.0x", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%05.0x", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%+.0x", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-.0x", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% .0x", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-+.0x", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%#x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% +x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+ x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% -x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- x", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- 10x", 0), Is.EqualTo("0         "));
            Assert.That(StringUtilities.SPrintF("%hhx", 0xFFE), Is.EqualTo("fe"));
            Assert.That(StringUtilities.SPrintF("%hhx", 0x3FE), Is.EqualTo("fe"));
            Assert.That(StringUtilities.SPrintF("%hhx", 0xF7F), Is.EqualTo("7f"));
            Assert.That(StringUtilities.SPrintF("%hhx", 0x37F), Is.EqualTo("7f"));
            Assert.That(StringUtilities.SPrintF("%llx", 0x8000000000000000UL), Is.EqualTo("8000000000000000"));
        }

        [Test]
        public void SPrintF_Octal()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%o", 16384), Is.EqualTo("40000"));
            Assert.That(StringUtilities.SPrintF("%2o", 16384), Is.EqualTo("40000"));
            Assert.That(StringUtilities.SPrintF("%.1o", 16384), Is.EqualTo("40000"));
            Assert.That(StringUtilities.SPrintF("%.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(StringUtilities.SPrintF("%010o", 16384), Is.EqualTo("0000040000"));
            Assert.That(StringUtilities.SPrintF("%-010o", 16384), Is.EqualTo("40000     "));
            Assert.That(StringUtilities.SPrintF("%04o", 16384), Is.EqualTo("40000"));
            Assert.That(StringUtilities.SPrintF("%010.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(StringUtilities.SPrintF("% 10.10o", 16384), Is.EqualTo("0000040000"));
            Assert.That(StringUtilities.SPrintF("%o", -49152), Is.EqualTo("37777640000"));
            Assert.That(StringUtilities.SPrintF("%2o", -49152), Is.EqualTo("37777640000"));
            Assert.That(StringUtilities.SPrintF("%.1o", -49152), Is.EqualTo("37777640000"));
            Assert.That(StringUtilities.SPrintF("%.10o", -49152), Is.EqualTo("37777640000"));
            Assert.That(StringUtilities.SPrintF("%05.10o", -49152), Is.EqualTo("37777640000"));
            Assert.That(StringUtilities.SPrintF("%12.10o", -49152), Is.EqualTo(" 37777640000"));
            Assert.That(StringUtilities.SPrintF("% 12.10o", -49152), Is.EqualTo(" 37777640000"));
            Assert.That(StringUtilities.SPrintF("%-0 15.10o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%-015.10o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%0 15.10o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(StringUtilities.SPrintF("%015.10o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(StringUtilities.SPrintF("%-0 15.2o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%-015.2o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%0 15.2o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(StringUtilities.SPrintF("%015.2o", -49152), Is.EqualTo("    37777640000"));
            Assert.That(StringUtilities.SPrintF("%-0 15o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%-015o", -49152), Is.EqualTo("37777640000    "));
            Assert.That(StringUtilities.SPrintF("%0 15o", -49152), Is.EqualTo("000037777640000"));
            Assert.That(StringUtilities.SPrintF("%015x", -49152), Is.EqualTo("0000000ffff4000"));
            Assert.That(StringUtilities.SPrintF("% o", -32768), Is.EqualTo("37777700000"));
            Assert.That(StringUtilities.SPrintF("%+o", -32768), Is.EqualTo("37777700000"));
            Assert.That(StringUtilities.SPrintF("%#o", -32768), Is.EqualTo("037777700000"));
            Assert.That(StringUtilities.SPrintF("% o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("%+o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("%#o", 65536), Is.EqualTo("0200000"));
            Assert.That(StringUtilities.SPrintF("% +o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("%+ o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("% -o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("%- o", 65536), Is.EqualTo("200000"));
            Assert.That(StringUtilities.SPrintF("%- 10o", 65536), Is.EqualTo("200000    "));
            Assert.That(StringUtilities.SPrintF("%.0o", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%5.0o", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%05.0o", 0), Is.EqualTo("     "));
            Assert.That(StringUtilities.SPrintF("%+.0o", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-.0o", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% .0o", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("%-+.0o", 0), Is.EqualTo(""));
            Assert.That(StringUtilities.SPrintF("% o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%#o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% +o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%+ o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("% -o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- o", 0), Is.EqualTo("0"));
            Assert.That(StringUtilities.SPrintF("%- 10o", 0), Is.EqualTo("0         "));
            Assert.That(StringUtilities.SPrintF("%hho", 0xFFE), Is.EqualTo("376"));
            Assert.That(StringUtilities.SPrintF("%hho", 0x3FE), Is.EqualTo("376"));
            Assert.That(StringUtilities.SPrintF("%hho", 0xF7F), Is.EqualTo("177"));
            Assert.That(StringUtilities.SPrintF("%hho", 0x37F), Is.EqualTo("177"));
            Assert.That(StringUtilities.SPrintF("%llo", 0x8000000000000000UL), Is.EqualTo("1000000000000000000000"));
        }

        [Test]
        public void SPrintF_FixedDouble()
        {
            Assert.That(StringUtilities.SPrintF("%f", 10), Is.EqualTo("10.000000"));
            Assert.That(StringUtilities.SPrintF("%f", 123456.789), Is.EqualTo("123456.789000"));
            Assert.That(StringUtilities.SPrintF("%.2f", 123456.789), Is.EqualTo("123456.79"));
            Assert.That(StringUtilities.SPrintF("%f", 0.00000000010), Is.EqualTo("0.000000"));
            Assert.That(StringUtilities.SPrintF("%f", 100000000000.0), Is.EqualTo("100000000000.000000"));
            Assert.That(StringUtilities.SPrintF("%12f", 10), Is.EqualTo("   10.000000"));
            Assert.That(StringUtilities.SPrintF("%12.0f", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%#12.0f", 10), Is.EqualTo("         10."));
            Assert.That(StringUtilities.SPrintF("%#.0f", 10), Is.EqualTo("10."));
            Assert.That(StringUtilities.SPrintF("%12.1f", 10), Is.EqualTo("        10.0"));
            Assert.That(StringUtilities.SPrintF("%12.8f", 10), Is.EqualTo(" 10.00000000"));
            Assert.That(StringUtilities.SPrintF("%12.10f", 10), Is.EqualTo("10.0000000000"));
            Assert.That(StringUtilities.SPrintF("%13.10f", 10), Is.EqualTo("10.0000000000"));
            Assert.That(StringUtilities.SPrintF("%14.10f", 10), Is.EqualTo(" 10.0000000000"));
            Assert.That(StringUtilities.SPrintF("%f", -10), Is.EqualTo("-10.000000"));
            Assert.That(StringUtilities.SPrintF("%12f", -10), Is.EqualTo("  -10.000000"));
            Assert.That(StringUtilities.SPrintF("%12.0f", -10), Is.EqualTo("         -10"));
            Assert.That(StringUtilities.SPrintF("%12.1f", -10), Is.EqualTo("       -10.0"));
            Assert.That(StringUtilities.SPrintF("%12.10f", -10), Is.EqualTo("-10.0000000000"));
            Assert.That(StringUtilities.SPrintF("% f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("% 2f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("% 8f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("% 9f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("% 10f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("% 12f", 10), Is.EqualTo("   10.000000"));
            Assert.That(StringUtilities.SPrintF("%- f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("%- 2f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("%- 8f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("%- 9f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("%- 10f", 10), Is.EqualTo(" 10.000000"));
            Assert.That(StringUtilities.SPrintF("%- 12f", 10), Is.EqualTo(" 10.000000  "));
            Assert.That(StringUtilities.SPrintF("%+f", 10), Is.EqualTo("+10.000000"));
            Assert.That(StringUtilities.SPrintF("%-f", 10), Is.EqualTo("10.000000"));
            Assert.That(StringUtilities.SPrintF("%#f", 10), Is.EqualTo("10.000000"));
            Assert.That(StringUtilities.SPrintF("%0f", 10), Is.EqualTo("10.000000"));
            Assert.That(StringUtilities.SPrintF("%012f", 10), Is.EqualTo("00010.000000"));
            Assert.That(StringUtilities.SPrintF("%012f", -10), Is.EqualTo("-0010.000000"));
            Assert.That(StringUtilities.SPrintF("%-012f", -10), Is.EqualTo("-10.000000  "));
            Assert.That(StringUtilities.SPrintF("%05f", -10), Is.EqualTo("-10.000000"));
            //Assert.That(StringUtilities.SPrintF("%f", 1e100), Is.EqualTo("10000000000000000159028911097599180468360810000000000000000000000000000000000000000000000000000000000.000000"));
            Assert.That(StringUtilities.SPrintF("%f", 1e100), Is.EqualTo("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000.000000"));
            Assert.That(StringUtilities.SPrintF("%f", 1e-100), Is.EqualTo("0.000000"));
            Assert.That(StringUtilities.SPrintF("%f", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%F", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("%10.2f", Double.NaN), Is.EqualTo("       nan"));
            Assert.That(StringUtilities.SPrintF("%10.2F", Double.NaN), Is.EqualTo("       NAN"));
            Assert.That(StringUtilities.SPrintF("%#f", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%#F", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("% f", Double.NaN), Is.EqualTo(" nan"));
            Assert.That(StringUtilities.SPrintF("% F", Double.NaN), Is.EqualTo(" NAN"));
            Assert.That(StringUtilities.SPrintF("%+f", Double.NaN), Is.EqualTo("+nan"));
            Assert.That(StringUtilities.SPrintF("%+F", Double.NaN), Is.EqualTo("+NAN"));
            Assert.That(StringUtilities.SPrintF("%-10f", Double.NaN), Is.EqualTo("nan       "));
            Assert.That(StringUtilities.SPrintF("%-10F", Double.NaN), Is.EqualTo("NAN       "));
            Assert.That(StringUtilities.SPrintF("%010f", Double.NaN), Is.EqualTo("       nan"));
            Assert.That(StringUtilities.SPrintF("%010F", Double.NaN), Is.EqualTo("       NAN"));
            Assert.That(StringUtilities.SPrintF("%f", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%F", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%10.2f", Double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(StringUtilities.SPrintF("%10.2F", Double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(StringUtilities.SPrintF("% f", Double.PositiveInfinity), Is.EqualTo(" inf"));
            Assert.That(StringUtilities.SPrintF("% F", Double.PositiveInfinity), Is.EqualTo(" INF"));
            Assert.That(StringUtilities.SPrintF("%+f", Double.PositiveInfinity), Is.EqualTo("+inf"));
            Assert.That(StringUtilities.SPrintF("%+F", Double.PositiveInfinity), Is.EqualTo("+INF"));
            Assert.That(StringUtilities.SPrintF("%#f", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%#F", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%-10f", Double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(StringUtilities.SPrintF("%-10F", Double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(StringUtilities.SPrintF("%010f", Double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(StringUtilities.SPrintF("%010F", Double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(StringUtilities.SPrintF("%f", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%F", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%10.2f", Double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(StringUtilities.SPrintF("%10.2F", Double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(StringUtilities.SPrintF("%#f", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%#F", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%-10f", Double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(StringUtilities.SPrintF("%-10F", Double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        [Test]
        public void SPrintF_ExponentDouble()
        {
            // Obtained when testing MSVCRT (GCC and MSVCRT differ in the number of zeroes in the exponent)
            Assert.That(StringUtilities.SPrintF("%e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%12e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%E", 10), Is.EqualTo("1.000000E+01"));
            Assert.That(StringUtilities.SPrintF("%12E", 10), Is.EqualTo("1.000000E+01"));
            Assert.That(StringUtilities.SPrintF("%e", 123456.789), Is.EqualTo("1.234568e+05"));
            Assert.That(StringUtilities.SPrintF("%.2e", 123456.789), Is.EqualTo("1.23e+05"));
            Assert.That(StringUtilities.SPrintF("%e", 0.00000000010), Is.EqualTo("1.000000e-10"));
            Assert.That(StringUtilities.SPrintF("%e", 100000000000.0), Is.EqualTo("1.000000e+11"));
            Assert.That(StringUtilities.SPrintF("%e", 1e101), Is.EqualTo("1.000000e+101"));
            Assert.That(StringUtilities.SPrintF("%e", 1e-101), Is.EqualTo("1.000000e-101"));
            Assert.That(StringUtilities.SPrintF("%e", 1e100), Is.EqualTo("1.000000e+100"));
            Assert.That(StringUtilities.SPrintF("%e", 1e-100), Is.EqualTo("1.000000e-100"));
            Assert.That(StringUtilities.SPrintF("%e", 1e99), Is.EqualTo("1.000000e+99"));
            Assert.That(StringUtilities.SPrintF("%e", 1e-99), Is.EqualTo("1.000000e-99"));
            Assert.That(StringUtilities.SPrintF("%12.0e", 10), Is.EqualTo("       1e+01"));
            Assert.That(StringUtilities.SPrintF("%#12.0e", 10), Is.EqualTo("      1.e+01"));
            Assert.That(StringUtilities.SPrintF("%12.1e", 10), Is.EqualTo("     1.0e+01"));
            Assert.That(StringUtilities.SPrintF("%12.8e", 10), Is.EqualTo("1.00000000e+01"));
            Assert.That(StringUtilities.SPrintF("%12.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(StringUtilities.SPrintF("%13.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(StringUtilities.SPrintF("%14.10e", 10), Is.EqualTo("1.0000000000e+01"));
            Assert.That(StringUtilities.SPrintF("%e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%12e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%12.0e", -10), Is.EqualTo("      -1e+01"));
            Assert.That(StringUtilities.SPrintF("%12.1e", -10), Is.EqualTo("    -1.0e+01"));
            Assert.That(StringUtilities.SPrintF("%12.10e", -10), Is.EqualTo("-1.0000000000e+01"));
            Assert.That(StringUtilities.SPrintF("% e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("% 2E", 10), Is.EqualTo(" 1.000000E+01"));
            Assert.That(StringUtilities.SPrintF("% 8E", 10), Is.EqualTo(" 1.000000E+01"));
            Assert.That(StringUtilities.SPrintF("% 9e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("% 10e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("% 12e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- 2e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- 8e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- 9e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- 10e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%- 12e", 10), Is.EqualTo(" 1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%+e", 10), Is.EqualTo("+1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%-e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%#e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%0e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%012e", 10), Is.EqualTo("1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%012e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%-012e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%015e", 10), Is.EqualTo("0001.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%015e", -10), Is.EqualTo("-001.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%-015e", -10), Is.EqualTo("-1.000000e+01  "));
            Assert.That(StringUtilities.SPrintF("%05e", -10), Is.EqualTo("-1.000000e+01"));
            Assert.That(StringUtilities.SPrintF("%e", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%E", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("%10.2e", Double.NaN), Is.EqualTo("       nan"));
            Assert.That(StringUtilities.SPrintF("%10.2E", Double.NaN), Is.EqualTo("       NAN"));
            Assert.That(StringUtilities.SPrintF("%#e", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%#E", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("%-10e", Double.NaN), Is.EqualTo("nan       "));
            Assert.That(StringUtilities.SPrintF("%-10E", Double.NaN), Is.EqualTo("NAN       "));
            Assert.That(StringUtilities.SPrintF("%e", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%E", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%10.2e", Double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(StringUtilities.SPrintF("%10.2E", Double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(StringUtilities.SPrintF("%#e", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%#E", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%-10e", Double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(StringUtilities.SPrintF("%-10E", Double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(StringUtilities.SPrintF("%e", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%E", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%10.2e", Double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(StringUtilities.SPrintF("%10.2E", Double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(StringUtilities.SPrintF("%#e", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%#E", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%-10e", Double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(StringUtilities.SPrintF("%-10E", Double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        [Test]
        public void SPrintF_GeneralDouble()
        {
            // Obtained when testing Cygwin x86 GCC 4.8.3
            Assert.That(StringUtilities.SPrintF("%g", 0.000001), Is.EqualTo("1e-06"));
            Assert.That(StringUtilities.SPrintF("%g", 0.00001), Is.EqualTo("1e-05"));
            Assert.That(StringUtilities.SPrintF("%g", 0.0001), Is.EqualTo("0.0001"));
            Assert.That(StringUtilities.SPrintF("%g", 0.001), Is.EqualTo("0.001"));
            Assert.That(StringUtilities.SPrintF("%g", 0.01), Is.EqualTo("0.01"));
            Assert.That(StringUtilities.SPrintF("%g", 0.1), Is.EqualTo("0.1"));
            Assert.That(StringUtilities.SPrintF("%g", 1), Is.EqualTo("1"));
            Assert.That(StringUtilities.SPrintF("%g", 10), Is.EqualTo("10"));
            Assert.That(StringUtilities.SPrintF("%g", 100), Is.EqualTo("100"));
            Assert.That(StringUtilities.SPrintF("%g", 1000), Is.EqualTo("1000"));
            Assert.That(StringUtilities.SPrintF("%g", 10000), Is.EqualTo("10000"));
            Assert.That(StringUtilities.SPrintF("%g", 100000), Is.EqualTo("100000"));
            Assert.That(StringUtilities.SPrintF("%g", 1000000), Is.EqualTo("1e+06"));
            Assert.That(StringUtilities.SPrintF("%g", 10000000), Is.EqualTo("1e+07"));
            Assert.That(StringUtilities.SPrintF("%g", 0.00000000010), Is.EqualTo("1e-10"));
            Assert.That(StringUtilities.SPrintF("%g", 100000000000.0), Is.EqualTo("1e+11"));
            Assert.That(StringUtilities.SPrintF("%.0g", 10), Is.EqualTo("1e+01"));
            Assert.That(StringUtilities.SPrintF("%.0G", 10), Is.EqualTo("1E+01"));
            Assert.That(StringUtilities.SPrintF("%g", 31.41), Is.EqualTo("31.41"));
            Assert.That(StringUtilities.SPrintF("%.0g", 31.41), Is.EqualTo("3e+01"));
            Assert.That(StringUtilities.SPrintF("%.0G", 31.41), Is.EqualTo("3E+01"));
            Assert.That(StringUtilities.SPrintF("%.2g", 31.41), Is.EqualTo("31"));
            Assert.That(StringUtilities.SPrintF("%.2G", 31.41), Is.EqualTo("31"));
            Assert.That(StringUtilities.SPrintF("%.3g", 31.4159), Is.EqualTo("31.4"));
            Assert.That(StringUtilities.SPrintF("%.3G", 31.4159), Is.EqualTo("31.4"));
            Assert.That(StringUtilities.SPrintF("%.3g", 3.14159), Is.EqualTo("3.14"));
            Assert.That(StringUtilities.SPrintF("%.3G", 3.14159), Is.EqualTo("3.14"));
            Assert.That(StringUtilities.SPrintF("%g", 123456.789), Is.EqualTo("123457"));
            Assert.That(StringUtilities.SPrintF("%.2g", 123456.789), Is.EqualTo("1.2e+05"));
            Assert.That(StringUtilities.SPrintF("%g", 1e101), Is.EqualTo("1e+101"));
            Assert.That(StringUtilities.SPrintF("%g", 1e-101), Is.EqualTo("1e-101"));
            Assert.That(StringUtilities.SPrintF("%g", 1e100), Is.EqualTo("1e+100"));
            Assert.That(StringUtilities.SPrintF("%g", 1e-100), Is.EqualTo("1e-100"));
            Assert.That(StringUtilities.SPrintF("%g", 1e99), Is.EqualTo("1e+99"));
            Assert.That(StringUtilities.SPrintF("%g", 1e-99), Is.EqualTo("1e-99"));
            Assert.That(StringUtilities.SPrintF("%12g", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%12.0g", 10), Is.EqualTo("       1e+01"));
            Assert.That(StringUtilities.SPrintF("%G", 10), Is.EqualTo("10"));
            Assert.That(StringUtilities.SPrintF("%12G", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%12.0G", 10), Is.EqualTo("       1E+01"));
            Assert.That(StringUtilities.SPrintF("%#12.0g", 10), Is.EqualTo("      1.e+01"));
            Assert.That(StringUtilities.SPrintF("%12.1g", 10), Is.EqualTo("       1e+01"));
            Assert.That(StringUtilities.SPrintF("%12.8g", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%12.10g", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%13.10g", 10), Is.EqualTo("           10"));
            Assert.That(StringUtilities.SPrintF("%14.10g", 10), Is.EqualTo("            10"));
            Assert.That(StringUtilities.SPrintF("%g", -10), Is.EqualTo("-10"));
            Assert.That(StringUtilities.SPrintF("%12g", -10), Is.EqualTo("         -10"));
            Assert.That(StringUtilities.SPrintF("%12.0g", -10), Is.EqualTo("      -1e+01"));
            Assert.That(StringUtilities.SPrintF("%12.1g", -10), Is.EqualTo("      -1e+01"));
            Assert.That(StringUtilities.SPrintF("%12.10g", -10), Is.EqualTo("         -10"));
            Assert.That(StringUtilities.SPrintF("% g", 10), Is.EqualTo(" 10"));
            Assert.That(StringUtilities.SPrintF("% 2G", 10), Is.EqualTo(" 10"));
            Assert.That(StringUtilities.SPrintF("% 8G", 10), Is.EqualTo("      10"));
            Assert.That(StringUtilities.SPrintF("% 9g", 10), Is.EqualTo("       10"));
            Assert.That(StringUtilities.SPrintF("% 10g", 10), Is.EqualTo("        10"));
            Assert.That(StringUtilities.SPrintF("% 12g", 10), Is.EqualTo("          10"));
            Assert.That(StringUtilities.SPrintF("%- g", 10), Is.EqualTo(" 10"));
            Assert.That(StringUtilities.SPrintF("%- 2g", 10), Is.EqualTo(" 10"));
            Assert.That(StringUtilities.SPrintF("%- 8g", 10), Is.EqualTo(" 10     "));
            Assert.That(StringUtilities.SPrintF("%- 9g", 10), Is.EqualTo(" 10      "));
            Assert.That(StringUtilities.SPrintF("%- 10g", 10), Is.EqualTo(" 10       "));
            Assert.That(StringUtilities.SPrintF("%- 12g", 10), Is.EqualTo(" 10         "));
            Assert.That(StringUtilities.SPrintF("%+g", 10), Is.EqualTo("+10"));
            Assert.That(StringUtilities.SPrintF("%-g", 10), Is.EqualTo("10"));
            Assert.That(StringUtilities.SPrintF("%#g", 10), Is.EqualTo("10.0000"));
            Assert.That(StringUtilities.SPrintF("%#g", 1000.0), Is.EqualTo("1000.00"));
            Assert.That(StringUtilities.SPrintF("%#g", 10000.0), Is.EqualTo("10000.0"));
            Assert.That(StringUtilities.SPrintF("%#g", 100000.0), Is.EqualTo("100000."));
            Assert.That(StringUtilities.SPrintF("%#g", 1000000.0), Is.EqualTo("1.00000e+06"));
            Assert.That(StringUtilities.SPrintF("%#g", 12000000.0), Is.EqualTo("1.20000e+07"));
            Assert.That(StringUtilities.SPrintF("%0g", 10), Is.EqualTo("10"));
            Assert.That(StringUtilities.SPrintF("%012g", 10), Is.EqualTo("000000000010"));
            Assert.That(StringUtilities.SPrintF("%012g", -10), Is.EqualTo("-00000000010"));
            Assert.That(StringUtilities.SPrintF("%-012g", -10), Is.EqualTo("-10         "));
            Assert.That(StringUtilities.SPrintF("%05g", -10), Is.EqualTo("-0010"));
            Assert.That(StringUtilities.SPrintF("%g", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%G", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("%10.2g", Double.NaN), Is.EqualTo("       nan"));
            Assert.That(StringUtilities.SPrintF("%10.2G", Double.NaN), Is.EqualTo("       NAN"));
            Assert.That(StringUtilities.SPrintF("%#g", Double.NaN), Is.EqualTo("nan"));
            Assert.That(StringUtilities.SPrintF("%#G", Double.NaN), Is.EqualTo("NAN"));
            Assert.That(StringUtilities.SPrintF("%-10g", Double.NaN), Is.EqualTo("nan       "));
            Assert.That(StringUtilities.SPrintF("%-10G", Double.NaN), Is.EqualTo("NAN       "));
            Assert.That(StringUtilities.SPrintF("%g", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%G", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%10.2g", Double.PositiveInfinity), Is.EqualTo("       inf"));
            Assert.That(StringUtilities.SPrintF("%10.2G", Double.PositiveInfinity), Is.EqualTo("       INF"));
            Assert.That(StringUtilities.SPrintF("%#g", Double.PositiveInfinity), Is.EqualTo("inf"));
            Assert.That(StringUtilities.SPrintF("%#G", Double.PositiveInfinity), Is.EqualTo("INF"));
            Assert.That(StringUtilities.SPrintF("%-10g", Double.PositiveInfinity), Is.EqualTo("inf       "));
            Assert.That(StringUtilities.SPrintF("%-10G", Double.PositiveInfinity), Is.EqualTo("INF       "));
            Assert.That(StringUtilities.SPrintF("%g", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%G", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%10.2g", Double.NegativeInfinity), Is.EqualTo("      -inf"));
            Assert.That(StringUtilities.SPrintF("%10.2G", Double.NegativeInfinity), Is.EqualTo("      -INF"));
            Assert.That(StringUtilities.SPrintF("%#g", Double.NegativeInfinity), Is.EqualTo("-inf"));
            Assert.That(StringUtilities.SPrintF("%#G", Double.NegativeInfinity), Is.EqualTo("-INF"));
            Assert.That(StringUtilities.SPrintF("%-10g", Double.NegativeInfinity), Is.EqualTo("-inf      "));
            Assert.That(StringUtilities.SPrintF("%-10G", Double.NegativeInfinity), Is.EqualTo("-INF      "));
        }

        private static int CountDigits(PrivateType pt, long value)
        {
            return (int)pt.InvokeStatic("CountDigits", new object[] { value });
        }

        // Used to test the count routine temporarily, but it needs to be made public
        [Test]
        public void SPrintF_CountDigitsLong()
        {
            PrivateType pt = new PrivateType(typeof(StringUtilities));

            Assert.That(CountDigits(pt, -1), Is.EqualTo(1));
            Assert.That(CountDigits(pt, 0), Is.EqualTo(1));
            Assert.That(CountDigits(pt, 1), Is.EqualTo(1));

            long value = 10;
            int digits = 2;
            while (digits <= 19) {
                Assert.That(CountDigits(pt, value - 1), Is.EqualTo(digits - 1));
                Assert.That(CountDigits(pt, value), Is.EqualTo(digits));
                Assert.That(CountDigits(pt, value + 1), Is.EqualTo(digits));
                digits++;
                value *= 10;
            }

            value = -10;
            digits = 2;
            while (digits <= 19) {
                Assert.That(CountDigits(pt, value - 1), Is.EqualTo(digits));
                Assert.That(CountDigits(pt, value), Is.EqualTo(digits));
                Assert.That(CountDigits(pt, value + 1), Is.EqualTo(digits - 1));
                digits++;
                value *= 10;
            }
        }

        private static int CountBitDigits(PrivateType pt, ulong value, int bitsPerDigit)
        {
            return (int)pt.InvokeStatic("CountBitDigits", new object[] { value, bitsPerDigit });
        }

        [Test]
        public void SPrintF_CountBitDigitsHex()
        {
            PrivateType pt = new PrivateType(typeof(StringUtilities));

            Assert.That(CountBitDigits(pt, 0, 4), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 16) {
                Assert.That(CountBitDigits(pt, value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                Assert.That(CountBitDigits(pt, value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                Assert.That(CountBitDigits(pt, value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                Assert.That(CountBitDigits(pt, value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                digits++;
            }
        }

        [Test]
        public void SPrintF_CountBitDigitsOctal()
        {
            PrivateType pt = new PrivateType(typeof(StringUtilities));

            Assert.That(CountBitDigits(pt, 0, 3), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 22) {
                Assert.That(CountBitDigits(pt, value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                Assert.That(CountBitDigits(pt, value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                Assert.That(CountBitDigits(pt, value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value = value << 1;
                digits++;
            }
        }
    }
}
