using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using CollectablesApp.DBStorage.Dao.Mongo;
using CollectablesApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CollectablesApp.DBStorage
{
    public class MongoDbStorage : IStorage
    {

        public Dao.IStorageDao Dao { get; } = new MongoDao();

        private MongoClient _client;

        private const string DbName = "CollectablesApp";

        public void SetupStorage()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress("192.168.10.31", 27017),

                SslSettings = new SslSettings {EnabledSslProtocols = SslProtocols.Tls12}
            };

            _client = new MongoClient(settings);

            var db = _client.GetDatabase(DbName);
            var collectionSettings = new MongoCollectionSettings {ReadPreference = ReadPreference.Nearest};
            (Dao.CollectableItemsDao as MongoCollectableItemsDao).CollectableItemsCollection =
                db.GetCollection<CollectableItem>("CollectionItems", collectionSettings);
            (Dao.UsersDao as MongoUsersDao).UsersCollection = db.GetCollection<User>("Users", collectionSettings);
        }
    }
}
