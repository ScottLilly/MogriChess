using System.Linq;
using MogriChess.Models;

namespace MogriChess.Services
{
    internal static class ExtensionMethods
    {
        internal static Square SquareAt(this Board board, int rank, int file)
        {
            return board.Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));
        }
    }
}