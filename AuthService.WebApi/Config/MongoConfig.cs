namespace AuthService.WebApi.Config
{
    public class MongoConfig
    {
        public string Database { get; set; } = default!;

        public string Host { get; set; } = default!;

        public int Port { get; set; }

        public string User { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                {
                    return $@"mongodb://{Host}:{Port}";
                }

                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}
