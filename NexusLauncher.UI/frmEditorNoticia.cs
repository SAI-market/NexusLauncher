using System;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmEditorNoticia : Form
    {
        private readonly NoticiaService _service = new NoticiaService();
        private Noticia _noticia;
        private bool _esNuevo;

        // Constructor para nuevo o editar (si noticia.Id == 0 se considera nueva)
        public frmEditorNoticia(Noticia noticia)
        {
            InitializeComponent();
            _noticia = noticia ?? throw new ArgumentNullException(nameof(noticia));
            _esNuevo = _noticia.Id <= 0;
        }

        // Mantengo el constructor sin parámetros por compatibilidad (no recomendado usarlo)
        public frmEditorNoticia() : this(new Noticia())
        {
        }

        private void frmEditorNoticia_Load(object sender, EventArgs e)
        {
            // Asegurarse controles editables para crear/editar
            txtTitulo.ReadOnly = false;
            txtContenido.ReadOnly = false;

            // Rellenar campos si existe noticia
            txtTitulo.Text = _noticia.Titulo ?? string.Empty;
            txtContenido.Text = _noticia.Contenido ?? string.Empty;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = txtTitulo.Text?.Trim();
                string contenido = txtContenido.Text?.Trim();

                if (string.IsNullOrWhiteSpace(titulo))
                {
                    MessageBox.Show("El título no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTitulo.Focus();
                    return;
                }

                // Actualizar el objeto
                _noticia.Titulo = titulo;
                _noticia.Contenido = contenido;
                if (_esNuevo)
                {
                    _noticia.FechaPublicacion = DateTime.Now;
                    int newId = _service.Create(_noticia);
                    if (newId > 0)
                    {
                        _noticia.Id = newId;
                        MessageBox.Show("Noticia creada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo crear la noticia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Para edición, no sobrescribimos la FechaPublicacion salvo que quieras permitirlo.
                    bool ok = _service.Update(_noticia);
                    if (ok)
                    {
                        MessageBox.Show("Noticia actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la noticia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
