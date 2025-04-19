using BertinaAccountingTool.BusinessLogic.ViewModel;
using System.Windows.Controls;

namespace BertinaAccountingTool.View;

/// <summary>
/// Interaction logic for TEYAView.xaml
/// </summary>
public partial class TEYAView : UserControl
{
    private TEYAViewModel viewModel;

    public TEYAView()
    {
        InitializeComponent();
        DataContext = viewModel = new TEYAViewModel();
    }
}