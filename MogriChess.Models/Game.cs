using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MogriChess.Models
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board { get; }

        public Game(Board board)
        {
            Board = board;
        }

        public Piece PieceAt(int rank, int file)
        {
            return SquareAt(rank, file).Piece;
        }

        private Square SquareAt(int rank, int file)
        {
            return Board.Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));
        }

        public List<Move> ValidDestinationsForPieceAt(int rank, int file)
        {
            Piece piece = PieceAt(rank, file);

            List<Move> validMoves = new List<Move>();

            // Need to handle different directions.
            // Dark pieces face "down", while light pieces face "up".
            int rankForwardMultiplier = piece.ColorType == Enums.ColorType.Light ? 1 : -1;
            int rankBackwardMultiplier = piece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileLeftMultiplier = piece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileRightMultiplier = piece.ColorType == Enums.ColorType.Light ? 1 : -1;

            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForward, rank, file, rankForwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForwardRight, rank, file, rankForwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresRight, rank, file, 0, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBackRight, rank, file, rankBackwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBack, rank, file, rankBackwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBackLeft, rank, file, rankBackwardMultiplier, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresLeft, rank, file, 0, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForwardLeft, rank, file, rankForwardMultiplier, fileLeftMultiplier));

            return validMoves;
        }

        private List<Move> ValidMovesInDirection(int squaresToCheck, int currentRank, int currentFile, 
            int rankMultiplier, int fileMultiplier)
        {
            List<Move> validMoves = new List<Move>();

            for (int i = 1; i <= squaresToCheck; i++)
            {
                int destinationRank = currentRank + (i * rankMultiplier);
                int destinationFile = currentFile + (i * fileMultiplier);

                // Off board, stop checking
                if (destinationRank is < 1 or > 8 || destinationFile is < 1 or > 8)
                {
                    break;
                }

                Piece pieceAtDestination = PieceAt(destinationRank, destinationFile);

                if (pieceAtDestination == null)
                {
                    // Square is empty
                    validMoves.Add(new Move(SquareAt(currentRank, currentFile), SquareAt(destinationRank, destinationFile)));
                }
                else if (pieceAtDestination.ColorType != PieceAt(currentRank, currentFile).ColorType)
                {
                    // Square is occupied by an opponent's piece
                    Move move = new Move(SquareAt(currentRank, currentFile), SquareAt(destinationRank, destinationFile));

                    move.IsCapturingMove = true;
                    move.IsWinningMove = pieceAtDestination.IsKing;

                    validMoves.Add(move);
                }
                else if (pieceAtDestination.ColorType == PieceAt(currentRank, currentFile).ColorType)
                {
                    // Square is occupied by a player's piece
                    break;
                }
            }

            return validMoves;
        }
    }
}