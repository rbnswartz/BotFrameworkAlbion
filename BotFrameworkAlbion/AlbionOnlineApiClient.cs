using BotFrameworkAlbion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotFrameworkAlbion
{
    public class AlbionOnlineApiClient
    {
        private string root = "https://gameinfo.albiononline.com/api/gameinfo";
        public async Task<SearchResult> SearchAsync(string name)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync($"{root}/search?q={name}");
            return JsonConvert.DeserializeObject<SearchResult>(await result.Content.ReadAsStringAsync());
        }
        public async Task<Player> GetPlayerAsync(string playerId)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync($"{root}/players/{playerId}");
            return JsonConvert.DeserializeObject<Player>(await result.Content.ReadAsStringAsync());
        }
    }
}
