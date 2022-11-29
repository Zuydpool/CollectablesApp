using System.Collections.Generic;
using System.Threading.Tasks;
using CollectablesApp.Models;

namespace CollectablesApp.DBStorage.Dao
{
    public interface ICollectableItemsDao : IDao<CollectableItem>
    {
        Task Add(ICollection<CollectableItem> items);

        Task<ICollection<CollectableItem>> GetBySeller(string seller);
    }
}
