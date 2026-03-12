using MogriChess.Engine.Core;
using MogriChess.Engine.Models;

namespace MogriChess.Engine.ViewModels;

/// <summary>
/// WPF-facing wrapper around the core <see cref="Move"/> engine type.
/// Provides <see cref="INotifyPropertyChanged"/> for binding without coupling the engine model to UI concerns.
/// </summary>
public class MoveViewModel(Move move) : ObservableObject
{
    public Move Move => move;

    public Color MovingPieceColor => move.MovingPieceColor;

    public Square OriginationSquare => move.OriginationSquare;

    public Square DestinationSquare => move.DestinationSquare;

    public bool PutsOpponentInCheckmate
    {
        get => move.PutsOpponentInCheckmate;
        set
        {
            if (move.PutsOpponentInCheckmate == value)
            {
                return;
            }

            move.PutsOpponentInCheckmate = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoveResult));
        }
    }

    public bool PutsOpponentInCheck
    {
        get => move.PutsOpponentInCheck;
        set
        {
            if (move.PutsOpponentInCheck == value)
            {
                return;
            }

            move.PutsOpponentInCheck = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoveResult));
        }
    }

    public bool IsCapturingMove
    {
        get => move.IsCapturingMove;
        set
        {
            if (move.IsCapturingMove == value)
            {
                return;
            }

            move.IsCapturingMove = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoveResult));
        }
    }

    public bool IsPromotingMove
    {
        get => move.IsPromotingMove;
        set
        {
            if (move.IsPromotingMove == value)
            {
                return;
            }

            move.IsPromotingMove = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoveResult));
        }
    }

    public bool IsDrawFromMaxMoves
    {
        get => move.IsDrawFromMaxMoves;
        set
        {
            if (move.IsDrawFromMaxMoves == value)
            {
                return;
            }

            move.IsDrawFromMaxMoves = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MoveResult));
        }
    }

    public string MoveShorthand => move.MoveShorthand;

    public string MoveResult => move.MoveResult;
}
