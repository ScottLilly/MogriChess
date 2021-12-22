using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MogriChess.Models;
using MogriChess.ViewModels;

namespace MogriChess.Twitch
{
    public partial class MainWindow : Window
    {
        private PlaySession CurrentSession => DataContext as PlaySession;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new PlaySession();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClickedOnSquare_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas selectedSquare)
            {
                return;
            }

            Square square = selectedSquare.DataContext as Square;

            CurrentSession.CurrentGame.SelectSquare(square);
        }
    }
}