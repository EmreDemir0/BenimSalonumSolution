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
        private System.Windows.Forms.Label lblExpireDate;
        private Syncfusion.Windows.Forms.Tools.AutoLabel loginErrorTimer;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            cmbLanguage = new ComboBox();
            linkForgotPassword = new LinkLabel();
            lblExpireDate = new Label();
            loginErrorTimer = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            timerLoginError = new System.Windows.Forms.Timer(components);
            splitContainerLogin = new SplitContainer();
            pictureBox1 = new PictureBox();
            pictureBoxLogin = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainerLogin).BeginInit();
            splitContainerLogin.Panel1.SuspendLayout();
            splitContainerLogin.Panel2.SuspendLayout();
            splitContainerLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogin).BeginInit();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Left;
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(82, 226);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 15);
            lblUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Left;
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(82, 255);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(0, 15);
            lblPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Location = new Point(164, 223);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(164, 252);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(204, 305);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 30);
            btnLogin.TabIndex = 4;
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.Dock = DockStyle.Top;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Location = new Point(0, 0);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(248, 23);
            cmbLanguage.TabIndex = 5;
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // linkForgotPassword
            // 
            linkForgotPassword.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            linkForgotPassword.AutoSize = true;
            linkForgotPassword.Location = new Point(204, 362);
            linkForgotPassword.Name = "linkForgotPassword";
            linkForgotPassword.Size = new Size(95, 15);
            linkForgotPassword.TabIndex = 9;
            linkForgotPassword.TabStop = true;
            linkForgotPassword.Text = "Şifremi Unuttum";
            linkForgotPassword.LinkClicked += linkForgotPassword_LinkClicked;
            // 
            // lblExpireDate
            // 
            lblExpireDate.AutoSize = true;
            lblExpireDate.Dock = DockStyle.Bottom;
            lblExpireDate.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblExpireDate.ForeColor = Color.FromArgb(192, 0, 0);
            lblExpireDate.Location = new Point(0, 494);
            lblExpireDate.Name = "lblExpireDate";
            lblExpireDate.Size = new Size(0, 13);
            lblExpireDate.TabIndex = 10;
            // 
            // loginErrorTimer
            // 
            loginErrorTimer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            loginErrorTimer.Location = new Point(230, 338);
            loginErrorTimer.Name = "loginErrorTimer";
            loginErrorTimer.Size = new Size(0, 15);
            loginErrorTimer.TabIndex = 7;
            // 
            // timerLoginError
            // 
            timerLoginError.Tick += timerLoginError_Tick;
            // 
            // splitContainerLogin
            // 
            splitContainerLogin.Dock = DockStyle.Fill;
            splitContainerLogin.Location = new Point(2, 2);
            splitContainerLogin.Name = "splitContainerLogin";
            // 
            // splitContainerLogin.Panel1
            // 
            splitContainerLogin.Panel1.Controls.Add(pictureBox1);
            splitContainerLogin.Panel1.Controls.Add(txtPassword);
            splitContainerLogin.Panel1.Controls.Add(loginErrorTimer);
            splitContainerLogin.Panel1.Controls.Add(lblExpireDate);
            splitContainerLogin.Panel1.Controls.Add(lblUsername);
            splitContainerLogin.Panel1.Controls.Add(linkForgotPassword);
            splitContainerLogin.Panel1.Controls.Add(lblPassword);
            splitContainerLogin.Panel1.Controls.Add(txtUsername);
            splitContainerLogin.Panel1.Controls.Add(btnLogin);
            // 
            // splitContainerLogin.Panel2
            // 
            splitContainerLogin.Panel2.Controls.Add(pictureBoxLogin);
            splitContainerLogin.Panel2.Controls.Add(cmbLanguage);
            splitContainerLogin.Size = new Size(730, 507);
            splitContainerLogin.SplitterDistance = 478;
            splitContainerLogin.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(182, 105);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(163, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // pictureBoxLogin
            // 
            pictureBoxLogin.Dock = DockStyle.Fill;
            pictureBoxLogin.Location = new Point(0, 23);
            pictureBoxLogin.Name = "pictureBoxLogin";
            pictureBoxLogin.Size = new Size(248, 484);
            pictureBoxLogin.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogin.TabIndex = 11;
            pictureBoxLogin.TabStop = false;
            // 
            // LoginForm
            // 
            ClientSize = new Size(734, 511);
            Controls.Add(splitContainerLogin);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            ThemeName = "";
            Load += LoginForm_Load;
            splitContainerLogin.Panel1.ResumeLayout(false);
            splitContainerLogin.Panel1.PerformLayout();
            splitContainerLogin.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerLogin).EndInit();
            splitContainerLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogin).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Timer timerLoginError;
        private System.ComponentModel.IContainer components;
        private SplitContainer splitContainerLogin;
        private PictureBox pictureBoxLogin;
        private PictureBox pictureBox1;
    }
}
