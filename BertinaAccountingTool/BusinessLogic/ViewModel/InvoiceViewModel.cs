using BertinaAccountingTool.BusinessLogic.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class InvoiceViewModel : ObservableObject
{
    [ObservableProperty]
    private string companyAccountNumber;
    [ObservableProperty]
    private string value;
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string accountNumber;
    [ObservableProperty]
    private string message;

    public InvoiceViewModel(string companyAccountNumber, string value, string name, string accountNumber, string message)
    {
        CompanyAccountNumber = companyAccountNumber;
        Value = value;
        Name = name;
        AccountNumber = accountNumber;
        Message = message;
    }

    public static explicit operator InvoiceViewModel(Invoice i)
    {
        return new(i.CompanyAccountNumber, i.Value, i.Name, i.AccountNumber, i.Message);
    }
}
