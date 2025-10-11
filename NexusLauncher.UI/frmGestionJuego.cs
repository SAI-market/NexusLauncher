using System;
using System.IO;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmGestionJuego : Form
    {
        private GameService _gameService;
        private int? _gameId; // Null = nuevo juego, con valor = edición
        private bool _esEdicion;

        // Constructor para AGREGAR nuevo juego
        public frmGestionJuego()
        {
            InitializeComponent();
            _gameService = new GameService();
            _gameId = null;
            _esEdicion = false;
            this.Text = "Agregar Nuevo Juego";
        }

        // Constructor para EDITAR juego existente
        public frmGestionJuego(int gameId)
        {
            InitializeComponent();
            _gameService = new GameService();
            _gameId = gameId;
            _esEdicion = true;
            this.Text = "Editar Juego";

            CargarDatosJuego();
        }

        private void CargarDatosJuego()
        {
            try
            {
                var game = _gameService.GetGame(_gameId.Value);
                if (game != null)
                {
                    txtTitle.Text = game.Title;
                    txtInstallPath.Text = game.InstallPath;
                    chkIsInstalled.Checked = game.IsInstalled;
                }
                else
                {
                    MessageBox.Show("No se encontró el juego.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el juego:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Ejecutables (*.exe)|*.exe|Todos los archivos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar ejecutable del juego";
                openFileDialog.CheckFileExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInstallPath.Text = openFileDialog.FileName;

                    // Si el título está vacío, sugerir el nombre del archivo
                    if (string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        txtTitle.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Por favor, ingresá un título para el juego.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInstallPath.Text))
            {
                MessageBox.Show("Por favor, seleccioná la ruta del ejecutable.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnExaminar.Focus();
                return;
            }

            if (!File.Exists(txtInstallPath.Text))
            {
                var resultado = MessageBox.Show(
                    "El archivo seleccionado no existe. ¿Querés guardar de todas formas?",
                    "Advertencia",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                    return;
            }

            try
            {
                var game = new Game
                {
                    Title = txtTitle.Text.Trim(),
                    InstallPath = txtInstallPath.Text.Trim(),
                    IsInstalled = chkIsInstalled.Checked
                };

                if (_esEdicion)
                {
                    // EDITAR juego existente
                    game.Id = _gameId.Value;
                    bool actualizado = _gameService.UpdateGame(game);

                    if (actualizado)
                    {
                        MessageBox.Show("Juego actualizado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el juego.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // CREAR nuevo juego
                    int nuevoId = _gameService.CreateGame(game);

                    if (nuevoId > 0)
                    {
                        MessageBox.Show("Juego agregado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el juego.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}