using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmNoticias : Form
    {
        private readonly NoticiaService _noticiaService = new NoticiaService();
        private List<Noticia> _noticiasCache = new List<Noticia>();

        public frmNoticias()
        {
            InitializeComponent();
        }

        private void frmNoticias_Load(object sender, EventArgs e)
        {
            CargarNoticiasEnGrid();
        }

        private void CargarNoticiasEnGrid()
        {
            try
            {
                // Obtener últimas noticias (podés cambiar GetAll por GetLatest)
                _noticiasCache = _noticiaService.GetAll();

                // Proyección para el grid: Id (oculta), Titulo y Fecha (formateada)
                var rows = _noticiasCache.Select(n => new
                {
                    n.Id,
                    Titulo = n.Titulo,
                    Fecha = n.FechaPublicacion == DateTime.MinValue ? "" : n.FechaPublicacion.ToString("yyyy-MM-dd HH:mm")
                }).ToList();

                dgv_Noticias.AutoGenerateColumns = false;
                dgv_Noticias.DataSource = rows;

                // Configurar columnas (si no están creadas en el diseñador las creamos aquí)
                if (dgv_Noticias.Columns.Count == 0)
                {
                    // Columna Id (oculta)
                    var colId = new DataGridViewTextBoxColumn
                    {
                        Name = "Id",
                        DataPropertyName = "Id",
                        Visible = false
                    };
                    dgv_Noticias.Columns.Add(colId);

                    // Columna Titulo
                    var colTitulo = new DataGridViewTextBoxColumn
                    {
                        Name = "Titulo",
                        HeaderText = "Título",
                        DataPropertyName = "Titulo",
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    };
                    dgv_Noticias.Columns.Add(colTitulo);

                    // Columna Fecha
                    var colFecha = new DataGridViewTextBoxColumn
                    {
                        Name = "Fecha",
                        HeaderText = "Fecha",
                        DataPropertyName = "Fecha",
                        Width = 140,
                        DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                    };
                    dgv_Noticias.Columns.Add(colFecha);
                }
                else
                {
                    // Si las columnas ya existen sólo actualizamos el datasource visual
                    foreach (DataGridViewColumn c in dgv_Noticias.Columns)
                        c.Visible = c.Name != "Id" ? true : false;
                }

                dgv_Noticias.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando noticias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_Noticias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignorar clics en cabecera o fuera de rango
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                // Obtenemos el Id de la fila
                var row = dgv_Noticias.Rows[e.RowIndex];
                object idObj = row.Cells["Id"].Value;
                if (idObj == null || idObj == DBNull.Value) return;

                int id = Convert.ToInt32(idObj);

                // Recuperamos la noticia completa desde el servicio (no usar sólo la caché si la quieres más reciente)
                var noticia = _noticiaService.GetById(id);
                if (noticia == null)
                {
                    MessageBox.Show("No se encontró la noticia seleccionada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir el formulario de noticia abierta
                using (var frm = new frmNoticiaAbierta(noticia))
                {
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
