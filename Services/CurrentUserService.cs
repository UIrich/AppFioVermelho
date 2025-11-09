using AppFioVermelho.Models;

namespace AppFioVermelho.Services
{
    public class CurrentUserService
    {
        private static CurrentUserService _instance;
        private User _currentUser;

        private CurrentUserService() { }

        public static CurrentUserService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CurrentUserService();

                return _instance;
            }
        }

        public User CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;
        public bool IsAdmin => _currentUser?.IsAdmin ?? false;
        public string Nome => _currentUser?.Nome ?? string.Empty;
        public string Email => _currentUser?.Email ?? string.Empty;

        public void SetUser(User user)
        {
            _currentUser = user;
        }

        public void ClearUser()
        {
            _currentUser = null;
        }
    }
}
