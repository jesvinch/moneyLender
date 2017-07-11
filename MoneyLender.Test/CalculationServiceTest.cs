using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLender.Services;
using MoneyLender.Services.Models;

namespace MoneyLender.Test
{
    [TestClass]
    public class CalculationServiceTest
    {
        public IList<LenderDetails> Lenders;
        [TestInitialize]
        public void TestInitialize()
        {
            Lenders = new List<LenderDetails>
            {
                new LenderDetails {Available = 12000, Rate = 7, Lender = "Bob"},
                new LenderDetails {Available = 80000, Rate = 0.069m, Lender = "Jane"},
                new LenderDetails {Available = 50000, Rate = 6, Lender = "Fred"}
            };
        }

        [TestMethod]
        public void Test_Calculate_ReturnsCorrectValues_WhenRequestIs1000()
        {
            var service = new CalculationService();
            service.DetermineIfAbleToBorrowFromLenders(Lenders, 1000);
            var result = service.Calculate();

            //Check each lender's amount lend. 
            Assert.AreEqual(801.66, Math.Round(service.Lenders[0].RepaymentAmountDue,2)); 
            Assert.AreEqual(239.34, Math.Round(service.Lenders[1].RepaymentAmountDue, 2));
            Assert.AreEqual(0, service.Lenders[2].TotalAmountLend, 2); //Ensure the lender with highest rate wasn't borrowed from. 

            //Check Total and monthly repayment
            Assert.AreEqual(1040.99, result.TotalRepayment);
            Assert.AreEqual(28.92, result.MonthlyRepayment);
        }

        [TestMethod]
        public void Test_Calculate_ReturnsCorrectValues_WhenRequestIs1500()
        {
            var service = new CalculationService();
            service.DetermineIfAbleToBorrowFromLenders(Lenders, 1500);
            var result = service.Calculate();
            
            Assert.AreEqual(1547.95, result.TotalRepayment);
            Assert.AreEqual(43, result.MonthlyRepayment);
            Assert.AreEqual(7, result.HighestRate);
        }

        /// <summary>
        /// Check if there is enough funds available (sum(1% of available from each lender)) to lend the requested amount
        /// </summary>
        [TestMethod]
        public void Test_DetermineIfAbleToBorrowFromLenders_ReturnsFalse_WhenRequestIs1500()
        {
            var service = new CalculationService();
            var result = service.DetermineIfAbleToBorrowFromLenders(Lenders, 1500);

            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Check if there is enough funds available (sum(1% of available from each lender)) to lend the requested amount
        /// </summary>
        [TestMethod]
        public void Test_DetermineIfAbleToBorrowFromLenders_ReturnsTrue_WhenRequestIs1400()
        {
            var service = new CalculationService();
            var result = service.DetermineIfAbleToBorrowFromLenders(Lenders, 1400);

            Assert.AreEqual(true, result);
        }
    }
}
