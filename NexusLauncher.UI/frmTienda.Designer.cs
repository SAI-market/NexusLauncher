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
            this.dgv_Tienda = new System.Windows.Forms.DataGridView();
            this.btn_Comprar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_NombreJuego = new System.Windows.Forms.Label();
            this.lbl_Precio = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Tienda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Tienda
            // 
            this.dgv_Tienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Tienda.Location = new System.Drawing.Point(12, 12);
            this.dgv_Tienda.Name = "dgv_Tienda";
            this.dgv_Tienda.RowHeadersWidth = 51;
            this.dgv_Tienda.RowTemplate.Height = 24;
            this.dgv_Tienda.Size = new System.Drawing.Size(533, 426);
            this.dgv_Tienda.TabIndex = 0;
            // 
            // btn_Comprar
            // 
            this.btn_Comprar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Comprar.Location = new System.Drawing.Point(645, 330);
            this.btn_Comprar.Name = "btn_Comprar";
            this.btn_Comprar.Size = new System.Drawing.Size(117, 108);
            this.btn_Comprar.TabIndex = 8;
            this.btn_Comprar.Text = "Comprar";
            this.btn_Comprar.UseVisualStyleBackColor = true;
            this.btn_Comprar.Click += new System.EventHandler(this.btn_Comprar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Juego seleccionado:";
            // 
            // lbl_NombreJuego
            // 
            this.lbl_NombreJuego.AutoSize = true;
            this.lbl_NombreJuego.Location = new System.Drawing.Point(587, 113);
            this.lbl_NombreJuego.Name = "lbl_NombreJuego";
            this.lbl_NombreJuego.Size = new System.Drawing.Size(94, 16);
            this.lbl_NombreJuego.TabIndex = 10;
            this.lbl_NombreJuego.Text = "NombreJuego";
            // 
            // lbl_Precio
            // 
            this.lbl_Precio.AutoSize = true;
            this.lbl_Precio.Location = new System.Drawing.Point(716, 113);
            this.lbl_Precio.Name = "lbl_Precio";
            this.lbl_Precio.Size = new System.Drawing.Size(46, 16);
            this.lbl_Precio.TabIndex = 11;
            this.lbl_Precio.Text = "Precio";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(570, 172);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 126);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // frmTienda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Precio);
            this.Controls.Add(this.lbl_NombreJuego);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Comprar);
            this.Controls.Add(this.dgv_Tienda);
            this.Name = "frmTienda";
            this.Text = "Tienda";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Tienda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Tienda;
        private System.Windows.Forms.Button btn_Comprar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_NombreJuego;
        private System.Windows.Forms.Label lbl_Precio;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}