namespace NexusLauncher.UI
{
    partial class frmAmigos
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
            this.dgv_Users = new System.Windows.Forms.DataGridView();
            this.lbl_UsuarioElegido = new System.Windows.Forms.Label();
            this.btn_Enviar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_SolicitudUsuario = new System.Windows.Forms.Label();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.btn_Rechazar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Users
            // 
            this.dgv_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Users.Location = new System.Drawing.Point(279, 37);
            this.dgv_Users.Name = "dgv_Users";
            this.dgv_Users.RowHeadersWidth = 51;
            this.dgv_Users.RowTemplate.Height = 24;
            this.dgv_Users.Size = new System.Drawing.Size(509, 187);
            this.dgv_Users.TabIndex = 0;
            // 
            // lbl_UsuarioElegido
            // 
            this.lbl_UsuarioElegido.AutoSize = true;
            this.lbl_UsuarioElegido.Location = new System.Drawing.Point(12, 50);
            this.lbl_UsuarioElegido.Name = "lbl_UsuarioElegido";
            this.lbl_UsuarioElegido.Size = new System.Drawing.Size(106, 16);
            this.lbl_UsuarioElegido.TabIndex = 1;
            this.lbl_UsuarioElegido.Text = "Usuario elegido:";
            // 
            // btn_Enviar
            // 
            this.btn_Enviar.Location = new System.Drawing.Point(12, 82);
            this.btn_Enviar.Name = "btn_Enviar";
            this.btn_Enviar.Size = new System.Drawing.Size(258, 72);
            this.btn_Enviar.TabIndex = 2;
            this.btn_Enviar.Text = "Enviar Solicitud Amistad";
            this.btn_Enviar.UseVisualStyleBackColor = true;
            this.btn_Enviar.Click += new System.EventHandler(this.btn_Enviar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Solicitudes recibidas:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 237);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(516, 201);
            this.dataGridView1.TabIndex = 4;
            // 
            // lbl_SolicitudUsuario
            // 
            this.lbl_SolicitudUsuario.AutoSize = true;
            this.lbl_SolicitudUsuario.Location = new System.Drawing.Point(534, 249);
            this.lbl_SolicitudUsuario.Name = "lbl_SolicitudUsuario";
            this.lbl_SolicitudUsuario.Size = new System.Drawing.Size(106, 16);
            this.lbl_SolicitudUsuario.TabIndex = 5;
            this.lbl_SolicitudUsuario.Text = "Usuario elegido:";
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.Location = new System.Drawing.Point(534, 285);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(258, 72);
            this.btn_Aceptar.TabIndex = 6;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = true;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // btn_Rechazar
            // 
            this.btn_Rechazar.Location = new System.Drawing.Point(534, 363);
            this.btn_Rechazar.Name = "btn_Rechazar";
            this.btn_Rechazar.Size = new System.Drawing.Size(258, 72);
            this.btn_Rechazar.TabIndex = 7;
            this.btn_Rechazar.Text = "Rechazar";
            this.btn_Rechazar.UseVisualStyleBackColor = true;
            this.btn_Rechazar.Click += new System.EventHandler(this.btn_Rechazar_Click);
            // 
            // frmAmigos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Rechazar);
            this.Controls.Add(this.btn_Aceptar);
            this.Controls.Add(this.lbl_SolicitudUsuario);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Enviar);
            this.Controls.Add(this.lbl_UsuarioElegido);
            this.Controls.Add(this.dgv_Users);
            this.Name = "frmAmigos";
            this.Text = "frmAmigos";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Users;
        private System.Windows.Forms.Label lbl_UsuarioElegido;
        private System.Windows.Forms.Button btn_Enviar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_SolicitudUsuario;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.Button btn_Rechazar;
    }
}