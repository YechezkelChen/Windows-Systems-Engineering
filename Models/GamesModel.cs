using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSE.Services;

namespace WSE.Models
{
    public class GamesModel
    {
        public List<Game> Games { get; set; }

        //
        public string GeneralInfo { get; set; }
        //

        public GamesModel()
        {
            Games = new List<Game>();
            LoadGames();

            //
            GeneralInfo = "Successs the binding!!!!!";
            //
        }

        public async void LoadGames()
        {
            var gamesDataHttpClient = new ServerHttpClient();
            var gamesData = await gamesDataHttpClient.GetGamesDataAsync();
            foreach (var gameData in gamesData)
            {
                if(! Games.Exists(x => x.name == gameData.name))
                    Games.Add(gameData);
            }
        }
    }
}
