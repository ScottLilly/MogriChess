using MogriChess.Core;

namespace MogriChess.ViewModels;

public class OKViewModel : ObservableObject
{
    public string Title { get; }
    public string Message { get; }

    public OKViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }
}