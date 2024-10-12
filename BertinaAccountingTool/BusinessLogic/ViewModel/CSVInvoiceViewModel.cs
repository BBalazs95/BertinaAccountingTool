using BertinaAccountingTool.BusinessLogic.Services;
using BertinaAccountingTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
        try
        {
            if (!(BookingSourceFilePath.EndsWith(".xls") || BookingSourceFilePath.EndsWith(".xlsx")) || !File.Exists(BookingSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(BookingSourceFilePath);

            var invoicesForCompanies = ExcelParser.ParseBookingExcel(fileInfo);

            foreach (var invoice in invoicesForCompanies)
            {
                var company = allData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
                var errorCompany = errorData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadSalaryAndTaxSourceFilePath()
    {
        try
        {
            if (!(SalaryAndTaxSourceFilePath.EndsWith(".xls") || SalaryAndTaxSourceFilePath.EndsWith(".xlsx")) || !File.Exists(SalaryAndTaxSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(SalaryAndTaxSourceFilePath);

            var invoicesForCompanies = ExcelParser.ParseSalaryAndTaxExcel(fileInfo);

            foreach (var invoice in invoicesForCompanies)
            {
                var company = allData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
                var errorCompany = errorData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadExpenseSourceFilePath()
    {
        try
        {
            if (!(ExpenseSourceFilePath.EndsWith(".xls") || ExpenseSourceFilePath.EndsWith(".xlsx")) || !File.Exists(ExpenseSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(ExpenseSourceFilePath);

            var invoicesForCompanies = ExcelParser.ParseExpenseExcel(fileInfo);

            foreach (var invoice in invoicesForCompanies)
            {
                var company = allData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
                var errorCompany = errorData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadOwnerSourceFilePath()
    {
        try
        {
            if (!(OwnerSourceFilePath.EndsWith(".xls") || OwnerSourceFilePath.EndsWith(".xlsx")) || !File.Exists(OwnerSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(OwnerSourceFilePath);

            var invoicesForCompanies = ExcelParser.ParseOwnerExcel(fileInfo);

            foreach (var invoice in invoicesForCompanies)
            {
                var company = allData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
                var errorCompany = errorData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadServiceSourceFilePath()
    {
        try
        {
            if (!(ServiceSourceFilePath.EndsWith(".xls") || ServiceSourceFilePath.EndsWith(".xlsx")) || !File.Exists(ServiceSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(ServiceSourceFilePath);

            var invoicesForCompanies = ExcelParser.ParseServiceExcel(fileInfo);

            foreach (var invoice in invoicesForCompanies)
            {
                var company = allData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
                var errorCompany = errorData.FirstOrDefault(c => c.Name.Equals(invoice.Key, StringComparison.InvariantCultureIgnoreCase));
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoadCompanyAccountNumbersSourceFilePath()
    {
        try
        {
            if (!(CompanyAccountNumbersSourceFilePath.EndsWith(".xls") || CompanyAccountNumbersSourceFilePath.EndsWith(".xlsx")) || !File.Exists(CompanyAccountNumbersSourceFilePath))
                throw new Exception();
            var fileInfo = new FileInfo(CompanyAccountNumbersSourceFilePath);

            ExcelParser.SetCompanyAccountNumbers(fileInfo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    public void LoadData()
    {
        allData.Clear();
        errorData.Clear();

        LoadCompanyAccountNumbersSourceFilePath();
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
            var baseFolder = $"{RootFolder}\\{company.Name}";

            Directory.CreateDirectory(baseFolder);

            string clearCompanyName = company.Name.Replace(" ", "_");

            var rightOwnerInvoices = company.OwnerInvoices.Where(i => !HasError(i) && i.Value != "0");
            var zeroOwnerInvoices = company.OwnerInvoices.Where(i => !HasError(i) && i.Value == "0");
            var errorOwnerInvoices = company.OwnerInvoices.Where(HasError);
            if (rightOwnerInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToCsv(rightOwnerInvoices, $"{baseFolder}\\Tulaj_Utalások_{clearCompanyName}.csv");
            if (errorOwnerInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(errorOwnerInvoices, $"{baseFolder}\\Tulaj_Utalások_{clearCompanyName}_hiba.xlsx");
            if (zeroOwnerInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(zeroOwnerInvoices, $"{baseFolder}\\Tulaj_Utalások_{clearCompanyName}_0.xlsx");

            var rightServiceInvoices = company.ServiceInvoices.Where(i => !HasError(i) && i.Value != "0");
            var zeroServiceInvoices = company.ServiceInvoices.Where(i => !HasError(i) && i.Value == "0");
            var errorServiceInvoices = company.ServiceInvoices.Where(HasError);
            if (rightServiceInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToCsv(rightServiceInvoices, $"{baseFolder}\\Szolgáltatói_Utalások_{clearCompanyName}.csv");
            if (errorServiceInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(errorServiceInvoices, $"{baseFolder}\\Szolgáltatói_Utalások_{clearCompanyName}_hiba.xlsx");
            if (zeroServiceInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(zeroServiceInvoices, $"{baseFolder}\\Szolgáltatói_Utalások_{clearCompanyName}_0.xlsx");

            var rightBookingInvoices = company.BookingInvoices.Where(i => !HasError(i) && i.Value != "0");
            var zeroBookingInvoices = company.BookingInvoices.Where(i => !HasError(i) && i.Value == "0");
            var errorBookingInvoices = company.BookingInvoices.Where(HasError);
            if (rightBookingInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToCsv(rightBookingInvoices, $"{baseFolder}\\Booking_Utalások_{clearCompanyName}.csv");
            if (errorBookingInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(errorBookingInvoices, $"{baseFolder}\\Booking_Utalások_{clearCompanyName}_hiba.xlsx");
            if (zeroBookingInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(zeroBookingInvoices, $"{baseFolder}\\Booking_Utalások_{clearCompanyName}_0.xlsx");

            var rightSalaryAndTaxInvoices = company.SalaryAndTaxInvoices.Where(i => !HasError(i) && i.Value != "0");
            var zeroSalaryAndTaxInvoices = company.SalaryAndTaxInvoices.Where(i => !HasError(i) && i.Value == "0");
            var errorSalaryAndTaxInvoices = company.SalaryAndTaxInvoices.Where(HasError);
            if (rightSalaryAndTaxInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToCsv(rightSalaryAndTaxInvoices, $"{baseFolder}\\Bérek+Adók_Utalások_{clearCompanyName}.csv");
            if (errorSalaryAndTaxInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(errorSalaryAndTaxInvoices, $"{baseFolder}\\Bérek+Adók_Utalások_{clearCompanyName}_hiba.xlsx");
            if (zeroSalaryAndTaxInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(zeroSalaryAndTaxInvoices, $"{baseFolder}\\Bérek+Adók_Utalások_{clearCompanyName}_0.xlsx");

            var rightExpenseInvoices = company.ExpenseInvoices.Where(i => !HasError(i) && i.Value != "0");
            var zeroExpenseInvoices = company.ExpenseInvoices.Where(i => !HasError(i) && i.Value == "0");
            var errorExpenseInvoices = company.ExpenseInvoices.Where(HasError);
            if (rightExpenseInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToCsv(rightExpenseInvoices, $"{baseFolder}\\Költség_Számlák_Utalások_{clearCompanyName}.csv");
            if (errorExpenseInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(errorExpenseInvoices, $"{baseFolder}\\Költség_Számlák_Utalások_{clearCompanyName}_hiba.xlsx");
            if (zeroExpenseInvoices.Count() > 0)
                ExportHelper.SaveInvoicesToExcel(zeroExpenseInvoices, $"{baseFolder}\\Költség_Számlák_Utalások_{clearCompanyName}_0.xlsx");
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
