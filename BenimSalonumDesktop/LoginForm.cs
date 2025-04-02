using Newtonsoft.Json;
using Syncfusion.WinForms.Controls;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BenimSalonumDesktop.Resources;

namespace BenimSalonumDesktop
{
    public partial class LoginForm : SfForm
    {
        private int loginAttempts = 0;
        private bool isFirstLoad = true;
        private HttpClient _httpClient;
        private string _baseUrl = "http://localhost:5000/api";
        private bool passwordVisible = false;
        private Button btnTogglePassword;
        private int failedLoginAttempts = 0;
        private int lockoutTimeRemaining = 0;
        private const int MAX_LOGIN_ATTEMPTS = 3;
        private const int LOCKOUT_TIME_SECONDS = 300; // 5 dakika

        public LoginForm()
        {
            InitializeComponent();

            // HttpClient oluştur
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);

            // DİL SEÇENEKLERİNİ EKLE
            cmbLanguage.Items.AddRange(new string[] { "Türkçe", "English", "Deutsch" });

            // Dil içeriği yüklendikten sonra index ver
            cmbLanguage.SelectedIndex = 0;
        }





        private async void LoginForm_Load(object sender, EventArgs e)
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

            // Eğer daha önceden giriş yapılmışsa ve token varsa bunu kullanarak lisans kontrolü yapalım
            if (!string.IsNullOrEmpty(Settings.Default.AccessToken))
            {
                await CheckLicenseStatus(Settings.Default.AccessToken);
            }
            else
            {
                lblExpireDate.Text = "Giriş yaparak lisans durumunu kontrol edebilirsiniz.";
            }
        }
        private void SetupLoginErrorTimer()
        {
            // Timer ayarları
            timerLoginError.Interval = 1000; // 1 saniyede bir
            timerLoginError.Tick += timerLoginError_Tick;
            loginErrorTimer.Visible = false; // Başlangıçta gizle
        }
        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;
            txtPassword.UseSystemPasswordChar = !passwordVisible;
            btnTogglePassword.Text = passwordVisible ? "🔒" : "👁️";
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

            try
            {
                // Login isteği gönder
                var loginData = new { KullaniciAdi = username, Parola = password };
                var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(responseJson);

                    string accessToken = json.accessToken;
                    string refreshToken = json.refreshToken;

                    // Token'ları sakla
                    Settings.Default.AccessToken = accessToken;
                    Settings.Default.RefreshToken = refreshToken;
                    Settings.Default.Save();

                    // Lisans durumunu kontrol et
                    await CheckLicenseStatus(accessToken);

                    // Ana ekrana yönlendir
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
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}");
            }
        }

        private async Task CheckLicenseStatus(string accessToken)
        {
            try
            {
                // Debug - Access Token değerini görelim
                Console.WriteLine($"Access Token: {accessToken}");

                // Her istekten önce mevcut Authorization header'ı temizle
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

                // Token'ı header'a ekle
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Debug - Header'ın değerini kontrol edelim
                Console.WriteLine($"Authorization Header: {_httpClient.DefaultRequestHeaders.Authorization}");

                // Lisans bilgilerini kontrol et - Tam URL belirtelim
                var response = await _httpClient.GetAsync(_baseUrl + "/firma/lisans");

                // Debug - Response kodunu görelim
                Console.WriteLine($"Response Status: {response.StatusCode}, RequestMessage: {response.RequestMessage.RequestUri}");

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    dynamic licenseInfo = JsonConvert.DeserializeObject(responseJson);

                    DateTime bitisTarihi = licenseInfo.bitisTarihi;
                    bool aktif = licenseInfo.aktif;
                    int kalanGun = licenseInfo.kalanGun;
                    string lisansTuru = licenseInfo.lisansTuru;

                    if (aktif)
                    {
                        if (kalanGun <= 0)
                        {
                            lblExpireDate.Text = $"Lisansınız süresi dolmuş! ({lisansTuru})";
                            lblExpireDate.ForeColor = Color.Red;
                        }
                        else if (kalanGun <= 7)
                        {
                            lblExpireDate.Text = $"Lisansınızın bitmesine {kalanGun} gün kaldı! ({lisansTuru})";
                            lblExpireDate.ForeColor = System.Drawing.Color.OrangeRed;
                        }
                        else
                        {
                            lblExpireDate.Text = string.Format(Strings.LicenseActive, kalanGun, lisansTuru);
                            lblExpireDate.ForeColor = Color.Green;
                        }
                    }
                    else
                    {
                        lblExpireDate.Text = "Lisansınız pasif durumdadır!";
                        lblExpireDate.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Token geçersiz
                    lblExpireDate.Text = "Oturum süresi dolmuş. Lütfen tekrar giriş yapın.";
                    lblExpireDate.ForeColor = System.Drawing.Color.Red;

                    // Token'ları temizle
                    Settings.Default.AccessToken = string.Empty;
                    Settings.Default.RefreshToken = string.Empty;
                    Settings.Default.Save();
                }
                else
                {
                    lblExpireDate.Text = "Lisans bilgisi alınamadı.";
                    lblExpireDate.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblExpireDate.Text = "Lisans kontrolü sırasında hata oluştu.";
                lblExpireDate.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine($"Lisans kontrol hatası: {ex.Message}");
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
            lblExpireDate.Text = Strings.LicenseInfoLoading;
            lblUsername.Text = Strings.UsernameLabel;
            lblPassword.Text = Strings.PasswordLabel;
            btnLogin.Text = Strings.LoginButton;
            linkForgotPassword.Text = Strings.ForgotPassword;
        }

        private void timerLoginError_Tick(object sender, EventArgs e)
        {
            if (lockoutTimeRemaining > 0)
            {
                lockoutTimeRemaining--;
                int minutes = lockoutTimeRemaining / 60;
                int seconds = lockoutTimeRemaining % 60;
                loginErrorTimer.Text = $"Kalan süre: {minutes}:{seconds:D2}";

                // Süre bittiğinde kilidi kaldır
                if (lockoutTimeRemaining <= 0)
                {
                    failedLoginAttempts = 0;
                    timerLoginError.Stop();
                    loginErrorTimer.Visible = false;
                    btnLogin.Enabled = true;
                    txtUsername.Enabled = true;
                    txtPassword.Enabled = true;
                }
            }
        }

    }
}
