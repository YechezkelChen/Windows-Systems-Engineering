using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WSE.Commands;
using WSE.Models;

namespace WSE.ViewModels
{
    public class ServersVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ServersInformation ServersInfo { get; set; }

        public ServersModel CurrentModel { get; set; }

        public ObservableCollection<GameServer> Servers { get; set; }

        public ServersVM()
        {
            CurrentModel = new ServersModel();
            Servers = new ObservableCollection<GameServer>(CurrentModel.Servers);
            ServersInfo = new ServersInformation();
            ServersInfo.ShouldDisplayServers += ServersInfo_ShouldDisplayServers;
        }

        private void ServersInfo_ShouldDisplayServers()
        {
            CurrentModel.LoadServers();
            Servers.Clear();
            foreach (var item in CurrentModel.Servers)
                Servers.Add(item);
        }
    }
}
