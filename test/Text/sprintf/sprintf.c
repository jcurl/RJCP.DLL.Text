#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>
#include <math.h>

void test_sprintf(char *, char *, ...);
void test_function(char *);
void test_function_end();

int main(void)
{
  test_function("Char");
  test_sprintf("65", "%c", 65);
  test_sprintf("'a'", "%c", 'a');
  test_sprintf("'a'", "%-2c", 'a');
  test_sprintf("'a'", "%2c", 'a');
  test_sprintf("'a'", "%+2c", 'a');
  test_sprintf("'a'", "% 2c", 'a');
  test_sprintf("'a'", "%#2c", 'a');
  test_sprintf("'a'", "%02c", 'a');
  test_sprintf("'a'", "%-2c", 'a');
  test_sprintf("'a'", "%+c", 'a');
  test_sprintf("'a'", "% c", 'a');
  test_sprintf("'a'", "%#c", 'a');
  test_sprintf("'a'", "%0c", 'a');
  test_sprintf("'a'", "%-c", 'a');
  test_sprintf("'a'", "%05c", 'a');
  test_function_end();

  test_function("String");
  test_sprintf("\"foo\"", "%s", "foo");
  test_sprintf("\"foo\"", "%-10s", "foo");
  test_sprintf("\"foo\"", "%10s", "foo");
  test_sprintf("\"foo\"", "%010s", "foo");
  test_sprintf("\"foo\"", "%+10s", "foo");
  test_sprintf("\"foo\"", "% 10s", "foo");
  test_sprintf("\"foo\"", "%#10s", "foo");
  test_sprintf("\"foobar\"", "%2s", "foobar");
  test_sprintf("\"foobar\"", "%-2s", "foobar");
  test_function_end();

  test_function("Integer");
  test_sprintf("16384", "%d", 16384);
  test_sprintf("16384", "%2d", 16384);
  test_sprintf("16384", "%.1d", 16384);
  test_sprintf("16384", "%.10d", 16384);
  test_sprintf("16384", "%-.10d", 16384);
  test_sprintf("16384", "%010d", 16384);
  test_sprintf("16384", "%-010d", 16384);
  test_sprintf("16384", "%-+10d", 16384);
  test_sprintf("16384", "%+10d", 16384);
  test_sprintf("16384", "%04d", 16384);
  test_sprintf("16384", "%010.10d", 16384);
  test_sprintf("16384", "%012.10d", 16384);
  test_sprintf("16384", "%012.0d", 16384);
  test_sprintf("16384", "%08.12d", 16384);
  test_sprintf("16384", "%010.12d", 16384);
  test_sprintf("16384", "% 10.10d", 16384);
  test_sprintf("-49152", "%d", -49152);
  test_sprintf("-49152", "%2d", -49152);
  test_sprintf("-49152", "%.1d", -49152);
  test_sprintf("-49152", "%.10d", -49152);
  test_sprintf("-49152", "%05.10d", -49152);
  test_sprintf("-49152", "%12.10d", -49152);
  test_sprintf("-49152", "% 12.10d", -49152);
  test_sprintf("-49152", "%-0 15.10d", -49152);
  test_sprintf("-49152", "%-015.10d", -49152);
  test_sprintf("-49152", "%0 15.10d", -49152);
  test_sprintf("-49152", "%015.10d", -49152);
  test_sprintf("-49152", "%-0 15.2d", -49152);
  test_sprintf("-49152", "%-015.2d", -49152);
  test_sprintf("-49152", "%0 15.2d", -49152);
  test_sprintf("-49152", "%015.2d", -49152);
  test_sprintf("-49152", "%-0 15d", -49152);
  test_sprintf("-49152", "%-015d", -49152);
  test_sprintf("-49152", "%0 15d", -49152);
  test_sprintf("-49152", "%015d", -49152);
  test_sprintf("-32768", "% d", -32768);
  test_sprintf("-32768", "%+d", -32768);
  test_sprintf("-32768", "%#d", -32768);
  test_sprintf("65536", "% d", 65536);
  test_sprintf("65536", "%+d", 65536);
  test_sprintf("65536", "%#d", 65536);
  test_sprintf("65536", "% +d", 65536);
  test_sprintf("65536", "%+ d", 65536);
  test_sprintf("65536", "% -d", 65536);
  test_sprintf("65536", "%- d", 65536);
  test_sprintf("65536", "%- 10d", 65536);
  test_sprintf("0", "%.0d", 0);
  test_sprintf("0", "%5.0d", 0);
  test_sprintf("0", "%05.0d", 0);
  test_sprintf("0", "%+.0d", 0);
  test_sprintf("0", "%-.0d", 0);
  test_sprintf("0", "% .0d", 0);
  test_sprintf("0", "%-+.0d", 0);
  test_sprintf("0", "% d", 0);
  test_sprintf("0", "%+d", 0);
  test_sprintf("0", "%#d", 0);
  test_sprintf("0", "% +d", 0);
  test_sprintf("0", "%+ d", 0);
  test_sprintf("0", "% -d", 0);
  test_sprintf("0", "%- d", 0);
  test_sprintf("0", "%- 10d", 0);
  test_sprintf("0xFFE", "%hhi", 0xFFE);
  test_sprintf("0x3FE", "%hhi", 0x3FE);
  test_sprintf("0xF7F", "%hhi", 0xF7F);
  test_sprintf("0x37F", "%hhi", 0x37F);
  test_sprintf("0x8000000000000000UL", "%lld", 0x8000000000000000ULL);
  test_function_end();

  test_function("UnsignedInteger");
  test_sprintf("16384", "%u", 16384);
  test_sprintf("16384", "%2u", 16384);
  test_sprintf("16384", "%.1u", 16384);
  test_sprintf("16384", "%.10u", 16384);
  test_sprintf("16384", "%010u", 16384);
  test_sprintf("16384", "%-010u", 16384);
  test_sprintf("16384", "%04u", 16384);
  test_sprintf("16384", "%010.10u", 16384);
  test_sprintf("16384", "% 10.10u", 16384);
  test_sprintf("-49152", "%u", -49152);
  test_sprintf("-49152", "%2u", -49152);
  test_sprintf("-49152", "%.1u", -49152);
  test_sprintf("-49152", "%.10u", -49152);
  test_sprintf("-49152", "%05.10u", -49152);
  test_sprintf("-49152", "%12.10u", -49152);
  test_sprintf("-49152", "% 12.10u", -49152);
  test_sprintf("-49152", "%-0 15.10u", -49152);
  test_sprintf("-49152", "%-015.10u", -49152);
  test_sprintf("-49152", "%0 15.10u", -49152);
  test_sprintf("-49152", "%015.10u", -49152);
  test_sprintf("-49152", "%-0 15.2u", -49152);
  test_sprintf("-49152", "%-015.2u", -49152);
  test_sprintf("-49152", "%0 15.2u", -49152);
  test_sprintf("-49152", "%015.2u", -49152);
  test_sprintf("-49152", "%-0 15u", -49152);
  test_sprintf("-49152", "%-015u", -49152);
  test_sprintf("-49152", "%0 15u", -49152);
  test_sprintf("-49152", "%015u", -49152);
  test_sprintf("-32768", "% u", -32768);
  test_sprintf("-32768", "%+u", -32768);
  test_sprintf("-32768", "%#u", -32768);
  test_sprintf("65536", "% u", 65536);
  test_sprintf("65536", "%+u", 65536);
  test_sprintf("65536", "%#u", 65536);
  test_sprintf("65536", "% +u", 65536);
  test_sprintf("65536", "%+ u", 65536);
  test_sprintf("65536", "% -u", 65536);
  test_sprintf("65536", "%- u", 65536);
  test_sprintf("65536", "%- 10u", 65536);
  test_sprintf("0", "%.0u", 0);
  test_sprintf("0", "%5.0u", 0);
  test_sprintf("0", "%05.0u", 0);
  test_sprintf("0", "%+.0u", 0);
  test_sprintf("0", "%-.0u", 0);
  test_sprintf("0", "% .0u", 0);
  test_sprintf("0", "%-+.0u", 0);
  test_sprintf("0", "% u", 0);
  test_sprintf("0", "%+u", 0);
  test_sprintf("0", "%#u", 0);
  test_sprintf("0", "% +u", 0);
  test_sprintf("0", "%+ u", 0);
  test_sprintf("0", "% -u", 0);
  test_sprintf("0", "%- u", 0);
  test_sprintf("0", "%- 10u", 0);
  test_sprintf("0xFFE", "%hhu", 0xFFE);
  test_sprintf("0x3FE", "%hhu", 0x3FE);
  test_sprintf("0xF7F", "%hhu", 0xF7F);
  test_sprintf("0x37F", "%hhu", 0x37F);
  test_sprintf("0x8000000000000000UL", "%llu", 0x8000000000000000ULL);
  test_function_end();

  test_function("Hexadecimal");
  test_sprintf("16384", "%x", 16384);
  test_sprintf("16384", "%2x", 16384);
  test_sprintf("16384", "%.1x", 16384);
  test_sprintf("16384", "%.10x", 16384);
  test_sprintf("16384", "%010x", 16384);
  test_sprintf("16384", "%-010x", 16384);
  test_sprintf("16384", "%04x", 16384);
  test_sprintf("16384", "%010.10x", 16384);
  test_sprintf("16384", "% 10.10x", 16384);
  test_sprintf("-49152", "%x", -49152);
  test_sprintf("-49152", "%2x", -49152);
  test_sprintf("-49152", "%.1x", -49152);
  test_sprintf("-49152", "%.10x", -49152);
  test_sprintf("-49152", "%05.10x", -49152);
  test_sprintf("-49152", "%12.10x", -49152);
  test_sprintf("-49152", "% 12.10x", -49152);
  test_sprintf("-49152", "%-0 15.10x", -49152);
  test_sprintf("-49152", "%-015.10x", -49152);
  test_sprintf("-49152", "%0 15.10x", -49152);
  test_sprintf("-49152", "%015.10x", -49152);
  test_sprintf("-49152", "%-0 15.2x", -49152);
  test_sprintf("-49152", "%-015.2x", -49152);
  test_sprintf("-49152", "%0 15.2x", -49152);
  test_sprintf("-49152", "%015.2x", -49152);
  test_sprintf("-49152", "%-0 15x", -49152);
  test_sprintf("-49152", "%-015x", -49152);
  test_sprintf("-49152", "%0 15x", -49152);
  test_sprintf("-49152", "%015x", -49152);
  test_sprintf("-32768", "% x", -32768);
  test_sprintf("-32768", "%+x", -32768);
  test_sprintf("-32768", "%#x", -32768);
  test_sprintf("65536", "% x", 65536);
  test_sprintf("65536", "%+x", 65536);
  test_sprintf("65536", "%#x", 65536);
  test_sprintf("65536", "% +x", 65536);
  test_sprintf("65536", "%+ x", 65536);
  test_sprintf("65536", "% -x", 65536);
  test_sprintf("65536", "%- x", 65536);
  test_sprintf("65536", "%- 10x", 65536);
  test_sprintf("0", "%.0x", 0);
  test_sprintf("0", "%5.0x", 0);
  test_sprintf("0", "%05.0x", 0);
  test_sprintf("0", "%+.0x", 0);
  test_sprintf("0", "%-.0x", 0);
  test_sprintf("0", "% .0x", 0);
  test_sprintf("0", "%-+.0x", 0);
  test_sprintf("0", "% x", 0);
  test_sprintf("0", "%+x", 0);
  test_sprintf("0", "%#x", 0);
  test_sprintf("0", "% +x", 0);
  test_sprintf("0", "%+ x", 0);
  test_sprintf("0", "% -x", 0);
  test_sprintf("0", "%- x", 0);
  test_sprintf("0", "%- 10x", 0);
  test_sprintf("0xFFE", "%hhx", 0xFFE);
  test_sprintf("0x3FE", "%hhx", 0x3FE);
  test_sprintf("0xF7F", "%hhx", 0xF7F);
  test_sprintf("0x37F", "%hhx", 0x37F);
  test_sprintf("0x8000000000000000UL", "%llx", 0x8000000000000000ULL);
  test_function_end();

  test_function("Octal");
  test_sprintf("16384", "%o", 16384);
  test_sprintf("16384", "%2o", 16384);
  test_sprintf("16384", "%.1o", 16384);
  test_sprintf("16384", "%.10o", 16384);
  test_sprintf("16384", "%010o", 16384);
  test_sprintf("16384", "%-010o", 16384);
  test_sprintf("16384", "%04o", 16384);
  test_sprintf("16384", "%010.10o", 16384);
  test_sprintf("16384", "% 10.10o", 16384);
  test_sprintf("-49152", "%o", -49152);
  test_sprintf("-49152", "%2o", -49152);
  test_sprintf("-49152", "%.1o", -49152);
  test_sprintf("-49152", "%.10o", -49152);
  test_sprintf("-49152", "%05.10o", -49152);
  test_sprintf("-49152", "%12.10o", -49152);
  test_sprintf("-49152", "% 12.10o", -49152);
  test_sprintf("-49152", "%-0 15.10o", -49152);
  test_sprintf("-49152", "%-015.10o", -49152);
  test_sprintf("-49152", "%0 15.10o", -49152);
  test_sprintf("-49152", "%015.10o", -49152);
  test_sprintf("-49152", "%-0 15.2o", -49152);
  test_sprintf("-49152", "%-015.2o", -49152);
  test_sprintf("-49152", "%0 15.2o", -49152);
  test_sprintf("-49152", "%015.2o", -49152);
  test_sprintf("-49152", "%-0 15o", -49152);
  test_sprintf("-49152", "%-015o", -49152);
  test_sprintf("-49152", "%0 15o", -49152);
  test_sprintf("-49152", "%015x", -49152);
  test_sprintf("-32768", "% o", -32768);
  test_sprintf("-32768", "%+o", -32768);
  test_sprintf("-32768", "%#o", -32768);
  test_sprintf("65536", "% o", 65536);
  test_sprintf("65536", "%+o", 65536);
  test_sprintf("65536", "%#o", 65536);
  test_sprintf("65536", "% +o", 65536);
  test_sprintf("65536", "%+ o", 65536);
  test_sprintf("65536", "% -o", 65536);
  test_sprintf("65536", "%- o", 65536);
  test_sprintf("65536", "%- 10o", 65536);
  test_sprintf("0", "%.0o", 0);
  test_sprintf("0", "%5.0o", 0);
  test_sprintf("0", "%05.0o", 0);
  test_sprintf("0", "%+.0o", 0);
  test_sprintf("0", "%-.0o", 0);
  test_sprintf("0", "% .0o", 0);
  test_sprintf("0", "%-+.0o", 0);
  test_sprintf("0", "% o", 0);
  test_sprintf("0", "%+o", 0);
  test_sprintf("0", "%#o", 0);
  test_sprintf("0", "% +o", 0);
  test_sprintf("0", "%+ o", 0);
  test_sprintf("0", "% -o", 0);
  test_sprintf("0", "%- o", 0);
  test_sprintf("0", "%- 10o", 0);
  test_sprintf("0xFFE", "%hho", 0xFFE);
  test_sprintf("0x3FE", "%hho", 0x3FE);
  test_sprintf("0xF7F", "%hho", 0xF7F);
  test_sprintf("0x37F", "%hho", 0x37F);
  test_sprintf("0x8000000000000000UL", "%llo", 0x8000000000000000ULL);
  test_function_end();

  test_function("FixedDouble");
  test_sprintf("10", "%f", 10.0);
  test_sprintf("123456.789", "%f", 123456.789);
  test_sprintf("123456.789", "%.2f", 123456.789);
  test_sprintf("0.00000000010", "%f", 0.00000000010);
  test_sprintf("100000000000.0", "%f", 100000000000.0);
  test_sprintf("10", "%12f", 10.0);
  test_sprintf("10", "%12.0f", 10.0);
  test_sprintf("10", "%#12.0f", 10.0);
  test_sprintf("10", "%#.0f", 10.0);
  test_sprintf("10", "%12.1f", 10.0);
  test_sprintf("10", "%12.8f", 10.0);
  test_sprintf("10", "%12.10f", 10.0);
  test_sprintf("10", "%13.10f", 10.0);
  test_sprintf("10", "%14.10f", 10.0);
  test_sprintf("-10", "%f", -10.0);
  test_sprintf("-10", "%12f", -10.0);
  test_sprintf("-10", "%12.0f", -10.0);
  test_sprintf("-10", "%12.1f", -10.0);
  test_sprintf("-10", "%12.10f", -10.0);
  test_sprintf("10", "% f", 10.0);
  test_sprintf("10", "% 2f", 10.0);
  test_sprintf("10", "% 8f", 10.0);
  test_sprintf("10", "% 9f", 10.0);
  test_sprintf("10", "% 10f", 10.0);
  test_sprintf("10", "% 12f", 10.0);
  test_sprintf("10", "%- f", 10.0);
  test_sprintf("10", "%- 2f", 10.0);
  test_sprintf("10", "%- 8f", 10.0);
  test_sprintf("10", "%- 9f", 10.0);
  test_sprintf("10", "%- 10f", 10.0);
  test_sprintf("10", "%- 12f", 10.0);
  test_sprintf("10", "%+f", 10.0);
  test_sprintf("10", "%-f", 10.0);
  test_sprintf("10", "%#f", 10.0);
  test_sprintf("10", "%0f", 10.0);
  test_sprintf("10", "%012f", 10.0);
  test_sprintf("-10", "%012f", -10.0);
  test_sprintf("-10", "%-012f", -10.0);
  test_sprintf("-10", "%05f", -10.0);
  test_sprintf("1e100", "%f", 1e100);
  test_sprintf("1e-100", "%f", 1e-100);
  test_sprintf("Double.NaN", "%f", NAN);
  test_sprintf("Double.NaN", "%F", NAN);
  test_sprintf("Double.NaN", "%10.2f", NAN);
  test_sprintf("Double.NaN", "%10.2F", NAN);
  test_sprintf("Double.NaN", "%#f", NAN);
  test_sprintf("Double.NaN", "%#F", NAN);
  test_sprintf("Double.NaN", "% f", NAN);
  test_sprintf("Double.NaN", "% F", NAN);
  test_sprintf("Double.NaN", "%+f", NAN);
  test_sprintf("Double.NaN", "%+F", NAN);
  test_sprintf("Double.NaN", "%-10f", NAN);
  test_sprintf("Double.NaN", "%-10F", NAN);
  test_sprintf("Double.NaN", "%010f", NAN);
  test_sprintf("Double.NaN", "%010F", NAN);
  test_sprintf("Double.PositiveInfinity", "%f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "% f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "% F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%+f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%+F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10F", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%010f", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%010F", INFINITY);
  test_sprintf("Double.NegativeInfinity", "%f", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%F", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2f", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2F", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#f", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#F", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10f", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10F", -INFINITY);
  test_function_end();

  test_function("ExponentDouble");
  test_sprintf("10", "%e", 10.0);
  test_sprintf("10", "%12e", 10.0);
  test_sprintf("10", "%E", 10.0);
  test_sprintf("10", "%12E", 10.0);
  test_sprintf("123456.789", "%e", 123456.789);
  test_sprintf("123456.789", "%.2e", 123456.789);
  test_sprintf("0.00000000010", "%e", 0.00000000010);
  test_sprintf("100000000000.0", "%e", 100000000000.0);
  test_sprintf("1e101", "%e", 1e101);
  test_sprintf("1e-101", "%e", 1e-101);
  test_sprintf("1e100", "%e", 1e100);
  test_sprintf("1e-100", "%e", 1e-100);
  test_sprintf("1e99", "%e", 1e99);
  test_sprintf("1e-99", "%e", 1e-99);
  test_sprintf("10", "%12.0e", 10.0);
  test_sprintf("10", "%#12.0e", 10.0);
  test_sprintf("10", "%12.1e", 10.0);
  test_sprintf("10", "%12.8e", 10.0);
  test_sprintf("10", "%12.10e", 10.0);
  test_sprintf("10", "%13.10e", 10.0);
  test_sprintf("10", "%14.10e", 10.0);
  test_sprintf("-10", "%e", -10.0);
  test_sprintf("-10", "%12e", -10.0);
  test_sprintf("-10", "%12.0e", -10.0);
  test_sprintf("-10", "%12.1e", -10.0);
  test_sprintf("-10", "%12.10e", -10.0);
  test_sprintf("10", "% e", 10.0);
  test_sprintf("10", "% 2E", 10.0);
  test_sprintf("10", "% 8E", 10.0);
  test_sprintf("10", "% 9e", 10.0);
  test_sprintf("10", "% 10e", 10.0);
  test_sprintf("10", "% 12e", 10.0);
  test_sprintf("10", "%- e", 10.0);
  test_sprintf("10", "%- 2e", 10.0);
  test_sprintf("10", "%- 8e", 10.0);
  test_sprintf("10", "%- 9e", 10.0);
  test_sprintf("10", "%- 10e", 10.0);
  test_sprintf("10", "%- 12e", 10.0);
  test_sprintf("10", "%+e", 10.0);
  test_sprintf("10", "%-e", 10.0);
  test_sprintf("10", "%#e", 10.0);
  test_sprintf("10", "%0e", 10.0);
  test_sprintf("10", "%012e", 10.0);
  test_sprintf("-10", "%012e", -10.0);
  test_sprintf("-10", "%-012e", -10.0);
  test_sprintf("10", "%015e", 10.0);
  test_sprintf("-10", "%015e", -10.0);
  test_sprintf("-10", "%-015e", -10.0);
  test_sprintf("-10", "%05e", -10.0);
  test_sprintf("Double.NaN", "%e", NAN);
  test_sprintf("Double.NaN", "%E", NAN);
  test_sprintf("Double.NaN", "%10.2e", NAN);
  test_sprintf("Double.NaN", "%10.2E", NAN);
  test_sprintf("Double.NaN", "%#e", NAN);
  test_sprintf("Double.NaN", "%#E", NAN);
  test_sprintf("Double.NaN", "%-10e", NAN);
  test_sprintf("Double.NaN", "%-10E", NAN);
  test_sprintf("Double.PositiveInfinity", "%e", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%E", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2e", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2E", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#e", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#E", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10e", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10E", INFINITY);
  test_sprintf("Double.NegativeInfinity", "%e", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%E", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2e", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2E", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#e", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#E", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10e", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10E", -INFINITY);
  test_function_end();

  test_function("GeneralDouble");
  test_sprintf("0.000001", "%g", 0.000001);
  test_sprintf("0.00001", "%g", 0.00001);
  test_sprintf("0.0001", "%g", 0.0001);
  test_sprintf("0.001", "%g", 0.001);
  test_sprintf("0.01", "%g", 0.01);
  test_sprintf("0.1", "%g", 0.1);
  test_sprintf("1", "%g", 1.0);
  test_sprintf("10", "%g", 10.0);
  test_sprintf("100", "%g", 100.0);
  test_sprintf("1000", "%g", 1000.0);
  test_sprintf("10000", "%g", 10000.0);
  test_sprintf("100000", "%g", 100000.0);
  test_sprintf("1000000", "%g", 1000000.0);
  test_sprintf("10000000", "%g", 10000000.0);
  test_sprintf("0.00000000010", "%g", 0.00000000010);
  test_sprintf("100000000000.0", "%g", 100000000000.0);
  test_sprintf("10", "%.0g", 10.0);
  test_sprintf("10", "%.0G", 10.0);
  test_sprintf("31.41", "%g", 31.41);
  test_sprintf("31.41", "%.0g", 31.41);
  test_sprintf("31.41", "%.0G", 31.41);
  test_sprintf("31.41", "%.2g", 31.41);
  test_sprintf("31.41", "%.2G", 31.41);
  test_sprintf("31.4159", "%.3g", 31.4159);
  test_sprintf("31.4159", "%.3G", 31.4159);
  test_sprintf("3.14159", "%.3g", 3.14159);
  test_sprintf("3.14159", "%.3G", 3.14159);
  test_sprintf("123456.789", "%g", 123456.789);
  test_sprintf("123456.789", "%.2g", 123456.789);
  test_sprintf("1e101", "%g", 1e101);
  test_sprintf("1e-101", "%g", 1e-101);
  test_sprintf("1e100", "%g", 1e100);
  test_sprintf("1e-100", "%g", 1e-100);
  test_sprintf("1e99", "%g", 1e99);
  test_sprintf("1e-99", "%g", 1e-99);
  test_sprintf("10", "%12g", 10.0);
  test_sprintf("10", "%12.0g", 10.0);
  test_sprintf("10", "%G", 10.0);
  test_sprintf("10", "%12G", 10.0);
  test_sprintf("10", "%12.0G", 10.0);
  test_sprintf("10", "%#12.0g", 10.0);
  test_sprintf("10", "%12.1g", 10.0);
  test_sprintf("10", "%12.8g", 10.0);
  test_sprintf("10", "%12.10g", 10.0);
  test_sprintf("10", "%13.10g", 10.0);
  test_sprintf("10", "%14.10g", 10.0);
  test_sprintf("-10", "%g", -10.0);
  test_sprintf("-10", "%12g", -10.0);
  test_sprintf("-10", "%12.0g", -10.0);
  test_sprintf("-10", "%12.1g", -10.0);
  test_sprintf("-10", "%12.10g", -10.0);
  test_sprintf("10", "% g", 10.0);
  test_sprintf("10", "% 2G", 10.0);
  test_sprintf("10", "% 8G", 10.0);
  test_sprintf("10", "% 9g", 10.0);
  test_sprintf("10", "% 10g", 10.0);
  test_sprintf("10", "% 12g", 10.0);
  test_sprintf("10", "%- g", 10.0);
  test_sprintf("10", "%- 2g", 10.0);
  test_sprintf("10", "%- 8g", 10.0);
  test_sprintf("10", "%- 9g", 10.0);
  test_sprintf("10", "%- 10g", 10.0);
  test_sprintf("10", "%- 12g", 10.0);
  test_sprintf("10", "%+g", 10.0);
  test_sprintf("10", "%-g", 10.0);
  test_sprintf("10", "%#g", 10.0);
  test_sprintf("1000.0", "%#g", 1000.0);
  test_sprintf("10000.0", "%#g", 10000.0);
  test_sprintf("100000.0", "%#g", 100000.0);
  test_sprintf("1000000.0", "%#g", 1000000.0);
  test_sprintf("12000000.0", "%#g", 12000000.0);
  test_sprintf("10", "%0g", 10.0);
  test_sprintf("10", "%012g", 10.0);
  test_sprintf("-10", "%012g", -10.0);
  test_sprintf("-10", "%-012g", -10.0);
  test_sprintf("-10", "%05g", -10.0);
  test_sprintf("Double.NaN", "%g", NAN);
  test_sprintf("Double.NaN", "%G", NAN);
  test_sprintf("Double.NaN", "%10.2g", NAN);
  test_sprintf("Double.NaN", "%10.2G", NAN);
  test_sprintf("Double.NaN", "%#g", NAN);
  test_sprintf("Double.NaN", "%#G", NAN);
  test_sprintf("Double.NaN", "%-10g", NAN);
  test_sprintf("Double.NaN", "%-10G", NAN);
  test_sprintf("Double.PositiveInfinity", "%g", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%G", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2g", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%10.2G", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#g", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%#G", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10g", INFINITY);
  test_sprintf("Double.PositiveInfinity", "%-10G", INFINITY);
  test_sprintf("Double.NegativeInfinity", "%g", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%G", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2g", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%10.2G", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#g", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%#G", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10g", -INFINITY);
  test_sprintf("Double.NegativeInfinity", "%-10G", -INFINITY);
  test_function_end();

  return EXIT_SUCCESS;
}

void test_function(char *function_name)
{
  printf("        [Test]\n");
  printf("        public void SPrintF_%s()\n", function_name);
  printf("        {\n");
}

void test_function_end()
{
  printf("        }\n");
  printf("\n");
}

// params is the parameter list in C# notation
// format is the C format string
void test_sprintf(char *params, char *format, ...)
{
  char buffer[1024];
  int result;
  
  va_list ap;
  va_start (ap, format);
  result = vsprintf(buffer, format, ap);
  if (params == NULL) {
    printf("            Assert.That(StringUtilities.SPrintF(\"%s\"), Is.EqualTo(\"%s\"));\n",
	   format, buffer);
  } else {
    printf("            Assert.That(StringUtilities.SPrintF(\"%s\", %s), Is.EqualTo(\"%s\"));\n",
	   format, params, buffer);
  }
  va_end(ap);
}
