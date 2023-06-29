using _1_CalculatorLibrary;

namespace _1_CalculatorTests
{
    [TestClass]
    public class CalculatorUnitTests
    {        
        [TestMethod]
        public void Add_TestMethod_Positive()
        {
            // 1 A: Arrange
            Calculator calc = new Calculator();
            int num1 = 10, num2 = 20;

            // 2 A: Action
            int result = calc.Add(num1, num2);

            // 3 A: Assert
            Assert.AreEqual(num1 + num2, result);
        }

        [TestMethod]
        public void Add_TestMethod_Negative()
        {
            // 1 A: Arrange
            Calculator calc = new Calculator();
            int num1 = 10, num2 = 20;

            // 2 A: Action
            int result = calc.Add(num1, num2);

            // 3 A: Assert
            Assert.AreNotEqual(num1 + num2 + 10,
                result);
        }
    }
}