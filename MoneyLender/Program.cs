using System;
using MoneyLender.Services;

namespace MoneyLender
{
    class Program
    {
        static void Main(string[] args)
        {
            LendingService lendingService = new LendingService(new CalculationService(), new ValidationService());
            lendingService.ReadDataAndGetQuote(args[0], args[1]);
            Console.ReadLine();
        }
    }
}
