using System.Windows;
using MogriChess.ViewModels;

namespace MogriChess.WPF.Windows
{
    public partial class SelectBoardColor : Window
    {
        public string SelectedBoardColorName =>
            (DataContext as SelectBoardColorViewModel).SelectedColorScheme.Name;

        public SelectBoardColor()
        {
            InitializeComponent();

            DataContext = new SelectBoardColorViewModel("Red");
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
