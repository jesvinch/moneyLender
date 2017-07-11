namespace MoneyLender.Services.Models
{
    /// <summary>
    /// Represents each record read from CSV file. 
    /// </summary>
    public class LenderDetails
    {
        public string Lender { get; set; }
        public decimal Rate { get; set; }
        public int Available { get; set; }
    }
}
