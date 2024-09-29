using BertinaAccountingTool.BusinessLogic.Services;
using BertinaAccountingTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

public partial class CSVInvoiceViewModel : ObservableObject
{
    private Dictionary<string, Dictionary<string, List<Invoice>>> parsedData = new();

    [ObservableProperty]
    private string sourceFilePath = string.Empty;
    [ObservableProperty]
    private string rootFolder = string.Empty;

    [RelayCommand]
    public void SourceBrowse()
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
        openFileDialog.Title = "Válasz egy excel fájlt";
        openFileDialog.Multiselect = true;

        if (openFileDialog.ShowDialog() == true)
        {
            SourceFilePath = string.Join('#', openFileDialog.FileNames);
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
        Properties.Settings.Default.LastSourceFilePath = SourceFilePath;
        Properties.Settings.Default.LastRootFolder = RootFolder;
        Properties.Settings.Default.Save();
    }

    [RelayCommand]
    public void Load()
    {
        SourceFilePath = Properties.Settings.Default.LastSourceFilePath;
        RootFolder = Properties.Settings.Default.LastRootFolder;
    }

    partial void OnSourceFilePathChanged(string value)
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
