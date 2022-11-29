namespace CollectablesApp.DBStorage.Dao.RestAPI
{
    public class RestAPIDao : IStorageDao
    {
        public ICollectableItemsDao CollectableItemsDao { get; } = new RestAPICollectableItemsDao();
        public IUsersDao UsersDao { get; } = new RestAPIUsersDao();
    }
}
