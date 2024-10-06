using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class CompanyViewModel : ObservableObject
{
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private Visibility companyVisibility;
    [ObservableProperty]
    private Visibility ownerVisibility;
    [ObservableProperty]
    private Visibility serviceVisibility;
    [ObservableProperty]
    private Visibility bookingVisibility;
    [ObservableProperty]
    private Visibility salaryAndTaxVisibility;
    [ObservableProperty]
    private Visibility expenseVisibility;
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> ownerInvoices = new();
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> serviceInvoices = new();
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> bookingInvoices = new();
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> salaryAndTaxInvoices = new();
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> expenseInvoices = new();

    public CompanyViewModel(string name)
    {
        Name = name;
        OwnerInvoices.CollectionChanged += UpdateOwnerVisibility;
        ServiceInvoices.CollectionChanged += UpdateServiceVisibility;
        BookingInvoices.CollectionChanged += UpdateBookingVisibility;
        SalaryAndTaxInvoices.CollectionChanged += UpdateSalaryAndTaxVisibility;
        ExpenseInvoices.CollectionChanged += UpdateExpenseVisibility;
        
        ExpenseVisibility = ExpenseInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        SalaryAndTaxVisibility = SalaryAndTaxInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        BookingVisibility = BookingInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        ServiceVisibility = ServiceInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        OwnerVisibility = OwnerInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateExpenseVisibility(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ExpenseVisibility = ExpenseInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateSalaryAndTaxVisibility(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SalaryAndTaxVisibility = SalaryAndTaxInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateBookingVisibility(object? sender, NotifyCollectionChangedEventArgs e)
    {
        BookingVisibility = BookingInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateServiceVisibility(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        ServiceVisibility = ServiceInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateOwnerVisibility(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OwnerVisibility = OwnerInvoices.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        CompanyVisibility = OwnerInvoices.Count > 0 ||
             ServiceInvoices.Count > 0 ||
             BookingInvoices.Count > 0 ||
             SalaryAndTaxInvoices.Count > 0 ||
             ExpenseInvoices.Count > 0
            ? Visibility.Visible : Visibility.Collapsed;
    }
}
