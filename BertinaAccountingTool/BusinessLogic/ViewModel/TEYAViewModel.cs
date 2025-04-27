using BertinaAccountingTool.BusinessLogic.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class TEYAViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<TransactionViewModel> mergedInvoices = new();
    [ObservableProperty]
    private string buyerInvoives = string.Empty;
    [ObservableProperty]
    private string transactions = string.Empty;
    [ObservableProperty]
    private string transactionsOutput = string.Empty;

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
            throw new Exception("Költség utalás nem egy excel fájl");
        if (!File.Exists(BuyerInvoives))
            throw new Exception("Költség utalás fájl nem létezik");

        var fileInfo = new FileInfo(BuyerInvoives);

        MergedInvoices = new ObservableCollection<TransactionViewModel>(ExcelParser.ParseBuyerInvoivesExcel(fileInfo));
    }

    [RelayCommand]
    private void CreateTransactionsOutput()
    {
    }
}
