using System.Windows.Controls;
using AdminApplication.ViewModels;

namespace AdminApplication.Views
{
    public partial class ReservationsView : UserControl
    {
        public ReservationsView()
        {
            InitializeComponent();
            DataContext = new ReservationsViewModel();
        }
    }
}