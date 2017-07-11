using System;
using System.Configuration;
using MoneyLender.Services.Models;

namespace MoneyLender.Services
{
    public class Logger
    {
        public static void PrintValidationErrorInvalidRequest(InputValidationState state)
        {
            switch (state)
            {
                    case InputValidationState.NotEnoughCapital:
                        LogToOutput(ConfigurationManager.AppSettings["NotEnoughCapital"]);
                    break;
                    case InputValidationState.RequestedLoanAmountOutOfRange:
                        LogToOutput(ConfigurationManager.AppSettings["RequestedLoanAmountOutOfRange"]);
                    break;
                    case InputValidationState.RequestedLoanAmountInvalid:
                        LogToOutput(ConfigurationManager.AppSettings["RequestedLoanAmountInvalid"]);
                    break;
                    default:
                    break;
            }
        }

        public static void ErrorReadingFromFile()
        {
            LogToOutput(ConfigurationManager.AppSettings["ErrorReadingFromFile"]);
        }

        public static void InvalidFileNameOrAmount()
        {
            LogToOutput(ConfigurationManager.AppSettings["InvalidFileNameOrAmount"]);
        }

        public static void FileNotFound(string exception)
        {
            LogToOutput(ConfigurationManager.AppSettings["FileNotFound"] + " " +exception);
        }

        public static void DisplayDetails(Repayment repayment)
        {
            LogToOutput("Requested Amount: £" + repayment.RequestedAmount);
            LogToOutput("Highest Rate: " + repayment.HighestRate + "%");
            LogToOutput("Monthly repayment: £" + repayment.MonthlyRepayment);
            LogToOutput("Total repayment: £" + repayment.TotalRepayment);
        }

        public static void LogToOutput(string message)
        {
            Console.WriteLine(message);
        }
    }
}
