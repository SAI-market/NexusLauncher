using NexusLauncher.DAL;
using NexusLauncher.Models;
using System;

namespace NexusLauncher.BLL
{
    public class UserService
    {
        private UserDAL userDAL = new UserDAL();

        public User Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return userDAL.Login(username, password);
        }

        public bool Register(string username, string email, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            User newUser = new User
            {
                NombreUsuario = username,
                Email = email,
                Password = password
            };
            return userDAL.Register(newUser);
        }

        public bool UpdatePassword(int userId, string newPassword, string confirmPassword)
        {
            if (userId <= 0 || string.IsNullOrEmpty(newPassword))
            {
                throw new Exception("Datos inválidos.");
            }
            if (newPassword != confirmPassword)
            {
                throw new Exception("Las contraseñas no coinciden.");
            }

            return userDAL.UpdatePassword(userId, newPassword);
        }
    }
}