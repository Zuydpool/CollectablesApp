using System;
using System.Collections.Generic;
using System.Text;

namespace CollectablesApp.DBStorage.Dao
{
    public interface IStorageDao
    {
        ICollectableItemsDao CollectableItemsDao { get; }

        IUsersDao UsersDao { get; }
    }
}
