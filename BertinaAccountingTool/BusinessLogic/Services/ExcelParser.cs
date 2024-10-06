using BertinaAccountingTool.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BertinaAccountingTool.BusinessLogic.Services
{
    public static class ExcelParser
    {
        public static Dictionary<string,List<Invoice>> ParseServiceExcel(FileInfo file)
        {
            var res=new Dictionary<string,List<Invoice>>();
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            int rows = worksheet.Dimension.Rows;
            for (int row = 2; row <= rows; row++)
            {
                var invoice=new Invoice();
                var compName = worksheet.Cells[row, 1].GetValue<string>() ?? "Üres";

                invoice.CompanyAccountNumber = worksheet.Cells[row, 1].GetValue<string>();
                invoice.Value = worksheet.Cells[row, 2].GetValue<string>();
                invoice.Name = worksheet.Cells[row, 4].GetValue<string>();
                invoice.AccountNumber = worksheet.Cells[row, 8].GetValue<string>();
                invoice.Message = worksheet.Cells[row, 3].GetValue<string>();

                if (!res.ContainsKey(compName))
                    res[compName] = new List<Invoice>();

                res[compName].Add(invoice);
            }

            return res;
        }
    }
}
