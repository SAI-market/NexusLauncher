using System;
using System.Windows.Forms;
using NexusLauncher.Models;
using NexusLauncher.BLL;

namespace NexusLauncher.UI
{
    public partial class frmMain : Form
    {
        private readonly GameService _gameService;

        public frmMain()
        {
            InitializeComponent();
            _gameService = new GameService();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Verificar que haya una sesión activa
            if (!SessionManager.Instance.IsLoggedIn())
            {
                MessageBox.Show("No hay una sesión activa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Obtener el usuario actual desde el SessionManager
            var currentUser = SessionManager.Instance.CurrentUser;

            // Bienvenida al usuario
            lblWelcome.Text = $"Bienvenido, {currentUser.DisplayName ?? currentUser.Username}";

            // Cargar la biblioteca de juegos
            LoadGames();
        }

        private void LoadGames()
        {
            try
            {
                var games = _gameService.GetLibrary();
                listBoxGames.Items.Clear();

                foreach (var g in games)
                {
                    listBoxGames.Items.Add($"{g.Title} - Instalado: {(g.IsInstalled ? "Sí" : "No")}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar juegos: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBiblioteca frmBiblio = new frmBiblioteca();
            frmBiblio.ShowDialog();

        }
    }
}
