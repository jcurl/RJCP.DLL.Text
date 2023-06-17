#ifndef TESTCASEBLOCK_HPP
#define TESTCASEBLOCK_HPP

#include <string>

class TestCaseBlock
{
public:
    TestCaseBlock(int &offset, const std::string end_block);
    ~TestCaseBlock();

private:
    int &offset_;
    const std::string end_block_;
};

#endif
