namespace AuthService.WebApi.Config
{
    public class AppConfig
    {
        public MongoConfig Mongo { get; set; } = default!;

        public AuthConfig Auth { get; set; } = default!;
    }
}
