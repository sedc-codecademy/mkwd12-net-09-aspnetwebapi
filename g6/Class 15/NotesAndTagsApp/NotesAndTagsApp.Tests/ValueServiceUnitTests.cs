
using NotesAndTagsApp.Services.Implementation;

namespace NotesAndTagsApp.Tests
{
    [TestClass]
    public class ValueServiceUnitTests
    {
        private readonly ValueService _valueService;

        public ValueServiceUnitTests()
        {
            _valueService = new ValueService();
        }

        [TestMethod]
        public void SumPositiveNumbers_should_return_null_on_negative_input()
        {
            //Arange
            int num1 = -3; //we are testing the case when we have negative input
            int num2 = 3;

            //Act
            int? result = _valueService.SumPositiveNumbers(num1, num2); //here we call the method that we test

            //Assert

            Assert.IsNull(result); //we expect our result to be null
        }

        [TestMethod]
        public void SumPositiveNumbers_should_return_positiveNumber_on_positive_inputs()
        {
            //Arange
            int num1 = 2;
            int num2 = 3;
            int expectedResult = 5;

            //Act
            int? result = _valueService.SumPositiveNumbers(num1, num2); 

            //Assert

            Assert.AreEqual(expectedResult, result); //we check if the result we expected is the same as the result that we got
        }

        [TestMethod]
        public void IsFirstNumberLarger_should_return_true()
        {
            //Arange
            int num1 = 3;
            int num2 = 2;

            //Act
            bool result = _valueService.IsFirstNumberLarger(num1, num2);

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFirstNumberLarger_should_return_false()
        {
            //Arange
            int num1 = 2;
            int num2 = 3;

            //Act
            bool result = _valueService.IsFirstNumberLarger(num1, num2);

            //Assert

            Assert.IsFalse(result);
        }
    }
}