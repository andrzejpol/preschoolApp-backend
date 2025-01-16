namespace PreschoolApp.Configuration
{
    public class AuthenticationConfiguration
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
        public int SaltSize { get; set; }
    }
}