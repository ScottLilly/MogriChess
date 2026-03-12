using MogriChess.Core;

namespace MogriChess.Models;

public class Piece(ColorScheme colorScheme, Enums.Color color, Enums.PieceType type,
    int squaresForward, int squaresForwardRight,
    int squaresRight, int squaresBackRight,
    int squaresBack, int squaresBackLeft,
    int squaresLeft, int squaresForwardLeft,
    bool isPromoted = false) : ObservableObject
{
    public ColorScheme ColorScheme { get; } = colorScheme;
    public Enums.Color Color { get; } = color;
    public Enums.PieceType PieceType { get; } = type;
    private bool _isPromoted = isPromoted;

    private int _forward = squaresForward;
    private int _forwardRight = squaresForwardRight;
    private int _right = squaresRight;
    private int _backRight = squaresBackRight;
    private int _back = squaresBack;
    private int _backLeft = squaresBackLeft;
    private int _left = squaresLeft;
    private int _forwardLeft = squaresForwardLeft;

    public bool IsPromoted
    {
        get => _isPromoted;
        set
        {
            if (SetProperty(ref _isPromoted, value))
            {
                OnPropertyChanged(nameof(IsUnpromotedPawn));
            }
        }
    }

    public int Forward
    {
        get => _forward;
        set => SetProperty(ref _forward, value);
    }
    public int ForwardRight
    {
        get => _forwardRight;
        set => SetProperty(ref _forwardRight, value);
    }
    public int Right
    {
        get => _right;
        set => SetProperty(ref _right, value);
    }
    public int BackRight
    {
        get => _backRight;
        set => SetProperty(ref _backRight, value);
    }
    public int Back
    {
        get => _back;
        set => SetProperty(ref _back, value);
    }
    public int BackLeft
    {
        get => _backLeft;
        set => SetProperty(ref _backLeft, value);
    }
    public int Left
    {
        get => _left;
        set => SetProperty(ref _left, value);
    }
    public int ForwardLeft
    {
        get => _forwardLeft;
        set => SetProperty(ref _forwardLeft, value);
    }

    public bool IsKing =>
        PieceType == Enums.PieceType.King;
    public bool IsUnpromotedPawn =>
        PieceType == Enums.PieceType.Pawn && !IsPromoted;
}