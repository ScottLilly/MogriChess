using MogriChess.Engine.Core;

namespace MogriChess.Engine.Models;

public class Move(Square originationSquare, Square destinationSquare) : ObservableObject
{
    public Color MovingPieceColor { get; } = originationSquare.Piece.Color;
    public Square OriginationSquare { get; } = originationSquare;
    public Square DestinationSquare { get; } = destinationSquare;

    private bool _putsOpponentInCheckmate;
    private bool _putsOpponentInCheck;
    private bool _isCapturingMove;
    private bool _isPromotingMove;
    private bool _isDrawFromMaxMoves;

    public bool PutsOpponentInCheckmate
    {
        get => _putsOpponentInCheckmate;
        set
        {
            if (SetProperty(ref _putsOpponentInCheckmate, value))
            {
                OnPropertyChanged(nameof(MoveResult));
            }
        }
    }

    public bool PutsOpponentInCheck
    {
        get => _putsOpponentInCheck;
        set
        {
            if (SetProperty(ref _putsOpponentInCheck, value))
            {
                OnPropertyChanged(nameof(MoveResult));
            }
        }
    }

    public bool IsCapturingMove
    {
        get => _isCapturingMove;
        set
        {
            if (SetProperty(ref _isCapturingMove, value))
            {
                OnPropertyChanged(nameof(MoveResult));
            }
        }
    }

    public bool IsPromotingMove
    {
        get => _isPromotingMove;
        set
        {
            if (SetProperty(ref _isPromotingMove, value))
            {
                OnPropertyChanged(nameof(MoveResult));
            }
        }
    }

    public bool IsDrawFromMaxMoves
    {
        get => _isDrawFromMaxMoves;
        set
        {
            if (SetProperty(ref _isDrawFromMaxMoves, value))
            {
                OnPropertyChanged(nameof(MoveResult));
            }
        }
    }

    public string MoveShorthand =>
        $"{OriginationSquare.SquareShorthand}:{DestinationSquare.SquareShorthand}";

    public string MoveResult =>
        PutsOpponentInCheckmate ? "Checkmate" :
        PutsOpponentInCheck ? "Check" :
        IsDrawFromMaxMoves ? "Draw" :
        IsCapturingMove ? "Capture" :
        IsPromotingMove ? "Promotion" : "";
}