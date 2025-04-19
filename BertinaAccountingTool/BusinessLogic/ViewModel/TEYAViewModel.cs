using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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
    }

    [RelayCommand]
    private void CreateTransactionsOutput()
    {
    }
}
