#include "testcaseblock.hpp"

#include <iostream>

TestCaseBlock::TestCaseBlock(int &offset, const std::string end_block)
    : offset_(offset)
    , end_block_(std::move(end_block))
{
    offset += 4;
}

TestCaseBlock::~TestCaseBlock()
{
    offset_ -= 4;
    std::cout << std::string(offset_, ' ') << end_block_ << std::endl;
}
