using System.Threading.Tasks;
using CollectablesApp.Models;

namespace CollectablesApp.DBStorage.Dao
{
    public interface IUsersDao : IDao<User>
    {

        Task<bool> CheckIfUsernameExists(string username);

        Task<User> GetByUsername(string username);
    }
}
