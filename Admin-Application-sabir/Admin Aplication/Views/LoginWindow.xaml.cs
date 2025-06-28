using System.Windows;
using System.Linq;
using AdminApplication.Data;

namespace AdminApplication.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AdminHotelDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new AdminHotelDbContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;

                var user = _context.Users.FirstOrDefault(u =>
                    (u.Username == username || u.Email == username) &&
                    u.Password == password &&
                    u.IsActive);

                if (user != null)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    ErrorMessage.Text = "Username or password incorrect";
                    PasswordBox.Password = ""; // Clear password
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "An error occurred. Please try again.";
                MessageBox.Show("Database error: " + ex.Message);
            }
        }
    }
}