using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using CollectablesApp.Models;
using MongoDB.Bson;

namespace CollectablesApp.DBStorage.Dao.Mongo
{
    public class MongoUsersDao : IUsersDao
    {

        public IMongoCollection<User> UsersCollection;

        public async Task<User> GetById(string id)
        {
            var idFilter = Builders<User>.Filter.Eq(user => user.Id, id);
            var result = await UsersCollection.FindAsync(idFilter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await UsersCollection.Find(new BsonDocument())
                .ToListAsync();
        }

        public async Task<bool> Delete(string id)
        {
            var idFilter = Builders<User>.Filter.Eq(user => user.Id, id);
            var deletedItem = await UsersCollection.DeleteOneAsync(idFilter);
            return deletedItem != null;
        }

        public async Task<User> Add(User entry)
        {
            await UsersCollection.InsertOneAsync(entry);
            return entry;
        }

        public async Task<User> Update(User entry)
        {
            var idFilter = Builders<User>.Filter.Eq(user => user.Id, entry.Id);
            var combinedUpdate = Builders<User>.Update.Combine(
                Builders<User>.Update.Set(i => i.Username, entry.Username),
                Builders<User>.Update.Set(i => i.Password, entry.Password),
                Builders<User>.Update.Set(i => i.Transactions, entry.Transactions)
                );

            await UsersCollection.UpdateOneAsync(idFilter, combinedUpdate);
            return entry;
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            var userNameFilter = Builders<User>.Filter.Eq(user => user.Username, username);
            var result = await UsersCollection.FindAsync(userNameFilter);
            return await result.AnyAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            var userNameFilter = Builders<User>.Filter.Eq(user => user.Username, username);
            var result = await UsersCollection.FindAsync(userNameFilter);
            return await result.FirstOrDefaultAsync();
        }
    }
}
