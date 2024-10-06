using BertinaAccountingTool.BusinessLogic.Model;
using OfficeOpenXml;
using System.IO;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    internal static class ExcelParser
    {
        internal static Dictionary<string, List<Invoice>> ParseServiceExcel(FileInfo file)
        {
            var res = new Dictionary<string, List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? "Üres";
                var invoice = new Invoice(
                    companyAccountNumber: worksheet.Cells[row, 1].GetValue<string>(),
                    name: worksheet.Cells[row, 4].GetValue<string>(),
                    accountNumber: worksheet.Cells[row, 8].GetValue<string>(),
                    value: worksheet.Cells[row, 2].GetValue<string>(),
                    message: worksheet.Cells[row, 3].GetValue<string>());

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }
    }
}
