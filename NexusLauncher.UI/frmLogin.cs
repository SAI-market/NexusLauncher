using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmLogin : Form
    {
        private UserService userService = new UserService();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User user = userService.Login(username, password);

            if (user != null)
            {
                SessionManager.Instance.Login(user);

                frmMain mainForm = new frmMain(user);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegistro registroForm = new frmRegistro();
            registroForm.ShowDialog();
        }
    }
}