using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RezhDumaASPCore_Backend.Model;

namespace RezhBot
{
    public class ApiService
    {
        public HttpClient Client = new HttpClient();
        public ApiService()
        {
            Client.BaseAddress = new Uri("https://localhost:5001/");
        }

        public async Task<List<T>> GetAll<T>()
        where T : DbEntity
        {
            List<T> result = null;
            var response = await Client.GetAsync(typeof(T).Name);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<T>>();
            }

            return result;
        }
    }
}
