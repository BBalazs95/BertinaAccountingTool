using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class TEYAViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<CompanyViewModel> mergedInvoices = new();
    [ObservableProperty]
    private string? buyerInvoives;
    [ObservableProperty]
    private string? transactions;
    [ObservableProperty]
    private string? transactionsOutput;

    [RelayCommand]
    private void BuyerInvoivesBrowse()
    {
    }

    [RelayCommand]
    private void TransactionsBrowse()
    {
    }

    [RelayCommand]
    private void TransactionsOutputBrowse()
    {
    }

    [RelayCommand]
    private void LoadData()
    {
    }

    [RelayCommand]
    private void CreateTransactionsOutput()
    {
    }
}
