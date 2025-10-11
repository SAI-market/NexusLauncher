using System;
using System.Windows.Forms;
using NexusLauncher.Models;
using NexusLauncher.BLL;

namespace NexusLauncher.UI
{
    public partial class frmMain : Form
    {
        private readonly User _currentUser;
        private readonly GameService _gameService;

        public frmMain(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _gameService = new GameService();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Bienvenida al usuario
            lblWelcome.Text = $"Bienvenido, {_currentUser.DisplayName ?? _currentUser.Username}";

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
