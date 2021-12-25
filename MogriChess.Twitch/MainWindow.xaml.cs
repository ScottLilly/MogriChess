using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MogriChess.Models;
using MogriChess.Twitch.Windows;
using MogriChess.ViewModels;

namespace MogriChess.Twitch
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        private PlaySession CurrentSession => DataContext as PlaySession;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
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

        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            Help help = _serviceProvider.GetRequiredService<Help>();
            help.Owner = this;
            help.Show();
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            About about = _serviceProvider.GetRequiredService<About>();
            about.Owner = this;
            about.ShowDialog();
        }
    }
}