using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class CompanyViewModel : ObservableObject
{
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private ObservableCollection<InvoiceViewModel> ownerInvoices=new();
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
    }
}
