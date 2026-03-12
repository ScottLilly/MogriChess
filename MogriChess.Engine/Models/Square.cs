using MogriChess.Core;

namespace MogriChess.Models;

public class Square(int rank, int file) : ObservableObject
{
    private string FileAsLetter =>
        "abcdefgh".Substring(File - 1, 1);

    public int Rank { get; } = rank;
    public int File { get; } = file;

    private Piece _piece;
    private bool _isSelected;
    private bool _isValidDestination;

    public Piece Piece
    {
        get => _piece;
        set
        {
            if (SetProperty(ref _piece, value))
            {
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public bool IsValidDestination
    {
        get => _isValidDestination;
        set => SetProperty(ref _isValidDestination, value);
    }

    public bool IsEmpty => Piece == null;
    public Enums.Color Color =>
        (Rank + File).IsEven()
            ? Enums.Color.Dark
            : Enums.Color.Light;
    public string SquareShorthand => $"{FileAsLetter}{Rank}";
    public int UiGridRow => Constants.NumberOfRanks - Rank;
    public int UiGridColumn => File - 1;
}