using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectablesApp.Models
{
    public class CollectableItem
    {

        // All properties have to be readable and writable.

        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("Price")]
        public double Price { get; set; }

        [BsonElement("OpenForTrading")]
        public bool OpenForTrading { get; set; }

        [BsonElement("Seller")]
        public string Seller { get; set; }

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }
    }
}
