using System.Windows;
using MogriChess.Engine.ViewModels;

namespace MogriChess.WPF.Windows;

public partial class OK : Window
{
    public OK(string title, string message)
    {
        InitializeComponent();

        DataContext = new OKViewModel(title, message);
    }

    private void OK_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}