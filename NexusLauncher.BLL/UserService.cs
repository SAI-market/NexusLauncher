using NexusLauncher.DAL;
using NexusLauncher.Models;
using System;
using System.Text.RegularExpressions;

namespace NexusLauncher.BLL
{
    public class UserService
    {
        private readonly UserDAL _userDal = new UserDAL();

        // Valida usuario y contraseña
        public bool Authenticate(string username, string password, out User user)
        {
            user = _userDal.GetByUsername(username);

            if (user == null)
                return false;

            // Por ahora comparamos directo (más adelante se puede usar hash de contraseña)
            if (user.Password == password)
                return true;

            user = null;
            return false;
        }

        // Registra un nuevo usuario
        public bool RegisterUser(string username, string password, string displayName, string email, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage = "El nombre de usuario no puede estar vacío.";
                return false;
            }

            if (username.Length < 3)
            {
                errorMessage = "El nombre de usuario debe tener al menos 3 caracteres.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "La contraseña no puede estar vacía.";
                return false;
            }

            if (!ValidarContraseñaSegura(password))
            {
                errorMessage = "La contraseña debe tener al menos 6 caracteres, incluir una mayúscula, una minúscula y un número.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "El email no puede estar vacío.";
                return false;
            }

            if (!ValidarEmail(email))
            {
                errorMessage = "El formato del email no es válido.";
                return false;
            }

            // Verificar si el usuario ya existe
            if (_userDal.GetByUsername(username) != null)
            {
                errorMessage = "El nombre de usuario ya existe.";
                return false;
            }

            // Verificar si el email ya existe
            if (_userDal.GetByEmail(email) != null)
            {
                errorMessage = "El email ya está registrado.";
                return false;
            }

            // Crear el nuevo usuario
            var newUser = new User
            {
                Username = username.Trim(),
                Password = password, // Más adelante se puede usar hash
                DisplayName = string.IsNullOrWhiteSpace(displayName) ? username : displayName.Trim(),
                Email = email.Trim()
            };

            int userId = _userDal.CreateUser(newUser);

            if (userId > 0)
            {
                return true;
            }
            else
            {
                errorMessage = "Error al crear el usuario en la base de datos.";
                return false;
            }
        }

        private bool ValidarContraseñaSegura(string password)
        {
            if (password.Length < 6)
                return false;

            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) tieneMayuscula = true;
                if (char.IsLower(c)) tieneMinuscula = true;
                if (char.IsDigit(c)) tieneNumero = true;
            }

            return tieneMayuscula && tieneMinuscula && tieneNumero;
        }

        private bool ValidarEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}