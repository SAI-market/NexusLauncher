using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NexusLauncher.BLL;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmAdminNoticias : Form
    {
        private readonly NoticiaService _service = new NoticiaService();
        private List<Noticia> _noticias = new List<Noticia>();

        public frmAdminNoticias()
        {
            InitializeComponent();
        }

        private void frmAdminNoticias_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarNoticias();
        }

        private void ConfigurarDataGridView()
        {
            dgv_Noticias.AutoGenerateColumns = false;
            dgv_Noticias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Noticias.MultiSelect = false;
            dgv_Noticias.Columns.Clear();

            // Id oculta
            dgv_Noticias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            // Titulo
            dgv_Noticias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Fecha
            dgv_Noticias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaPublicacion",
                HeaderText = "Fecha",
                DataPropertyName = "FechaPublicacion",
                Width = 160,
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm", Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv_Noticias.ClearSelection();
        }

        private void CargarNoticias()
        {
            try
            {
                _noticias = _service.GetAll() ?? new List<Noticia>();
                dgv_Noticias.DataSource = null;
                dgv_Noticias.DataSource = _noticias;
                dgv_Noticias.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando noticias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? GetSelectedNoticiaId()
        {
            if (dgv_Noticias.SelectedRows.Count == 0) return null;
            var val = dgv_Noticias.SelectedRows[0].Cells["Id"].Value;
            if (val == null || val == DBNull.Value) return null;
            return Convert.ToInt32(val);
        }

        private void dgv_Noticias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // seleccion automática ya ocurre, no hace falta más aquí; este handler queda para futuras necesidades
        }

        private void btn_AgregarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                var nueva = new Noticia();
                using (var editor = new frmEditorNoticia(nueva))
                {
                    if (editor.ShowDialog(this) == DialogResult.OK)
                    {
                        CargarNoticias();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_EditarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                var id = GetSelectedNoticiaId();
                if (id == null)
                {
                    MessageBox.Show("Por favor, seleccioná una noticia para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var noticia = _service.GetById(id.Value);
                if (noticia == null)
                {
                    MessageBox.Show("No se encontró la noticia seleccionada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarNoticias();
                    return;
                }

                using (var editor = new frmEditorNoticia(noticia))
                {
                    if (editor.ShowDialog(this) == DialogResult.OK)
                    {
                        CargarNoticias();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_QuitarNoticia_Click(object sender, EventArgs e)
        {
            try
            {
                var id = GetSelectedNoticiaId();
                if (id == null)
                {
                    MessageBox.Show("Por favor, seleccioná una noticia para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var titulo = dgv_Noticias.SelectedRows[0].Cells["Titulo"].Value?.ToString() ?? "la noticia";
                var confirm = MessageBox.Show($"¿Estás seguro de eliminar '{titulo}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                bool ok = _service.Delete(id.Value);
                if (ok)
                {
                    MessageBox.Show("Noticia eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarNoticias();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la noticia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar noticia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
