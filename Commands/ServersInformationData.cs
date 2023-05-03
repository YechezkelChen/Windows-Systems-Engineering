using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WSE.Commands
{
    public class ServersInformationData : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public event Action<string, DateTime, DateTime> ShouldDisplayServersData;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            object[] values = (object[])parameter;
            GameServer gameServer = (GameServer)values[0];
            DateTime startDate = (DateTime)values[1];
            DateTime endDate = (DateTime)values[2];

            if (ShouldDisplayServersData != null)
                ShouldDisplayServersData(gameServer.serverName, startDate, endDate);
        }
    }
}
