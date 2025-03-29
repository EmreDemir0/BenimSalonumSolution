
using System;
using System.Windows.Forms;

namespace BenimSalonumDesktop
{
    public partial class MainScreen : Form
    {
        private string accessToken;
        private string refreshToken;

        public MainScreen(string accessToken, string refreshToken)
        {
            InitializeComponent();
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
        }
    }
}
