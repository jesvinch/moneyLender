using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using MoneyLender.Services.Models;

namespace MoneyLender.Services
{
    public class FileReader 
    {
        public static IList<LenderDetails> GetLenderDetails(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(filePath)))
                {
                    var csv = new CsvReader(reader);
                    return csv.GetRecords<LenderDetails>().ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.FileNotFound(ex.ToString());
                return null;
            }
        }
    }
}
