using System.Linq;
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

            CurrentSession.CurrentGame.MoveHistory.CollectionChanged += MoveHistory_CollectionChanged;
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

        private void MoveHistory_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MoveHistoryDataGrid.ScrollIntoView(CurrentSession.CurrentGame.MoveHistory.Last());
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}