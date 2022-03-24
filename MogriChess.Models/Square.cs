using System.ComponentModel;
using MogriChess.Core;

namespace MogriChess.Models;

public class Square : INotifyPropertyChanged
{
    public ColorScheme ColorScheme { get; set; }

    private string FileAsLetter =>
        "abcdefgh".Substring(File - 1, 1);

    public int Rank { get; }
    public int File { get; }
    public Piece Piece { get; set; }
    public bool IsSelected { get; set; }
    public bool IsValidDestination { get; set; }

    public bool IsEmpty => Piece == null;
    public Enums.Color Color =>
        (Rank + File).IsEven()
            ? Enums.Color.Dark
            : Enums.Color.Light;
    public string SquareColor =>
        Color == Enums.Color.Light
            ? ColorScheme.LightColor
            : ColorScheme.DarkColor;
    public string SquareShorthand => $"{FileAsLetter}{Rank}";
    public int UiGridRow => Constants.NumberOfRanks - Rank;
    public int UiGridColumn => File - 1;

    public event PropertyChangedEventHandler PropertyChanged;

    public Square(int rank, int file, ColorScheme colorScheme)
    {
        Rank = rank;
        File = file;

        ColorScheme = colorScheme;
    }
}