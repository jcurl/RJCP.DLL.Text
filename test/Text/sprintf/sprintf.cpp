#include "testcasegen.hpp"
#include "testcaseblock.hpp"

#include <iostream>
#include <limits>
#include <string>

auto TestChar(TestCaseGen &gen) -> void;
auto TestString(TestCaseGen &gen) -> void;
auto TestInteger(TestCaseGen &gen) -> void;
auto TestUnsignedInteger(TestCaseGen &gen) -> void;
auto TestHexadecimal(TestCaseGen &gen) -> void;
auto TestOctal(TestCaseGen &gen) -> void;
auto TestFixedDouble(TestCaseGen &gen) -> void;
auto TestExponentDouble(TestCaseGen &gen) -> void;
auto TestGeneralDouble(TestCaseGen &gen) -> void;
auto TestDoubleBinary(TestCaseGen &gen) -> void;
auto TestFloatBinary(TestCaseGen &gen) -> void;

auto main(void) -> int
{
    TestCaseGen testGen{};
    TestChar(testGen);
    std::cout << std::endl;

    TestString(testGen);
    std::cout << std::endl;

    TestInteger(testGen);
    std::cout << std::endl;

    TestUnsignedInteger(testGen);
    std::cout << std::endl;

    TestHexadecimal(testGen);
    std::cout << std::endl;

    TestOctal(testGen);
    std::cout << std::endl;

    TestFixedDouble(testGen);
    std::cout << std::endl;

    TestExponentDouble(testGen);
    std::cout << std::endl;

    TestGeneralDouble(testGen);
    std::cout << std::endl;

    TestDoubleBinary(testGen);
    std::cout << std::endl;

    TestFloatBinary(testGen);
    std::cout << std::endl;
}

auto TestChar(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("Char");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("65", "%c", 65);
    gen.TestSPrintF("'a'", "%c", 'a');
    gen.TestSPrintF("'a'", "%-2c", 'a');
    gen.TestSPrintF("'a'", "%2c", 'a');
    gen.TestSPrintF("'a'", "%+2c", 'a');
    gen.TestSPrintF("'a'", "% 2c", 'a');
    gen.TestSPrintF("'a'", "%#2c", 'a');
    gen.TestSPrintF("'a'", "%02c", 'a');
    gen.TestSPrintF("'a'", "%-2c", 'a');
    gen.TestSPrintF("'a'", "%+c", 'a');
    gen.TestSPrintF("'a'", "% c", 'a');
    gen.TestSPrintF("'a'", "%#c", 'a');
    gen.TestSPrintF("'a'", "%0c", 'a');
    gen.TestSPrintF("'a'", "%-c", 'a');
    gen.TestSPrintF("'a'", "%05c", 'a');
}

auto TestString(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("String");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("\"foo\"", "%s", "foo");
    gen.TestSPrintF("\"foo\"", "%-10s", "foo");
    gen.TestSPrintF("\"foo\"", "%10s", "foo");
    gen.TestSPrintF("\"foo\"", "%010s", "foo");
    gen.TestSPrintF("\"foo\"", "%+10s", "foo");
    gen.TestSPrintF("\"foo\"", "% 10s", "foo");
    gen.TestSPrintF("\"foo\"", "%#10s", "foo");
    gen.TestSPrintF("\"foobar\"", "%2s", "foobar");
    gen.TestSPrintF("\"foobar\"", "%-2s", "foobar");
}

