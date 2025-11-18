using System;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmEditarPerfil : Form
    {
        private readonly UserService _userService = new UserService();
        private User _currentUser;

        public frmEditarPerfil()
        {
            InitializeComponent();
        }

        private void frmEditarPerfil_Load(object sender, EventArgs e)
        {
            // Tomamos los datos desde la sesión
            _currentUser = SessionManager.Instance?.CurrentUser;
            if (_currentUser == null)
            {
                MessageBox.Show("No hay usuario logueado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Rellenar controles
            txtNombrePublico.Text = _currentUser.DisplayName;
            txtMail.Text = _currentUser.Email;

            // Asegurarse de que los campos de contraseña estén vacíos
            txtContraseñaActual.Text = string.Empty;
            txtContraseñaNueva.Text = string.Empty;
            txtContraseñaNuevaConfirm.Text = string.Empty;
        }

        // Cambiar display name (btnNombrePublico)
        private void btnNombrePublico_Click(object sender, EventArgs e)
        {
            string nuevoDisplay = txtNombrePublico.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nuevoDisplay))
            {
                MessageBox.Show("El nombre público no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nuevoDisplay == _currentUser.DisplayName)
            {
                MessageBox.Show("No cambiaste el nombre público.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_userService.UpdateProfile(_currentUser.Id, _currentUser.Username, nuevoDisplay, _currentUser.Email, out string err))
            {
                _currentUser.DisplayName = nuevoDisplay;
                SessionManager.Instance.Login(_currentUser);
                MessageBox.Show("Nombre público actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombrePublico.Text = _currentUser.DisplayName;
            }
        }

        // Cambiar email (btnMail)
        private void btnMail_Click(object sender, EventArgs e)
        {
            string nuevoEmail = txtMail.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nuevoEmail))
            {
                MessageBox.Show("El email no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nuevoEmail == _currentUser.Email)
            {
                MessageBox.Show("No cambiaste el email.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_userService.UpdateProfile(_currentUser.Id, _currentUser.Username, _currentUser.DisplayName, nuevoEmail, out string err))
            {
                _currentUser.Email = nuevoEmail;
                SessionManager.Instance.Login(_currentUser);
                MessageBox.Show("Email actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMail.Text = _currentUser.Email;
            }
        }

        // Cambiar contraseña (btnContraseña)
        private void btnContraseña_Click(object sender, EventArgs e)
        {
            string actual = txtContraseñaActual.Text ?? string.Empty;
            string nueva = txtContraseñaNueva.Text ?? string.Empty;
            string confirm = txtContraseñaNuevaConfirm.Text ?? string.Empty;

            if (string.IsNullOrEmpty(actual) || string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Por favor completá todos los campos de contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nueva != confirm)
            {
                MessageBox.Show("La nueva contraseña y su confirmación no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_userService.ChangePassword(_currentUser.Id, actual, nueva, out string err))
            {
                // Opcional: actualizar el User.Password en sesión
                _currentUser.Password = nueva;
                SessionManager.Instance.Login(_currentUser);

                MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                txtContraseñaActual.Text = string.Empty;
                txtContraseñaNueva.Text = string.Empty;
                txtContraseñaNuevaConfirm.Text = string.Empty;
            }
            else
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
