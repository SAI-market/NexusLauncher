using System;
using System.Windows.Forms;
using NexusLauncher.Models;

namespace NexusLauncher.UI
{
    public partial class frmElegirUserOAdmin : Form
    {
        private readonly User _user;

        public frmElegirUserOAdmin(User user)
        {
            InitializeComponent();
            _user = user ?? throw new ArgumentNullException(nameof(user));

            // Mostrar info del admin en la UI (ajusta el nombre del label si en tu diseñador es distinto)
            label1.Text = $"Has iniciado sesión como '{_user.Username}' (Administrador). ¿Cómo querés entrar?";
        }

        // Botón: Entrar como usuario normal
        private void bt_User_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Botón: Entrar como admin (biblioteca)
        private void bt_Admin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        // Si el usuario cierra la ventana con la X -> cancelar
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK && this.DialogResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            base.OnFormClosing(e);
        }
    }
}
