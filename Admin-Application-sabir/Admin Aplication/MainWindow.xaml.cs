using System.Windows;
using System.Windows.Controls;
using AdminApplication.Views;

namespace AdminApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(new DashboardView());
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new DashboardView());
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new CustomersView());
        }
        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new RoomsView());
        }

        private void RoomTypes_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new RoomTypesView());
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ReservationsView());
        }

        private void Payments_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new PaymentsView());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}