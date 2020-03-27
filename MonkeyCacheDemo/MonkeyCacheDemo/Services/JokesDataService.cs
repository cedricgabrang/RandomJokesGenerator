using MonkeyCache.SQLite;
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

            if(Connectivity.NetworkAccess != NetworkAccess.Internet
                && !Barrel.Current.IsExpired(endpoint)) 
            {
                var cache = Barrel.Current.Get<List<Jokes>>(endpoint);
                return cache;
            }

            else 
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint);
                string httpResult = httpResponse.Content.ReadAsStringAsync().Result;
                var httpData = JsonConvert.DeserializeObject<List<Jokes>>(httpResult);
                Barrel.Current.Empty(endpoint);
                Barrel.Current.Add(key: endpoint, data: httpData, expireIn: TimeSpan.FromDays(1));
                return httpData;
            }
        }
    }
}
