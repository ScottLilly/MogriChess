using System.Collections.ObjectModel;
using System.Linq;
using MogriChess.Engine.Core;
using MogriChess.Engine.Models;

namespace MogriChess.Engine.ViewModels;

public class SelectBoardColorViewModel : ObservableObject
{
    private ColorScheme _selectedColorScheme;

    public ColorScheme SelectedColorScheme
    {
        get => _selectedColorScheme;
        set => SetProperty(ref _selectedColorScheme, value);
    }

    public ObservableCollection<ColorScheme> ColorSchemes { get; } =
        [];

    public SelectBoardColorViewModel()
    {
        foreach (ColorScheme colorScheme in Constants.ColorSchemes)
        {
            ColorSchemes.Add(colorScheme);
        }

        SelectedColorScheme = ColorSchemes.First();
        ;
    }
}