auto TestInteger(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("Integer");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("16384", "%d", 16384);
    gen.TestSPrintF("16384", "%2d", 16384);
    gen.TestSPrintF("16384", "%.1d", 16384);
    gen.TestSPrintF("16384", "%.10d", 16384);
    gen.TestSPrintF("16384", "%-.10d", 16384);
    gen.TestSPrintF("16384", "%010d", 16384);
    gen.TestSPrintF("16384", "%-010d", 16384);
    gen.TestSPrintF("16384", "%-+10d", 16384);
    gen.TestSPrintF("16384", "%+10d", 16384);
    gen.TestSPrintF("16384", "%04d", 16384);
    gen.TestSPrintF("16384", "%010.10d", 16384);
    gen.TestSPrintF("16384", "%012.10d", 16384);
    gen.TestSPrintF("16384", "%012.0d", 16384);
    gen.TestSPrintF("16384", "%08.12d", 16384);
    gen.TestSPrintF("16384", "%010.12d", 16384);
    gen.TestSPrintF("16384", "% 10.10d", 16384);
    gen.TestSPrintF("-49152", "%d", -49152);
    gen.TestSPrintF("-49152", "%2d", -49152);
    gen.TestSPrintF("-49152", "%.1d", -49152);
    gen.TestSPrintF("-49152", "%.10d", -49152);
    gen.TestSPrintF("-49152", "%05.10d", -49152);
    gen.TestSPrintF("-49152", "%12.10d", -49152);
    gen.TestSPrintF("-49152", "% 12.10d", -49152);
    gen.TestSPrintF("-49152", "%-0 15.10d", -49152);
    gen.TestSPrintF("-49152", "%-015.10d", -49152);
    gen.TestSPrintF("-49152", "%0 15.10d", -49152);
    gen.TestSPrintF("-49152", "%015.10d", -49152);
    gen.TestSPrintF("-49152", "%-0 15.2d", -49152);
    gen.TestSPrintF("-49152", "%-015.2d", -49152);
    gen.TestSPrintF("-49152", "%0 15.2d", -49152);
    gen.TestSPrintF("-49152", "%015.2d", -49152);
    gen.TestSPrintF("-49152", "%-0 15d", -49152);
    gen.TestSPrintF("-49152", "%-015d", -49152);
    gen.TestSPrintF("-49152", "%0 15d", -49152);
    gen.TestSPrintF("-49152", "%015d", -49152);
    gen.TestSPrintF("-32768", "% d", -32768);
    gen.TestSPrintF("-32768", "%+d", -32768);
    gen.TestSPrintF("-32768", "%#d", -32768);
    gen.TestSPrintF("65536", "% d", 65536);
    gen.TestSPrintF("65536", "%+d", 65536);
    gen.TestSPrintF("65536", "%#d", 65536);
    gen.TestSPrintF("65536", "% +d", 65536);
    gen.TestSPrintF("65536", "%+ d", 65536);
    gen.TestSPrintF("65536", "% -d", 65536);
    gen.TestSPrintF("65536", "%- d", 65536);
    gen.TestSPrintF("65536", "%- 10d", 65536);
    gen.TestSPrintF("0", "%.0d", 0);
    gen.TestSPrintF("0", "%5.0d", 0);
    gen.TestSPrintF("0", "%05.0d", 0);
    gen.TestSPrintF("0", "%+.0d", 0);
    gen.TestSPrintF("0", "%-.0d", 0);
    gen.TestSPrintF("0", "% .0d", 0);
    gen.TestSPrintF("0", "%-+.0d", 0);
    gen.TestSPrintF("0", "% d", 0);
    gen.TestSPrintF("0", "%+d", 0);
    gen.TestSPrintF("0", "%#d", 0);
    gen.TestSPrintF("0", "% +d", 0);
    gen.TestSPrintF("0", "%+ d", 0);
    gen.TestSPrintF("0", "% -d", 0);
    gen.TestSPrintF("0", "%- d", 0);
    gen.TestSPrintF("0", "%- 10d", 0);
    gen.TestSPrintF("0xFFE", "%hhi", 0xFFE);
    gen.TestSPrintF("0x3FE", "%hhi", 0x3FE);
    gen.TestSPrintF("0xF7F", "%hhi", 0xF7F);
    gen.TestSPrintF("0x37F", "%hhi", 0x37F);
    gen.TestSPrintF("0x8000000000000000UL", "%lld", 0x8000000000000000ULL);
}

