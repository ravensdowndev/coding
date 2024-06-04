using SpreadyMcSpreader.Services;

namespace SpreadyMcSpreader.Tests.Services
{
    public class SpreadCalculatorTests : TestFixture
    {
        private readonly ISpreadCalculator _spreadCalculator;

        public SpreadCalculatorTests()
        {
            _spreadCalculator = SpreadCalculatorService();
        }

        [Theory]
        [InlineData("0111;0101;1111;0011|12|12111009;09090906;06060504;04040302", "4|1|3|75")]
        [InlineData("55;66|25|2015;0903", "0|0|0|100")]
        [InlineData("010;101;010|5|050404;040303;020000", "5|3|2|44")]
        [InlineData("111111111;000000000;111111111;000000000;000000000;000000000;000000000;000000000;000000000|20|191817161514131211;111111111111111111;100908070605040302;020202020202020202;020202020202020202;020202020202020202;020202020202020202;020202020202020202;020202020202020202", "0|0|0|100")]
        public void Calculate_ReturnsExpectedResult(string input, string expected)
        {
            string result = _spreadCalculator.Calculate(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_EmptyInput_ThrowsArgumentNullException()
        {
            string input = "";

            Assert.Throws<ArgumentNullException>(() => _spreadCalculator.Calculate(input));
        }

        [Fact]
        public void Calculate_InvalidInput_ThrowsArgumentException()
        {
            string input = "A&B&C";

            Assert.Throws<ArgumentException>(() => _spreadCalculator.Calculate(input));
        }
    }
}