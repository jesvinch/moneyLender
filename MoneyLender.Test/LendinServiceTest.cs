using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLender.Services;

namespace MoneyLender.Test
{
    /// <summary>
    /// Integration tests
    /// </summary>
    [TestClass]
    public class LendinServiceTest
    {
        [TestMethod]
        public void Test_ReadDataAndGetQuote_ReturnsTrue_ForValidFileAndLoanAmount()
        {
            LendingService service = new LendingService(new CalculationService(), new ValidationService());
            var result  = service.ReadDataAndGetQuote("market.csv", "1000");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Test_ReadDataAndGetQuote_ReturnsFalse_WhenRequestedAmountIsGreaterThanAvailableCapital()
        {
            LendingService service = new LendingService(new CalculationService(), new ValidationService());
            var result = service.ReadDataAndGetQuote("market.csv", "15000");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_ReadDataAndGetQuote_ReturnsFalse_WhenRequestedFileIsNotPresent()
        {
            LendingService service = new LendingService(new CalculationService(), new ValidationService());
            var result = service.ReadDataAndGetQuote("market1.csv", "1000");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_ReadDataAndGetQuote_ReturnsFalse_WhenRequestedLoanAmountIsInvalidFormat()
        {
            LendingService service = new LendingService(new CalculationService(), new ValidationService());
            var result = service.ReadDataAndGetQuote("market.csv", "Testing");
            Assert.AreEqual(false, result);
        }
    }
}
