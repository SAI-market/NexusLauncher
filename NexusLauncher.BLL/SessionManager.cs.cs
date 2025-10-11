using NexusLauncher.Models;
using System;

namespace NexusLauncher.BLL
{
    public sealed class SessionManager
    {
        private static SessionManager _instance = null;
        private static readonly object _lock = new object();

        private User _currentUser;

        // Constructor privado para evitar instanciación externa
        private SessionManager()
        {
            _currentUser = null;
        }

        // Propiedad para obtener la instancia única
        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        // Propiedad para obtener el usuario actual
        public User CurrentUser
        {
            get { return _currentUser; }
        }

        // Método para iniciar sesión
        public void Login(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _currentUser = user;
        }

        // Método para cerrar sesión
        public void Logout()
        {
            _currentUser = null;
        }

        // Método para verificar si hay una sesión activa
        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }
    }
}