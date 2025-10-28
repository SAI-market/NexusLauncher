using NexusLauncher.BLL;
using NexusLauncher.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmBiblioteca : Form
    {
        private GameService gameService = new GameService();

        public frmBiblioteca()
        {
            InitializeComponent();
        }

        private void frmBiblioteca_Load(object sender, System.EventArgs e)
        {
            LoadUserLibrary();
        }

        private void LoadUserLibrary()
        {
            int userId = SessionManager.Instance.CurrentUser.Id;
            List<Game> myGames = gameService.GetGamesByUserId(userId);

            dgvMisJuegos.DataSource = null;
            dgvMisJuegos.DataSource = myGames;

            // CORREGIDO: Usar 'Title' e 'InstallPath'
            dgvMisJuegos.Columns["Id"].Visible = false;
            dgvMisJuegos.Columns["InstallPath"].Visible = false; // Ocultar la ruta
            dgvMisJuegos.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Ocultar las otras columnas que no queremos ver
            dgvMisJuegos.Columns["IsInstalled"].Visible = false;
            dgvMisJuegos.Columns["PurchaseDate"].Visible = false;
            dgvMisJuegos.Columns["Owned"].Visible = false;
            dgvMisJuegos.Columns["Price"].Visible = false;
            dgvMisJuegos.Columns["Image"].Visible = false;
            dgvMisJuegos.Columns["ImageFileName"].Visible = false;
            dgvMisJuegos.Columns["ImageContentType"].Visible = false;
        }

        private void btnJugar_Click(object sender, System.EventArgs e)
        {
            if (dgvMisJuegos.CurrentRow != null)
            {
                Game selectedGame = (Game)dgvMisJuegos.CurrentRow.DataBoundItem;
                try
                {
                    // CORREGIDO: Usar 'InstallPath'
                    System.Diagnostics.Process.Start(selectedGame.InstallPath);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"No se pudo iniciar el juego: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}