using System;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmLogin : Form
    {
        private readonly UserService _userService = new UserService();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Por favor, ingresá tu nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, ingresá tu contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (_userService.Authenticate(txtUsername.Text.Trim(), txtPassword.Text, out User user))
            {
                // Guardar sesión (ya lo tenías)
                SessionManager.Instance.Login(user);

                // Si NO es admin, abrir Main directamente
                if (!user.Admin)
                {
                    var main = new frmMain();
                    main.Show();
                    this.Hide();
                    return;
                }

                // Si es admin, pedir que elija cómo entrar
                using (var elegir = new frmElegirUserOAdmin(user))
                {
                    // mostramos modal y le pasamos this como owner
                    var result = elegir.ShowDialog(this);

                    if (result == DialogResult.OK) // Entrar como user normal
                    {
                        var main = new frmMain();
                        main.Show();
                        this.Hide();
                        return;
                    }
                    else if (result == DialogResult.Yes) // Entrar como admin -> Biblioteca general
                    {
                        var biblioteca = new frmBibliotecaGeneral();
                        biblioteca.Show();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        // Canceló o cerró la ventana de elección: quedarse en login
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Credenciales inválidas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            var frmRegistro = new frmRegistro();
            frmRegistro.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
