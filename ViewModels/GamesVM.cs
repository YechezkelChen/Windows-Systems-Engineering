using BusinessEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSE.Commands;
using WSE.Models;

namespace WSE.ViewModels
{
    public class GamesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public GamesInformation GamesInfo { get; set; }

        public GamesModel CurrentModel { get; set; }

        public ObservableCollection<Game> Games { get; set; }

        public GamesVM()
        {
            CurrentModel = new GamesModel();
            Games = new ObservableCollection<Game>(CurrentModel.Games);
            GamesInfo = new GamesInformation();
            GamesInfo.ShouldDisplayGames += GamesInfo_ShouldDisplayGames;
        }

        private void GamesInfo_ShouldDisplayGames()
        {
            CurrentModel.LoadGames();
            Games.Clear();
            foreach (var item in CurrentModel.Games)
                Games.Add(item);
        }
    }
}
