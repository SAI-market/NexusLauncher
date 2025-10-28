using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmMain : Form
    {
        private readonly GameService _gameService;

        public frmMain()
        {
            InitializeComponent();

            _gameService = new GameService();

            // Si el diseñador no conectó los eventos, los enlazamos aquí para seguridad
            this.Load += frmMain_Load;

            // Intentamos enlazar eventos de controles (si existen)
            if (this.Controls["btnBiblioteca"] is Button btnBiblio)
                btnBiblio.Click += btnTienda_Click;
            if (this.Controls["dgvGames"] is DataGridView dgv)
                dgv.CellDoubleClick += dgvGames_CellDoubleClick;
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
            if (this.Controls["lblWelcome"] is Label lbl)
                lbl.Text = $"Bienvenido, {currentUser.DisplayName ?? currentUser.Username}. Sus juegos:";

            // Configurar y cargar la biblioteca del usuario
            if (this.Controls["dgvGames"] is DataGridView)
                ConfigurarDataGridView();

            LoadGamesForCurrentUser();
        }

        private void ConfigurarDataGridView()
        {
            var dgv = this.Controls["dgvGames"] as DataGridView;
            if (dgv == null) return;

            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // Columna ID (oculta)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "colId",
                Visible = false
            });

            // Título
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Título",
                Name = "colTitle",
                Width = 250
            });

            // Ruta de instalación
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InstallPath",
                HeaderText = "Ruta",
                Name = "colInstallPath",
                Width = 300
            });

            // Instalado (checkbox)
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsInstalled",
                HeaderText = "Instalado",
                Name = "colIsInstalled",
                Width = 80
            });

            // Fecha de compra (opcional)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PurchaseDate",
                HeaderText = "Fecha compra",
                Name = "colPurchaseDate",
                Width = 140
            });

            // Owned (opcional)
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Owned",
                HeaderText = "Owned",
                Name = "colOwned",
                Width = 60
            });

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
        }

        private void LoadGamesForCurrentUser()
        {
            try
            {
                var currentUser = SessionManager.Instance.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Usuario no encontrado en la sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var games = _gameService.GetLibrary(currentUser) ?? new List<Game>();

                var dgv = this.Controls["dgvGames"] as DataGridView;
                if (dgv != null)
                {
                    dgv.DataSource = null;
                    dgv.DataSource = games;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar juegos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Doble clic en fila -> acción sobre el juego (placeholder: mostrar detalles / ejecutar)
        private void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null) return;
            if (e.RowIndex < 0) return;

            var item = dgv.Rows[e.RowIndex].DataBoundItem as Game;
            if (item == null) return;

            // Aquí podés lanzar el ejecutable o abrir la gestión del juego.
            // Por ahora mostramos detalles.
            MessageBox.Show($"Juego: {item.Title}\nRuta: {item.InstallPath ?? "(no disponible)"}\nInstalado: {(item.IsInstalled ? "Sí" : "No")}",
                "Detalles del juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botón: abrir la tienda
        private void btnTienda_Click(object sender, EventArgs e)
        {
            using (var frmTienda = new frmTienda())
            {
                frmTienda.ShowDialog();
            }

            // Recargar por si se agregaron/quitaron/editaron juegos
            LoadGamesForCurrentUser();
        }

        private void imagenPerfil_Click(object sender, EventArgs e)
        {

        }

        private void btn_Noticias_Click(object sender, EventArgs e)
        {
            using (var fremNoticias = new frmNoticias())
            {
                fremNoticias.ShowDialog();
            }
        }
    }
}
