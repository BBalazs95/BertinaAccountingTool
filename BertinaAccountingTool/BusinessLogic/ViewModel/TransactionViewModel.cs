using CommunityToolkit.Mvvm.ComponentModel;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;
internal partial class TransactionViewModel : ObservableObject
{
    [ObservableProperty]
    private string value;
    [ObservableProperty]
    private DateTime date;
    [ObservableProperty]
    private string accountNumber;

    public TransactionViewModel(string value, DateTime date, string accountNumber = "")
    {
        this.value = value;
        this.date = date;
        this.accountNumber = accountNumber;
    }
}
