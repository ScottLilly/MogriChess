namespace MogriChess.Models;

public class ColorScheme
{
    public string Name { get; set; }
    public string LightColor { get; set; }
    public string DarkColor { get; set; }

    public ColorScheme()
    {
    }

    public ColorScheme(string name, string lightColor, string darkColor)
    {
        Name = name;
        LightColor = lightColor;
        DarkColor = darkColor;
    }
}