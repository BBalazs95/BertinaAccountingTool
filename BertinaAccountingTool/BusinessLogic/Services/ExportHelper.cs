using BertinaAccountingTool.BusinessLogic.ViewModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;

namespace BertinaAccountingTool.BusinessLogic.Services;

internal static class ExportHelper
{
    public static void SaveInvoicesToCsv(IEnumerable<InvoiceViewModel> invoices, string filePath)
    {
        var csvLines = new List<string>();

        foreach (var invoice in invoices)
        {
            var csvLine = $"{invoice.CompanyAccountNumber};{invoice.Name};{invoice.AccountNumber};{invoice.Value};{invoice.Message};;;;;;;;;;;;;;";
            csvLines.Add(csvLine);
        }

        File.WriteAllLines(filePath, csvLines, new UTF8Encoding(false));
    }

    public static void SaveInvoicesToExcel(IEnumerable<InvoiceViewModel> invoices, string filePath)
    {
        using var package = new ExcelPackage();

        var worksheet = package.Workbook.Worksheets.Add("Invoices");

        worksheet.Cells[1, 1].Value = "Terhelendő számla";
        worksheet.Cells[1, 2].Value = "Kedvezményezett neve";
        worksheet.Cells[1, 3].Value = "Kedvezményezett számlaszám";
        worksheet.Cells[1, 4].Value = "Összeg";
        worksheet.Cells[1, 5].Value = "Közlemény";

        using (var range = worksheet.Cells[1, 1, 1, 5])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
        }

        int row = 2;
        foreach (var invoice in invoices)
        {
            worksheet.Cells[row, 1].Value = invoice.CompanyAccountNumber;
            worksheet.Cells[row, 2].Value = invoice.Name;
            worksheet.Cells[row, 3].Value = invoice.AccountNumber;
            worksheet.Cells[row, 4].Value = invoice.Value;
            worksheet.Cells[row, 5].Value = invoice.Message;
            row++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        var fileInfo = new FileInfo(filePath);
        package.SaveAs(fileInfo);
    }
}
