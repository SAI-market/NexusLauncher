using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmPerfil : Form
    {
        private UserService userService = new UserService();
        private User currentUser;

        public frmPerfil()
        {
            InitializeComponent();
            this.currentUser = SessionManager.Instance.CurrentUser;
        }

        private void frmPerfil_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                txtUsername.Text = currentUser.NombreUsuario; // O 'Username' si así se llama en tu clase
                txtEmail.Text = currentUser.Email;
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string newPass = txtNuevaPassword.Text;
            string confirmPass = txtConfirmarPassword.Text;

            if (string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                MessageBox.Show("Por favor, completa ambos campos de contraseña.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = userService.UpdatePassword(currentUser.Id, newPass, confirmPass);

                if (success)
                {
                    MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}