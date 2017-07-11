using System.Collections.Generic;
using MoneyLender.Services.Models;

namespace MoneyLender.Services.Interfaces
{
    public interface ICalculationService
    {
        Repayment Calculate();
        bool DetermineIfAbleToBorrowFromLenders(IList<LenderDetails> lenderDetails, int requestedAmount);
    }
}
