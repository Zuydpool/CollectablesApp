using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectablesApp.Models
{
    public class Transaction : ISupportInitialize
    {
        [BsonElement("CollectableItemId")]
        public string CollectableItemId { get; set; }

        [BsonElement("Buyer")]
        public string Buyer { get; set; }

        [BsonElement("Seller")]
        public string Seller { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }


        [BsonIgnore]
        public CollectableItem CollectableItem { get; set; }

        public void BeginInit()
        {
            // Do nothing
        }

        public async void EndInit()
        {
            CollectableItem = await App.GetInstance().Storage.Dao.CollectableItemsDao.GetById(CollectableItemId);
        }
    }
}
