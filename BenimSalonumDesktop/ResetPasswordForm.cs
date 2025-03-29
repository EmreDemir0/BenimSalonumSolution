
using Newtonsoft.Json;
using Syncfusion.WinForms.Controls;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenimSalonumDesktop
{
    public partial class ResetPasswordForm : SfForm
    {
        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string answer = txtAnswer.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(answer) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show(Resources.Strings.AllFieldsRequired);
                return;
            }

            var httpClient = new HttpClient();
            var resetData = new { KullaniciAdi = username, HatirlatmaCevap = answer, YeniParola = newPassword };
            var content = new StringContent(JsonConvert.SerializeObject(resetData), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:5000/api/auth/reset-password", content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(Resources.Strings.ResetSuccess);
                this.Close();
            }
            else
            {
                MessageBox.Show(Resources.Strings.ResetFail);
            }
        }
    }
}
