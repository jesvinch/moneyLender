namespace MoneyLender.Services.Models
{
    public class LenderInvestmentDetails
    {
        public string Lender { get; set; }
        public decimal Rate { get; set; }
        public int TotalAmountLend { get; set; }
        public int Available { get; set; }
        public double RepaymentAmountDue { get; set; }
    }
}
