using System;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmBibliotecaGeneral : Form
    {
        private GameService _gameService;

        public frmBibliotecaGeneral()
        {
            InitializeComponent();
            _gameService = new GameService();

            ConfigurarDataGridView();
            CargarJuegos();
        }

        private void ConfigurarDataGridView()
        {
            // Limpiar columnas auto-generadas
            dgvGames.AutoGenerateColumns = false;
            dgvGames.Columns.Clear();

            // Columna ID (oculta pero necesaria)
            dgvGames.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "colId",
                Visible = false
            });

            // Columna Título
            dgvGames.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Título del Juego",
                Name = "colTitle",
                Width = 250
            });

            // Columna Ruta de Instalación
            dgvGames.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InstallPath",
                HeaderText = "Ruta de Instalación",
                Name = "colInstallPath",
                Width = 350
            });

            // Columna Instalado (Checkbox)
            dgvGames.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsInstalled",
                HeaderText = "Instalado",
                Name = "colIsInstalled",
                Width = 80
            });
        }

        private void CargarJuegos()
        {
            try
            {
                var games = _gameService.GetLibrary();
                dgvGames.DataSource = null; // Limpiar
                dgvGames.DataSource = games;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los juegos:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmGestion = new frmGestionJuego();
            if (frmGestion.ShowDialog() == DialogResult.OK)
            {
                CargarJuegos(); // Recargar la lista después de agregar
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccioná un juego para editar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el ID del juego seleccionado
            int gameId = Convert.ToInt32(dgvGames.SelectedRows[0].Cells["colId"].Value);

            var frmGestion = new frmGestionJuego(gameId);
            if (frmGestion.ShowDialog() == DialogResult.OK)
            {
                CargarJuegos(); // Recargar después de editar
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGames.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccioná un juego para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string titulo = dgvGames.SelectedRows[0].Cells["colTitle"].Value?.ToString();

            var confirmacion = MessageBox.Show(
                $"¿Estás seguro de que querés eliminar '{titulo}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int gameId = Convert.ToInt32(dgvGames.SelectedRows[0].Cells["colId"].Value);
                    bool eliminado = _gameService.DeleteGame(gameId);

                    if (eliminado)
                    {
                        MessageBox.Show("Juego eliminado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarJuegos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el juego.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar:\n{ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Noticias_Click(object sender, EventArgs e)
        {
            try
            {
                bool isAdminMode = false;
                var currentUser = SessionManager.Instance.CurrentUser;
                bool isAdminUser = currentUser != null && currentUser.Admin;

                if (isAdminMode || isAdminUser)
                {
                    using (var adminForm = new frmAdminNoticias())
                    {
                        adminForm.ShowDialog(this);
                    }
                }
                else
                {
                    using (var frm = new frmNoticias())
                    {
                        frm.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir noticias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}