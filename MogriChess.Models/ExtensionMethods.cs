namespace MogriChess.Models
{
    internal static class ExtensionMethods
    {
        internal static Enums.ColorType OpponentColorType(this Enums.ColorType colorType)
        {
            return colorType == Enums.ColorType.Light
                ? Enums.ColorType.Dark
                : Enums.ColorType.Light;
        }
    }
}