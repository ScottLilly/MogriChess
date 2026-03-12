using System.Collections.Generic;
using System.Linq;
using MogriChess.Engine.DTOs;
using MogriChess.Engine.Models;
using MogriChess.Engine.ViewModels;

namespace MogriChess.Engine.Services;

public static class Mapper
{
    public static GameStateDTO ToGameStateDto(Game game)
    {
        return new GameStateDTO
        {
            BoardColorScheme = game.Board.BoardColorScheme,
            PieceColorScheme = game.Board.PieceColorScheme,
            CurrentPlayerColor = game.CurrentPlayerColor.ToString(),
            MoveHistory = ToMoveHistoryDtos(game.MoveHistory),
            Squares = game.Board.Squares.Values
                .Select(ToSquareDto)
                .ToList()
        };
    }

    public static List<MoveHistoryDTO> ToMoveHistoryDtos(IEnumerable<MoveStruct> moves)
    {
        return moves
            .Select(m => new MoveHistoryDTO
            {
                MovingPieceColor = m.MovingPieceColor,
                MoveShorthand = m.MoveShorthand,
                MoveResult = m.MoveResult
            })
            .ToList();
    }

    public static SquareDTO ToSquareDto(Square square)
    {
        return new SquareDTO
        {
            Rank = square.Rank,
            File = square.File,
            Piece = ToPieceDto(square.Piece),
            IsSelected = square.IsSelected,
            IsValidDestination = square.IsValidDestination
        };
    }

    public static PieceDTO ToPieceDto(Piece piece)
    {
        if (piece == null)
        {
            return null;
        }

        return new PieceDTO
        {
            Color = piece.Color.ToString(),
            ForwardSquares = piece.Forward,
            ForwardRightSquares = piece.ForwardRight,
            RightSquares = piece.Right,
            BackRightSquares = piece.BackRight,
            BackSquares = piece.Back,
            BackLeftSquares = piece.BackLeft,
            LeftSquares = piece.Left,
            ForwardLeftSquares = piece.ForwardLeft,
            IsKing = piece.IsKing,
            IsUnpromotedPawn = piece.IsUnpromotedPawn
        };
    }
}

