using MongoDB.Driver;

namespace CollegeERPSystem.Services.Domain
{
    public class MongoDbSettings
    {
        public string DbName { get; set; } = "CollegeERP";
        public string ConnString { get; set; } = "mongodb://localhost:27017";
        public string MarksCollection { get; set; } = "Marks";
        private MongoClient? Client;
        public IMongoDatabase? _db;

        public MongoDbSettings()
        {
            var settings = MongoClientSettings.FromConnectionString(ConnString);
            settings.LinqProvider = MongoDB.Driver.Linq.LinqProvider.V3;
            Client = new MongoClient(settings);
            _db = Client.GetDatabase(DbName);
        }
    }
}
