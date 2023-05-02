using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSE.Services;

namespace WSE.Models
{
    public class ServersModel
    {
        public List<GameServer> Servers { get; set; }
        public ServersModel()
        {
            Servers = new List<GameServer>();
            LoadServers();
        }

        public async void LoadServers()
        {
            var serversDataHttpClient = new ServerHttpClient();
            var serversData = await serversDataHttpClient.GetServersListAsync();
            foreach (var serverData in serversData)
            {
                if (!Servers.Exists(x => x.serverName == serverData.serverName))
                    Servers.Add(serverData);
            }
        }
    }
}
