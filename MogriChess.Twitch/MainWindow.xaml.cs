using System.Windows;
using MogriChess.Services;

namespace MogriChess.Twitch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext = GameFactory.GetNewGame();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}