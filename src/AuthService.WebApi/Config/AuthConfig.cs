namespace AuthService.WebApi.Config
{
    public class AuthConfig
    {
        public string Issuer { get; set; } = default!;

        public string Audience { get; set; } = default!;

        public string SecurityKey { get; set; } = default!;

        public int LifetimeDays { get; set; }
    }
}
