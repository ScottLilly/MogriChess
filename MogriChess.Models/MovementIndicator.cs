using System;
using System.ComponentModel;

namespace MogriChess.Models;

public class MovementIndicator : INotifyPropertyChanged
{
    public int Squares { get; private set; }

    public bool DrawOneSquareIndicator => Squares is 1 or 2;
    public bool DrawTwoSquaresIndicator => Squares == 2;
    public bool DrawInfiniteSquaresIndicator => Squares == Constants.UnlimitedMoves;

    public event PropertyChangedEventHandler PropertyChanged;

    public void MergeMovementAbility(int squares) =>
        Squares = Math.Max(Squares, squares);
}