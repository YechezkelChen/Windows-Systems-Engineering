using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSE.ViewModels;

namespace WSE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GamesVM GamesVM { get; set; }
        public ServersVM ServersVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            GamesVM = new GamesVM();
            ServersVM = new ServersVM();
        }

        private void GamesInformationButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = GamesVM;
            ServersGrid.Visibility = Visibility.Hidden;
            AnalyzeGrid.Visibility = Visibility.Hidden;
            GamesGrid.Visibility = Visibility.Visible;
        }

        private void GeneralInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesBox.SelectedItem != null)
            {
                ChatWithGptGrid.Visibility = Visibility.Hidden;
                InfoGameScrollViewer.Visibility = Visibility.Visible;
                InfoGameText.DataContext = GamesBox.SelectedItem;
            }
        }

        private void AskGPTButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesBox.SelectedItem != null)
            {
                InfoGameScrollViewer.Visibility = Visibility.Hidden;
                ChatWithGptGrid.Visibility = Visibility.Visible;
            }
            ///לממש את החיבור עם Gpt יש פונקציה מתחת שגם עושה דברים!!
        }

        private void SendGPTButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve user input from TextBox
            string userInput = UserInputGPT.Text;

            // Send user input to ChatGPT API for generating response
            //string chatbotResponse = await ChatGPTAPI.GetResponse(userInput);

            // Display response in TextBlock
            //ChatMessages.Text += $"You: {userInput}\nChatGPT: {chatbotResponse}\n";
            ChatMessages.Text = "You Success, just stay to connect the chat with really GPT";

            // Clear user input TextBox
            UserInputGPT.Text = "";
        }

        private void ServersInformationButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = ServersVM;
            GamesGrid.Visibility = Visibility.Hidden;
            AnalyzeGrid.Visibility = Visibility.Hidden;
            ServersGrid.Visibility = Visibility.Visible;
        }

        private void HealthMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServersBox.SelectedItem != null)
            {
                PlayersMonitorGrid.Visibility = Visibility.Hidden;
                HealthMonitorGrid.Visibility = Visibility.Visible;
                HealthMonitorGrid.DataContext = ServersBox.SelectedItem;
            }
        }

        private void PlayersMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServersBox.SelectedItem != null)
            {
                HealthMonitorGrid.Visibility = Visibility.Hidden;
                PlayersMonitorGrid.Visibility = Visibility.Visible;
                PlayersMonitorGrid.DataContext = ServersBox.SelectedItem;

                GameServer server = (GameServer)ServersBox.SelectedItem;

                object[] values = { server, DateTime.Now.AddDays(-1), DateTime.Now };
                ServersVM.ServersInfoData.Execute(values);

                List<GameServer> serversList = ServersVM.ServersData.ToList();
                if (serversList.Count() != 0)
                {
                    TotalPlayersToday.Text = serversList.Sum(x => x.playerCount).ToString();
                    MaxScore.Text = serversList.Max(x => x.maxScore).ToString();
                    AveragePlayerTimeMin.Text = serversList.Average(x => x.playerTimeMin).ToString();
                    CurrentPlayers.Text = server.playerCount.ToString();
                }
            }
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = ServersVM;
            GamesGrid.Visibility = Visibility.Hidden;
            ServersGrid.Visibility = Visibility.Hidden;
            AnalyzeGrid.Visibility = Visibility.Visible;
        }

        private void SearchServersButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServersComboBox.SelectedItem == null || !StartDateCalendar.SelectedDate.HasValue || !EndDateCalendar.SelectedDate.HasValue || StartDateCalendar.SelectedDate.Value > EndDateCalendar.SelectedDate.Value)
            {
                MessageBox.Show("One from the values is empty!");
                return;
            }

            List<GameServer> serversData = ServersVM.ServersData.ToList();

            // First, group the servers by season
            var seasonGroups = serversData.GroupBy(s => GetSeason(s.dateTime));

            foreach (var seasonGroup in seasonGroups)
            {
                int season = seasonGroup.Key;

                // Next, group the servers by day of the week within each season
                var dayGroups = seasonGroup.GroupBy(s => s.dateTime.DayOfWeek);

                Console.WriteLine($"Season {season}:");
                foreach (var dayGroup in dayGroups)
                {
                    string dayOfWeek = dayGroup.Key.ToString();
                    int serverCount = dayGroup.Count();
                    Console.WriteLine($"{dayOfWeek}: {serverCount} servers");
                }
                Console.WriteLine();
            }

            if (serversData.Count() != 0)
            {
                chartCanvas.Visibility = Visibility.Visible;
                float high = ((float)serversData.Where(x => x.playerCount >= 70000).Count() / serversData.Count()) * 100;
                float medium = ((float)serversData.Where(x => x.playerCount >= 30000 && x.playerCount < 70000).Count() / serversData.Count()) * 100;
                float low = ((float)serversData.Where(x => x.playerCount >= 0 && x.playerCount < 30000).Count() / serversData.Count()) * 100;

                HighRectangle.Height = high;
                MediumRectangle.Height = medium;
                LowRectangle.Height = low;
            }

            this.DataGamesGrid.ItemsSource = serversData;
        }

        private int GetSeason(DateTime date)
        {
            int month = date.Month;
            if (month >= 3 && month <= 5)
            {
                return 1; // Spring
            }
            else if (month >= 6 && month <= 8)
            {
                return 2; // Summer
            }
            else if (month >= 9 && month <= 11)
            {
                return 3; // Autumn/Fall
            }
            else
            {
                return 4; // Winter
            }
        }
    }
}
