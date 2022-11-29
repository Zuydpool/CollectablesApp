using System.Collections.Generic;
using System.Threading.Tasks;
using CollectablesApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CollectablesApp.DBStorage.Dao.Mongo
{
    public class MongoCollectableItemsDao : ICollectableItemsDao
    {
        public IMongoCollection<CollectableItem> CollectableItemsCollection;

        public async Task Add(ICollection<CollectableItem> items) => await CollectableItemsCollection.InsertManyAsync(items);

        public async Task<CollectableItem> GetById(string id)
        {
            var idFilter = Builders<CollectableItem>.Filter.Eq(entry => entry.Id, id);
            var result = await CollectableItemsCollection.FindAsync(idFilter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<ICollection<CollectableItem>> GetBySeller(string seller)
        {
            var sellerFilter = Builders<CollectableItem>.Filter.Eq(entry => entry.Seller, seller);
            var result = await CollectableItemsCollection.FindAsync(sellerFilter);
            return await result.ToListAsync();
        }

        public async Task<List<CollectableItem>> GetAll()
        {
            var allItems = await CollectableItemsCollection
                .Find(new BsonDocument())
                .ToListAsync();

            return allItems;
        }

        public async Task<bool> Delete(string id)
        {
            var idFilter = Builders<CollectableItem>.Filter.Eq(entry => entry.Id, id);
            var deletedItem = await CollectableItemsCollection.DeleteOneAsync(idFilter);
            return deletedItem != null;
        }

        public async Task<CollectableItem> Add(CollectableItem entry)
        {
            await CollectableItemsCollection.InsertOneAsync(entry);
            return entry;
        }

        public async Task<CollectableItem> Update(CollectableItem entry)
        {
            var filterDefinition = Builders<CollectableItem>.Filter.Eq(i => i.Id, entry.Id);
            var setProductName = Builders<CollectableItem>.Update.Set(i => i.Name, entry.Name);
            var setProductDescription = Builders<CollectableItem>.Update.Set(i => i.Description, entry.Description);
            var setProductQuantity = Builders<CollectableItem>.Update.Set(i => i.Quantity, entry.Quantity);
            var setProductPrice = Builders<CollectableItem>.Update.Set(i => i.Price, entry.Price);
            var setProductOpenForTrading = Builders<CollectableItem>.Update.Set(i => i.OpenForTrading, entry.OpenForTrading);
            var setProductSeller = Builders<CollectableItem>.Update.Set(i => i.Seller, entry.Seller);
            var setImageUrl = Builders<CollectableItem>.Update.Set(i => i.ImageUrl, entry.ImageUrl);
            var combinedUpdate = Builders<CollectableItem>.Update.Combine(setProductName, setProductDescription, setProductQuantity, setProductOpenForTrading, setProductSeller, setProductPrice, setImageUrl);
            await CollectableItemsCollection.UpdateOneAsync(filterDefinition, combinedUpdate);
            return entry;
        }
    }
}
