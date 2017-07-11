using System;
using System.Collections.Generic;
using MoneyLender.Services.Models;

namespace MoneyLender.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateRequestedLoan(int requestedAmount, IList<LenderDetails> lenderDetails);
    }
}
