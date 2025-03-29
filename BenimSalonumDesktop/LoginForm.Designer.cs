
namespace BenimSalonumDesktop
{
    partial class LoginForm
    {
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.LinkLabel linkForgotPassword;

        private void InitializeComponent()
        {
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            cmbLanguage = new ComboBox();
            linkForgotPassword = new LinkLabel();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(30, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 15);
            lblUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(0, 15);
            lblPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(150, 27);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(150, 67);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(150, 110);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 30);
            btnLogin.TabIndex = 4;
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Location = new Point(150, 150);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(200, 23);
            cmbLanguage.TabIndex = 5;
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // linkForgotPassword
            // 
            linkForgotPassword.Location = new Point(150, 190);
            linkForgotPassword.Name = "linkForgotPassword";
            linkForgotPassword.Size = new Size(200, 23);
            linkForgotPassword.TabIndex = 6;
            linkForgotPassword.LinkClicked += linkForgotPassword_LinkClicked;
            // 
            // LoginForm
            // 
            ClientSize = new Size(400, 250);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(cmbLanguage);
            Controls.Add(linkForgotPassword);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            Text = "Login";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
