using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectablesApp.Models
{
    public class User : ISupportInitialize
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [BsonIgnore]
        public ICollection<CollectableItem> CollectableItems { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public User() { }
        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }


        public void BeginInit()
        {
            // Do nothing
        }

        public async void EndInit()
        {
            CollectableItems = await App.GetInstance().Storage.Dao.CollectableItemsDao.GetBySeller(Username);
        }
    }
}
