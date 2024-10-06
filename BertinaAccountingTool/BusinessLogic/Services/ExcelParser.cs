using BertinaAccountingTool.BusinessLogic.Model;
using BertinaAccountingTool.Model;
using OfficeOpenXml;
using System.IO;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal static class ExcelParser
    {
        private static Dictionary<string, string> companyAccountNumbers = new();
        internal static Dictionary<string, List<Invoice>> ParseServiceExcel(FileInfo file)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 1].GetValue<string>())),
                    name: worksheet.Cells[row, 4].GetValue<string>(),
                    accountNumber: CleareAccountNumber(worksheet.Cells[row, 6].GetValue<string>()),
                    value: worksheet.Cells[row, 2].GetValue<string>(),
                    message: worksheet.Cells[row, 3].GetValue<string>());

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        public static void SetCompanyAccountNumbers(FileInfo file)
        {
            companyAccountNumbers.Clear();
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int rows = worksheet.Dimension.Rows;
            for (int row = 1; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>();
                var accountNumber = worksheet.Cells[row, 2].GetValue<string>();

                companyAccountNumbers[compName] = accountNumber;
            }
        }

        private static string GetAccountNumber(string companyName)
        {
            if (companyName == null)
                return Constants.errorValue;

            if (companyAccountNumbers.TryGetValue(companyName, out string res))
                return res;

            return Constants.errorValue;
        }

        private static string CleareAccountNumber(string accountNumber)
        {
            if(accountNumber == null)
                return Constants.errorValue;

            accountNumber = accountNumber.Trim();
            accountNumber = accountNumber.Replace("-", "");
            if (accountNumber.Length == 16)
                accountNumber += Constants.last8Digit;
            if (accountNumber.Length < 24)
                accountNumber = Constants.errorValue;

            return accountNumber;
        }
    }
}
