using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmVerNoticia : Form
    {
        private int newsId;
        private NewsService newsService = new NewsService();

        public frmVerNoticia(int id)
        {
            InitializeComponent();
            this.newsId = id;
        }

        private void frmVerNoticia_Load(object sender, EventArgs e)
        {
            News newsItem = newsService.GetNewsById(newsId);

            if (newsItem != null)
            {
                lblTitulo.Text = newsItem.Title;
                lblFecha.Text = newsItem.PublicationDate.ToString("dd 'de' MMMM 'de' yyyy");
                lblContenido.Text = newsItem.Content;
                this.Text = newsItem.Title; // Título de la ventana
            }
            else
            {
                MessageBox.Show("No se pudo cargar la noticia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void frmVerNoticia_Load_1(object sender, EventArgs e)
        {

        }
    }
}