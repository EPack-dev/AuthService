namespace AuthService.WebApi.Config
{
    public class AppConfig
    {
        public AppConfig()
        {
            Mongo = new MongoConfig();
        }

        public MongoConfig Mongo { get; set; }
    }
}
