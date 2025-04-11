using BertinaAccountingTool.BusinessLogic.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BertinaAccountingTool.View
{
    /// <summary>
    /// Interaction logic for CSVInvoiceView.xaml
    /// </summary>
    public partial class CSVInvoiceView : UserControl
    {
        private CSVInvoiceViewModel viewModel;
        public CSVInvoiceView()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            viewModel = new CSVInvoiceViewModel();
            DataContext = viewModel;

            if (viewModel.LoadCommand.CanExecute(null))
                viewModel.LoadCommand.Execute(null);

            Application.Current.Exit += (sender, e) =>
            {
                if (viewModel.SaveCommand.CanExecute(null))
                    viewModel.SaveCommand.Execute(null);
            };
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scroller = sender as ScrollViewer;
            scroller?.ScrollToVerticalOffset(scroller.VerticalOffset - e.Delta / 3);
            e.Handled = true;
        }
    }
}
