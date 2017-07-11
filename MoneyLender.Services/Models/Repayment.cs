namespace MoneyLender.Services.Models
{
    public class Repayment
    {
        public int RequestedAmount { get; set; }
        public decimal HighestRate { get; set; }
        public double TotalRepayment { get; set; }
        public double MonthlyRepayment { get; set; }
    }
}
