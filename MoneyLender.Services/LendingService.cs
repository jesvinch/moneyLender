using System.Collections.Generic;
using System.IO;
using MoneyLender.Services.Interfaces;
using MoneyLender.Services.Models;

namespace MoneyLender.Services
{
    public class LendingService
    {
        private ICalculationService CalculationService { get;}
        private IValidationService ValidationService { get; }

        public LendingService(ICalculationService calculationService, IValidationService validationService)
        {
            CalculationService = calculationService;
            ValidationService = validationService;
        }

        public bool ReadDataAndGetQuote(string fileName, string requestedAmount)
        {
            int value;
            if (string.IsNullOrEmpty(fileName) || !int.TryParse(requestedAmount, out value))
            {
                Logger.InvalidFileNameOrAmount();
                return false;
            }

            var lenderDetails = ReadDataFromFile(fileName);
            return GetQuote(lenderDetails, value);
        }

        private IList<LenderDetails> ReadDataFromFile(string fileName)
        {
            var path = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(path, fileName);
            var lenderDetails = FileReader.GetLenderDetails(filePath);
            return lenderDetails;
        }

        private bool GetQuote(IList<LenderDetails> lenderDetails, int value)
        {
            if (lenderDetails == null)
            {
                Logger.ErrorReadingFromFile();
                return false;
            }
            var requestedAmountValid = ValidationService.ValidateRequestedLoan(value, lenderDetails);
            if (requestedAmountValid)
            {
                var ableToLendRequestedAmount = CalculationService.DetermineIfAbleToBorrowFromLenders(lenderDetails, value);
                return CalculateRepayment(ableToLendRequestedAmount);
            }
            return false;
        }

        private bool CalculateRepayment(bool ableToLendRequestedAmount)
        {
            if (ableToLendRequestedAmount)
            {
                var repayment = CalculationService.Calculate();
                Logger.DisplayDetails(repayment);
                return true;
            }

            Logger.PrintValidationErrorInvalidRequest(InputValidationState.NotEnoughCapital);
            return false;
        }
    }
}
