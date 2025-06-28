// ViewModels/DashboardViewModel.cs
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using AdminApplication.Data;
using AdminApplication.Models;

namespace AdminApplication.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;

        public int TotalCustomers { get; private set; }
        public int TotalRooms { get; private set; }
        public int ActiveReservations { get; private set; }
        public int AvailableRooms { get; private set; }
        public ObservableCollection<RoomTypeStats> PopularRoomTypes { get; private set; }
        public ObservableCollection<Reservation> RecentReservations { get; private set; }

        public DashboardViewModel()
        {
            _context = new AdminHotelDbContext();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Charger les statistiques de base
            TotalCustomers = _context.Customers.Count();
            TotalRooms = _context.Rooms.Count();
            ActiveReservations = _context.Reservations
                .Count(r => r.CheckOutDate >= DateTime.Today);
            AvailableRooms = _context.Rooms.Count(r => r.IsAvailable);

            // Charger les types de chambres populaires
            var popularRooms = _context.RoomTypes
                .Select(rt => new RoomTypeStats
                {
                    Name = rt.Name,
                    BookingCount = rt.Rooms.SelectMany(r => r.Reservations).Count()
                })
                .OrderByDescending(rt => rt.BookingCount)
                .Take(5)
                .ToList();
            PopularRoomTypes = new ObservableCollection<RoomTypeStats>(popularRooms);

            // Charger les réservations récentes
            var recentReservations = _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .OrderByDescending(r => r.CheckInDate)
                .Take(5)
                .ToList();
            RecentReservations = new ObservableCollection<Reservation>(recentReservations);
        }
    }

    public class RoomTypeStats
    {
        public string Name { get; set; }
        public int BookingCount { get; set; }
    }
}