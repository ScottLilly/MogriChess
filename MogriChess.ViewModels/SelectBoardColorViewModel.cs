using System.Collections.ObjectModel;
using System.Linq;
using MogriChess.Core;
using MogriChess.Models;

namespace MogriChess.ViewModels;

public class SelectBoardColorViewModel : ObservableObject
{
    private ColorScheme _selectedColorScheme;

    public ColorScheme SelectedColorScheme
    {
        get => _selectedColorScheme;
        set => SetProperty(ref _selectedColorScheme, value);
    }

    public ObservableCollection<ColorScheme> ColorSchemes { get; } =
        new ObservableCollection<ColorScheme>();

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