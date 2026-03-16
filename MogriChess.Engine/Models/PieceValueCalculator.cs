namespace MogriChess.Engine.Models;

public class PieceValueCalculator
{
    public int CalculatePieceValue(Piece piece)
    {
        if (piece.IsKing)
        {
            return Constants.PieceValue_King;
        }

        return
            CalcForDirection(piece.Forward,
                Constants.PieceValue_ForwardOne,
                Constants.PieceValue_ForwardTwo,
                Constants.PieceValue_ForwardInfinite) +
            CalcForDirection(piece.ForwardRight,
                Constants.PieceValue_ForwardRightOne,
                Constants.PieceValue_ForwardRightTwo,
                Constants.PieceValue_ForwardRightInfinite) +
            CalcForDirection(piece.Right,
                Constants.PieceValue_RightOne,
                Constants.PieceValue_RightTwo,
                Constants.PieceValue_RightInfinite) +
            CalcForDirection(piece.BackRight,
                Constants.PieceValue_BackRightOne,
                Constants.PieceValue_BackRightTwo,
                Constants.PieceValue_BackRightInfinite) +
            CalcForDirection(piece.Back,
                Constants.PieceValue_BackOne,
                Constants.PieceValue_BackTwo,
                Constants.PieceValue_BackInfinite) +
            CalcForDirection(piece.BackLeft,
                Constants.PieceValue_BackLeftOne,
                Constants.PieceValue_BackLeftTwo,
                Constants.PieceValue_BackLeftInfinite) +
            CalcForDirection(piece.Left,
                Constants.PieceValue_LeftOne,
                Constants.PieceValue_LeftTwo,
                Constants.PieceValue_LeftInfinite) +
            CalcForDirection(piece.ForwardLeft,
                Constants.PieceValue_ForwardLeftOne,
                Constants.PieceValue_ForwardLeftTwo,
                Constants.PieceValue_ForwardLeftInfinite);
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