auto TestUnsignedInteger(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("UnsignedInteger");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("16384", "%u", 16384);
    gen.TestSPrintF("16384", "%2u", 16384);
    gen.TestSPrintF("16384", "%.1u", 16384);
    gen.TestSPrintF("16384", "%.10u", 16384);
    gen.TestSPrintF("16384", "%010u", 16384);
    gen.TestSPrintF("16384", "%-010u", 16384);
    gen.TestSPrintF("16384", "%04u", 16384);
    gen.TestSPrintF("16384", "%010.10u", 16384);
    gen.TestSPrintF("16384", "% 10.10u", 16384);
    gen.TestSPrintF("-49152", "%u", -49152);
    gen.TestSPrintF("-49152", "%2u", -49152);
    gen.TestSPrintF("-49152", "%.1u", -49152);
    gen.TestSPrintF("-49152", "%.10u", -49152);
    gen.TestSPrintF("-49152", "%05.10u", -49152);
    gen.TestSPrintF("-49152", "%12.10u", -49152);
    gen.TestSPrintF("-49152", "% 12.10u", -49152);
    gen.TestSPrintF("-49152", "%-0 15.10u", -49152);
    gen.TestSPrintF("-49152", "%-015.10u", -49152);
    gen.TestSPrintF("-49152", "%0 15.10u", -49152);
    gen.TestSPrintF("-49152", "%015.10u", -49152);
    gen.TestSPrintF("-49152", "%-0 15.2u", -49152);
    gen.TestSPrintF("-49152", "%-015.2u", -49152);
    gen.TestSPrintF("-49152", "%0 15.2u", -49152);
    gen.TestSPrintF("-49152", "%015.2u", -49152);
    gen.TestSPrintF("-49152", "%-0 15u", -49152);
    gen.TestSPrintF("-49152", "%-015u", -49152);
    gen.TestSPrintF("-49152", "%0 15u", -49152);
    gen.TestSPrintF("-49152", "%015u", -49152);
    gen.TestSPrintF("-32768", "% u", -32768);
    gen.TestSPrintF("-32768", "%+u", -32768);
    gen.TestSPrintF("-32768", "%#u", -32768);
    gen.TestSPrintF("65536", "% u", 65536);
    gen.TestSPrintF("65536", "%+u", 65536);
    gen.TestSPrintF("65536", "%#u", 65536);
    gen.TestSPrintF("65536", "% +u", 65536);
    gen.TestSPrintF("65536", "%+ u", 65536);
    gen.TestSPrintF("65536", "% -u", 65536);
    gen.TestSPrintF("65536", "%- u", 65536);
    gen.TestSPrintF("65536", "%- 10u", 65536);
    gen.TestSPrintF("0", "%.0u", 0);
    gen.TestSPrintF("0", "%5.0u", 0);
    gen.TestSPrintF("0", "%05.0u", 0);
    gen.TestSPrintF("0", "%+.0u", 0);
    gen.TestSPrintF("0", "%-.0u", 0);
    gen.TestSPrintF("0", "% .0u", 0);
    gen.TestSPrintF("0", "%-+.0u", 0);
    gen.TestSPrintF("0", "% u", 0);
    gen.TestSPrintF("0", "%+u", 0);
    gen.TestSPrintF("0", "%#u", 0);
    gen.TestSPrintF("0", "% +u", 0);
    gen.TestSPrintF("0", "%+ u", 0);
    gen.TestSPrintF("0", "% -u", 0);
    gen.TestSPrintF("0", "%- u", 0);
    gen.TestSPrintF("0", "%- 10u", 0);
    gen.TestSPrintF("0xFFE", "%hhu", 0xFFE);
    gen.TestSPrintF("0x3FE", "%hhu", 0x3FE);
    gen.TestSPrintF("0xF7F", "%hhu", 0xF7F);
    gen.TestSPrintF("0x37F", "%hhu", 0x37F);
    gen.TestSPrintF("0x8000000000000000UL", "%llu", 0x8000000000000000ULL);
}

