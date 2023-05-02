using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            this.DataGamesGrid.ItemsSource = new AnalyzeGameData[]
            {
                new AnalyzeGameData{ DayOfWeek = 1, Hour = 5, Season = 4, Load = 8 },
                new AnalyzeGameData{ DayOfWeek = 2, Hour = 6, Season = 3, Load = 7 },
                new AnalyzeGameData{ DayOfWeek = 3, Hour = 7, Season = 2, Load = 6 },
                new AnalyzeGameData{ DayOfWeek = 4, Hour = 8, Season = 1, Load = 5 },
            };

            HighRectangle.Height= 50;
            MediumRectangle.Height= 90;
            LowRectangle.Height= 60;
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
            }
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            GamesGrid.Visibility = Visibility.Hidden;
            ServersGrid.Visibility = Visibility.Hidden;
            AnalyzeGrid.Visibility = Visibility.Visible;
        }

        private void DistributionButton_Click(object sender, RoutedEventArgs e)
        {
            chartCanvas.Visibility = Visibility.Visible;
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            chartCanvas.Visibility = Visibility.Hidden;
        }
    }

    public class AnalyzeGameData
    {
        public int DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Season { get; set; }
        public int Load { get; set; }
    }
}
