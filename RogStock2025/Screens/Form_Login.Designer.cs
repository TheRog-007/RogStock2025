namespace RogStock2025
{
    partial class frmLogin
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
            this.BTNCancel = new System.Windows.Forms.Button();
            this.BTNLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTUser = new System.Windows.Forms.TextBox();
            this.TXTPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTNCancel
            // 
            this.BTNCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNCancel.Location = new System.Drawing.Point(168, 121);
            this.BTNCancel.Name = "BTNCancel";
            this.BTNCancel.Size = new System.Drawing.Size(75, 23);
            this.BTNCancel.TabIndex = 3;
            this.BTNCancel.Text = "Cancel";
            this.BTNCancel.UseVisualStyleBackColor = true;
            this.BTNCancel.Click += new System.EventHandler(this.BTNCancel_Click);
            // 
            // BTNLogin
            // 
            this.BTNLogin.Location = new System.Drawing.Point(24, 121);
            this.BTNLogin.Name = "BTNLogin";
            this.BTNLogin.Size = new System.Drawing.Size(75, 23);
            this.BTNLogin.TabIndex = 2;
            this.BTNLogin.Text = "Login";
            this.BTNLogin.UseVisualStyleBackColor = true;
            this.BTNLogin.Click += new System.EventHandler(this.BTNLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name:";
            // 
            // TXTUser
            // 
            this.TXTUser.Location = new System.Drawing.Point(105, 13);
            this.TXTUser.MaxLength = 30;
            this.TXTUser.Name = "TXTUser";
            this.TXTUser.Size = new System.Drawing.Size(100, 20);
            this.TXTUser.TabIndex = 0;
            // 
            // TXTPassword
            // 
            this.TXTPassword.Location = new System.Drawing.Point(105, 50);
            this.TXTPassword.MaxLength = 10;
            this.TXTPassword.Name = "TXTPassword";
            this.TXTPassword.PasswordChar = '*';
            this.TXTPassword.Size = new System.Drawing.Size(100, 20);
            this.TXTPassword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTNCancel;
            this.ClientSize = new System.Drawing.Size(270, 157);
            this.ControlBox = false;
            this.Controls.Add(this.TXTPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TXTUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTNLogin);
            this.Controls.Add(this.BTNCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmLogin_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNCancel;
        private System.Windows.Forms.Button BTNLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTUser;
        private System.Windows.Forms.TextBox TXTPassword;
        private System.Windows.Forms.Label label2;
    }
}

