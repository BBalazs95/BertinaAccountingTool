using BertinaAccountingTool.BusinessLogic.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class CSVInvoiceViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<CompanyViewModel> data = new();
    [ObservableProperty]
    private string companyAccountNumbersSourceFilePath = string.Empty;
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

    partial void OnSalaryAndTaxSourceFilePathChanged(string value)
    {
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

        var invoicesForCompanies = ExcelParser.ParseSalaryAndTaxExcel(fileInfo);

        if (invoicesForCompanies.Keys.Count != Data.Count)
        {
            //TODO
        }

        foreach (var invoice in invoicesForCompanies)
        {
            var company = Data.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                Data.Add(company);
            }

            company.SalaryAndTaxInvoices.Clear();
            invoice.Value.ForEach(c => company.SalaryAndTaxInvoices.Add((InvoiceViewModel)c));
        }
    }

    partial void OnExpenseSourceFilePathChanged(string value)
    {
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

        var invoicesForCompanies = ExcelParser.ParseExpenseExcel(fileInfo);

        if (invoicesForCompanies.Keys.Count != Data.Count)
        {
            //TODO
        }

        foreach (var invoice in invoicesForCompanies)
        {
            var company = Data.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                Data.Add(company);
            }

            company.ExpenseInvoices.Clear();
            invoice.Value.ForEach(c => company.ExpenseInvoices.Add((InvoiceViewModel)c));
        }
    }

    partial void OnOwnerSourceFilePathChanged(string value)
    {
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

        var invoicesForCompanies = ExcelParser.ParseOwnerExcel(fileInfo);

        if (invoicesForCompanies.Keys.Count != Data.Count)
        {
            //TODO
        }

        foreach (var invoice in invoicesForCompanies)
        {
            var company = Data.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                Data.Add(company);
            }

            company.OwnerInvoices.Clear();
            invoice.Value.ForEach(c => company.OwnerInvoices.Add((InvoiceViewModel)c));
        }
    }

    partial void OnServiceSourceFilePathChanged(string value)
    {
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

        var invoicesForCompanies = ExcelParser.ParseServiceExcel(fileInfo);

        if (invoicesForCompanies.Keys.Count != Data.Count)
        {
            //TODO
        }

        foreach (var invoice in invoicesForCompanies)
        {
            var company = Data.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                Data.Add(company);
            }

            company.ServiceInvoices.Clear();
            invoice.Value.ForEach(c => company.ServiceInvoices.Add((InvoiceViewModel)c));
        }
    }

    partial void OnCompanyAccountNumbersSourceFilePathChanged(string value)
    {
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

        ExcelParser.SetCompanyAccountNumbers(fileInfo);
        OnServiceSourceFilePathChanged(ServiceSourceFilePath);
        OnOwnerSourceFilePathChanged(ServiceSourceFilePath);
        OnBookingSourceFilePathChanged(ServiceSourceFilePath);
        OnSalaryAndTaxSourceFilePathChanged(ServiceSourceFilePath);
        OnExpenseSourceFilePathChanged(ServiceSourceFilePath);
    }

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
        Properties.Settings.Default.LastCompanyAccountNumbersSourceFilePath = CompanyAccountNumbersSourceFilePath;
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
        CompanyAccountNumbersSourceFilePath = Properties.Settings.Default.LastCompanyAccountNumbersSourceFilePath;
        OwnerSourceFilePath = Properties.Settings.Default.LastOwnerSourceFilePath;
        ServiceSourceFilePath = Properties.Settings.Default.LastServiceSourceFilePath;
        BookingSourceFilePath = Properties.Settings.Default.LastBookingSourceFilePath;
        SalaryAndTaxSourceFilePath = Properties.Settings.Default.LastSalaryAndTaxSourceFilePath;
        ExpenseSourceFilePath = Properties.Settings.Default.LastExpenseSourceFilePath;
        RootFolder = Properties.Settings.Default.LastRootFolderPath;
    }
}
