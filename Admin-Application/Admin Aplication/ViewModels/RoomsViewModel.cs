using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AdminApplication.Data;
using AdminApplication.Helpers;
using AdminApplication.Models;
using AdminApplication.Views;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AdminApplication.ViewModels
{
    public class RoomsViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;
        private ObservableCollection<Room> _rooms;
        private Room _selectedRoom;

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public RoomsViewModel()
        {
            _context = new AdminHotelDbContext();
            LoadRooms();

            AddCommand = new RelayCommand(AddRoom);
            EditCommand = new RelayCommand(EditRoom, () => SelectedRoom != null);
            DeleteCommand = new RelayCommand(DeleteRoom, () => SelectedRoom != null);
        }

        private void LoadRooms()
        {
            var list = _context.Rooms
                .Include(r => r.RoomType)
                .ToList();
            Rooms = new ObservableCollection<Room>(list);
        }

        private void AddRoom()
        {
            var window = new AddEditRoomWindow();
            if (window.ShowDialog() == true)
            {
                try
                {
                    _context.Rooms.Add(window.Room);
                    _context.SaveChanges();
                    LoadRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditRoom()
        {
            var window = new AddEditRoomWindow(SelectedRoom);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadRooms();
            }
        }

        private void DeleteRoom()
        {
            if (MessageBox.Show("Are you sure you want to delete this room?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Rooms.Remove(SelectedRoom);
                _context.SaveChanges();
                LoadRooms();
            }
        }
    }
}