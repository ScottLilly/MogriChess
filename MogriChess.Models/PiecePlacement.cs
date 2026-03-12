namespace MogriChess.Models;

public class PiecePlacement(int rank, int file, Piece piece)
{
    public Piece Piece { get; } = piece;
    public string Shorthand { get; } = ModelFunctions.GetShorthand(rank, file);
}