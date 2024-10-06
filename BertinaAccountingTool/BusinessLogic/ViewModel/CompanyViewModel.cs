using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BertinaAccountingTool.BusinessLogic.ViewModel;

internal partial class CompanyViewModel : ObservableObject
{
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private List<InvoiceViewModel> ownerInvoices=new();
    [ObservableProperty]
    private List<InvoiceViewModel> serviceInvoices = new();
    [ObservableProperty]
    private List<InvoiceViewModel> bookingInvoices = new();
    [ObservableProperty]
    private List<InvoiceViewModel> salaryAndTaxInvoices = new();
    [ObservableProperty]
    private List<InvoiceViewModel> expenseInvoices = new();

    public CompanyViewModel(string name)
    {
        Name = name;
    }
}
