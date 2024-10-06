using BertinaAccountingTool.BusinessLogic.Model;
using BertinaAccountingTool.Model;
using OfficeOpenXml;
using System.IO;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal static class ExcelParser
    {
        private static Dictionary<string, string> companyAccountNumbers = new();

        internal static Dictionary<string, List<Invoice>> ParseOwnerExcel(FileInfo fileInfo)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 1].GetValue<string>())),
                    name: worksheet.Cells[row, 4].GetValue<string>(),
                    accountNumber: CleareAccountNumber(worksheet.Cells[row, 5].GetValue<string>()),
                    value: CleareValue(worksheet.Cells[row, 3].GetValue<double>()),
                    message: $"Bevétel és ktg. kül. {worksheet.Cells[row, 2].GetValue<string>()} {DateTime.Now.ToString("yyyy.MM")}");

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        internal static Dictionary<string, List<Invoice>> ParseServiceExcel(FileInfo fileInfo)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 1].GetValue<string>())),
                    name: worksheet.Cells[row, 4].GetValue<string>(),
                    accountNumber: CleareAccountNumber(worksheet.Cells[row, 6].GetValue<string>()),
                    value: CleareValue(worksheet.Cells[row, 2].GetValue<double>()),
                    message: worksheet.Cells[row, 3].GetValue<string>());

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        internal static Dictionary<string, List<Invoice>> ParseBookingExcel(FileInfo fileInfo)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 5].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 5].GetValue<string>())),
                    name: "Booking.com",
                    accountNumber: "108000077000000014509011",
                    value: CleareValue(worksheet.Cells[row, 4].GetValue<double>()),
                    message: $"{worksheet.Cells[row, 2].GetValue<string>()}, {worksheet.Cells[row, 3].GetValue<string>()}");

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        internal static Dictionary<string, List<Invoice>> ParseSalaryAndTaxExcel(FileInfo fileInfo)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 1].GetValue<string>())),
                    name: worksheet.Cells[row, 3].GetValue<string>(),
                    accountNumber: CleareAccountNumber(worksheet.Cells[row, 4].GetValue<string>()),
                    value: CleareValue(worksheet.Cells[row, 5].GetValue<double>()),
                    message: worksheet.Cells[row, 6].GetValue<string>());

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        internal static Dictionary<string, List<Invoice>> ParseExpenseExcel(FileInfo fileInfo)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 2].GetValue<string>() ?? Constants.errorValue;
                var invoice = new Invoice(
                    companyAccountNumber: CleareAccountNumber(GetAccountNumber(worksheet.Cells[row, 2].GetValue<string>())),
                    name: worksheet.Cells[row, 6].GetValue<string>(),
                    accountNumber: CleareAccountNumber(worksheet.Cells[row, 7].GetValue<string>()),
                    value: CleareValue(worksheet.Cells[row, 4].GetValue<double>()),
                    message: worksheet.Cells[row, 1].GetValue<string>());

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }

        public static void SetCompanyAccountNumbers(FileInfo fileInfo)
        {
            companyAccountNumbers.Clear();
            using ExcelPackage package = new ExcelPackage(fileInfo);
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
            if (accountNumber == null)
                return Constants.errorValue;

            accountNumber = accountNumber.Trim();
            accountNumber = accountNumber.Replace("-", "");
            if (accountNumber.Length == 16)
                accountNumber += Constants.last8Digit;
            if (accountNumber.Length < 24)
                accountNumber = Constants.errorValue;

            return accountNumber;
        }

        private static string CleareValue(double value)
        {
            value = Math.Abs(value);
            value = Math.Round(value, 0);

            return value.ToString("F0");
        }
    }
}
