using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    // a class that contains the data of a game
    public class Game
    {
        // the url of the images server by the api
        public string name { get; set; }
        public string artworkUrl { get; set; }
        public int id { get; set; }
        public string summary { get; set; }
    };
}
