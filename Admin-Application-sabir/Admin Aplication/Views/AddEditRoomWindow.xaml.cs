using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using AdminApplication.Data;
using AdminApplication.Models;

namespace AdminApplication.Views
{
    public partial class AddEditRoomWindow : Window
    {
        public Room Room { get; private set; }
        public ObservableCollection<RoomType> RoomTypes { get; private set; }

        public AddEditRoomWindow(Room room = null)
        {
            InitializeComponent();

            using (var context = new AdminHotelDbContext())
            {
                RoomTypes = new ObservableCollection<RoomType>(context.RoomTypes.ToList());
            }

            Room = room ?? new Room
            {
                IsAvailable = true,
                Status = "Available"
            };

            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Room.RoomNumber))
            {
                MessageBox.Show("Room number is required.", "Validation Error");
                return;
            }

            if (Room.RoomType == null)
            {
                MessageBox.Show("Please select a room type.", "Validation Error");
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