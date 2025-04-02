namespace BenimSalonumDesktop
{
    partial class ResetPasswordForm
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
            txtUsername = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtQuestion = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtAnswer = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            txtNewPassword = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnReset = new Syncfusion.WinForms.Controls.SfButton();
            ((System.ComponentModel.ISupportInitialize)txtUsername).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtQuestion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAnswer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPassword).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BeforeTouchSize = new Size(549, 23);
            txtUsername.Location = new Point(136, 146);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(549, 23);
            txtUsername.TabIndex = 0;
            txtUsername.Text = "Kullanıcı Adı";
            // 
            // txtQuestion
            // 
            txtQuestion.BeforeTouchSize = new Size(549, 23);
            txtQuestion.Location = new Point(136, 175);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(549, 23);
            txtQuestion.TabIndex = 1;
            txtQuestion.Text = "Soru";
            // 
            // txtAnswer
            // 
            txtAnswer.BeforeTouchSize = new Size(549, 23);
            txtAnswer.Location = new Point(136, 204);
            txtAnswer.Name = "txtAnswer";
            txtAnswer.Size = new Size(549, 23);
            txtAnswer.TabIndex = 2;
            txtAnswer.Text = "Gizli Soru Cevap";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BeforeTouchSize = new Size(549, 23);
            txtNewPassword.Location = new Point(136, 233);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(549, 23);
            txtNewPassword.TabIndex = 3;
            txtNewPassword.Text = "Parola Belirleyiniz";
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI Semibold", 9F);
            btnReset.Location = new Point(589, 274);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(96, 28);
            btnReset.TabIndex = 4;
            btnReset.Click += btnReset_Click;
            // 
            // ResetPasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnReset);
            Controls.Add(txtNewPassword);
            Controls.Add(txtAnswer);
            Controls.Add(txtQuestion);
            Controls.Add(txtUsername);
            Name = "ResetPasswordForm";
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            Text = "ResetPasswordForm";
            ((System.ComponentModel.ISupportInitialize)txtUsername).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtQuestion).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAnswer).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNewPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtUsername;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtQuestion;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtAnswer;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtNewPassword;
        private Syncfusion.WinForms.Controls.SfButton btnReset;
    }
}