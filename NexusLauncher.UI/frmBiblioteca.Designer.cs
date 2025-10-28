namespace NexusLauncher.UI
{
    partial class frmBiblioteca
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
            this.dgvMisJuegos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisJuegos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMisJuegos
            // 
            this.dgvMisJuegos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMisJuegos.Location = new System.Drawing.Point(272, 139);
            this.dgvMisJuegos.Name = "dgvMisJuegos";
            this.dgvMisJuegos.Size = new System.Drawing.Size(336, 219);
            this.dgvMisJuegos.TabIndex = 0;
            // 
            // frmBiblioteca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvMisJuegos);
            this.Name = "frmBiblioteca";
            this.Text = "frmBiblioteca";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMisJuegos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMisJuegos;
    }
}