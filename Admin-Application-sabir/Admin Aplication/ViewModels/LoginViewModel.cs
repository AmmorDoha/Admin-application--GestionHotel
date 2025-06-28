namespace AdminApplication.ViewModels;
using AdminApplication.Data;  // Add this

    public class LoginViewModel : BaseViewModel
    {
        private readonly AdminHotelDbContext _context;

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            _context = new AdminHotelDbContext();
        }

        public bool ValidateLogin()
        {
            return _context.Users.Any(u =>
                u.Username == Username &&
                u.Password == Password &&
                u.IsActive);
        }
    }