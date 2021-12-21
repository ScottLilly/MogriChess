using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MogriChess.Models;
using MogriChess.Services;
using MogriChess.Twitch.CustomControls;

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

        private void ClickedOnSquare_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Canvas selectedSquare)
            {
                return;
            }

            Square square = selectedSquare.DataContext as Square;

            Piece piece = square.Piece;

            if (piece != null)
            {
                ;
            }
        }
    }
}