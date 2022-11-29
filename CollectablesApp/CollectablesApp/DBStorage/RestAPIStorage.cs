using CollectablesApp.DBStorage.Dao.RestAPI;

namespace CollectablesApp.DBStorage
{
    public class RestAPIStorage : IStorage
    {
        public Dao.IStorageDao Dao { get; } = new RestAPIDao();

        public void SetupStorage()
        {
             
        }
    }
}
