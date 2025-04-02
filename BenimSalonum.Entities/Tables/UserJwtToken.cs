public class UserJwtToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; } = DateTime.UtcNow.AddHours(2); // NULL kabul etmeyen bir tarih
}
