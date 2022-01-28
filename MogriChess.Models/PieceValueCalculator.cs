namespace MogriChess.Models;

public class PieceValueCalculator
{
    private readonly PieceValueCalculatorGenome _genome;

    public PieceValueCalculator(PieceValueCalculatorGenome genome)
    {
        _genome = genome;
    }

    public int CalculatePieceValue(Piece piece)
    {
        if (piece.IsKing)
        {
            return _genome.KingValue;
        }

        return
            CalcForDirection(piece.Forward.Squares, _genome.ForwardOne, _genome.ForwardTwo, _genome.ForwardInfinite) +
            CalcForDirection(piece.ForwardRight.Squares, _genome.ForwardRightOne, _genome.ForwardRightTwo, _genome.ForwardRightInfinite) +
            CalcForDirection(piece.Right.Squares, _genome.RightOne, _genome.RightTwo, _genome.RightInfinite) +
            CalcForDirection(piece.BackRight.Squares, _genome.BackRightOne, _genome.BackRightTwo, _genome.BackRightInfinite) +
            CalcForDirection(piece.Back.Squares, _genome.BackOne, _genome.BackTwo, _genome.BackInfinite) +
            CalcForDirection(piece.BackLeft.Squares, _genome.BackLeftOne, _genome.BackLeftTwo, _genome.BackLeftInfinite) +
            CalcForDirection(piece.Left.Squares, _genome.LeftOne, _genome.LeftTwo, _genome.LeftInfinite) +
            CalcForDirection(piece.ForwardLeft.Squares, _genome.ForwardLeftOne, _genome.ForwardLeftTwo, _genome.ForwardLeftInfinite);
    }

    private static int CalcForDirection(int squares,
        int valueOne, int valueTwo, int valueInfinite)
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