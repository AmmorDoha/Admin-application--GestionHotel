using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using AdminApplication.Models;
using AdminApplication.Data;

namespace AdminApplication.Views
{
    public partial class AddEditReservationWindow : Window
    {
        private readonly AdminHotelDbContext _context;
        public Reservation Reservation { get; private set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Room> AvailableRooms { get; set; }
        public string[] StatusOptions { get; } = new[] { "Pending", "Confirmed", "Checked In", "Checked Out", "Cancelled" };

        public AddEditReservationWindow(Reservation reservation = null)
        {
            InitializeComponent();
            _context = new AdminHotelDbContext();

            // Charger les données
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
            AvailableRooms = new ObservableCollection<Room>(
                _context.Rooms.Where(r => r.IsAvailable).ToList()
            );

            // Initialiser la réservation
            if (reservation == null)
            {
                Reservation = new Reservation
                {
                    CheckInDate = DateTime.Today,
                    CheckOutDate = DateTime.Today.AddDays(1),
                    Status = "Pending"
                };
            }
            else
            {
                Reservation = reservation;
                if (reservation.Room != null && !AvailableRooms.Contains(reservation.Room))
                {
                    AvailableRooms.Add(reservation.Room);
                }
            }

            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Reservation.Customer == null)
            {
                MessageBox.Show("Please select a customer");
                return;
            }

            if (Reservation.Room == null)
            {
                MessageBox.Show("Please select a room");
                return;
            }

            if (Reservation.CheckOutDate <= Reservation.CheckInDate)
            {
                MessageBox.Show("Check-out date must be after check-in date");
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