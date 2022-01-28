namespace MogriChess.Models;

public class ColorScheme
{
    public string LightColor { get; }
    public string DarkColor { get; }

    public ColorScheme(string lightColor, string darkColor)
    {
        LightColor = lightColor;
        DarkColor = darkColor;
    }
}