using System.Collections.Generic;

namespace MogriChess.Engine.Models;

public static class Constants
{
    public const int NumberOfFiles = 8;
    public const int NumberOfRanks = 8;

    public const int UnlimitedMoves = 99;

    public const int BackRankDark = 8;
    public const int PawnRankDark = 7;
    public const int PawnRankLight = 2;
    public const int BackRankLight = 1;

    public const string DefaultBoardColorSchemeName = "Grey";

    public const string DefaultPieceColorSchemeName = "DefaultPieces";
    public const string DefaultLightPieceColorHex = "#FFFFFF";
    public const string DefaultDarkPieceColorHex = "#000000";

    public static List<ColorScheme> ColorSchemes { get; } =
        [];

    static Constants()
    {
        ColorSchemes.Add(new ColorScheme(DefaultBoardColorSchemeName, "#DDDDDD", "#ADADAD"));
        ColorSchemes.Add(new ColorScheme("Blue", "#A2CEE5", "#809AAA"));
        ColorSchemes.Add(new ColorScheme("Brown", "#EDB67B", "#BE6837"));
        ColorSchemes.Add(new ColorScheme("Tan", "#EAC89A", "#B58863"));
        ColorSchemes.Add(new ColorScheme("Green", "#C5D37C", "#769656"));
        ColorSchemes.Add(new ColorScheme("Orange", "#F9C966", "#D08B18"));
        ColorSchemes.Add(new ColorScheme("Purple", "#C9BBCC", "#8877B7"));
        ColorSchemes.Add(new ColorScheme("Red", "#efa9a4", "#BA5546"));
    }
}
