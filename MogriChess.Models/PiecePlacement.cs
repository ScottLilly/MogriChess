namespace MogriChess.Models;

public class PiecePlacement
{
    public Piece Piece { get; }
    public string Shorthand { get; }

    public PiecePlacement(int rank, int file, Piece piece)
    {
        Piece = piece;
        Shorthand = ModelFunctions.GetShorthand(rank, file);
    }
}