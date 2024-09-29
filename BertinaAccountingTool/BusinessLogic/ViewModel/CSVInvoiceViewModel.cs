using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

public partial class CSVInvoiceViewModel : ObservableObject
{
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
        openFileDialog.Multiselect = false;

        if (openFileDialog.ShowDialog() == true)
        {
            SourceFilePath = openFileDialog.FileName;
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
}
