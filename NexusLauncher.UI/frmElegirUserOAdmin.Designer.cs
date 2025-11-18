namespace NexusLauncher.UI
{
    partial class frmElegirUserOAdmin
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
            this.label1 = new System.Windows.Forms.Label();
            this.bt_Admin = new System.Windows.Forms.Button();
            this.bt_User = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(768, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usted tiene acceso a admin\r\nDesea acceder como tal? O prefiere acceder como usuar" +
    "io?";
            // 
            // bt_Admin
            // 
            this.bt_Admin.Location = new System.Drawing.Point(140, 284);
            this.bt_Admin.Name = "bt_Admin";
            this.bt_Admin.Size = new System.Drawing.Size(177, 23);
            this.bt_Admin.TabIndex = 1;
            this.bt_Admin.Text = "Acceder como admin";
            this.bt_Admin.UseVisualStyleBackColor = true;
            this.bt_Admin.Click += new System.EventHandler(this.bt_Admin_Click);
            // 
            // bt_User
            // 
            this.bt_User.Location = new System.Drawing.Point(443, 284);
            this.bt_User.Name = "bt_User";
            this.bt_User.Size = new System.Drawing.Size(177, 23);
            this.bt_User.TabIndex = 2;
            this.bt_User.Text = "Acceder como usuario";
            this.bt_User.UseVisualStyleBackColor = true;
            this.bt_User.Click += new System.EventHandler(this.bt_User_Click);
            // 
            // frmElegirUserOAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bt_User);
            this.Controls.Add(this.bt_Admin);
            this.Controls.Add(this.label1);
            this.Name = "frmElegirUserOAdmin";
            this.Text = "frmElegirUserOAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_Admin;
        private System.Windows.Forms.Button bt_User;
    }
}