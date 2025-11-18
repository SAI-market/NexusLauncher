using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmAmigos : Form
    {
        private readonly FriendService _friendService;
        private List<FriendViewModel> _allUsers;
        private List<FriendRequestDto> _receivedRequests;

        public frmAmigos()
        {
            InitializeComponent();
            _friendService = new FriendService();

            this.Load += FrmAmigos_Load;

            // enlazamos eventos de selección
            dgv_Users.SelectionChanged += Dgv_Users_SelectionChanged;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void FrmAmigos_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadReceivedRequests();
        }

        private void LoadUsers()
        {
            try
            {
                var current = SessionManager.Instance.CurrentUser;
                if (current == null) return;

                _allUsers = _friendService.GetAllUsersWithFriendStatus(current.Id);

                // asignamos data source
                dgv_Users.DataSource = null;
                dgv_Users.AutoGenerateColumns = false;
                dgv_Users.Columns.Clear();

                dgv_Users.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Name = "colUserId", Visible = false });
                dgv_Users.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DisplayName", HeaderText = "DisplayName", Name = "colUserDisplay", Width = 200 });
                dgv_Users.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Username", HeaderText = "Username", Name = "colUserName", Width = 150 });
                dgv_Users.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FriendStatus", HeaderText = "Estado Relación", Name = "colUserFriendStatus", Width = 120 });

                dgv_Users.DataSource = _allUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message);
            }
        }

        private void LoadReceivedRequests()
        {
            try
            {
                var current = SessionManager.Instance.CurrentUser;
                if (current == null) return;

                _receivedRequests = _friendService.GetReceivedRequests(current.Id);

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FriendshipId", HeaderText = "Id", Visible = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FromDisplayName", HeaderText = "De", Width = 200 });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FromUserId", HeaderText = "UserId", Visible = false });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedAt", HeaderText = "Fecha", Width = 160 });

                dataGridView1.DataSource = _receivedRequests;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando solicitudes: " + ex.Message);
            }
        }

        private void Dgv_Users_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Users.SelectedRows.Count == 0)
            {
                lbl_UsuarioElegido.Text = "Usuario elegido:";
                return;
            }

            var row = dgv_Users.SelectedRows[0];
            var vm = row.DataBoundItem as FriendViewModel;
            if (vm == null) return;

            lbl_UsuarioElegido.Text = $"Usuario elegido: \"{(string.IsNullOrWhiteSpace(vm.DisplayName) ? vm.Username : vm.DisplayName)}\"";
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                lbl_SolicitudUsuario.Text = "Usuario elegido:";
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            var dto = row.DataBoundItem as FriendRequestDto;
            if (dto == null) return;

            lbl_SolicitudUsuario.Text = $"Usuario elegido: \"{dto.FromDisplayName}\"";
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
        {
            if (dgv_Users.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para enviar la solicitud.");
                return;
            }

            var vm = dgv_Users.SelectedRows[0].DataBoundItem as FriendViewModel;
            if (vm == null) return;

            var current = SessionManager.Instance.CurrentUser;
            if (current == null) return;

            // No permitir enviar a quien ya es amigo o ya tiene solicitud
            if (vm.FriendStatus == "Friend")
            {
                MessageBox.Show("Ya es tu amigo.");
                return;
            }
            if (vm.FriendStatus == "PendingSent")
            {
                MessageBox.Show("Ya enviaste una solicitud a este usuario.");
                return;
            }
            if (vm.FriendStatus == "PendingReceived")
            {
                MessageBox.Show("Este usuario te envió una solicitud. Aceptala desde Solicitudes recibidas.");
                return;
            }

            var ok = _friendService.SendFriendRequest(current.Id, vm.Id);
            if (ok)
            {
                MessageBox.Show("Solicitud enviada.");
            }
            else
            {
                MessageBox.Show("No se pudo enviar la solicitud (posible duplicado).");
            }

            // refrescar lista de usuarios
            LoadUsers();
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una solicitud para aceptar.");
                return;
            }

            var dto = dataGridView1.SelectedRows[0].DataBoundItem as FriendRequestDto;
            if (dto == null) return;

            var current = SessionManager.Instance.CurrentUser;
            if (current == null) return;

            var ok = _friendService.AcceptRequest(dto.FriendshipId, current.Id);
            if (ok)
            {
                MessageBox.Show("Solicitud aceptada.");
            }
            else
            {
                MessageBox.Show("No se pudo aceptar la solicitud.");
            }

            LoadReceivedRequests();
            LoadUsers(); // actualizar estados
        }

        private void btn_Rechazar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una solicitud para rechazar.");
                return;
            }

            var dto = dataGridView1.SelectedRows[0].DataBoundItem as FriendRequestDto;
            if (dto == null) return;

            var current = SessionManager.Instance.CurrentUser;
            if (current == null) return;

            var ok = _friendService.RejectRequest(dto.FriendshipId, current.Id);
            if (ok)
            {
                MessageBox.Show("Solicitud rechazada.");
            }
            else
            {
                MessageBox.Show("No se pudo rechazar la solicitud.");
            }

            LoadReceivedRequests();
            LoadUsers();
        }
    }
}
