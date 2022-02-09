namespace MogriChess.Models;

public static class ExtensionMethods
{
    public static Enums.Color OppositeColor(this Enums.Color color)
    {
        return color == Enums.Color.Light
            ? Enums.Color.Dark
            : Enums.Color.Light;
    }
}