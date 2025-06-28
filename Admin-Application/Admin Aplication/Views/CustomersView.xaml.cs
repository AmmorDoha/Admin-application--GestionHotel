using System.Windows.Controls;
using AdminApplication.ViewModels;

namespace AdminApplication.Views
{
    public partial class CustomersView : UserControl
    {
        public CustomersView()
        {
            InitializeComponent();
            DataContext = new CustomersViewModel();
        }
    }
}