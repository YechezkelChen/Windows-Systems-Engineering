using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class GameServerData
    {
        public string serverName { get; set; }
        public DateTime dateTime { get; set; }
        public float cpuUsage { get; set; }
        public int playerCount { get; set; }
        public float memoryUsage { get; set; }
        public bool serverUp { get; set; }
        public int temperature { get; set; }
        public int maxScore { get; set; }
        public int playerTimeMin { get; set; }
    }
}