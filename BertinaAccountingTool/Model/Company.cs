using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BertinaAccountingTool.Model;

internal class Company
{
    public List<Invoice> OwnerInvoices { get; set; } = new();
    public List<Invoice> ServiceInvoices { get; set; } = new();
    public List<Invoice> BookingInvoices { get; set; } = new();
    public List<Invoice> SalaryAndTaxInvoices { get; set; } = new();
    public List<Invoice> ExpenseInvoices { get; set; } = new();
}
