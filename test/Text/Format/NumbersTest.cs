namespace RJCP.Core.Text.Format
{
    using NUnit.Framework;

    [TestFixture]
    public class NumbersTest
    {
        [Test]
        public void CountDigitsLong()
        {
            Assert.That(NumbersAccessor.CountDigits(-1), Is.EqualTo(1));
            Assert.That(NumbersAccessor.CountDigits(0), Is.EqualTo(1));
            Assert.That(NumbersAccessor.CountDigits(1), Is.EqualTo(1));

            long value = 1;
            int digits = 2;
            while (digits <= 19) {
                value *= 10;
                Assert.That(NumbersAccessor.CountDigits(value - 1), Is.EqualTo(digits - 1));
                Assert.That(NumbersAccessor.CountDigits(value), Is.EqualTo(digits));
                Assert.That(NumbersAccessor.CountDigits(value + 1), Is.EqualTo(digits));
                digits++;
            }

            value = -1;
            digits = 2;
            while (digits <= 19) {
                value *= 10;
                Assert.That(NumbersAccessor.CountDigits(value - 1), Is.EqualTo(digits));
                Assert.That(NumbersAccessor.CountDigits(value), Is.EqualTo(digits));
                Assert.That(NumbersAccessor.CountDigits(value + 1), Is.EqualTo(digits - 1));
                digits++;
            }
        }

        [Test]
        public void CountBitDigitsHex()
        {
            Assert.That(NumbersAccessor.CountBitDigits(0, 4), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 16) {
                Assert.That(NumbersAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(NumbersAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(NumbersAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(NumbersAccessor.CountBitDigits(value, 4), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                digits++;
            }
        }

        [Test]
        public void CountBitDigitsOctal()
        {
            Assert.That(NumbersAccessor.CountBitDigits(0, 3), Is.EqualTo(1));

            ulong value = 1;
            int digits = 1;
            while (digits < 22) {
                Assert.That(NumbersAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(NumbersAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                Assert.That(NumbersAccessor.CountBitDigits(value, 3), Is.EqualTo(digits), "Value 0x{0:X16}", value);
                value <<= 1;
                digits++;
            }
        }
    }
}
