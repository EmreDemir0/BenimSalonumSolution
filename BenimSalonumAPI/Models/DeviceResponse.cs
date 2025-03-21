public class DeviceResponse
{
    public int Id { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string DeviceName { get; set; }
    public string Platform { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Expires { get; set; }
}
