using System.Windows;
using MogriChess.Models;
using MogriChess.ViewModels;

namespace MogriChess.WPF.Windows
{
    public partial class SelectBoardColor : Window
    {
        public ColorScheme SelectedBoardColor =>
            (DataContext as SelectBoardColorViewModel).SelectedColorScheme;

        public SelectBoardColor()
        {
            InitializeComponent();

            DataContext = new SelectBoardColorViewModel();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}