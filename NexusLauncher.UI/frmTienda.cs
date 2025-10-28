using NexusLauncher.BLL;
using NexusLauncher.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmTienda : Form
    {
        private GameService gameService = new GameService();

        public frmTienda()
        {
            InitializeComponent();
        }

        private void frmTienda_Load(object sender, System.EventArgs e)
        {
            LoadAllGames();
        }

        private void LoadAllGames()
        {
            List<Game> allGames = gameService.GetAllGames();
            dgvTienda.DataSource = null;
            dgvTienda.DataSource = allGames;

            // CORREGIDO: Ocultar columnas según tu modelo
            dgvTienda.Columns["Id"].Visible = false;
            dgvTienda.Columns["InstallPath"].Visible = false;
            dgvTienda.Columns["IsInstalled"].Visible = false;
            dgvTienda.Columns["PurchaseDate"].Visible = false;
            dgvTienda.Columns["Owned"].Visible = false;
            dgvTienda.Columns["Image"].Visible = false;
            dgvTienda.Columns["ImageFileName"].Visible = false;
            dgvTienda.Columns["ImageContentType"].Visible = false;

            // Dejar visibles 'Title' y 'Price'
            dgvTienda.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvTienda.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void btnComprar_Click(object sender, System.EventArgs e)
        {
            if (dgvTienda.CurrentRow != null)
            {
                Game selectedGame = (Game)dgvTienda.CurrentRow.DataBoundItem;
                User currentUser = SessionManager.Instance.CurrentUser;

                // CORREGIDO: Usar 'Title'
                DialogResult result = MessageBox.Show($"¿Deseas comprar {selectedGame.Title}?", "Confirmar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = gameService.BuyGame(currentUser.Id, selectedGame.Id);

                    if (success)
                    {
                        // CORREGIDO: Usar 'Title'
                        MessageBox.Show($"{selectedGame.Title} se ha añadido a tu biblioteca.", "Compra Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ya posees este juego o hubo un error.", "Error en la Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un juego para comprar.", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}