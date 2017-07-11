using System.Collections.Generic;
using System.Configuration;
using MoneyLender.Services.Interfaces;
using MoneyLender.Services.Models;

namespace MoneyLender.Services
{
    public class ValidationService: IValidationService
    {
        public bool ValidateRequestedLoan(int requestedAmount, IList<LenderDetails> lenderDetails)
        {
            if (requestedAmount < int.Parse(ConfigurationManager.AppSettings["MinValue"]) || 
                requestedAmount > int.Parse(ConfigurationManager.AppSettings["MaxValue"]))
            {
                Logger.PrintValidationErrorInvalidRequest(InputValidationState.RequestedLoanAmountOutOfRange);
                return false;
            }
            if (requestedAmount > GetTotalAmountToLend(lenderDetails))
            {
                Logger.PrintValidationErrorInvalidRequest(InputValidationState.NotEnoughCapital);
                return false;
            }
            if (requestedAmount % int.Parse(ConfigurationManager.AppSettings["IncrementAmount"]) != 0)
            {
                Logger.PrintValidationErrorInvalidRequest(InputValidationState.RequestedLoanAmountInvalid);
                return false;
            }

            return true;
        }

        private int GetTotalAmountToLend(IList<LenderDetails> lenderDetails)
        {
            int total = 0;
            foreach (var lender in lenderDetails)
            {
                total += lender.Available;
            }
            return total;
        }
    } 
}
