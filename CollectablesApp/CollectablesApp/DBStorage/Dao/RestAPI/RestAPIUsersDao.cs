using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CollectablesApp.Models;
using Newtonsoft.Json;

namespace CollectablesApp.DBStorage.Dao.RestAPI
{
    public class RestAPIUsersDao : IUsersDao
    {
        private const string BaseUrl = "http://192.168.10.31:8888";

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/user/" + username);

            return response.IsSuccessStatusCode;
        }

        public async Task<User> GetByUsername(string username)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/user/" + username);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

        public async Task<User> GetById(string id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/user/" + id);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/user");

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<User>>(content);
            return user;
        }

        public async Task<bool> Delete(string id)
        {
            using var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(BaseUrl + "/user/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<User> Add(User entry)
        {
            using var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(entry);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(BaseUrl + "/user", httpContent);
            return !response.IsSuccessStatusCode ? null : entry;
        }

        public async Task<User> Update(User entry)
        {
            using var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(entry);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(BaseUrl + "/user", httpContent);
            return !response.IsSuccessStatusCode ? null : entry;
        }
    }
}
