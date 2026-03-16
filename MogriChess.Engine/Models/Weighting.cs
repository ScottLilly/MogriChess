namespace MogriChess.Engine.Models;

public static class Weighting
{
    // Forward
    public const int PieceValue_ForwardOne = 10;
    public const int PieceValue_ForwardTwo = 15;
    public const int PieceValue_ForwardInfinite = 30;

    // Forward-right
    public const int PieceValue_ForwardRightOne = 12;
    public const int PieceValue_ForwardRightTwo = 18;
    public const int PieceValue_ForwardRightInfinite = 32;

    // Right
    public const int PieceValue_RightOne = 10;
    public const int PieceValue_RightTwo = 15;
    public const int PieceValue_RightInfinite = 30;

    // Back-right
    public const int PieceValue_BackRightOne = 12;
    public const int PieceValue_BackRightTwo = 18;
    public const int PieceValue_BackRightInfinite = 32;

    // Back
    public const int PieceValue_BackOne = 8;
    public const int PieceValue_BackTwo = 12;
    public const int PieceValue_BackInfinite = 25;

    // Back-left
    public const int PieceValue_BackLeftOne = 12;
    public const int PieceValue_BackLeftTwo = 18;
    public const int PieceValue_BackLeftInfinite = 32;

    // Left
    public const int PieceValue_LeftOne = 10;
    public const int PieceValue_LeftTwo = 15;
    public const int PieceValue_LeftInfinite = 30;

    // Forward-left
    public const int PieceValue_ForwardLeftOne = 12;
    public const int PieceValue_ForwardLeftTwo = 18;
    public const int PieceValue_ForwardLeftInfinite = 32;

    // King base value
    public const int PieceValue_King = 10000;

    // Bot evaluation weights
    public const int MaterialWeight = 10;
    public const int MobilityWeight = 2;

    public const int KingInCheckPenalty = 1000;
    public const int OpponentKingInCheckBonus = 500;

    public const int CaptureMoveBonus = 10;
    public const int PromotionMoveBonus = 50;

    // Search configuration
    public const int SearchDepth = 2;
    public const int RootMoveBeamWidth = 12;
}
