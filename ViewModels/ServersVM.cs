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

        public ServersInformationData ServersInfoData { get; set; }

        public ServersModel CurrentModel { get; set; }

        public ObservableCollection<GameServer> Servers { get; set; }

        public ObservableCollection<GameServer> ServersData { get; set; }

        public ServersVM()
        {
            CurrentModel = new ServersModel();
            Servers = new ObservableCollection<GameServer>(CurrentModel.Servers);
            ServersData = new ObservableCollection<GameServer>(CurrentModel.ServersData);
            ServersInfo = new ServersInformation();
            ServersInfo.ShouldDisplayServers += ServersInfo_ShouldDisplayServers;
            ServersInfoData = new ServersInformationData();
            ServersInfoData.ShouldDisplayServersData += ServersInfoData_ShouldDisplayServersData;
        }

        private void ServersInfo_ShouldDisplayServers()
        {
            CurrentModel.LoadServers();
            Servers.Clear();
            foreach (var item in CurrentModel.Servers)
                Servers.Add(item);
        }


        private void ServersInfoData_ShouldDisplayServersData(string gameServer, DateTime startDate, DateTime endDate)
        {
            CurrentModel.LoadServersData(gameServer, startDate, endDate);
            ServersData.Clear();
            foreach (var item in CurrentModel.ServersData)
                ServersData.Add(item);
        }
    }
}
