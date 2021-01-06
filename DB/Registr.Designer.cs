namespace DB
{
    partial class Registr
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
            this.TextBLogin = new System.Windows.Forms.TextBox();
            this.TextBPassword = new System.Windows.Forms.TextBox();
            this.TextBMail = new System.Windows.Forms.TextBox();
            this.LabLogin = new System.Windows.Forms.Label();
            this.LabPassword = new System.Windows.Forms.Label();
            this.LabMail = new System.Windows.Forms.Label();
            this.LabInfo = new System.Windows.Forms.Label();
            this.LabName = new System.Windows.Forms.Label();
            this.TextBName = new System.Windows.Forms.TextBox();
            this.BtnAdminRegistr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBLogin
            // 
            this.TextBLogin.Location = new System.Drawing.Point(75, 72);
            this.TextBLogin.Name = "TextBLogin";
            this.TextBLogin.Size = new System.Drawing.Size(175, 22);
            this.TextBLogin.TabIndex = 0;
            // 
            // TextBPassword
            // 
            this.TextBPassword.Location = new System.Drawing.Point(75, 106);
            this.TextBPassword.Name = "TextBPassword";
            this.TextBPassword.Size = new System.Drawing.Size(175, 22);
            this.TextBPassword.TabIndex = 1;
            // 
            // TextBMail
            // 
            this.TextBMail.Location = new System.Drawing.Point(75, 135);
            this.TextBMail.Name = "TextBMail";
            this.TextBMail.Size = new System.Drawing.Size(175, 22);
            this.TextBMail.TabIndex = 2;
            this.TextBMail.TextChanged += new System.EventHandler(this.TextBMail_TextChanged);
            // 
            // LabLogin
            // 
            this.LabLogin.AutoSize = true;
            this.LabLogin.Location = new System.Drawing.Point(15, 75);
            this.LabLogin.Name = "LabLogin";
            this.LabLogin.Size = new System.Drawing.Size(47, 17);
            this.LabLogin.TabIndex = 3;
            this.LabLogin.Text = "Логин";
            // 
            // LabPassword
            // 
            this.LabPassword.AutoSize = true;
            this.LabPassword.Location = new System.Drawing.Point(12, 106);
            this.LabPassword.Name = "LabPassword";
            this.LabPassword.Size = new System.Drawing.Size(57, 17);
            this.LabPassword.TabIndex = 4;
            this.LabPassword.Text = "Пароль";
            // 
            // LabMail
            // 
            this.LabMail.AutoSize = true;
            this.LabMail.Location = new System.Drawing.Point(13, 135);
            this.LabMail.Name = "LabMail";
            this.LabMail.Size = new System.Drawing.Size(49, 17);
            this.LabMail.TabIndex = 5;
            this.LabMail.Text = "Почта";
            // 
            // LabInfo
            // 
            this.LabInfo.AutoSize = true;
            this.LabInfo.Location = new System.Drawing.Point(33, 9);
            this.LabInfo.Name = "LabInfo";
            this.LabInfo.Size = new System.Drawing.Size(193, 17);
            this.LabInfo.TabIndex = 6;
            this.LabInfo.Text = "Регистрация нового админа";
            // 
            // LabName
            // 
            this.LabName.AutoSize = true;
            this.LabName.Location = new System.Drawing.Point(18, 43);
            this.LabName.Name = "LabName";
            this.LabName.Size = new System.Drawing.Size(35, 17);
            this.LabName.TabIndex = 7;
            this.LabName.Text = "Имя";
            // 
            // TextBName
            // 
            this.TextBName.Location = new System.Drawing.Point(75, 43);
            this.TextBName.Name = "TextBName";
            this.TextBName.Size = new System.Drawing.Size(175, 22);
            this.TextBName.TabIndex = 8;
            // 
            // BtnAdminRegistr
            // 
            this.BtnAdminRegistr.Location = new System.Drawing.Point(3, 172);
            this.BtnAdminRegistr.Name = "BtnAdminRegistr";
            this.BtnAdminRegistr.Size = new System.Drawing.Size(274, 31);
            this.BtnAdminRegistr.TabIndex = 9;
            this.BtnAdminRegistr.Text = "Регистрация";
            this.BtnAdminRegistr.UseVisualStyleBackColor = true;
            this.BtnAdminRegistr.Click += new System.EventHandler(this.BtnAdminRegistr_Click);
            // 
            // Registr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 205);
            this.Controls.Add(this.BtnAdminRegistr);
            this.Controls.Add(this.TextBName);
            this.Controls.Add(this.LabName);
            this.Controls.Add(this.LabInfo);
            this.Controls.Add(this.LabMail);
            this.Controls.Add(this.LabPassword);
            this.Controls.Add(this.LabLogin);
            this.Controls.Add(this.TextBMail);
            this.Controls.Add(this.TextBPassword);
            this.Controls.Add(this.TextBLogin);
            this.Name = "Registr";
            this.Text = "Registr";
            this.Load += new System.EventHandler(this.FormRegistr_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBLogin;
        private System.Windows.Forms.TextBox TextBPassword;
        private System.Windows.Forms.TextBox TextBMail;
        private System.Windows.Forms.Label LabLogin;
        private System.Windows.Forms.Label LabPassword;
        private System.Windows.Forms.Label LabMail;
        private System.Windows.Forms.Label LabInfo;
        private System.Windows.Forms.Label LabName;
        private System.Windows.Forms.TextBox TextBName;
        private System.Windows.Forms.Button BtnAdminRegistr;
    }
}