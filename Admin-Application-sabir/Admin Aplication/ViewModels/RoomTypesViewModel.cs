using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AdminApplication.Data;
using AdminApplication.Helpers;
using AdminApplication.Models;
using AdminApplication.Views;
using System.Linq;

namespace AdminApplication.ViewModels
{
    public class RoomTypesViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;
        private ObservableCollection<RoomType> _roomTypes;
        private RoomType _selectedRoomType;

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set => SetProperty(ref _roomTypes, value);
        }

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set => SetProperty(ref _selectedRoomType, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public RoomTypesViewModel()
        {
            _context = new AdminHotelDbContext();
            LoadRoomTypes();

            AddCommand = new RelayCommand(AddRoomType);
            EditCommand = new RelayCommand(EditRoomType, () => SelectedRoomType != null);
            DeleteCommand = new RelayCommand(DeleteRoomType, () => SelectedRoomType != null);
        }

        private void LoadRoomTypes()
        {
            var list = _context.RoomTypes.ToList();
            RoomTypes = new ObservableCollection<RoomType>(list);
        }

        private void AddRoomType()
        {
            var window = new AddEditRoomTypeWindow();
            if (window.ShowDialog() == true)
            {
                _context.RoomTypes.Add(window.RoomType);
                _context.SaveChanges();
                LoadRoomTypes();
            }
        }

        private void EditRoomType()
        {
            var window = new AddEditRoomTypeWindow(SelectedRoomType);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadRoomTypes();
            }
        }

        private void DeleteRoomType()
        {
            if (MessageBox.Show("Are you sure you want to delete this room type?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.RoomTypes.Remove(SelectedRoomType);
                _context.SaveChanges();
                LoadRoomTypes();
            }
        }
    }
}