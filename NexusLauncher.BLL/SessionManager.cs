using NexusLauncher.Models;

namespace NexusLauncher.BLL
{
    public sealed class SessionManager
    {
        private static readonly SessionManager instance = new SessionManager();
        private User _currentUser;

        private SessionManager() { }

        public static SessionManager Instance
        {
            get { return instance; }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
        }

        public bool IsUserLoggedIn
        {
            get { return _currentUser != null; }
        }

        public void Login(User user)
        {
            _currentUser = user;
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}