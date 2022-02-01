using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace MogriChess.WPF.Windows;

public partial class About : Window
{
    public About()
    {
        InitializeComponent();

        VersionNumber.Text =
            $"Version: {FileVersionInfo.GetVersionInfo("MogriChess.WPF.exe").ProductVersion}";
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ProcessStartInfo processStartInfo =
            new(e.Uri.ToString())
            {
                UseShellExecute = true
            };

        Process.Start(processStartInfo);
    }

    private void OK_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}