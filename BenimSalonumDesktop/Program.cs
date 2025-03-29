using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BenimSalonumDesktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Uygulama açılışında kayıtlı dili ayarla
            var savedCulture = Settings.Default.AppCulture;
            if (!string.IsNullOrEmpty(savedCulture))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(savedCulture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedCulture);
            }

            ApplicationConfiguration.Initialize();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXxccXRTRGdcUUd+W0A=");
            Application.Run(new LoginForm());
        }
    }
}
