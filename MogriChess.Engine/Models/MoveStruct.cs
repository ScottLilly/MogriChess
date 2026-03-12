namespace MogriChess.Engine.Models;

public readonly record struct MoveStruct(
    string MovingPieceColor,
    string MoveShorthand,
    string MoveResult,
    bool IsCapture,
    bool IsPawnMove);