auto TestHexadecimal(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("Hexadecimal");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("16384", "%x", 16384);
    gen.TestSPrintF("16384", "%2x", 16384);
    gen.TestSPrintF("16384", "%.1x", 16384);
    gen.TestSPrintF("16384", "%.10x", 16384);
    gen.TestSPrintF("16384", "%010x", 16384);
    gen.TestSPrintF("16384", "%-010x", 16384);
    gen.TestSPrintF("16384", "%04x", 16384);
    gen.TestSPrintF("16384", "%010.10x", 16384);
    gen.TestSPrintF("16384", "% 10.10x", 16384);
    gen.TestSPrintF("-49152", "%x", -49152);
    gen.TestSPrintF("-49152", "%2x", -49152);
    gen.TestSPrintF("-49152", "%.1x", -49152);
    gen.TestSPrintF("-49152", "%.10x", -49152);
    gen.TestSPrintF("-49152", "%05.10x", -49152);
    gen.TestSPrintF("-49152", "%12.10x", -49152);
    gen.TestSPrintF("-49152", "% 12.10x", -49152);
    gen.TestSPrintF("-49152", "%-0 15.10x", -49152);
    gen.TestSPrintF("-49152", "%-015.10x", -49152);
    gen.TestSPrintF("-49152", "%0 15.10x", -49152);
    gen.TestSPrintF("-49152", "%015.10x", -49152);
    gen.TestSPrintF("-49152", "%-0 15.2x", -49152);
    gen.TestSPrintF("-49152", "%-015.2x", -49152);
    gen.TestSPrintF("-49152", "%0 15.2x", -49152);
    gen.TestSPrintF("-49152", "%015.2x", -49152);
    gen.TestSPrintF("-49152", "%-0 15x", -49152);
    gen.TestSPrintF("-49152", "%-015x", -49152);
    gen.TestSPrintF("-49152", "%0 15x", -49152);
    gen.TestSPrintF("-49152", "%015x", -49152);
    gen.TestSPrintF("-32768", "% x", -32768);
    gen.TestSPrintF("-32768", "%+x", -32768);
    gen.TestSPrintF("-32768", "%#x", -32768);
    gen.TestSPrintF("65536", "% x", 65536);
    gen.TestSPrintF("65536", "%+x", 65536);
    gen.TestSPrintF("65536", "%#x", 65536);
    gen.TestSPrintF("65536", "% +x", 65536);
    gen.TestSPrintF("65536", "%+ x", 65536);
    gen.TestSPrintF("65536", "% -x", 65536);
    gen.TestSPrintF("65536", "%- x", 65536);
    gen.TestSPrintF("65536", "%- 10x", 65536);
    gen.TestSPrintF("0", "%.0x", 0);
    gen.TestSPrintF("0", "%5.0x", 0);
    gen.TestSPrintF("0", "%05.0x", 0);
    gen.TestSPrintF("0", "%+.0x", 0);
    gen.TestSPrintF("0", "%-.0x", 0);
    gen.TestSPrintF("0", "% .0x", 0);
    gen.TestSPrintF("0", "%-+.0x", 0);
    gen.TestSPrintF("0", "% x", 0);
    gen.TestSPrintF("0", "%+x", 0);
    gen.TestSPrintF("0", "%#x", 0);
    gen.TestSPrintF("0", "% +x", 0);
    gen.TestSPrintF("0", "%+ x", 0);
    gen.TestSPrintF("0", "% -x", 0);
    gen.TestSPrintF("0", "%- x", 0);
    gen.TestSPrintF("0", "%- 10x", 0);
    gen.TestSPrintF("0xFFE", "%hhx", 0xFFE);
    gen.TestSPrintF("0x3FE", "%hhx", 0x3FE);
    gen.TestSPrintF("0xF7F", "%hhx", 0xF7F);
    gen.TestSPrintF("0x37F", "%hhx", 0x37F);
    gen.TestSPrintF("0x8000000000000000UL", "%llx", 0x8000000000000000ULL);
}

