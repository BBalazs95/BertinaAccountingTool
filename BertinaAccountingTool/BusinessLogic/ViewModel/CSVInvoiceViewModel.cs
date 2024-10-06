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
    private Dictionary<string, Dictionary<string, List<Invoice>>> parsedData = new();

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
        Properties.Settings.Default.LastRootFolder = RootFolder;
        Properties.Settings.Default.Save();
    }

    [RelayCommand]
    public void Load()
    {
        RootFolder = Properties.Settings.Default.LastRootFolder;
    }

    partial void OnServiceSourceFilePathChanged(string value)
    {
        parsedData.Clear();
        List<FileInfo> fileInfos = new();
        var files = value.Split('#');
        try
        {
            foreach (var file in files)
            {
                if (!(file.EndsWith(".xls") || file.EndsWith(".xlsx")) || !File.Exists(file))
                    throw new Exception();
                fileInfos.Add(new FileInfo(file));
            }
        }
        catch
        {
            return;
        }

        foreach (var file in fileInfos)
        {
            var from = file.Name.IndexOf('_') + 1;
            var to = file.Name.LastIndexOf(".");
            var invoiceType = file.Name[from..to];
            var invoicesForCompanies = ExcelParser.ParseExcel(file);

            foreach (var invoices in invoicesForCompanies)
            {
                if (!parsedData.ContainsKey(invoices.Key))
                    parsedData[invoices.Key] = new Dictionary<string, List<Invoice>>();

                parsedData[invoices.Key][invoiceType] = invoices.Value;
            }
        }
    }
}
