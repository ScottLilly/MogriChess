namespace MogriChess.Models;

public class ColorScheme
{
    public string LightColor { get; set; }
    public string DarkColor { get; set; }

    public ColorScheme()
    {
    }

    public ColorScheme(string lightColor, string darkColor)
    {
        LightColor = lightColor;
        DarkColor = darkColor;
    }
}