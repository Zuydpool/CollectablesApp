using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CollectablesApp.Models;
using Newtonsoft.Json;

namespace CollectablesApp.DBStorage.Dao.RestAPI
{
    public class RestAPICollectableItemsDao : ICollectableItemsDao
    {
        private const string BaseUrl = "http://192.168.10.31:8888";

        public async Task Add(ICollection<CollectableItem> items)
        {
            using var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(items);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await httpClient.PostAsync(BaseUrl + "/collectableitem", httpContent);
        }

        public async Task<CollectableItem> GetById(string id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/collectableitem/" + id);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var collectableItem = JsonConvert.DeserializeObject<CollectableItem>(content);
            return collectableItem;
        }

        public async Task<ICollection<CollectableItem>> GetBySeller(string seller)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/collectableitem/seller/" + seller);

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var collectableItems = JsonConvert.DeserializeObject<List<CollectableItem>>(content);
            return collectableItems;
        }

        public async Task<List<CollectableItem>> GetAll()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(BaseUrl + "/collectableitem");

            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var collectableItems = JsonConvert.DeserializeObject<List<CollectableItem>>(content);
            return collectableItems;
        }

        public async Task<bool> Delete(string id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(BaseUrl + "/collectableitem/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<CollectableItem> Add(CollectableItem entry)
        {
            using var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(entry);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(BaseUrl + "/collectableitem", httpContent);
            return !response.IsSuccessStatusCode ? null : entry;
        }

        public async Task<CollectableItem> Update(CollectableItem entry)
        {
            using var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(entry);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(BaseUrl + "/collectableitem", httpContent);
            return !response.IsSuccessStatusCode ? null : entry;
        }
    }
}