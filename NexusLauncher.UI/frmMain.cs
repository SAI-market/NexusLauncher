using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmMain : Form
    {
        private User loggedInUser;
        private NewsService newsService = new NewsService(); // Usar NewsService

        public frmMain(User user)
        {
            InitializeComponent();
            this.loggedInUser = user;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Bienvenido, {loggedInUser.NombreUsuario}";
            ConfigureAdminVisibility();
            //LoadNews();
        }

        private void ConfigureAdminVisibility()
        {
        
            btnAdminNoticias.Visible = loggedInUser.IsAdmin;
            btnGestionJuegos.Visible = loggedInUser.IsAdmin;
        }

        private void LoadNews()
        {
            flpNoticias.Controls.Clear();
            List<News> newsList = newsService.GetLatestNews(5); // Traer 5 noticias

            foreach (var newsItem in newsList)
            {
                Label lblNewsTitle = new Label();
                lblNewsTitle.Text = newsItem.Title; // Usar Title
                lblNewsTitle.Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
                lblNewsTitle.AutoSize = true;
                lblNewsTitle.Cursor = Cursors.Hand;
                lblNewsTitle.ForeColor = Color.Blue;
                lblNewsTitle.Tag = newsItem.ID;
                lblNewsTitle.Click += LblNewsTitle_Click;

                Label lblNewsDate = new Label();
                lblNewsDate.Text = newsItem.PublicationDate.ToShortDateString(); // Usar PublicationDate
                lblNewsDate.Font = new Font(this.Font.FontFamily, 8, FontStyle.Italic);
                lblNewsDate.AutoSize = true;
                lblNewsDate.Margin = new Padding(3, 0, 3, 10);

                flpNoticias.Controls.Add(lblNewsTitle);
                flpNoticias.Controls.Add(lblNewsDate);
            }
        }

        private void LblNewsTitle_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                int newsId = (int)clickedLabel.Tag;
                frmVerNoticia viewNewsForm = new frmVerNoticia(newsId);
                viewNewsForm.ShowDialog();
            }
        }

        private void btnBiblioteca_Click(object sender, EventArgs e)
        {
            frmBiblioteca bibliotecaForm = new frmBiblioteca();
            bibliotecaForm.ShowDialog();
        }

        private void btnTienda_Click(object sender, EventArgs e)
        {
            frmTienda tiendaForm = new frmTienda();
            tiendaForm.ShowDialog();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            frmPerfil perfilForm = new frmPerfil();
            perfilForm.ShowDialog();
        }

        private void btnAdminNoticias_Click(object sender, EventArgs e)
        {
            //frmAdminNoticias adminNoticiasForm = new frmAdminNoticias();
            //adminNoticiasForm.ShowDialog();
            //LoadNews(); // Recargar noticias
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SessionManager.Instance.Logout();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}