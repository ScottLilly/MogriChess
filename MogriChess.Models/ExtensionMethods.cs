namespace MogriChess.Models
{
    internal static class ExtensionMethods
    {
        internal static Enums.Color OppositeColor(this Enums.Color color)
        {
            return color == Enums.Color.Light
                ? Enums.Color.Dark
                : Enums.Color.Light;
        }
    }
}