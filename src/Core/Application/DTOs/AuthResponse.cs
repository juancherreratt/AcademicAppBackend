namespace Core.Application.DTOs
{
    public class AuthResponse
    {
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiresOn { get; set; }
    }
}
