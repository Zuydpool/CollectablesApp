using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CollectablesApp.DBStorage.Dao
{
    public interface IDao<T>
    {
        Task<T> GetById(string id);

        Task<List<T>> GetAll();

        Task<bool> Delete(string id);

        Task<T> Add(T entry);

        Task<T> Update(T entry);
    }
}
