using System.Windows.Controls;
using AdminApplication.ViewModels;

namespace AdminApplication.Views
{
    public partial class RoomTypesView : UserControl
    {
        public RoomTypesView()
        {
            InitializeComponent();
            DataContext = new RoomTypesViewModel();
        }
    }
}