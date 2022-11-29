using System;
using System.Collections.Generic;
using System.Text;

namespace CollectablesApp.DBStorage
{
    public class StorageFactory
    {

        public IStorage GetInstance()
        {
            var storage = CreateNewImplementation(StorageType.MONGODB);
            storage.SetupStorage();
            return storage;
        }

        private IStorage CreateNewImplementation(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.MONGODB:
                    return new MongoDbStorage();
                case StorageType.RESTAPI:
                    return new RestAPIStorage();
            }

            throw new Exception("Unknown storage type " + storageType);
        }
    }
}
