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
            ///////        fix!!!!!!!!!!!!!!!!!!!!!
            //var gameServer = "1";
            //var startDate = DateTime.Now;
            //var endDate = DateTime.Now;


            var p = parameter as string;


            object[] values = (object[])parameter;
            string gameServer = (string)values[0];
            DateTime startDate = (DateTime)values[1];
            DateTime endDate = (DateTime)values[2];

            if (ShouldDisplayServersData != null)
                ShouldDisplayServersData(gameServer, startDate, endDate);
        }
    }
}
