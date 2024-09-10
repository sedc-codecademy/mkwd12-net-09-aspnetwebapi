using UnitTestingDemo.Services;

namespace UnitTestingDemo.Tests
{
    [TestClass]
    public class NumbersServiceTests
    {
        private readonly NumbersService _numbersService;

        public NumbersServiceTests()
        {
            _numbersService = new NumbersService();
        }

        // ===> Naming Convention
        // MethodName_StateUnderTest_ExpectedBehavior

        // ===> AAA => Arange - Act - Assert
        // *Arrange* => Set up the test by preparing the objects, mocking dependencies, and setting the initial state.
        // *Act* => Execute the action or method that is being tested.
        // *Assert* =>  Verify that the outcome from the action is as expected, confirming that the method works correctly.

        // ===> Mocking is a technique used in unit testing to create fake objects

        [TestMethod]
        public void Sum_ValidNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int num1 = 10;
            int num2 = 20;

            // Act
            int? result = _numbersService.Sum(num1, num2);

            // Assert
            Assert.AreEqual(expected: 30, actual: result);
        }

        [TestMethod]
        public void Sum_NegativeOverflow_ReturnsNull()
        {
            // Arrange
            int num1 = int.MaxValue;
            int num2 = 1;

            // Act 
            var result = _numbersService.Sum(num1, num2);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_FirstNumberLarger_ReturnsTrue()
        {
            // Arrange
            int num1 = 10;
            int num2 = 5;

            // Act
            bool result = _numbersService.IsFirstNumberLarger(num1, num2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_SecondNumberLarger_ReturnsFalse()
        {
            // Arrange
            int num1 = 1;
            int num2 = 50;

            // Act
            bool result = _numbersService.IsFirstNumberLarger(num1, num2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetDigitName_ValidDigit_ReturnsCorrectName()
        {
            // Arrange
            int digit = 5;

            // Act
            string digitName = _numbersService.GetDigitName(digit);

            // Assert
            Assert.AreEqual("Five", digitName);
        }

        [TestMethod]  
        [DataRow(0, "Zero")]
        [DataRow(1, "One")]
        [DataRow(2, "Two")]
        [DataRow(3, "Three")]
        [DataRow(4, "Four")]
        [DataRow(5, "Five")]
        [DataRow(6, "Six")]
        [DataRow(7, "Seven")]
        [DataRow(8, "Eight")]
        [DataRow(9, "Nine")]
        public void GetDigitName_AllValidDigits_ReturnsCorrectName(int num, string expectedName)
        {
            // Act
            var result = _numbersService.GetDigitName(num);

            // Assert
            Assert.AreEqual(expectedName, result);
        }

        [TestMethod]
        public void GetDigitName_InvalidDigit_ThrowsException()
        {
            // Arrange
            int digit = 50;

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _numbersService.GetDigitName(digit)); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetDigitName_InvalidDigit_AttributeThrowsException()
        {
            // Arrange
            int digit = 50;

            // Act
            _numbersService.GetDigitName(digit);
        }
    }
}