auto TestOctal(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("Octal");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("16384", "%o", 16384);
    gen.TestSPrintF("16384", "%2o", 16384);
    gen.TestSPrintF("16384", "%.1o", 16384);
    gen.TestSPrintF("16384", "%.10o", 16384);
    gen.TestSPrintF("16384", "%010o", 16384);
    gen.TestSPrintF("16384", "%-010o", 16384);
    gen.TestSPrintF("16384", "%04o", 16384);
    gen.TestSPrintF("16384", "%010.10o", 16384);
    gen.TestSPrintF("16384", "% 10.10o", 16384);
    gen.TestSPrintF("-49152", "%o", -49152);
    gen.TestSPrintF("-49152", "%2o", -49152);
    gen.TestSPrintF("-49152", "%.1o", -49152);
    gen.TestSPrintF("-49152", "%.10o", -49152);
    gen.TestSPrintF("-49152", "%05.10o", -49152);
    gen.TestSPrintF("-49152", "%12.10o", -49152);
    gen.TestSPrintF("-49152", "% 12.10o", -49152);
    gen.TestSPrintF("-49152", "%-0 15.10o", -49152);
    gen.TestSPrintF("-49152", "%-015.10o", -49152);
    gen.TestSPrintF("-49152", "%0 15.10o", -49152);
    gen.TestSPrintF("-49152", "%015.10o", -49152);
    gen.TestSPrintF("-49152", "%-0 15.2o", -49152);
    gen.TestSPrintF("-49152", "%-015.2o", -49152);
    gen.TestSPrintF("-49152", "%0 15.2o", -49152);
    gen.TestSPrintF("-49152", "%015.2o", -49152);
    gen.TestSPrintF("-49152", "%-0 15o", -49152);
    gen.TestSPrintF("-49152", "%-015o", -49152);
    gen.TestSPrintF("-49152", "%0 15o", -49152);
    gen.TestSPrintF("-49152", "%015x", -49152);
    gen.TestSPrintF("-32768", "% o", -32768);
    gen.TestSPrintF("-32768", "%+o", -32768);
    gen.TestSPrintF("-32768", "%#o", -32768);
    gen.TestSPrintF("65536", "% o", 65536);
    gen.TestSPrintF("65536", "%+o", 65536);
    gen.TestSPrintF("65536", "%#o", 65536);
    gen.TestSPrintF("65536", "% +o", 65536);
    gen.TestSPrintF("65536", "%+ o", 65536);
    gen.TestSPrintF("65536", "% -o", 65536);
    gen.TestSPrintF("65536", "%- o", 65536);
    gen.TestSPrintF("65536", "%- 10o", 65536);
    gen.TestSPrintF("0", "%.0o", 0);
    gen.TestSPrintF("0", "%5.0o", 0);
    gen.TestSPrintF("0", "%05.0o", 0);
    gen.TestSPrintF("0", "%+.0o", 0);
    gen.TestSPrintF("0", "%-.0o", 0);
    gen.TestSPrintF("0", "% .0o", 0);
    gen.TestSPrintF("0", "%-+.0o", 0);
    gen.TestSPrintF("0", "% o", 0);
    gen.TestSPrintF("0", "%+o", 0);
    gen.TestSPrintF("0", "%#o", 0);
    gen.TestSPrintF("0", "% +o", 0);
    gen.TestSPrintF("0", "%+ o", 0);
    gen.TestSPrintF("0", "% -o", 0);
    gen.TestSPrintF("0", "%- o", 0);
    gen.TestSPrintF("0", "%- 10o", 0);
    gen.TestSPrintF("0xFFE", "%hho", 0xFFE);
    gen.TestSPrintF("0x3FE", "%hho", 0x3FE);
    gen.TestSPrintF("0xF7F", "%hho", 0xF7F);
    gen.TestSPrintF("0x37F", "%hho", 0x37F);
    gen.TestSPrintF("0x8000000000000000UL", "%llo", 0x8000000000000000ULL);
}

