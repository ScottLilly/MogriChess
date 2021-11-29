using System.Windows;
using MogriChess.ViewModels;

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
            DataContext = new Game();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}