using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    }

    [RelayCommand]
    public void RootFolderBrowse()
    {

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
