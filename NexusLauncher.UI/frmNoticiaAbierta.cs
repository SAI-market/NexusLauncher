using System;
using System.Windows.Forms;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmNoticiaAbierta : Form
    {
        private readonly Noticia _noticia;

        public frmNoticiaAbierta(Noticia noticia)
        {
            InitializeComponent();
            _noticia = noticia ?? throw new ArgumentNullException(nameof(noticia));
        }

        private void frmNoticiaAbierta_Load(object sender, EventArgs e)
        {
            MostrarNoticia();
            txtContenido.ReadOnly = true;
        }

        private void MostrarNoticia()
        {
            try
            {
                lblTitulo.Text = _noticia.Titulo ?? "(Sin título)";

                if (_noticia.FechaPublicacion == DateTime.MinValue)
                    lblFecha.Text = "";
                else
                    lblFecha.Text = _noticia.FechaPublicacion.ToString("yyyy-MM-dd HH:mm");

                txtContenido.Text = _noticia.Contenido ?? "";
                txtContenido.SelectionStart = 0;
                txtContenido.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error mostrando noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
