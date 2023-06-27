#ifndef TESTCASEGEN_HPP
#define TESTCASEGEN_HPP

#include "testcaseblock.hpp"

#include <cstdint>
#include <string>

class TestCaseGen
{
public:
    TestCaseGen();
    auto Function(const std::string functionName) -> TestCaseBlock;
    auto TestMultiple() -> TestCaseBlock;
    auto TestSPrintF(const std::string params, const std::string format...) -> void;
    auto TestSPrintFDouble(const std::string format, uint64_t binaryDouble) -> void;
    auto TestSPrintFSingle(const std::string format, uint32_t binaryFloat) -> void;
    auto Comment(const std::string comment) -> void;

private:
    int offset_;
};

#endif
