namespace AuthService.Database.Entities
{
    public class UserSession
    {
        public int UserId { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }

        public User? User { get; set; }
    }
}
