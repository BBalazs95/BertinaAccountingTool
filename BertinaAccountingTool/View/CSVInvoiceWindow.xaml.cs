using BertinaAccountingTool.BusinessLogic.ViewModel;
using OfficeOpenXml;
using System.Windows;

namespace BertinaAccountingTool.View
{
    /// <summary>
    /// Interaction logic for CSVInvoiceWindow.xaml
    /// </summary>
    public partial class CSVInvoiceWindow : Window
    {
        private CSVInvoiceViewModel viewModel;
        public CSVInvoiceWindow()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            viewModel = new CSVInvoiceViewModel();
            DataContext = viewModel;

            if (viewModel.LoadCommand.CanExecute(null))
                viewModel.LoadCommand.Execute(null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.SaveCommand.CanExecute(null))
                viewModel.SaveCommand.Execute(null);
        }
    }
}
