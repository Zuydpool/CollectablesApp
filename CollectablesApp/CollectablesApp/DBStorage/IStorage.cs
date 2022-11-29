namespace CollectablesApp.DBStorage
{
    public interface IStorage
    {
        Dao.IStorageDao Dao { get; }

        void SetupStorage();
    }
}
