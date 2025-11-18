namespace NexusLauncher.UI
{
    partial class frmOperarJuego
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
            this.imagenJuego = new System.Windows.Forms.PictureBox();
            this.btn_Jugar = new System.Windows.Forms.Button();
            this.btn_ChangePath = new System.Windows.Forms.Button();
            this.btn_Desinstalar = new System.Windows.Forms.Button();
            this.btn_Instalar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imagenJuego)).BeginInit();
            this.SuspendLayout();
            // 
            // imagenJuego
            // 
            this.imagenJuego.Location = new System.Drawing.Point(26, 25);
            this.imagenJuego.Name = "imagenJuego";
            this.imagenJuego.Size = new System.Drawing.Size(216, 311);
            this.imagenJuego.TabIndex = 5;
            this.imagenJuego.TabStop = false;
            // 
            // btn_Jugar
            // 
            this.btn_Jugar.Location = new System.Drawing.Point(279, 25);
            this.btn_Jugar.Name = "btn_Jugar";
            this.btn_Jugar.Size = new System.Drawing.Size(246, 67);
            this.btn_Jugar.TabIndex = 6;
            this.btn_Jugar.Text = "Jugar";
            this.btn_Jugar.UseVisualStyleBackColor = true;
            this.btn_Jugar.Click += new System.EventHandler(this.btn_Jugar_Click);
            // 
            // btn_ChangePath
            // 
            this.btn_ChangePath.Location = new System.Drawing.Point(277, 148);
            this.btn_ChangePath.Name = "btn_ChangePath";
            this.btn_ChangePath.Size = new System.Drawing.Size(246, 67);
            this.btn_ChangePath.TabIndex = 7;
            this.btn_ChangePath.Text = "Cambiar ubicacion";
            this.btn_ChangePath.UseVisualStyleBackColor = true;
            this.btn_ChangePath.Click += new System.EventHandler(this.btn_ChangePath_Click);
            // 
            // btn_Desinstalar
            // 
            this.btn_Desinstalar.Location = new System.Drawing.Point(279, 258);
            this.btn_Desinstalar.Name = "btn_Desinstalar";
            this.btn_Desinstalar.Size = new System.Drawing.Size(246, 67);
            this.btn_Desinstalar.TabIndex = 8;
            this.btn_Desinstalar.Text = "Desinstalar";
            this.btn_Desinstalar.UseVisualStyleBackColor = true;
            this.btn_Desinstalar.Click += new System.EventHandler(this.btn_Desinstalar_Click);
            // 
            // btn_Instalar
            // 
            this.btn_Instalar.Location = new System.Drawing.Point(542, 258);
            this.btn_Instalar.Name = "btn_Instalar";
            this.btn_Instalar.Size = new System.Drawing.Size(246, 67);
            this.btn_Instalar.TabIndex = 9;
            this.btn_Instalar.Text = "Instalar";
            this.btn_Instalar.UseVisualStyleBackColor = true;
            this.btn_Instalar.Click += new System.EventHandler(this.btn_Instalar_Click);
            // 
            // frmOperarJuego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 363);
            this.Controls.Add(this.btn_Instalar);
            this.Controls.Add(this.btn_Desinstalar);
            this.Controls.Add(this.btn_ChangePath);
            this.Controls.Add(this.btn_Jugar);
            this.Controls.Add(this.imagenJuego);
            this.Name = "frmOperarJuego";
            this.Text = "frmOperarJuego";
            ((System.ComponentModel.ISupportInitialize)(this.imagenJuego)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imagenJuego;
        private System.Windows.Forms.Button btn_Jugar;
        private System.Windows.Forms.Button btn_ChangePath;
        private System.Windows.Forms.Button btn_Desinstalar;
        private System.Windows.Forms.Button btn_Instalar;
    }
}