using AuthService.Model;
using AuthService.WebApi.Config;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace AuthService.WebApi.Database
{
    public class AppDbContext
    {
        public AppDbContext(MongoConfig config)
        {
            SetConventions();
            RegisterClassMaps();

            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase database = client.GetDatabase(config.Database);

            Accounts = database.GetCollection<Account>("Accounts");
        }

        public IMongoCollection<Account> Accounts { get; }

        private void SetConventions()
        {
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("Convention", pack, t => true);
        }

        private void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<Account>(cm => { cm.AutoMap(); });
        }
    }
}
