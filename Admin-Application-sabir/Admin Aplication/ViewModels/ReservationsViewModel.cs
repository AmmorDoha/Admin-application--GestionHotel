using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AdminApplication.Data;
using AdminApplication.Helpers;
using AdminApplication.Models;
using AdminApplication.Views;
using AdminApplication.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace AdminApplication.ViewModels
{
    public class ReservationsViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;
        private readonly PrintService _printService;
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;

        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set => SetProperty(ref _reservations, value);
        }

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set => SetProperty(ref _selectedReservation, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand PrintVoucherCommand { get; }

        public ReservationsViewModel()
        {
            _context = new AdminHotelDbContext();
            _printService = new PrintService();
            LoadReservations();

            AddCommand = new RelayCommand(AddReservation);
            EditCommand = new RelayCommand(EditReservation, () => SelectedReservation != null);
            DeleteCommand = new RelayCommand(DeleteReservation, () => SelectedReservation != null);
            PrintVoucherCommand = new RelayCommand(PrintVoucher, () => SelectedReservation != null);
        }

        private void LoadReservations()
        {
            var list = _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .ThenInclude(r => r.RoomType)
                .OrderByDescending(r => r.CheckInDate)
                .ToList();
            Reservations = new ObservableCollection<Reservation>(list);
        }

        private void AddReservation()
        {
            var window = new AddEditReservationWindow();
            if (window.ShowDialog() == true)
            {
                _context.Reservations.Add(window.Reservation);
                _context.SaveChanges();
                LoadReservations();
            }
        }

        private void EditReservation()
        {
            var window = new AddEditReservationWindow(SelectedReservation);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadReservations();
            }
        }

        private void DeleteReservation()
        {
            if (MessageBox.Show("Are you sure you want to delete this reservation?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Reservations.Remove(SelectedReservation);
                _context.SaveChanges();
                LoadReservations();
            }
        }

        private void PrintVoucher()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                FileName = $"Reservation_{SelectedReservation.Id}.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _printService.PrintReservationVoucher(SelectedReservation, dialog.FileName);
                    MessageBox.Show("Voucher generated successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating voucher: {ex.Message}", "Error");
                }
            }
        }
    }
}