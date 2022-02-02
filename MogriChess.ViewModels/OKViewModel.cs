using System.ComponentModel;

namespace MogriChess.ViewModels;

public class OKViewModel : INotifyPropertyChanged
{
    public string Title { get; }
    public string Message { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public OKViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }
}