using System;
using System.Collections.Generic;
using System.Linq;
using MoneyLender.Services.Interfaces;
using MoneyLender.Services.Models;

namespace MoneyLender.Services
{
    public class CalculationService : ICalculationService
    {
        private const int LendingBlock = 10;

        public IList<LenderInvestmentDetails> Lenders { get; private set; }
        public Repayment Repayment { get; private set; }
        
        public CalculationService()
        {
            Lenders = new List<LenderInvestmentDetails>();
            Repayment = new Repayment();
        }
        public bool DetermineIfAbleToBorrowFromLenders(IList<LenderDetails> lenderDetails, int requestedAmount)
        {
            AddLenderInvestmentDetailForEachLender(lenderDetails);
            return AbleToBorrowCapitalFromLenders(requestedAmount);
        }

        public Repayment Calculate()
        {
            CalculateRepaymentForEachLender();
            CalculateTotalAndMonthlyRepayment();
            return Repayment;
        }

        private void CalculateTotalAndMonthlyRepayment()
        {
            double totalRepayment = 0.0;
            foreach (var lender in Lenders)
            {
                totalRepayment += lender.RepaymentAmountDue;
            }
            Repayment.TotalRepayment = Math.Round(totalRepayment, 2);
            Repayment.MonthlyRepayment = Math.Round(totalRepayment / 36, 2);
        }

        /// <summary>
        /// Check if there is enough funds available (sum(1% of available from each lender)) to lend the requested amount
        /// </summary>
        private bool AbleToBorrowCapitalFromLenders(int requestedAmount)
        {
            Repayment.RequestedAmount = requestedAmount;

            // Borrow from cheapest lenders first 
            var orderedList = Lenders.OrderBy(x => x.Rate);
            foreach (var lender in orderedList)
            { 
                var onePrecentOfAvailableAmount = lender.Available / 100;
                //For each lender, do not lend more than 1% to each borrower. 
                while (AbleToLend(requestedAmount, lender, onePrecentOfAvailableAmount))
                {
                    lender.Available -= LendingBlock;
                    requestedAmount -= LendingBlock;
                    lender.TotalAmountLend += LendingBlock;
                }
            }

            Lenders = orderedList.ToList();
            if (requestedAmount !=0)
            {
                return false;
            }
            return true;
        }

        private bool AbleToLend(int requestedAmount, LenderInvestmentDetails lender, int onePrecentOfAvailableAmount)
        {
            return lender.TotalAmountLend < onePrecentOfAvailableAmount && requestedAmount != 0 && lender.Available > LendingBlock;
        }

        private void CalculateRepaymentForEachLender()
        {
            foreach (var lender in Lenders)
            {
                //determine the highest interest
                Repayment.HighestRate = lender.Rate > Repayment.HighestRate
                    ? Math.Round(lender.Rate, 1)
                    : Repayment.HighestRate;

                decimal balance = lender.TotalAmountLend;
                //Calculate monthly compound interest balance for each lender. 
                for (int i = 0; i < 36; i++)
                {
                    balance += (balance / 100) * (lender.Rate / 12); 
                }
                lender.RepaymentAmountDue = (double)balance;
            }
        }

        private void AddLenderInvestmentDetailForEachLender(IList<LenderDetails> lenderDetails)
        {
            foreach (var lender in lenderDetails)
            {
                Lenders.Add(new LenderInvestmentDetails() { Lender = lender.Lender, Rate = lender.Rate, TotalAmountLend = 0, Available = lender.Available });
            }
        }
    }
}
