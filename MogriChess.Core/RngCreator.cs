using System.Security.Cryptography;

namespace MogriChess.Core
{
    public static class RngCreator
    {
        public static int GetNumberBetween(int minimumValue, int maximumValue)
        {
            // Need to add one to maximumValue, because otherwise,
            // this function will never generate a value that matches the maximumValue.
            // For example: to get a value from 1 to 10 (inclusive),
            // The code must (effectively) call: RandomNumberGenerator.GetInt32(1, 11);
            return RandomNumberGenerator.GetInt32(minimumValue, maximumValue + 1);
        }
    }
}