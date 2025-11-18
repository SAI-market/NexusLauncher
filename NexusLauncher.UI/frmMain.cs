using NexusLauncher.BLL;
using NexusLauncher.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NexusLauncher.UI
{
    public partial class frmMain : Form
    {
        private readonly GameService _gameService;
        private readonly FriendService _friendService;
        private System.Windows.Forms.Timer presenceTimer;

        // Estado local de reproducción
        private bool _isPlaying = false;
        private string _playingTitle = null;

        public frmMain()
        {
            InitializeComponent();

            _gameService = new GameService();
            _friendService = new FriendService();

            // Si el diseñador no conectó los eventos, los enlazamos aquí para seguridad
            this.Load += frmMain_Load;

            // Intentamos enlazar eventos de controles (si existen)
            if (this.Controls["btn_Tienda"] is Button btnBiblio)
                btnBiblio.Click += btnTienda_Click;
            if (this.Controls["dgvGames"] is DataGridView dgv)
                dgv.CellDoubleClick += dgvGames_CellDoubleClick;

            // Configurar dgv_Amigos si ya existe
            var dgvFriendsCandidate = FindControlRecursive<DataGridView>("dgv_Amigos");
            if (dgvFriendsCandidate != null)
            {
                ConfigureDgvFriends(dgvFriendsCandidate);
                dgvFriendsCandidate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFriendsCandidate.MultiSelect = false;
                dgvFriendsCandidate.ReadOnly = true;
                dgvFriendsCandidate.AllowUserToAddRows = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Verificar que haya una sesión activa
            if (!SessionManager.Instance.IsLoggedIn())
            {
                MessageBox.Show("No hay una sesión activa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Obtener el usuario actual desde el SessionManager
            var currentUser = SessionManager.Instance.CurrentUser;

            // Bienvenida al usuario (buscamos recursivamente)
            var lblWelcome = FindControlRecursive<Label>("lblWelcome");
            if (lblWelcome != null)
                lblWelcome.Text = $"Bienvenido, {currentUser.DisplayName ?? currentUser.Username}. Sus juegos:";

            // Actualizar labels de perfil
            UpdateUserProfileLabels();

            // Configurar y cargar la biblioteca del usuario
            if (FindControlRecursive<DataGridView>("dgvGames") != null)
                ConfigurarDataGridView();

            LoadGamesForCurrentUser();

            // Cargar amigos en el DataGridView
            LoadFriends();

            // Iniciar timer que simula presencia (cada 5s)
            presenceTimer = new System.Windows.Forms.Timer();
            presenceTimer.Interval = 5000;
            presenceTimer.Tick += PresenceTimer_Tick;
            presenceTimer.Start();
        }

        #region Helper buscar controles recursivamente

        // Busca un control por nombre de manera recursiva en el form.
        private T FindControlRecursive<T>(string name) where T : Control
        {
            return FindControlRecursiveInternal(this, name) as T;
        }

        private Control FindControlRecursiveInternal(Control parent, string name)
        {
            if (parent == null) return null;
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))
                    return c;
                var found = FindControlRecursiveInternal(c, name);
                if (found != null) return found;
            }
            return null;
        }

        #endregion

        #region DataGridView Juegos

        private void ConfigurarDataGridView()
        {
            var dgv = FindControlRecursive<DataGridView>("dgvGames");
            if (dgv == null) return;

            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // Columna ID (oculta)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "colId",
                Visible = false
            });

            // Título
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Título",
                Name = "colTitle",
                Width = 250
            });

            // Ruta de instalación
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InstallPath",
                HeaderText = "Ruta",
                Name = "colInstallPath",
                Width = 300
            });

            // Instalado (checkbox)
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsInstalled",
                HeaderText = "Instalado",
                Name = "colIsInstalled",
                Width = 80
            });

            // Fecha de compra (opcional)
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PurchaseDate",
                HeaderText = "Fecha compra",
                Name = "colPurchaseDate",
                Width = 140
            });

            // Owned (opcional)
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Owned",
                HeaderText = "Owned",
                Name = "colOwned",
                Width = 60
            });

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
        }

        private void LoadGamesForCurrentUser()
        {
            try
            {
                var currentUser = SessionManager.Instance.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Usuario no encontrado en la sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var games = _gameService.GetLibrary(currentUser) ?? new List<Game>();

                var dgv = FindControlRecursive<DataGridView>("dgvGames");
                if (dgv != null)
                {
                    dgv.DataSource = null;
                    dgv.DataSource = games;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar juegos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null) return;
            if (e.RowIndex < 0) return;

            var item = dgv.Rows[e.RowIndex].DataBoundItem as Game;
            if (item == null) return;

            // Abrir formulario de operación del juego (modal), con this como Owner
            using (var f = new frmOperarJuego())
            {
                f.CurrentGame = item;
                var dr = f.ShowDialog(this);

                // Si f pudo modificar la ruta/instalación, recargamos la biblioteca
                LoadGamesForCurrentUser();
            }
        }

        #endregion

        #region Perfil y Estado
        private void UpdateUserProfileLabels()
        {
            var currentUser = SessionManager.Instance.CurrentUser;
            if (currentUser != null)
            {
                // Mostrar DisplayName si existe, si no mostrar Username
                var display = string.IsNullOrWhiteSpace(currentUser.DisplayName) ? currentUser.Username : currentUser.DisplayName;

                // Buscar label de nombre recursivamente y actualizar
                var lblNombre = FindControlRecursive<Label>("lbl_Nombre");
                if (lblNombre != null) lblNombre.Text = display;

                // Buscar label de estado y establecerlo con prioridad al estado real del usuario (si existe)
                var lblEstado = FindControlRecursive<Label>("lbl_EstadoUsuario");
                if (lblEstado != null)
                {
                    var statusFromUser = currentUser.CurrentStatus;
                    if (!string.IsNullOrWhiteSpace(statusFromUser))
                    {
                        lblEstado.Text = statusFromUser;
                    }
                    else
                    {
                        lblEstado.Text = "Online";
                    }
                }
            }
        }


        #endregion

        #region Amigos / Presencia

        private void ConfigureDgvFriends(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Name = "colFriendId", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DisplayName", HeaderText = "Nombre", Name = "colFriendDisplay", Width = 160 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Username", HeaderText = "Usuario", Name = "colFriendUser", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Presence", HeaderText = "Estado", Name = "colFriendPresence", Width = 100 });
        }

        private void LoadFriends()
        {
            try
            {
                var currentUser = SessionManager.Instance.CurrentUser;
                if (currentUser == null) return;

                // Usar la instancia correcta _friendService (no _friend_service)
                var friends = _friendService.GetFriendsForUser(currentUser.Id);

                var dgv = FindControlRecursive<DataGridView>("dgv_Amigos");
                if (dgv != null)
                {
                    dgv.DataSource = null;
                    dgv.DataSource = friends;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar amigos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Nota: este timer SIMULA presencia; no debe sobreescribir el estado "Jugando:"
        private void PresenceTimer_Tick(object sender, EventArgs e)
        {
            var dgv = FindControlRecursive<DataGridView>("dgv_Amigos");
            if (dgv == null || dgv.DataSource == null) return;

            // Intentamos convertir el DataSource a lista manipulable
            var list = dgv.DataSource as List<FriendViewModel>;
            if (list == null)
            {
                try
                {
                    var tempList = new List<FriendViewModel>();
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.DataBoundItem is FriendViewModel vm)
                            tempList.Add(vm);
                    }
                    list = tempList;
                }
                catch
                {
                    return;
                }
            }

            var rnd = new Random();
            foreach (var f in list)
            {
                var r = rnd.Next(0, 3);
                f.Presence = r == 0 ? "Offline" : (r == 1 ? "Online" : "Jugando");
            }

            // refrescamos binding: reasignamos datasource para asegurar que se refresque correctamente
            dgv.DataSource = null;
            dgv.DataSource = list;

            // actualizar estado local SOLO si no estamos jugando: ahora usamos el estado real del usuario en SessionManager
            var currentUser = SessionManager.Instance.CurrentUser;
            var lblEstado = FindControlRecursive<Label>("lbl_EstadoUsuario");
            if (currentUser != null && lblEstado != null)
            {
                // Si el usuario tiene un CurrentStatus en la sesión, respetamos ese valor.
                // Solo si no hay CurrentStatus, mostramos estados simulados.
                if (!string.IsNullOrWhiteSpace(currentUser.CurrentStatus))
                {
                    lblEstado.Text = currentUser.CurrentStatus;
                    return;
                }

                // Si no hay status real, mostramos uno simple (opcional)
                var r2 = rnd.Next(0, 3);
                lblEstado.Text = r2 == 0 ? "Offline" : (r2 == 1 ? "Online" : "Jugando");
            }
        }


        #endregion

        #region Lanzar juego y registro de sesión (expuestos para frmOperarJuego)

        /// <summary>
        /// Llamalo para indicar que el usuario empezó a jugar (actualiza label en UI).
        /// Puede llamarlo frmOperarJuego via: if (this.Owner is frmMain m) m.SetPlaying(title);
        /// </summary>
        public void SetPlaying(string title)
        {
            _isPlaying = true;
            _playingTitle = title ?? "(sin nombre)";

            // Actualizar también el modelo del usuario en sesión
            var user = SessionManager.Instance.CurrentUser;
            if (user != null)
            {
                user.CurrentStatus = $"Jugando: \"{_playingTitle}\"";
            }

            // actualizar label en hilo UI
            var lblEstado = FindControlRecursive<Label>("lbl_EstadoUsuario");
            if (lblEstado == null) return;

            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    lblEstado.Text = $"Jugando: \"{_playingTitle}\"";
                }));
            }
            else
            {
                lblEstado.Text = $"Jugando: \"{_playingTitle}\"";
            }
        }


        /// <summary>
        /// Llamalo para indicar que terminó la partida.
        /// </summary>
        public void SetOnline()
        {
            _isPlaying = false;
            _playingTitle = null;

            // Actualizar también el modelo del usuario en sesión
            var user = SessionManager.Instance.CurrentUser;
            if (user != null)
            {
                user.CurrentStatus = "Online";
            }

            var lblEstado = FindControlRecursive<Label>("lbl_EstadoUsuario");
            if (lblEstado == null) return;

            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() =>
                {
                    lblEstado.Text = "Online";
                }));
            }
            else
            {
                lblEstado.Text = "Online";
            }
        }


        /// <summary>
        /// Método original para lanzar juegos desde frmMain (si lo usás).
        /// También usa SetPlaying/SetOnline internamente.
        /// </summary>
        private void LaunchGame(Game game)
        {
            if (game == null)
            {
                MessageBox.Show("Juego inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(game.InstallPath) || !Directory.Exists(game.InstallPath))
            {
                MessageBox.Show("El juego no está instalado o la ruta no existe. Instale o cambie la ruta desde Operar Juego.", "No instalado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string launchError;
            var proc = _gameService.LaunchGameProcess(game, out launchError);
            if (proc == null)
            {
                MessageBox.Show("No se pudo lanzar el juego: " + launchError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var currentUser = SessionManager.Instance.CurrentUser;

            // indicar que estamos jugando
            SetPlaying(game.Title);

            DateTime startTime = DateTime.Now;
            proc.EnableRaisingEvents = true;
            proc.Exited += (s, ev) =>
            {
                try
                {
                    DateTime endTime = DateTime.Now;
                    int duration = (int)(endTime - startTime).TotalSeconds;
                    if (currentUser != null)
                    {
                        _gameService.RecordPlaySession(currentUser.Id, game.Id, startTime, endTime, duration);
                    }
                }
                catch
                {
                    // swallow
                }
                finally
                {
                    // volver a Online
                    SetOnline();

                    // recargar UI en hilo UI
                    if (this.InvokeRequired)
                    {
                        this.Invoke((Action)(() => LoadGamesForCurrentUser()));
                    }
                    else
                    {
                        LoadGamesForCurrentUser();
                    }
                }
            };
        }

        #endregion

        #region Botones auxiliares

        private void btn_Amigos_Click(object sender, EventArgs e)
        {
            using (var f = new frmAmigos())
            {
                f.ShowDialog();
            }
            // recargamos amigos por si hubo cambios
            LoadFriends();
        }

        private void btnTienda_Click(object sender, EventArgs e)
        {
            using (var frmTienda = new frmTienda())
            {
                frmTienda.ShowDialog();
            }

            // Recargar por si se agregaron/quitaron/editaron juegos
            LoadGamesForCurrentUser();
        }

        private void imagenPerfil_Click(object sender, EventArgs e)
        {
            // placeholder: abrir edición de perfil o cambiar imagen
        }

        private void btn_Noticias_Click(object sender, EventArgs e)
        {
            using (var fremNoticias = new frmNoticias())
            {
                fremNoticias.ShowDialog();
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            using (var fremEditar = new frmEditarPerfil())
            {
                fremEditar.ShowDialog();
            }

            SessionManager.Instance.RefreshCurrentUser();

            UpdateUserProfileLabels();
            LoadFriends();
            LoadGamesForCurrentUser();
        }
        #endregion
    }
}
