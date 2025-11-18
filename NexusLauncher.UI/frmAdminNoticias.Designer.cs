namespace NexusLauncher.UI
{
    partial class frmAdminNoticias
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
            this.dgv_Noticias = new System.Windows.Forms.DataGridView();
            this.btn_AgregarNoticia = new System.Windows.Forms.Button();
            this.btn_QuitarNoticia = new System.Windows.Forms.Button();
            this.btn_EditarNoticia = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Noticias)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Noticias
            // 
            this.dgv_Noticias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Noticias.Location = new System.Drawing.Point(12, 12);
            this.dgv_Noticias.Name = "dgv_Noticias";
            this.dgv_Noticias.RowHeadersWidth = 51;
            this.dgv_Noticias.RowTemplate.Height = 24;
            this.dgv_Noticias.Size = new System.Drawing.Size(776, 352);
            this.dgv_Noticias.TabIndex = 1;
            this.dgv_Noticias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Noticias_CellClick);
            // 
            // btn_AgregarNoticia
            // 
            this.btn_AgregarNoticia.Location = new System.Drawing.Point(12, 385);
            this.btn_AgregarNoticia.Name = "btn_AgregarNoticia";
            this.btn_AgregarNoticia.Size = new System.Drawing.Size(105, 29);
            this.btn_AgregarNoticia.TabIndex = 2;
            this.btn_AgregarNoticia.Text = "Agregar";
            this.btn_AgregarNoticia.UseVisualStyleBackColor = true;
            this.btn_AgregarNoticia.Click += new System.EventHandler(this.btn_AgregarNoticia_Click);
            // 
            // btn_QuitarNoticia
            // 
            this.btn_QuitarNoticia.Location = new System.Drawing.Point(137, 385);
            this.btn_QuitarNoticia.Name = "btn_QuitarNoticia";
            this.btn_QuitarNoticia.Size = new System.Drawing.Size(105, 29);
            this.btn_QuitarNoticia.TabIndex = 3;
            this.btn_QuitarNoticia.Text = "Quitar";
            this.btn_QuitarNoticia.UseVisualStyleBackColor = true;
            this.btn_QuitarNoticia.Click += new System.EventHandler(this.btn_QuitarNoticia_Click);
            // 
            // btn_EditarNoticia
            // 
            this.btn_EditarNoticia.Location = new System.Drawing.Point(268, 385);
            this.btn_EditarNoticia.Name = "btn_EditarNoticia";
            this.btn_EditarNoticia.Size = new System.Drawing.Size(105, 29);
            this.btn_EditarNoticia.TabIndex = 4;
            this.btn_EditarNoticia.Text = "Editar";
            this.btn_EditarNoticia.UseVisualStyleBackColor = true;
            this.btn_EditarNoticia.Click += new System.EventHandler(this.btn_EditarNoticia_Click);
            // 
            // frmAdminNoticias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_EditarNoticia);
            this.Controls.Add(this.btn_QuitarNoticia);
            this.Controls.Add(this.btn_AgregarNoticia);
            this.Controls.Add(this.dgv_Noticias);
            this.Name = "frmAdminNoticias";
            this.Text = "frmAdminNoticias";
            this.Load += new System.EventHandler(this.frmAdminNoticias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Noticias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Noticias;
        private System.Windows.Forms.Button btn_AgregarNoticia;
        private System.Windows.Forms.Button btn_QuitarNoticia;
        private System.Windows.Forms.Button btn_EditarNoticia;
    }
}