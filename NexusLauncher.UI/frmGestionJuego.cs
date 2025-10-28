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

        // --- ESTE ES EL MÉTODO CORREGIDO ---
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones (Esto está perfecto como lo tienes)
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
                // El objeto 'Game' se crea correctamente
                var game = new Game
                {
                    Title = txtTitle.Text.Trim(),
                    InstallPath = txtInstallPath.Text.Trim(),
                    IsInstalled = chkIsInstalled.Checked
                    // NOTA: Tu modelo 'Game.cs' también tiene Price e Image
                    // Deberías añadirlos aquí si los tienes en el formulario
                    // Price = numPrice.Value,
                    // Image = (byte[])picImage.Tag
                };

                if (_esEdicion)
                {
                    // --- INICIO DE CORRECCIÓN ---

                    // EDITAR juego existente
                    game.Id = _gameId.Value;

                    // 1. Simplemente llamamos al método. No devuelve 'bool'.
                    _gameService.UpdateGame(game);

                    // 2. Si la línea anterior no lanzó una excepción, asumimos éxito.
                    //    Quitamos el 'if (actualizado)' y el 'else'.
                    MessageBox.Show("Juego actualizado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    // --- FIN DE CORRECCIÓN ---
                }
                else
                {
                    // --- INICIO DE CORRECCIÓN ---

                    // CREAR nuevo juego

                    // 1. Simplemente llamamos al método. No devuelve 'int'.
                    _gameService.CreateGame(game);

                    // 2. Si la línea anterior no lanzó una excepción, asumimos éxito.
                    //    Quitamos el 'if (nuevoId > 0)' y el 'else'.
                    MessageBox.Show("Juego agregado correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    // --- FIN DE CORRECCIÓN ---
                }
            }
            catch (Exception ex)
            {
                // Si la BLL o DAL fallan, la excepción se atrapa aquí.
                MessageBox.Show($"Error al guardar:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // --- FIN DEL MÉTODO CORREGIDO ---


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}