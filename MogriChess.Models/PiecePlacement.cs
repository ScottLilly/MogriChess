namespace MogriChess.Models;

public class PiecePlacement
{
    public Piece Piece { get; }
    public int Rank { get; }
    public int File { get; }

    public string Shorthand =>
        ModelFunctions.GetShorthand(Rank, File);

    public PiecePlacement(int rank, int file, Piece piece)
    {
        Rank = rank;
        File = file;
        Piece = piece;
    }
}