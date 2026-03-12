namespace MogriChess.Engine.Models;

public class PieceValueCalculator(PieceValueCalculatorGenome genome)
{
    public int CalculatePieceValue(Piece piece)
    {
        if (piece.IsKing)
        {
            return genome.KingValue;
        }

        return
            CalcForDirection(piece.Forward, genome.ForwardOne, genome.ForwardTwo, genome.ForwardInfinite) +
            CalcForDirection(piece.ForwardRight, genome.ForwardRightOne, genome.ForwardRightTwo, genome.ForwardRightInfinite) +
            CalcForDirection(piece.Right, genome.RightOne, genome.RightTwo, genome.RightInfinite) +
            CalcForDirection(piece.BackRight, genome.BackRightOne, genome.BackRightTwo, genome.BackRightInfinite) +
            CalcForDirection(piece.Back, genome.BackOne, genome.BackTwo, genome.BackInfinite) +
            CalcForDirection(piece.BackLeft, genome.BackLeftOne, genome.BackLeftTwo, genome.BackLeftInfinite) +
            CalcForDirection(piece.Left, genome.LeftOne, genome.LeftTwo, genome.LeftInfinite) +
            CalcForDirection(piece.ForwardLeft, genome.ForwardLeftOne, genome.ForwardLeftTwo, genome.ForwardLeftInfinite);
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