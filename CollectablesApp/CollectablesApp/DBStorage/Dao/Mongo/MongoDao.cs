namespace CollectablesApp.DBStorage.Dao.Mongo
{
    public class MongoDao : IStorageDao
    {
        public ICollectableItemsDao CollectableItemsDao { get; } = new MongoCollectableItemsDao();

        public IUsersDao UsersDao { get; } = new MongoUsersDao();
    }
}
