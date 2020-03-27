using MonkeyCacheDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MonkeyCacheDemo.Services
{
    public class JokesDataService
    {
        static readonly HttpClient httpClient = new HttpClient();

        public async Task<List<Jokes>> GetRandomJokesAsync()
        {
            var endpoint = string.Format(Constants.BASE_URL, "ten");
            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);
            string httpResult = httpResponse.Content.ReadAsStringAsync().Result;
            var httpData = JsonConvert.DeserializeObject<List<Jokes>>(httpResult);
            return httpData;
            
        }
    }
}
