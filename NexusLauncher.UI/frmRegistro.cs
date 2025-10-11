using System;
using System.Windows.Forms;
using NexusLauncher.BLL;

namespace NexusLauncher.UI
{
    public partial class frmRegistro : Form
    {
        private readonly UserService _userService = new UserService();

        public frmRegistro()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Por favor, ingresá un nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, ingresá una contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Por favor, confirmá tu contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Por favor, ingresá un email.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Intentar registrar el usuario
            string errorMessage;
            bool registrado = _userService.RegisterUser(
                txtUsername.Text.Trim(),
                txtPassword.Text,
                txtDisplayName.Text.Trim(),
                txtEmail.Text.Trim(),
                out errorMessage
            );

            if (registrado)
            {
                MessageBox.Show("Usuario registrado correctamente. Ahora podés iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}