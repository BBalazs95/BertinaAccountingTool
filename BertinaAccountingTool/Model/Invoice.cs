using CommunityToolkit.Mvvm.ComponentModel;

namespace BertinaAccountingTool.BusinessLogic.Model;

internal partial class Invoice
{
    public string CompanyAccountNumber { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string Value { get; set; }
    public string Message { get; set; }

    public Invoice(string companyAccountNumber, string name, string accountNumber, string value, string message)
    {
        CompanyAccountNumber = companyAccountNumber;
        Name = name;
        AccountNumber = accountNumber;
        Value = value;
        Message = message;
    }
}
