namespace NexusLauncher.UI
{
    partial class frmMain
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
            this.dgvGames = new System.Windows.Forms.DataGridView();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btn_Tienda = new System.Windows.Forms.Button();
            this.imagenPerfil = new System.Windows.Forms.PictureBox();
            this.gb_InfoPerfil = new System.Windows.Forms.GroupBox();
            this.btn_Modificar = new System.Windows.Forms.Button();
            this.lbl_NombreMostrar = new System.Windows.Forms.Label();
            this.lbl_Nombre = new System.Windows.Forms.Label();
            this.btn_Noticias = new System.Windows.Forms.Button();
            this.btnAdminNoticias = new System.Windows.Forms.Button();
            this.flpNoticias = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenPerfil)).BeginInit();
            this.gb_InfoPerfil.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGames
            // 
            this.dgvGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGames.Location = new System.Drawing.Point(10, 37);
            this.dgvGames.Name = "dgvGames";
            this.dgvGames.RowHeadersWidth = 51;
            this.dgvGames.Size = new System.Drawing.Size(426, 230);
            this.dgvGames.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(7, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(35, 13);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "label1";
            // 
            // btn_Tienda
            // 
            this.btn_Tienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tienda.Location = new System.Drawing.Point(441, 37);
            this.btn_Tienda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Tienda.Name = "btn_Tienda";
            this.btn_Tienda.Size = new System.Drawing.Size(88, 88);
            this.btn_Tienda.TabIndex = 3;
            this.btn_Tienda.Text = "Tienda";
            this.btn_Tienda.UseVisualStyleBackColor = true;
            this.btn_Tienda.Click += new System.EventHandler(this.btnTienda_Click);
            // 
            // imagenPerfil
            // 
            this.imagenPerfil.Location = new System.Drawing.Point(105, 17);
            this.imagenPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.imagenPerfil.Name = "imagenPerfil";
            this.imagenPerfil.Size = new System.Drawing.Size(112, 122);
            this.imagenPerfil.TabIndex = 4;
            this.imagenPerfil.TabStop = false;
            // 
            // gb_InfoPerfil
            // 
            this.gb_InfoPerfil.Controls.Add(this.btn_Modificar);
            this.gb_InfoPerfil.Controls.Add(this.lbl_NombreMostrar);
            this.gb_InfoPerfil.Controls.Add(this.imagenPerfil);
            this.gb_InfoPerfil.Controls.Add(this.lbl_Nombre);
            this.gb_InfoPerfil.Location = new System.Drawing.Point(552, 8);
            this.gb_InfoPerfil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_InfoPerfil.Name = "gb_InfoPerfil";
            this.gb_InfoPerfil.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gb_InfoPerfil.Size = new System.Drawing.Size(222, 150);
            this.gb_InfoPerfil.TabIndex = 5;
            this.gb_InfoPerfil.TabStop = false;
            // 
            // btn_Modificar
            // 
            this.btn_Modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Modificar.Location = new System.Drawing.Point(4, 93);
            this.btn_Modificar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Modificar.Name = "btn_Modificar";
            this.btn_Modificar.Size = new System.Drawing.Size(88, 46);
            this.btn_Modificar.TabIndex = 8;
            this.btn_Modificar.Text = "Modificar Perfil";
            this.btn_Modificar.UseVisualStyleBackColor = true;
            // 
            // lbl_NombreMostrar
            // 
            this.lbl_NombreMostrar.AutoSize = true;
            this.lbl_NombreMostrar.Location = new System.Drawing.Point(13, 66);
            this.lbl_NombreMostrar.Name = "lbl_NombreMostrar";
            this.lbl_NombreMostrar.Size = new System.Drawing.Size(79, 13);
            this.lbl_NombreMostrar.TabIndex = 8;
            this.lbl_NombreMostrar.Text = "NombreMostrar";
            // 
            // lbl_Nombre
            // 
            this.lbl_Nombre.AutoSize = true;
            this.lbl_Nombre.Location = new System.Drawing.Point(13, 33);
            this.lbl_Nombre.Name = "lbl_Nombre";
            this.lbl_Nombre.Size = new System.Drawing.Size(44, 13);
            this.lbl_Nombre.TabIndex = 6;
            this.lbl_Nombre.Text = "Nombre";
            // 
            // btn_Noticias
            // 
            this.btn_Noticias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Noticias.Location = new System.Drawing.Point(441, 164);
            this.btn_Noticias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Noticias.Name = "btn_Noticias";
            this.btn_Noticias.Size = new System.Drawing.Size(88, 88);
            this.btn_Noticias.TabIndex = 7;
            this.btn_Noticias.Text = "Noticias";
            this.btn_Noticias.UseVisualStyleBackColor = true;
            // 
            // btnAdminNoticias
            // 
            this.btnAdminNoticias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminNoticias.Location = new System.Drawing.Point(556, 192);
            this.btnAdminNoticias.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdminNoticias.Name = "btnAdminNoticias";
            this.btnAdminNoticias.Size = new System.Drawing.Size(88, 88);
            this.btnAdminNoticias.TabIndex = 8;
            this.btnAdminNoticias.Text = "admin Noticias";
            this.btnAdminNoticias.UseVisualStyleBackColor = true;
            this.btnAdminNoticias.Click += new System.EventHandler(this.button1_Click);
            // 
            // flpNoticias
            // 
            this.flpNoticias.Location = new System.Drawing.Point(657, 192);
            this.flpNoticias.Name = "flpNoticias";
            this.flpNoticias.Size = new System.Drawing.Size(96, 87);
            this.flpNoticias.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 291);
            this.Controls.Add(this.flpNoticias);
            this.Controls.Add(this.btnAdminNoticias);
            this.Controls.Add(this.btn_Noticias);
            this.Controls.Add(this.gb_InfoPerfil);
            this.Controls.Add(this.btn_Tienda);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.dgvGames);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenPerfil)).EndInit();
            this.gb_InfoPerfil.ResumeLayout(false);
            this.gb_InfoPerfil.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGames;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btn_Tienda;
        private System.Windows.Forms.PictureBox imagenPerfil;
        private System.Windows.Forms.GroupBox gb_InfoPerfil;
        private System.Windows.Forms.Label lbl_Nombre;
        private System.Windows.Forms.Button btn_Noticias;
        private System.Windows.Forms.Label lbl_NombreMostrar;
        private System.Windows.Forms.Button btn_Modificar;
        private System.Windows.Forms.Button btnAdminNoticias;
        private System.Windows.Forms.FlowLayoutPanel flpNoticias;
    }
}