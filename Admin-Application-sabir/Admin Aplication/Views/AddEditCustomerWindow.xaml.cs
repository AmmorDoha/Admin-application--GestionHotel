using System;
using System.Windows;
using AdminApplication.Models;

namespace AdminApplication.Views
{
    public partial class AddEditCustomerWindow : Window
    {
        public Customer Customer { get; set; }

        public AddEditCustomerWindow(Customer customer = null)
        {
            InitializeComponent();
            Customer = customer ?? new Customer();
            DataContext = Customer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Customer.FirstName) ||
                string.IsNullOrWhiteSpace(Customer.LastName))
            {
                MessageBox.Show("First name and last name are required.");
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