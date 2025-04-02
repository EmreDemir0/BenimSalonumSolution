using System;

namespace BenimSalonum.Entities.DTOs
{
    public class LogoutOtherSessionsDto
    {
        public string NewRefreshToken { get; set; }
    }

    public class KeepAllSessionsDto
    {
        public string NewRefreshToken { get; set; }
    }
    
    public class DeviceInfoDto
    {
        public string DeviceName { get; set; }
        public string Platform { get; set; }
        public string UserAgent { get; set; }
    }
}
