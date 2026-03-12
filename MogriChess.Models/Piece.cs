using MogriChess.Core;

namespace MogriChess.Models;

public class Piece : ObservableObject
{
    public ColorScheme ColorScheme { get; }
    public Enums.Color Color { get; }
    public Enums.PieceType PieceType { get; }
    private bool _isPromoted;

    private int _forward;
    private int _forwardRight;
    private int _right;
    private int _backRight;
    private int _back;
    private int _backLeft;
    private int _left;
    private int _forwardLeft;

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
    public string UiColor =>
        Color == Enums.Color.Light
            ? ColorScheme.LightColor
            : ColorScheme.DarkColor;
    public string KingIndicatorUiColor =>
        IsKing
            ? Color == Enums.Color.Light
                ? ColorScheme.DarkColor
                : ColorScheme.LightColor
            : UiColor;
    public int PieceColorTransformAngle =>
        Color == Enums.Color.Light ? 0 : 180;

    public Piece(ColorScheme colorScheme, Enums.Color color, Enums.PieceType type,
        int squaresForward, int squaresForwardRight,
        int squaresRight, int squaresBackRight,
        int squaresBack, int squaresBackLeft,
        int squaresLeft, int squaresForwardLeft,
        bool isPromoted = false)
    {
        ColorScheme = colorScheme;
        Color = color;
        PieceType = type;
        _isPromoted = isPromoted;

        _forward = squaresForward;
        _forwardRight = squaresForwardRight;
        _right = squaresRight;
        _backRight = squaresBackRight;
        _back = squaresBack;
        _backLeft = squaresBackLeft;
        _left = squaresLeft;
        _forwardLeft = squaresForwardLeft;
    }
}