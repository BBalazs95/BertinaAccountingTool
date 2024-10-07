using BertinaAccountingTool.BusinessLogic.Services;
using BertinaAccountingTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class CSVInvoiceViewModel : ObservableObject
{
    private ObservableCollection<CompanyViewModel> allData = new();
    private ObservableCollection<CompanyViewModel> errorData = new();

    [ObservableProperty]
    private ObservableCollection<CompanyViewModel> showedData = new();
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

    private void LoadBookingSourceFilePath()
    {
        FileInfo fileInfo;
        try
        {
            if (!(BookingSourceFilePath.EndsWith(".xls") || BookingSourceFilePath.EndsWith(".xlsx")) || !File.Exists(BookingSourceFilePath))
                throw new Exception();
            fileInfo = new FileInfo(BookingSourceFilePath);
        }
        catch
        {
            return;
        }

        var invoicesForCompanies = ExcelParser.ParseBookingExcel(fileInfo);

        foreach (var invoice in invoicesForCompanies)
        {
            var company = allData.FirstOrDefault(c => c.Name == invoice.Key);
            var errorCompany = errorData.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                allData.Add(company);
            }
            if (errorCompany == null)
            {
                errorCompany = new CompanyViewModel(invoice.Key);
                errorData.Add(errorCompany);
            }

            company.BookingInvoices.Clear();
            errorCompany.BookingInvoices.Clear();
            invoice.Value.ForEach(c =>
            {
                var invoice = (InvoiceViewModel)c;
                company.BookingInvoices.Add(invoice);
                if (HasError(invoice))
                    errorCompany.BookingInvoices.Add(invoice);
            });
        }
    }

    private void LoadSalaryAndTaxSourceFilePath()
    {
        FileInfo fileInfo;
        try
        {
            if (!(SalaryAndTaxSourceFilePath.EndsWith(".xls") || SalaryAndTaxSourceFilePath.EndsWith(".xlsx")) || !File.Exists(SalaryAndTaxSourceFilePath))
                throw new Exception();
            fileInfo = new FileInfo(SalaryAndTaxSourceFilePath);
        }
        catch
        {
            return;
        }

        var invoicesForCompanies = ExcelParser.ParseSalaryAndTaxExcel(fileInfo);

        foreach (var invoice in invoicesForCompanies)
        {
            var company = allData.FirstOrDefault(c => c.Name == invoice.Key);
            var errorCompany = errorData.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                allData.Add(company);
            }
            if (errorCompany == null)
            {
                errorCompany = new CompanyViewModel(invoice.Key);
                errorData.Add(errorCompany);
            }

            company.SalaryAndTaxInvoices.Clear();
            errorCompany.SalaryAndTaxInvoices.Clear();
            invoice.Value.ForEach(c =>
            {
                var invoice = (InvoiceViewModel)c;
                company.SalaryAndTaxInvoices.Add(invoice);
                if (HasError(invoice))
                    errorCompany.SalaryAndTaxInvoices.Add(invoice);
            });
        }
    }

    private void LoadExpenseSourceFilePath()
    {
        FileInfo fileInfo;
        try
        {
            if (!(ExpenseSourceFilePath.EndsWith(".xls") || ExpenseSourceFilePath.EndsWith(".xlsx")) || !File.Exists(ExpenseSourceFilePath))
                throw new Exception();
            fileInfo = new FileInfo(ExpenseSourceFilePath);
        }
        catch
        {
            return;
        }

        var invoicesForCompanies = ExcelParser.ParseExpenseExcel(fileInfo);

        foreach (var invoice in invoicesForCompanies)
        {
            var company = allData.FirstOrDefault(c => c.Name == invoice.Key);
            var errorCompany = errorData.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                allData.Add(company);
            }
            if (errorCompany == null)
            {
                errorCompany = new CompanyViewModel(invoice.Key);
                errorData.Add(errorCompany);
            }

            company.ExpenseInvoices.Clear();
            errorCompany.ExpenseInvoices.Clear();
            invoice.Value.ForEach(c =>
            {
                var invoice = (InvoiceViewModel)c;
                company.ExpenseInvoices.Add(invoice);
                if (HasError(invoice))
                    errorCompany.ExpenseInvoices.Add(invoice);
            });
        }
    }

    private void LoadOwnerSourceFilePath()
    {
        FileInfo fileInfo;
        try
        {
            if (!(OwnerSourceFilePath.EndsWith(".xls") || OwnerSourceFilePath.EndsWith(".xlsx")) || !File.Exists(OwnerSourceFilePath))
                throw new Exception();
            fileInfo = new FileInfo(OwnerSourceFilePath);
        }
        catch
        {
            return;
        }

        var invoicesForCompanies = ExcelParser.ParseOwnerExcel(fileInfo);

        foreach (var invoice in invoicesForCompanies)
        {
            var company = allData.FirstOrDefault(c => c.Name == invoice.Key);
            var errorCompany = errorData.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                allData.Add(company);
            }
            if (errorCompany == null)
            {
                errorCompany = new CompanyViewModel(invoice.Key);
                errorData.Add(errorCompany);
            }

            company.OwnerInvoices.Clear();
            errorCompany.OwnerInvoices.Clear();
            invoice.Value.ForEach(c =>
            {
                var invoice = (InvoiceViewModel)c;
                company.OwnerInvoices.Add(invoice);
                if (HasError(invoice))
                    errorCompany.OwnerInvoices.Add(invoice);
            });
        }
    }

    private void LoadServiceSourceFilePath()
    {
        FileInfo fileInfo;
        try
        {
            if (!(ServiceSourceFilePath.EndsWith(".xls") || ServiceSourceFilePath.EndsWith(".xlsx")) || !File.Exists(ServiceSourceFilePath))
                throw new Exception();
            fileInfo = new FileInfo(ServiceSourceFilePath);
        }
        catch
        {
            return;
        }

        var invoicesForCompanies = ExcelParser.ParseServiceExcel(fileInfo);

        foreach (var invoice in invoicesForCompanies)
        {
            var company = allData.FirstOrDefault(c => c.Name == invoice.Key);
            var errorCompany = errorData.FirstOrDefault(c => c.Name == invoice.Key);
            if (company == null)
            {
                company = new CompanyViewModel(invoice.Key);
                allData.Add(company);
            }
            if (errorCompany == null)
            {
                errorCompany = new CompanyViewModel(invoice.Key);
                errorData.Add(errorCompany);
            }

            company.ServiceInvoices.Clear();
            errorCompany.ServiceInvoices.Clear();
            invoice.Value.ForEach(c =>
            {
                var invoice = (InvoiceViewModel)c;
                company.ServiceInvoices.Add(invoice);
                if (HasError(invoice))
                    errorCompany.ServiceInvoices.Add(invoice);
            });
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
    public void LoadData()
    {
        allData.Clear();
        errorData.Clear();

        LoadOwnerSourceFilePath();
        LoadServiceSourceFilePath();
        LoadBookingSourceFilePath();
        LoadSalaryAndTaxSourceFilePath();
        LoadExpenseSourceFilePath();

        ShowedData = allData;
    }

    [RelayCommand]
    public void ShowAllInvoice()
    {
        ShowedData = allData;
    }

    [RelayCommand]
    public void ShowErrorInvoice()
    {
        ShowedData = errorData;
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
        foreach (var company in allData)
        {
            var baseFolder = $"{RootFolder}\\{company.Name}".Replace(".", "");

            Directory.CreateDirectory(baseFolder);

            if (company.OwnerInvoices.Count > 0)
                CsvHelper.SaveInvoicesToCsv(company.OwnerInvoices, $"{baseFolder}\\Tulaj_Utalások_{company.Name.Replace(" ", "_").Replace(".", "")}.csv");
            if (company.ServiceInvoices.Count > 0)
                CsvHelper.SaveInvoicesToCsv(company.ServiceInvoices, $"{baseFolder}\\Szolgáltatói_Utalások_{company.Name.Replace(" ", "_").Replace(".", "")}.csv");
            if (company.BookingInvoices.Count > 0)
                CsvHelper.SaveInvoicesToCsv(company.BookingInvoices, $"{baseFolder}\\Booking_Utalások_{company.Name.Replace(" ", "_").Replace(".", "")}.csv");
            if (company.SalaryAndTaxInvoices.Count > 0)
                CsvHelper.SaveInvoicesToCsv(company.SalaryAndTaxInvoices, $"{baseFolder}\\Bérek+Adók_Utalások_{company.Name.Replace(" ", "_").Replace(".", "")}.csv");
            if (company.ExpenseInvoices.Count > 0)
                CsvHelper.SaveInvoicesToCsv(company.ExpenseInvoices, $"{baseFolder}\\Költség_Számlák_Utalások_{company.Name.Replace(" ", "_").Replace(".", "")}.csv");
        }
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

    private bool HasError(InvoiceViewModel invoice)
    {
        return string.IsNullOrWhiteSpace(invoice.Value) || invoice.Value == Constants.errorValue ||
               string.IsNullOrWhiteSpace(invoice.AccountNumber) || invoice.AccountNumber == Constants.errorValue ||
               string.IsNullOrWhiteSpace(invoice.CompanyAccountNumber) || invoice.CompanyAccountNumber == Constants.errorValue ||
               string.IsNullOrWhiteSpace(invoice.Name) || invoice.Name == Constants.errorValue ||
               string.IsNullOrWhiteSpace(invoice.Message) || invoice.Message == Constants.errorValue;
    }
}
