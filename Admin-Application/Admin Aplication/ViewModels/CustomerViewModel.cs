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
using Microsoft.Win32;

namespace AdminApplication.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;
        private readonly ExportService _exportService;
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ExportToExcelCommand { get; }
        public ICommand ExportToCsvCommand { get; }

        public CustomersViewModel()
        {
            _context = new AdminHotelDbContext();
            _exportService = new ExportService(_context);
            LoadCustomers();

            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer, () => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(DeleteCustomer, () => SelectedCustomer != null);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            ExportToCsvCommand = new RelayCommand(ExportToCsv);
        }

        private void LoadCustomers()
        {
            var list = _context.Customers.ToList();
            Customers = new ObservableCollection<Customer>(list);
        }

        private void AddCustomer()
        {
            var customerWindow = new AddEditCustomerWindow();
            if (customerWindow.ShowDialog() == true)
            {
                _context.Customers.Add(customerWindow.Customer);
                _context.SaveChanges();
                LoadCustomers();
            }
        }

        private void EditCustomer()
        {
            var customerWindow = new AddEditCustomerWindow(SelectedCustomer);
            if (customerWindow.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadCustomers();
            }
        }

        private void DeleteCustomer()
        {
            if (MessageBox.Show("Are you sure you want to delete this customer?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Customers.Remove(SelectedCustomer);
                _context.SaveChanges();
                LoadCustomers();
            }
        }

        private void ExportToExcel()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _exportService.ExportToExcel(dialog.FileName);
                    MessageBox.Show("Export completed successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during export: {ex.Message}", "Error");
                }
            }
        }

        private void ExportToCsv()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _exportService.ExportToCsv(dialog.FileName, Customers);
                    MessageBox.Show("Export completed successfully!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during export: {ex.Message}", "Error");
                }
            }
        }
    }
}