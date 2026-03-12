using MogriChess.Core;

namespace MogriChess.ViewModels;

public class OKViewModel(string title, string message) : ObservableObject
{
    public string Title { get; } = title;
    public string Message { get; } = message;
}