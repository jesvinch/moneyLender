using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLender.Services;
using MoneyLender.Services.Models;

namespace MoneyLender.Test
{
    [TestClass]
    public class ValidationServiceTest
    {
        public IList<LenderDetails> Lenders;
        [TestInitialize]
        public void TestInitialize()
        {
            Lenders = new List<LenderDetails>
            {
                new LenderDetails {Available = 500, Rate = 0.075m, Lender = "Bob"},
                new LenderDetails {Available = 500, Rate = 0.069m, Lender = "Jane"}
            };
        }

        [TestMethod]
        public void Test_DetermineLendingStatus_ReturnsFalse_WhenRequestIsGreaterThan15000()
        {
            var service = new ValidationService();
            var result  = service.ValidateRequestedLoan(16000, Lenders);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_DetermineLendingStatus_ReturnsFalse_WhenRequestIsLessThan1000()
        {
            var service = new ValidationService();
            var result = service.ValidateRequestedLoan(500, Lenders);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_DetermineLendingStatus_ReturnsFalse_WhenRequestIsNotDivisibleOf100()
        {
            var service = new ValidationService();
            var result = service.ValidateRequestedLoan(1230, Lenders);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_DetermineLendingStatus_ReturnsFalse_WhenRequestIsGreaterThanAvailableCapital()
        {
            var service = new ValidationService();
            var result = service.ValidateRequestedLoan(1200, Lenders);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Test_DetermineLendingStatus_ReturnsTrue_WhenRequestAmountIsValid()
        {
            var service = new ValidationService();
            var result = service.ValidateRequestedLoan(1000, Lenders);
            Assert.AreEqual(true, result);
        }
    }
}
