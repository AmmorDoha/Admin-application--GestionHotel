using System.Windows.Controls;
using AdminApplication.ViewModels;

namespace AdminApplication.Views
{
    public partial class RoomsView : UserControl
    {
        public RoomsView()
        {
            InitializeComponent();
            DataContext = new RoomsViewModel();
        }
    }
}