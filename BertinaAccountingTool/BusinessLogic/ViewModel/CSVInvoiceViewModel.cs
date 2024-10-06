using BertinaAccountingTool.BusinessLogic.Services;
using BertinaAccountingTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

public partial class CSVInvoiceViewModel : ObservableObject
{
    private Dictionary<string, Company> parsedData = new();

    [ObservableProperty]
    private string ownerSourceFilePath = string.Empty;
    [ObservableProperty]
    private string serviceSourceFilePath = string.Empty;
    [ObservableProperty]
    private string bookingSourceFilePath = string.Empty;
    [ObservableProperty]
    private string salaryAndTaxSourceFilePath = string.Empty;
    [ObservableProperty]
    private string expenseSourceFilePath = string.Empty;
    [ObservableProperty]
    private string rootFolder = string.Empty;

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
    public void RootFolderBrowse()
    {
        OpenFolderDialog openFolderDialog = new();
        openFolderDialog.Title = "Válasz egy törzs könyvtárat";
        openFolderDialog.Multiselect = false;

        if (openFolderDialog.ShowDialog() == true)
        {
            RootFolder = openFolderDialog.FolderName;
        }
    }

    [RelayCommand]
    public void CreateCSV()
    {
    }

    [RelayCommand]
    public void Save()
    {
        Properties.Settings.Default.LastOwnerSourceFilePath = OwnerSourceFilePath;
        Properties.Settings.Default.LastServiceSourceFilePath = ServiceSourceFilePath;
        Properties.Settings.Default.LastBookingSourceFilePath = BookingSourceFilePath;
        Properties.Settings.Default.LastSalaryAndTaxSourceFilePath = SalaryAndTaxSourceFilePath;
        Properties.Settings.Default.LastExpenseSourceFilePath = ExpenseSourceFilePath;
        Properties.Settings.Default.LastRootFolderPath = RootFolder;
        Properties.Settings.Default.Save();
    }

    [RelayCommand]
    public void Load()
    {
        OwnerSourceFilePath = Properties.Settings.Default.LastOwnerSourceFilePath;
        ServiceSourceFilePath = Properties.Settings.Default.LastServiceSourceFilePath;
        BookingSourceFilePath = Properties.Settings.Default.LastBookingSourceFilePath;
        SalaryAndTaxSourceFilePath = Properties.Settings.Default.LastSalaryAndTaxSourceFilePath;
        ExpenseSourceFilePath = Properties.Settings.Default.LastExpenseSourceFilePath;
        RootFolder = Properties.Settings.Default.LastRootFolderPath;
    }

    partial void OnServiceSourceFilePathChanged(string value)
    {
        parsedData.Clear();
        FileInfo fileInfo;
        try
        {
            if (!(value.EndsWith(".xls") || value.EndsWith(".xlsx")) || !File.Exists(value))
                throw new Exception();
            fileInfo = new FileInfo(value);
        }
        catch
        {
            return;
        }

        var from = fileInfo.Name.IndexOf('_') + 1;
        var to = fileInfo.Name.LastIndexOf(".");
        var invoiceType = fileInfo.Name[from..to];
        var invoicesForCompanies = ExcelParser.ParseServiceExcel(fileInfo);

        foreach (var invoices in invoicesForCompanies)
        {
            if (!parsedData.ContainsKey(invoices.Key))
                parsedData[invoices.Key] = new();

            parsedData[invoices.Key].ServiceInvoices = invoices.Value;
        }
    }
}
