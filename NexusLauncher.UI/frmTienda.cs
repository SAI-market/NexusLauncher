using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmTienda : Form
    {
        private readonly GameService _gameService;
        private List<Game> _storeGames = new List<Game>();

        public frmTienda()
        {
            InitializeComponent();

            _gameService = new GameService();

            this.Load += FrmTienda_Load;
            dgv_Tienda.SelectionChanged += Dgv_Tienda_SelectionChanged;
            dgv_Tienda.CellDoubleClick += Dgv_Tienda_CellDoubleClick;
        }

        private void FrmTienda_Load(object sender, EventArgs e)
        {
            ConfigureDataGrid();
            LoadStoreGames();
            ClearSelectionDetails();
        }

        private void ConfigureDataGrid()
        {
            dgv_Tienda.AutoGenerateColumns = false;
            dgv_Tienda.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Visible = false
            };
            dgv_Tienda.Columns.Add(colId);

            var colImg = new DataGridViewImageColumn
            {
                Name = "colImage",
                HeaderText = "Portada",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 100
            };
            dgv_Tienda.Columns.Add(colImg);

            var colTitle = new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                HeaderText = "Título",
                Width = 280
            };
            dgv_Tienda.Columns.Add(colTitle);

            var colPrice = new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Precio",
                Width = 100
            };
            dgv_Tienda.Columns.Add(colPrice);

            dgv_Tienda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Tienda.MultiSelect = false;
            dgv_Tienda.AllowUserToAddRows = false;
            dgv_Tienda.AllowUserToDeleteRows = false;
            dgv_Tienda.ReadOnly = true;
            dgv_Tienda.RowTemplate.Height = 80;
        }

        private void LoadStoreGames()
        {
            try
            {
                // <-- cambio: usar GetLibrary() sin parámetros (devuelve todos los juegos)
                _storeGames = _gameService.GetLibrary() ?? new List<Game>();

                dgv_Tienda.Rows.Clear();

                foreach (var g in _storeGames)
                {
                    Image thumb = null;
                    if (g.Image != null && g.Image.Length > 0)
                    {
                        try
                        {
                            using (var ms = new MemoryStream(g.Image))
                            {
                                thumb = Image.FromStream(ms);
                            }
                        }
                        catch
                        {
                            thumb = null;
                        }
                    }

                    var rowIndex = dgv_Tienda.Rows.Add();
                    var row = dgv_Tienda.Rows[rowIndex];
                    row.Cells["colId"].Value = g.Id;
                    row.Cells["colImage"].Value = thumb;
                    row.Cells["colTitle"].Value = g.Title;
                    row.Cells["colPrice"].Value = g.Price.ToString("C2");
                    row.Tag = g;
                }

                if (dgv_Tienda.Rows.Count > 0)
                    dgv_Tienda.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la tienda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dgv_Tienda_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Tienda.SelectedRows.Count == 0)
            {
                ClearSelectionDetails();
                return;
            }

            var row = dgv_Tienda.SelectedRows[0];
            if (row?.Tag is Game game)
            {
                ShowGameDetails(game);
            }
            else
            {
                ClearSelectionDetails();
            }
        }

        private void Dgv_Tienda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgv_Tienda.Rows[e.RowIndex];
            if (row?.Tag is Game game)
            {
                TryPurchaseGame(game);
            }
        }

        private void ShowGameDetails(Game game)
        {
            lbl_NombreJuego.Text = game.Title ?? "—";
            lbl_Precio.Text = game.Price.ToString("C2");

            if (game.Image != null && game.Image.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(game.Image))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

            var currentUser = SessionManager.Instance?.CurrentUser;
            if (currentUser == null)
            {
                btn_Comprar.Enabled = false;
            }
            else
            {
                var owned = false;
                try
                {
                    // <-- cambio: pasar el objeto User (no el id)
                    owned = _gameService.UserOwnsGame(currentUser, game.Id);
                }
                catch
                {
                    owned = false;
                }

                btn_Comprar.Enabled = !owned;
                btn_Comprar.Text = owned ? "Ya comprado" : "Comprar";
            }
        }

        private void ClearSelectionDetails()
        {
            lbl_NombreJuego.Text = "NombreJuego";
            lbl_Precio.Text = "Precio";
            pictureBox1.Image = null;
            btn_Comprar.Enabled = false;
            btn_Comprar.Text = "Comprar";
        }

        private void btn_Comprar_Click(object sender, EventArgs e)
        {
            if (dgv_Tienda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un juego para comprar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgv_Tienda.SelectedRows[0];
            if (!(row?.Tag is Game game))
            {
                MessageBox.Show("Juego inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TryPurchaseGame(game);
        }

        private void TryPurchaseGame(Game game)
        {
            var currentUser = SessionManager.Instance?.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("No hay una sesión activa. Inicie sesión para comprar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_gameService.UserOwnsGame(currentUser, game.Id))
                {
                    MessageBox.Show("Usted ya posee este juego.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Comprar.Enabled = false;
                    btn_Comprar.Text = "Ya comprado";
                    return;
                }

                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Seleccione la carpeta base donde instalar el juego (se creará una carpeta con el nombre del juego)";
                    fbd.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "NexusLauncher", "Games");
                    if (fbd.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("Instalación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string baseInstallRoot = fbd.SelectedPath;

                    string installError;
                    var ok = _gameService.InstallAndRegisterGameForUser(currentUser, game, baseInstallRoot, out installError);
                    if (!ok)
                    {
                        MessageBox.Show($"No se pudo completar la instalación/compra: {installError}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show($"Compra e instalación completadas: {game.Title}\nRuta: {game.InstallPath}", "Compra OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btn_Comprar.Enabled = false;
                    btn_Comprar.Text = "Ya comprado";

                    LoadStoreGames();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar comprar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Main_Click(object sender, EventArgs e)
        {

        }
    }
}
