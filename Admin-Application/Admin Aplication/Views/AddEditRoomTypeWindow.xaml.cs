using System.Windows;
using AdminApplication.Models;

namespace AdminApplication.Views
{
    public partial class AddEditRoomTypeWindow : Window
    {
        public RoomType RoomType { get; private set; }

        public AddEditRoomTypeWindow(RoomType roomType = null)
        {
            InitializeComponent();
            RoomType = roomType ?? new RoomType();
            DataContext = RoomType;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RoomType.Name))
            {
                MessageBox.Show("Name is required!");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}