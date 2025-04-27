using BertinaAccountingTool.BusinessLogic.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class TEYAViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<TransactionViewModel> errorTransactions = new();
    [ObservableProperty]
    private string buyerInvoives = string.Empty;
    [ObservableProperty]
    private string transactions = string.Empty;
    [ObservableProperty]
    private string transactionsOutput = string.Empty;
    private List<TransactionViewModel> allAccountNumbers = new();
    private List<TransactionViewModel> allTransactions = new();

    [RelayCommand]
    public void SourceBrowse(object textBox)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
        openFileDialog.Title = "Válasz egy excel fájlt";
        openFileDialog.Multiselect = false;

        if (openFileDialog.ShowDialog() == true && textBox is TextBox box)
        {
            box.Text = openFileDialog.FileName;
        }
    }

    [RelayCommand]
    private void LoadData()
    {
        if (!(BuyerInvoives.EndsWith(".xls") || BuyerInvoives.EndsWith(".xlsx")))
            throw new Exception("Vevői számlák nem egy excel fájl");
        if (!File.Exists(BuyerInvoives))
            throw new Exception("Vevői számlák fájl nem létezik");
        if (!(Transactions.EndsWith(".xls") || Transactions.EndsWith(".xlsx")))
            throw new Exception("Tranzakciók nem egy excel fájl");
        if (!File.Exists(Transactions))
            throw new Exception("Tranzakciók fájl nem létezik");

        var fileInfo = new FileInfo(BuyerInvoives);

        allAccountNumbers = ExcelParser.ParseBuyerInvoivesExcel(fileInfo);

        fileInfo = new FileInfo(Transactions);

        allTransactions = ExcelParser.ParseTransactionsExcel(fileInfo);

        ErrorTransactions.Clear();
        foreach (var transaction in allTransactions)
        {
            try
            {
                transaction.AccountNumber = allAccountNumbers.Single(t => t.Value == transaction.Value && t.Date.Date == transaction.Date.Date).AccountNumber;
            }
            catch (Exception)
            {
                ErrorTransactions.Add(transaction);
            }
        }
        if (ErrorTransactions.Count == 0)
            MessageBox.Show("Nincsenek hibás tranzakciók", "Hibás tranzakciók", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    [RelayCommand]
    private void CreateTransactionsOutput()
    {
    }
}
