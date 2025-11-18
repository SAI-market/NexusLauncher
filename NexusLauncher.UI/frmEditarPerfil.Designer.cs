namespace NexusLauncher.UI
{
    partial class frmEditarPerfil
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
            this.btnNombrePublico = new System.Windows.Forms.Button();
            this.btnContraseña = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.txtNombrePublico = new System.Windows.Forms.TextBox();
            this.txtContraseñaActual = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.txtContraseñaNueva = new System.Windows.Forms.TextBox();
            this.txtContraseñaNuevaConfirm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNombrePublico
            // 
            this.btnNombrePublico.Location = new System.Drawing.Point(359, 11);
            this.btnNombrePublico.Name = "btnNombrePublico";
            this.btnNombrePublico.Size = new System.Drawing.Size(195, 23);
            this.btnNombrePublico.TabIndex = 1;
            this.btnNombrePublico.Text = "Guardar Nombre Publico";
            this.btnNombrePublico.UseVisualStyleBackColor = true;
            this.btnNombrePublico.Click += new System.EventHandler(this.btnNombrePublico_Click);
            // 
            // btnContraseña
            // 
            this.btnContraseña.Location = new System.Drawing.Point(359, 150);
            this.btnContraseña.Name = "btnContraseña";
            this.btnContraseña.Size = new System.Drawing.Size(195, 23);
            this.btnContraseña.TabIndex = 2;
            this.btnContraseña.Text = "Guardar Contraseña";
            this.btnContraseña.UseVisualStyleBackColor = true;
            this.btnContraseña.Click += new System.EventHandler(this.btnContraseña_Click);
            // 
            // btnMail
            // 
            this.btnMail.Location = new System.Drawing.Point(359, 68);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(195, 23);
            this.btnMail.TabIndex = 3;
            this.btnMail.Text = "Guardar Mail";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // txtNombrePublico
            // 
            this.txtNombrePublico.Location = new System.Drawing.Point(125, 12);
            this.txtNombrePublico.Name = "txtNombrePublico";
            this.txtNombrePublico.Size = new System.Drawing.Size(180, 22);
            this.txtNombrePublico.TabIndex = 5;
            // 
            // txtContraseñaActual
            // 
            this.txtContraseñaActual.Location = new System.Drawing.Point(149, 121);
            this.txtContraseñaActual.Name = "txtContraseñaActual";
            this.txtContraseñaActual.Size = new System.Drawing.Size(206, 22);
            this.txtContraseñaActual.TabIndex = 6;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(86, 69);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(219, 22);
            this.txtMail.TabIndex = 7;
            // 
            // txtContraseñaNueva
            // 
            this.txtContraseñaNueva.Location = new System.Drawing.Point(149, 152);
            this.txtContraseñaNueva.Name = "txtContraseñaNueva";
            this.txtContraseñaNueva.Size = new System.Drawing.Size(206, 22);
            this.txtContraseñaNueva.TabIndex = 8;
            // 
            // txtContraseñaNuevaConfirm
            // 
            this.txtContraseñaNuevaConfirm.Location = new System.Drawing.Point(149, 180);
            this.txtContraseñaNuevaConfirm.Name = "txtContraseñaNuevaConfirm";
            this.txtContraseñaNuevaConfirm.Size = new System.Drawing.Size(206, 22);
            this.txtContraseñaNuevaConfirm.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nombre publico:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Mail:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Contraseña actual:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Contraseña nueva:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Contraseña confirm:";
            // 
            // frmEditarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 228);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContraseñaNuevaConfirm);
            this.Controls.Add(this.txtContraseñaNueva);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.txtContraseñaActual);
            this.Controls.Add(this.txtNombrePublico);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnContraseña);
            this.Controls.Add(this.btnNombrePublico);
            this.Name = "frmEditarPerfil";
            this.Text = "Perfil";
            this.Load += new System.EventHandler(this.frmEditarPerfil_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNombrePublico;
        private System.Windows.Forms.Button btnContraseña;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.TextBox txtNombrePublico;
        private System.Windows.Forms.TextBox txtContraseñaActual;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.TextBox txtContraseñaNueva;
        private System.Windows.Forms.TextBox txtContraseñaNuevaConfirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}