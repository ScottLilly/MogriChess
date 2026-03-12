using MogriChess.Engine.Core;

namespace MogriChess.Engine.ViewModels;

public class OKViewModel(string title, string message) : ObservableObject
{
    public string Title { get; } = title;
    public string Message { get; } = message;
}