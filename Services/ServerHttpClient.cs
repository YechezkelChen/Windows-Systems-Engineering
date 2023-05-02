using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using BusinessEntity;
using HandyControl.Tools.Extension;
using System.Text.Json.Serialization;

namespace WSE.Services
{
    public class ServerHttpClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public ServerHttpClient()
        {
            _baseUrl = "http://localhost:5000";
            _httpClient = new HttpClient(); 
        }

        public async Task<List<Game>> GetGamesDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/GamesData");
                response.EnsureSuccessStatusCode(); // Throw an exception if the status code is not successful
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Game>>(content);
            }
            catch (Exception)
            {
                return new List<Game>();
            }
        }

        public async Task<List<GameServer>> GetServersListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/ServerGame/ServersList");
                response.EnsureSuccessStatusCode(); // Throw an exception if the status code is not successful
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<GameServer>>(content);
            }
            catch(Exception)
            {
                return new List<GameServer>();
            }
        }

        public async Task<List<GameServer>> GetServerDataListAsync(string serverName, DateTime? from = null, DateTime? until = null)
        {
            var query = $"?serverName={serverName}";
            if (from.HasValue && until.HasValue)
            {
                query += $"&from={from.Value.ToString("s")}&until={until.Value.ToString("s")}";
            }
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/ServerGame/ServerStat{query}");
            response.EnsureSuccessStatusCode(); // Throw an exception if the status code is not successful
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GameServer>>(content);
        }
    }
}