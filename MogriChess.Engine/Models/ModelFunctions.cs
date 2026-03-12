using System;

namespace MogriChess.Engine.Models;

public static class ModelFunctions
{
    private const string Files = "abcdefgh";
    private const string Ranks = "12345678";

    public static string GetShorthand(int rank, int file)
    {
        if (!IsValidRank(rank))
        {
            throw new ArgumentOutOfRangeException(nameof(rank),
                $"Rank must be between 1 and {Constants.NumberOfRanks}.");
        }

        if (!IsValidFile(file))
        {
            throw new ArgumentOutOfRangeException(nameof(file),
                $"File must be between 1 and {Constants.NumberOfFiles}.");
        }

        char fileChar = Files[file - 1];
        char rankChar = Ranks[rank - 1];

        return $"{fileChar}{rankChar}";
    }

    public static bool IsValidRank(int rank) =>
        rank is >= 1 and <= Constants.NumberOfRanks;

    public static bool IsValidFile(int file) =>
        file is >= 1 and <= Constants.NumberOfFiles;
}
