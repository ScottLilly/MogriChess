namespace MogriChess.Models
{
    public static class ModelFunctions
    {
        public static string GetShorthand(int rank, int file)
        {
            return $"{"abcdefgh".Substring(file - 1, 1)}{"12345678".Substring(rank - 1, 1)}";
        }
    }
}