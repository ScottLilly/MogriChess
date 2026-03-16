namespace MogriChess.Engine.Models;

public class PieceValueCalculator
{
    public int CalculatePieceValue(Piece piece)
    {
        if (piece.IsKing)
        {
            return Weighting.PieceValue_King;
        }

        return
            CalcForDirection(piece.Forward,
                Weighting.PieceValue_ForwardOne,
                Weighting.PieceValue_ForwardTwo,
                Weighting.PieceValue_ForwardInfinite) +
            CalcForDirection(piece.ForwardRight,
                Weighting.PieceValue_ForwardRightOne,
                Weighting.PieceValue_ForwardRightTwo,
                Weighting.PieceValue_ForwardRightInfinite) +
            CalcForDirection(piece.Right,
                Weighting.PieceValue_RightOne,
                Weighting.PieceValue_RightTwo,
                Weighting.PieceValue_RightInfinite) +
            CalcForDirection(piece.BackRight,
                Weighting.PieceValue_BackRightOne,
                Weighting.PieceValue_BackRightTwo,
                Weighting.PieceValue_BackRightInfinite) +
            CalcForDirection(piece.Back,
                Weighting.PieceValue_BackOne,
                Weighting.PieceValue_BackTwo,
                Weighting.PieceValue_BackInfinite) +
            CalcForDirection(piece.BackLeft,
                Weighting.PieceValue_BackLeftOne,
                Weighting.PieceValue_BackLeftTwo,
                Weighting.PieceValue_BackLeftInfinite) +
            CalcForDirection(piece.Left,
                Weighting.PieceValue_LeftOne,
                Weighting.PieceValue_LeftTwo,
                Weighting.PieceValue_LeftInfinite) +
            CalcForDirection(piece.ForwardLeft,
                Weighting.PieceValue_ForwardLeftOne,
                Weighting.PieceValue_ForwardLeftTwo,
                Weighting.PieceValue_ForwardLeftInfinite);
    }

    private static int CalcForDirection(int squares, int valueOne, int valueTwo, int valueInfinite)
    {
        switch (squares)
        {
            case 1:
                return valueOne;
            case 2:
                return valueTwo;
            case Constants.UnlimitedMoves:
                return valueInfinite;
            default:
                return 0;
        }
    }
}