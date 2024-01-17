namespace Shared.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; } = null!;
        public int ExpirationMinutes { get; set; }
        
        public const string SectionName = "Jwt";
    }
}