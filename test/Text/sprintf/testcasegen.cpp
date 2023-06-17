#include "testcasegen.hpp"

#include <cstdarg>
#include <iomanip>
#include <iostream>

TestCaseGen::TestCaseGen()
    : offset_(8) { }

auto TestCaseGen::Function(const std::string functionName) -> TestCaseBlock
{
    std::cout << std::string(offset_, ' ') << "[Test]" << std::endl;
    std::cout << std::string(offset_, ' ') << "public void SPrintF_" << functionName << "()" << std::endl;
    std::cout << std::string(offset_, ' ') << "{" << std::endl;

    TestCaseBlock block{offset_, "}"};
    return block;
}

auto TestCaseGen::TestMultiple() -> TestCaseBlock
{
    std::cout << std::string(offset_, ' ') << "Assert.Multiple(() => {" << std::endl;
    TestCaseBlock block{offset_, "});"};
    return block;
}

auto TestCaseGen::TestSPrintF(const std::string params, const std::string format...) -> void
{
    va_list args;
    va_start(args, format);

    char buffer[1024];
    int result = vsprintf(buffer, format.c_str(), args);
    buffer[result] = 0;
    va_end(args);

    std::cout << std::string(offset_, ' ')
        << "Assert.That(SPrintF(\"" << format << "\", " << params << "), "
        << "Is.EqualTo(\"" << buffer << "\"));" << std::endl;
}

auto TestCaseGen::TestSPrintFDouble(uint64_t binaryDouble) -> void
{
    // User provided direct 64-bit binary form of their double float, so they
    // can test the IEEE754 bit fields directly.
    double value = *reinterpret_cast<double *>(&binaryDouble);

    char doubleValue[64];
    int result = snprintf(doubleValue, 64, "%.15g", value);
    doubleValue[result] = 0;

    std::ios_base::fmtflags f(std::cout.flags());
    std::cout << std::string(offset_, ' ')
        << "Assert.That(SPrintF(\"%.15g\", "
        << "UInt64ToDouble(0x" << std::setfill('0') << std::setw(16) << std::hex << binaryDouble << ")), "
        << "Is.EqualTo(\"" << doubleValue << "\"));" << std::endl;
    std::cout.flags(f);
}

auto TestCaseGen::Comment(const std::string comment) -> void
{
    std::cout << std::string(offset_, ' ') << "// " << comment << std::endl;
}
