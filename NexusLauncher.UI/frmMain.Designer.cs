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
            this.lbl_EstadoUsuario = new System.Windows.Forms.Label();
            this.lbl_Nombre = new System.Windows.Forms.Label();
            this.btn_Noticias = new System.Windows.Forms.Button();
            this.dgv_Amigos = new System.Windows.Forms.DataGridView();
            this.btn_Amigos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenPerfil)).BeginInit();
            this.gb_InfoPerfil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Amigos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGames
            // 
            this.dgvGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGames.Location = new System.Drawing.Point(13, 45);
            this.dgvGames.Margin = new System.Windows.Forms.Padding(4);
            this.dgvGames.Name = "dgvGames";
            this.dgvGames.RowHeadersWidth = 51;
            this.dgvGames.Size = new System.Drawing.Size(568, 376);
            this.dgvGames.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(9, 25);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(44, 16);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "label1";
            // 
            // btn_Tienda
            // 
            this.btn_Tienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tienda.Location = new System.Drawing.Point(588, 46);
            this.btn_Tienda.Name = "btn_Tienda";
            this.btn_Tienda.Size = new System.Drawing.Size(117, 108);
            this.btn_Tienda.TabIndex = 3;
            this.btn_Tienda.Text = "Tienda";
            this.btn_Tienda.UseVisualStyleBackColor = true;
            this.btn_Tienda.Click += new System.EventHandler(this.btnTienda_Click);
            // 
            // imagenPerfil
            // 
            this.imagenPerfil.Location = new System.Drawing.Point(215, 50);
            this.imagenPerfil.Name = "imagenPerfil";
            this.imagenPerfil.Size = new System.Drawing.Size(123, 121);
            this.imagenPerfil.TabIndex = 4;
            this.imagenPerfil.TabStop = false;
            this.imagenPerfil.Click += new System.EventHandler(this.imagenPerfil_Click);
            // 
            // gb_InfoPerfil
            // 
            this.gb_InfoPerfil.Controls.Add(this.btn_Modificar);
            this.gb_InfoPerfil.Controls.Add(this.lbl_EstadoUsuario);
            this.gb_InfoPerfil.Controls.Add(this.imagenPerfil);
            this.gb_InfoPerfil.Controls.Add(this.lbl_Nombre);
            this.gb_InfoPerfil.Location = new System.Drawing.Point(711, 45);
            this.gb_InfoPerfil.Name = "gb_InfoPerfil";
            this.gb_InfoPerfil.Size = new System.Drawing.Size(344, 185);
            this.gb_InfoPerfil.TabIndex = 5;
            this.gb_InfoPerfil.TabStop = false;
            // 
            // btn_Modificar
            // 
            this.btn_Modificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Modificar.Location = new System.Drawing.Point(6, 115);
            this.btn_Modificar.Name = "btn_Modificar";
            this.btn_Modificar.Size = new System.Drawing.Size(117, 56);
            this.btn_Modificar.TabIndex = 8;
            this.btn_Modificar.Text = "Modificar Perfil";
            this.btn_Modificar.UseVisualStyleBackColor = true;
            this.btn_Modificar.Click += new System.EventHandler(this.btn_Modificar_Click);
            // 
            // lbl_EstadoUsuario
            // 
            this.lbl_EstadoUsuario.AutoSize = true;
            this.lbl_EstadoUsuario.Location = new System.Drawing.Point(7, 81);
            this.lbl_EstadoUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_EstadoUsuario.Name = "lbl_EstadoUsuario";
            this.lbl_EstadoUsuario.Size = new System.Drawing.Size(100, 16);
            this.lbl_EstadoUsuario.TabIndex = 8;
            this.lbl_EstadoUsuario.Text = "Estado Usuario";
            // 
            // lbl_Nombre
            // 
            this.lbl_Nombre.AutoSize = true;
            this.lbl_Nombre.Location = new System.Drawing.Point(7, 50);
            this.lbl_Nombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Nombre.Name = "lbl_Nombre";
            this.lbl_Nombre.Size = new System.Drawing.Size(56, 16);
            this.lbl_Nombre.TabIndex = 6;
            this.lbl_Nombre.Text = "Nombre";
            // 
            // btn_Noticias
            // 
            this.btn_Noticias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Noticias.Location = new System.Drawing.Point(588, 180);
            this.btn_Noticias.Name = "btn_Noticias";
            this.btn_Noticias.Size = new System.Drawing.Size(117, 108);
            this.btn_Noticias.TabIndex = 7;
            this.btn_Noticias.Text = "Noticias";
            this.btn_Noticias.UseVisualStyleBackColor = true;
            this.btn_Noticias.Click += new System.EventHandler(this.btn_Noticias_Click);
            // 
            // dgv_Amigos
            // 
            this.dgv_Amigos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Amigos.Location = new System.Drawing.Point(759, 239);
            this.dgv_Amigos.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_Amigos.Name = "dgv_Amigos";
            this.dgv_Amigos.RowHeadersWidth = 51;
            this.dgv_Amigos.Size = new System.Drawing.Size(296, 182);
            this.dgv_Amigos.TabIndex = 8;
            // 
            // btn_Amigos
            // 
            this.btn_Amigos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Amigos.Location = new System.Drawing.Point(588, 313);
            this.btn_Amigos.Name = "btn_Amigos";
            this.btn_Amigos.Size = new System.Drawing.Size(117, 108);
            this.btn_Amigos.TabIndex = 9;
            this.btn_Amigos.Text = "Amigos";
            this.btn_Amigos.UseVisualStyleBackColor = true;
            this.btn_Amigos.Click += new System.EventHandler(this.btn_Amigos_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 434);
            this.Controls.Add(this.btn_Amigos);
            this.Controls.Add(this.dgv_Amigos);
            this.Controls.Add(this.btn_Noticias);
            this.Controls.Add(this.gb_InfoPerfil);
            this.Controls.Add(this.btn_Tienda);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.dgvGames);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenPerfil)).EndInit();
            this.gb_InfoPerfil.ResumeLayout(false);
            this.gb_InfoPerfil.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Amigos)).EndInit();
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
        private System.Windows.Forms.Label lbl_EstadoUsuario;
        private System.Windows.Forms.Button btn_Modificar;
        private System.Windows.Forms.DataGridView dgv_Amigos;
        private System.Windows.Forms.Button btn_Amigos;
    }
}