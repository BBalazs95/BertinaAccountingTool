using BertinaAccountingTool.BusinessLogic.ViewModel;
using System.IO;
using System.Text;

namespace BertinaAccountingTool.BusinessLogic.Services;

internal static class CsvHelper
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
}
