
using Newtonsoft.Json;
using Syncfusion.WinForms.Controls;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BenimSalonumDesktop.Resources;

namespace BenimSalonumDesktop
{
    public partial class LoginForm : SfForm
    {
        private int loginAttempts = 0;
        private bool isFirstLoad = true;

        public LoginForm()
        {
            InitializeComponent();
            // 🟢 DİL SEÇENEKLERİNİ EKLE
            cmbLanguage.Items.AddRange(new string[] { "Türkçe", "English", "Deutsch" });

            // ✅ Dil içeriği yüklendikten sonra index ver
            cmbLanguage.SelectedIndex = 0;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            isFirstLoad = true;

            string culture = Settings.Default.AppCulture;
            if (culture == "en-US")
                cmbLanguage.SelectedIndex = 1;
            else if (culture == "de-DE")
                cmbLanguage.SelectedIndex = 2;
            else
                cmbLanguage.SelectedIndex = 0;

            ApplyLocalization();
            isFirstLoad = false;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(Strings.AllFieldsRequired);
                return;
            }

            var httpClient = new HttpClient();
            var loginData = new { KullaniciAdi = username, Parola = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:5000/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(responseJson);

                string accessToken = json.accessToken;
                string refreshToken = json.refreshToken;

                MainScreen mainForm = new MainScreen(accessToken, refreshToken);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                loginAttempts++;
                MessageBox.Show($"{Strings.LoginError} {loginAttempts}");

                if (loginAttempts >= 5)
                {
                    MessageBox.Show(Strings.TooManyAttempts);
                    btnLogin.Enabled = false;
                    await Task.Delay(TimeSpan.FromMinutes(5));
                    btnLogin.Enabled = true;
                    loginAttempts = 0;
                }
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFirstLoad) return;

            string selectedCulture = cmbLanguage.SelectedIndex switch
            {
                0 => "tr-TR",
                1 => "en-US",
                2 => "de-DE",
                _ => "tr-TR"
            };

            Settings.Default.AppCulture = selectedCulture;
            Settings.Default.Save();

            Application.Restart();
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ResetPasswordForm().ShowDialog();
        }

        private void ApplyLocalization()
        {
            lblUsername.Text = Strings.UsernameLabel;
            lblPassword.Text = Strings.PasswordLabel;
            btnLogin.Text = Strings.LoginButton;
            linkForgotPassword.Text = Strings.ForgotPassword;
        }
    }
}
