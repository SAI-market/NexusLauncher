namespace NexusLauncher.UI
{
    partial class frmTienda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTienda = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienda)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTienda
            // 
            this.dgvTienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTienda.Location = new System.Drawing.Point(12, 62);
            this.dgvTienda.Name = "dgvTienda";
            this.dgvTienda.Size = new System.Drawing.Size(240, 150);
            this.dgvTienda.TabIndex = 0;
            // 
            // frmTienda
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dgvTienda);
            this.Name = "frmTienda";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Tienda;
        private System.Windows.Forms.Button btn_Comprar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_NombreJuego;
        private System.Windows.Forms.Label lbl_Precio;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvTienda;
    }
}