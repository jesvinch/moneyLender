using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLender.Services;

namespace MoneyLender.Test
{
    [TestClass]
    public class FileReaderTest
    {
        [TestMethod]
        public void Test_GetLenderDetails_ReadsAllDataFromFile()
        {
            var fileName = "market.csv";
            var path = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(path, fileName);
            var result = FileReader.GetLenderDetails(filePath);
            Assert.AreEqual(7, result.Count);
        }

        [TestMethod]
        public void Test_GetLenderDetails_ReturnsNull_WhenWrongFileNameIsProvided()
        {
            var fileName = "market1.csv";
            var path = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(path, fileName);
            var result = FileReader.GetLenderDetails(filePath);
            Assert.AreEqual(null, result);
        }
    }
}
