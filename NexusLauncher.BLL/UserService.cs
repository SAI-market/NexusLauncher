using NexusLauncher.DAL;
using NexusLauncher.Models;
using System;
using System.Text.RegularExpressions;

namespace NexusLauncher.BLL
{
    public class UserService
    {
        private readonly UserDAL _userDal = new UserDAL();

        // Autentica y devuelve user via out
        public bool Authenticate(string username, string password, out User user)
        {
            user = _userDal.GetByUsername(username);

            if (user == null)
                return false;

            // Comparación directa (recomendado cambiar por hash en el futuro)
            if (user.Password == password)
                return true;

            user = null;
            return false;
        }

        // <<< ESTE método es el que llama frmRegistro y debe existir exactamente así >>>
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

            // Verificar existencia
            if (_userDal.UsernameExists(username))
            {
                errorMessage = "El nombre de usuario ya existe.";
                return false;
            }
            if (_userDal.EmailExists(email))
            {
                errorMessage = "El email ya está registrado.";
                return false;
            }

            // Crear nuevo usuario
            var newUser = new User
            {
                Username = username.Trim(),
                Password = password, // considerar hashing en el futuro
                DisplayName = string.IsNullOrWhiteSpace(displayName) ? username.Trim() : displayName.Trim(),
                Email = email.Trim(),
                Admin = false
            };

            int userId = _userDal.CreateUser(newUser);
            if (userId > 0) return true;

            errorMessage = "Error al crear el usuario en la base de datos.";
            return false;
        }

        // Otros wrappers/funciones
        public User GetById(int id) => _userDal.GetById(id);

        public bool UpdateProfile(int userId, string newUsername, string newDisplayName, string newEmail, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (userId <= 0) { errorMessage = "Usuario inválido."; return false; }
            if (string.IsNullOrWhiteSpace(newUsername)) { errorMessage = "El nombre de usuario no puede estar vacío."; return false; }
            if (newUsername.Length < 3) { errorMessage = "El nombre de usuario debe tener al menos 3 caracteres."; return false; }
            if (string.IsNullOrWhiteSpace(newEmail)) { errorMessage = "El email no puede estar vacío."; return false; }
            if (!ValidarEmail(newEmail)) { errorMessage = "El formato del email no es válido."; return false; }

            if (_userDal.UsernameExists(newUsername, userId)) { errorMessage = "El nombre de usuario ya está en uso."; return false; }
            if (_userDal.EmailExists(newEmail, userId)) { errorMessage = "El email ya está registrado por otro usuario."; return false; }

            bool ok = _userDal.UpdateProfile(userId, newUsername.Trim(), newDisplayName?.Trim(), newEmail.Trim());
            if (!ok) errorMessage = "No se pudo guardar la información en la base de datos.";
            return ok;
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (userId <= 0) { errorMessage = "Usuario inválido."; return false; }
            if (string.IsNullOrWhiteSpace(newPassword)) { errorMessage = "La nueva contraseña no puede estar vacía."; return false; }
            if (!ValidarContraseñaSegura(newPassword)) { errorMessage = "La contraseña debe tener al menos 6 caracteres, incluir una mayúscula, una minúscula y un número."; return false; }

            var user = _userDal.GetById(userId);
            if (user == null) { errorMessage = "Usuario no encontrado."; return false; }

            if (user.Password != currentPassword) { errorMessage = "La contraseña actual es incorrecta."; return false; }

            bool ok = _userDal.UpdatePassword(userId, newPassword);
            if (!ok) errorMessage = "No se pudo actualizar la contraseña en la base de datos.";
            return ok;
        }

        public bool ChangeUsername(int userId, string newUsername, out string errorMessage)
        {
            var user = _userDal.GetById(userId);
            if (user == null) { errorMessage = "Usuario no encontrado."; return false; }
            return UpdateProfile(userId, newUsername, user.DisplayName, user.Email, out errorMessage);
        }

        // Validaciones auxiliares
        private bool ValidarContraseñaSegura(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6) return false;
            bool may = false, min = false, num = false;
            foreach (char c in password) { if (char.IsUpper(c)) may = true; if (char.IsLower(c)) min = true; if (char.IsDigit(c)) num = true; }
            return may && min && num;
        }

        private bool ValidarEmail(string email)
        {
            try { var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); return regex.IsMatch(email); }
            catch { return false; }
        }
        public User GetCurrentUser()
        {
            // Usamos el SessionManager (ya presente en tu proyecto) como fuente de verdad para el usuario actual.
            // Esto evita duplicar estado en múltiples servicios.
            return SessionManager.Instance?.CurrentUser;
        }
        public bool SetStatus(string status)
        {
            var user = SessionManager.Instance?.CurrentUser;
            if (user == null) return false;

            // Actualizamos el estado en memoria (se reflejará en la UI cuando se recupere el usuario)
            user.CurrentStatus = status;

            // Intentamos persistir en BD si existe un mecanismo (opcional).
            // No llamamos a métodos del DAL por seguridad aquí porque tu DAL puede no tener un UpdateStatus.
            // Si querés persistir, añade un método en UserDAL (p.ej. UpdateStatus(userId, status)) y llámalo aquí.
            return true;
        }
    }
}