auto TestFixedDouble(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("FixedDouble");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("10", "%f", 10.0);
    gen.TestSPrintF("123456.789", "%f", 123456.789);
    gen.TestSPrintF("123456.789", "%.2f", 123456.789);
    gen.TestSPrintF("0.00000000010", "%f", 0.00000000010);
    gen.TestSPrintF("100000000000.0", "%f", 100000000000.0);
    gen.TestSPrintF("10", "%12f", 10.0);
    gen.TestSPrintF("10", "%12.0f", 10.0);
    gen.TestSPrintF("10", "%#12.0f", 10.0);
    gen.TestSPrintF("10", "%#.0f", 10.0);
    gen.TestSPrintF("10", "%12.1f", 10.0);
    gen.TestSPrintF("10", "%12.8f", 10.0);
    gen.TestSPrintF("10", "%12.10f", 10.0);
    gen.TestSPrintF("10", "%13.10f", 10.0);
    gen.TestSPrintF("10", "%14.10f", 10.0);
    gen.TestSPrintF("-10", "%f", -10.0);
    gen.TestSPrintF("-10", "%12f", -10.0);
    gen.TestSPrintF("-10", "%12.0f", -10.0);
    gen.TestSPrintF("-10", "%12.1f", -10.0);
    gen.TestSPrintF("-10", "%12.10f", -10.0);
    gen.TestSPrintF("10", "% f", 10.0);
    gen.TestSPrintF("10", "% 2f", 10.0);
    gen.TestSPrintF("10", "% 8f", 10.0);
    gen.TestSPrintF("10", "% 9f", 10.0);
    gen.TestSPrintF("10", "% 10f", 10.0);
    gen.TestSPrintF("10", "% 12f", 10.0);
    gen.TestSPrintF("10", "%- f", 10.0);
    gen.TestSPrintF("10", "%- 2f", 10.0);
    gen.TestSPrintF("10", "%- 8f", 10.0);
    gen.TestSPrintF("10", "%- 9f", 10.0);
    gen.TestSPrintF("10", "%- 10f", 10.0);
    gen.TestSPrintF("10", "%- 12f", 10.0);
    gen.TestSPrintF("10", "%+f", 10.0);
    gen.TestSPrintF("10", "%-f", 10.0);
    gen.TestSPrintF("10", "%#f", 10.0);
    gen.TestSPrintF("10", "%0f", 10.0);
    gen.TestSPrintF("10", "%012f", 10.0);
    gen.TestSPrintF("-10", "%012f", -10.0);
    gen.TestSPrintF("-10", "%-012f", -10.0);
    gen.TestSPrintF("-10", "%05f", -10.0);
    gen.TestSPrintF("1e100", "%f", 1e100);
    gen.TestSPrintF("1e-100", "%f", 1e-100);
    gen.TestSPrintF("Double.NaN", "%f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "% f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "% F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%+f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%+F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%010f", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%010F", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.PositiveInfinity", "%f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "% f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "% F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%+f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%+F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%010f", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%010F", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%f", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%F", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2f", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2F", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#f", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#F", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10f", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10F", -std::numeric_limits<double>::infinity());
}

auto TestExponentDouble(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("ExponentDouble");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("10", "%e", 10.0);
    gen.TestSPrintF("10", "%12e", 10.0);
    gen.TestSPrintF("10", "%E", 10.0);
    gen.TestSPrintF("10", "%12E", 10.0);
    gen.TestSPrintF("123456.789", "%e", 123456.789);
    gen.TestSPrintF("123456.789", "%.2e", 123456.789);
    gen.TestSPrintF("0.00000000010", "%e", 0.00000000010);
    gen.TestSPrintF("100000000000.0", "%e", 100000000000.0);
    gen.TestSPrintF("1e101", "%e", 1e101);
    gen.TestSPrintF("1e-101", "%e", 1e-101);
    gen.TestSPrintF("1e100", "%e", 1e100);
    gen.TestSPrintF("1e-100", "%e", 1e-100);
    gen.TestSPrintF("1e99", "%e", 1e99);
    gen.TestSPrintF("1e-99", "%e", 1e-99);
    gen.TestSPrintF("10", "%12.0e", 10.0);
    gen.TestSPrintF("10", "%#12.0e", 10.0);
    gen.TestSPrintF("10", "%12.1e", 10.0);
    gen.TestSPrintF("10", "%12.8e", 10.0);
    gen.TestSPrintF("10", "%12.10e", 10.0);
    gen.TestSPrintF("10", "%13.10e", 10.0);
    gen.TestSPrintF("10", "%14.10e", 10.0);
    gen.TestSPrintF("-10", "%e", -10.0);
    gen.TestSPrintF("-10", "%12e", -10.0);
    gen.TestSPrintF("-10", "%12.0e", -10.0);
    gen.TestSPrintF("-10", "%12.1e", -10.0);
    gen.TestSPrintF("-10", "%12.10e", -10.0);
    gen.TestSPrintF("10", "% e", 10.0);
    gen.TestSPrintF("10", "% 2E", 10.0);
    gen.TestSPrintF("10", "% 8E", 10.0);
    gen.TestSPrintF("10", "% 9e", 10.0);
    gen.TestSPrintF("10", "% 10e", 10.0);
    gen.TestSPrintF("10", "% 12e", 10.0);
    gen.TestSPrintF("10", "%- e", 10.0);
    gen.TestSPrintF("10", "%- 2e", 10.0);
    gen.TestSPrintF("10", "%- 8e", 10.0);
    gen.TestSPrintF("10", "%- 9e", 10.0);
    gen.TestSPrintF("10", "%- 10e", 10.0);
    gen.TestSPrintF("10", "%- 12e", 10.0);
    gen.TestSPrintF("10", "%+e", 10.0);
    gen.TestSPrintF("10", "%-e", 10.0);
    gen.TestSPrintF("10", "%#e", 10.0);
    gen.TestSPrintF("10", "%0e", 10.0);
    gen.TestSPrintF("10", "%012e", 10.0);
    gen.TestSPrintF("-10", "%012e", -10.0);
    gen.TestSPrintF("-10", "%-012e", -10.0);
    gen.TestSPrintF("10", "%015e", 10.0);
    gen.TestSPrintF("-10", "%015e", -10.0);
    gen.TestSPrintF("-10", "%-015e", -10.0);
    gen.TestSPrintF("-10", "%05e", -10.0);
    gen.TestSPrintF("Double.NaN", "%e", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%E", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2e", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2E", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#e", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#E", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10e", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10E", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.PositiveInfinity", "%e", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%E", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2e", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2E", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#e", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#E", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10e", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10E", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%e", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%E", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2e", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2E", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#e", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#E", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10e", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10E", -std::numeric_limits<double>::infinity());
}

auto TestGeneralDouble(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("GeneralDouble");
    auto multi_block = gen.TestMultiple();
    gen.TestSPrintF("0.000001", "%g", 0.000001);
    gen.TestSPrintF("0.00001", "%g", 0.00001);
    gen.TestSPrintF("0.0001", "%g", 0.0001);
    gen.TestSPrintF("0.001", "%g", 0.001);
    gen.TestSPrintF("0.01", "%g", 0.01);
    gen.TestSPrintF("0.1", "%g", 0.1);
    gen.TestSPrintF("1", "%g", 1.0);
    gen.TestSPrintF("10", "%g", 10.0);
    gen.TestSPrintF("100", "%g", 100.0);
    gen.TestSPrintF("1000", "%g", 1000.0);
    gen.TestSPrintF("10000", "%g", 10000.0);
    gen.TestSPrintF("100000", "%g", 100000.0);
    gen.TestSPrintF("1000000", "%g", 1000000.0);
    gen.TestSPrintF("10000000", "%g", 10000000.0);
    gen.TestSPrintF("0.00000000010", "%g", 0.00000000010);
    gen.TestSPrintF("100000000000.0", "%g", 100000000000.0);
    gen.TestSPrintF("10", "%.0g", 10.0);
    gen.TestSPrintF("10", "%.0G", 10.0);
    gen.TestSPrintF("31.41", "%g", 31.41);
    gen.TestSPrintF("31.41", "%.0g", 31.41);
    gen.TestSPrintF("31.41", "%.0G", 31.41);
    gen.TestSPrintF("31.41", "%.2g", 31.41);
    gen.TestSPrintF("31.41", "%.2G", 31.41);
    gen.TestSPrintF("31.4159", "%.3g", 31.4159);
    gen.TestSPrintF("31.4159", "%.3G", 31.4159);
    gen.TestSPrintF("3.14159", "%.3g", 3.14159);
    gen.TestSPrintF("3.14159", "%.3G", 3.14159);
    gen.TestSPrintF("123456.789", "%g", 123456.789);
    gen.TestSPrintF("123456.789", "%.2g", 123456.789);
    gen.TestSPrintF("1e101", "%g", 1e101);
    gen.TestSPrintF("1e-101", "%g", 1e-101);
    gen.TestSPrintF("1e100", "%g", 1e100);
    gen.TestSPrintF("1e-100", "%g", 1e-100);
    gen.TestSPrintF("1e99", "%g", 1e99);
    gen.TestSPrintF("1e-99", "%g", 1e-99);
    gen.TestSPrintF("10", "%12g", 10.0);
    gen.TestSPrintF("10", "%12.0g", 10.0);
    gen.TestSPrintF("10", "%G", 10.0);
    gen.TestSPrintF("10", "%12G", 10.0);
    gen.TestSPrintF("10", "%12.0G", 10.0);
    gen.TestSPrintF("10", "%#12.0g", 10.0);
    gen.TestSPrintF("10", "%12.1g", 10.0);
    gen.TestSPrintF("10", "%12.8g", 10.0);
    gen.TestSPrintF("10", "%12.10g", 10.0);
    gen.TestSPrintF("10", "%13.10g", 10.0);
    gen.TestSPrintF("10", "%14.10g", 10.0);
    gen.TestSPrintF("-10", "%g", -10.0);
    gen.TestSPrintF("-10", "%12g", -10.0);
    gen.TestSPrintF("-10", "%12.0g", -10.0);
    gen.TestSPrintF("-10", "%12.1g", -10.0);
    gen.TestSPrintF("-10", "%12.10g", -10.0);
    gen.TestSPrintF("10", "% g", 10.0);
    gen.TestSPrintF("10", "% 2G", 10.0);
    gen.TestSPrintF("10", "% 8G", 10.0);
    gen.TestSPrintF("10", "% 9g", 10.0);
    gen.TestSPrintF("10", "% 10g", 10.0);
    gen.TestSPrintF("10", "% 12g", 10.0);
    gen.TestSPrintF("10", "%- g", 10.0);
    gen.TestSPrintF("10", "%- 2g", 10.0);
    gen.TestSPrintF("10", "%- 8g", 10.0);
    gen.TestSPrintF("10", "%- 9g", 10.0);
    gen.TestSPrintF("10", "%- 10g", 10.0);
    gen.TestSPrintF("10", "%- 12g", 10.0);
    gen.TestSPrintF("10", "%+g", 10.0);
    gen.TestSPrintF("10", "%-g", 10.0);
    gen.TestSPrintF("10", "%#g", 10.0);
    gen.TestSPrintF("1000.0", "%#g", 1000.0);
    gen.TestSPrintF("10000.0", "%#g", 10000.0);
    gen.TestSPrintF("100000.0", "%#g", 100000.0);
    gen.TestSPrintF("1000000.0", "%#g", 1000000.0);
    gen.TestSPrintF("12000000.0", "%#g", 12000000.0);
    gen.TestSPrintF("10", "%0g", 10.0);
    gen.TestSPrintF("10", "%012g", 10.0);
    gen.TestSPrintF("-10", "%012g", -10.0);
    gen.TestSPrintF("-10", "%-012g", -10.0);
    gen.TestSPrintF("-10", "%05g", -10.0);
    gen.TestSPrintF("Double.NaN", "%g", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%G", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2g", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%10.2G", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#g", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%#G", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10g", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.NaN", "%-10G", std::numeric_limits<double>::quiet_NaN());
    gen.TestSPrintF("Double.PositiveInfinity", "%g", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%G", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2g", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%10.2G", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#g", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%#G", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10g", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.PositiveInfinity", "%-10G", std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%g", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%G", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2g", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%10.2G", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#g", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%#G", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10g", -std::numeric_limits<double>::infinity());
    gen.TestSPrintF("Double.NegativeInfinity", "%-10G", -std::numeric_limits<double>::infinity());
}

auto TestDoubleBinary(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("DoubleBinary");
    auto multi_block = gen.TestMultiple();
    gen.Comment("Zero special case");
    gen.TestSPrintFDouble(0x0000000000000000);
    gen.Comment("Subnormals have exponent with bias of zero");
    gen.TestSPrintFDouble(0x0000000000000001);
    gen.TestSPrintFDouble(0x0000000000000002);
    gen.TestSPrintFDouble(0x0000000000000004);
    gen.TestSPrintFDouble(0x0000000000000008);
    gen.TestSPrintFDouble(0x0000000000000010);
    gen.TestSPrintFDouble(0x0000000000000100);
    gen.TestSPrintFDouble(0x0000000000001000);
    gen.TestSPrintFDouble(0x0000000000010000);
    gen.TestSPrintFDouble(0x0000000000100000);
    gen.TestSPrintFDouble(0x0000000001000000);
    gen.TestSPrintFDouble(0x0000000010000000);
    gen.TestSPrintFDouble(0x0000000100000000);
    gen.TestSPrintFDouble(0x0001000000000000);
    gen.TestSPrintFDouble(0x000FFFFFFFFFFFFF);
    gen.Comment("All possible exponents");
    for (uint64_t i = 1; i < 2047; i++) {
        gen.TestSPrintFDouble(i << 52);
    }
    for (uint64_t i = 1; i < 2047; i++) {
        gen.TestSPrintFDouble(0x000FFFFFFFFFFFFF | (i << 52));
    }
}

auto TestFloatBinary(TestCaseGen &gen) -> void
{
    auto function_block = gen.Function("FloatBinary");
    auto multi_block = gen.TestMultiple();
    gen.Comment("Zero special case");
    gen.TestSPrintFSingle(0x00000000);
    gen.Comment("Subnormals have exponent with bias of zero");
    gen.TestSPrintFSingle(0x00000001);
    gen.TestSPrintFSingle(0x00000002);
    gen.TestSPrintFSingle(0x00000004);
    gen.TestSPrintFSingle(0x00000008);
    gen.TestSPrintFSingle(0x00000010);
    gen.TestSPrintFSingle(0x00000100);
    gen.TestSPrintFSingle(0x00001000);
    gen.TestSPrintFSingle(0x00010000);
    gen.TestSPrintFSingle(0x00100000);
    gen.TestSPrintFSingle(0x00200000);
    gen.TestSPrintFSingle(0x00400000);
    gen.TestSPrintFSingle(0x007FFFFF);
    gen.Comment("All possible exponents");
    for (uint32_t i = 1; i < 255; i++) {
        gen.TestSPrintFSingle(i << 23);
    }
    for (uint32_t i = 1; i < 255; i++) {
        gen.TestSPrintFSingle(0x007FFFFF | (i << 23));
    }
}
