namespace Test.SpreadyCalculator
{
    [TestClass]
    public class TestSimpleCalculator
    {
        [TestMethod]
        public void TestCorrectFunction()
        {
            var rawInput = "0111;0101;1111;0011|12|12111009;09090906;06060504;04040302";
            var expectedOutput = "4|1|3|75";

            var uut = new SimpleCalculator();

            var result = uut.CalculateStatistics(rawInput);
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIncorrectFunctionMismatchedData()
        {
            var rawInput = "0111;0101;1111;0011|12|12111009;0909090606;06060504;04040302";
            
            var uut = new SimpleCalculator();
            uut.CalculateStatistics(rawInput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIncorrectFunctionNonNumeric()
        {
            var rawInput = "0111;0101;1111;0011|12|12111009;09090t06;06060504;04040302";
            
            var uut = new SimpleCalculator();
            uut.CalculateStatistics(rawInput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIncorrectFunctionMissingSection()
        {
            var rawInput = "0111;0101;1111;0011|12111009;0909090606;06060504;04040302";

            var uut = new SimpleCalculator();
            uut.CalculateStatistics(rawInput);
        }
    }
}