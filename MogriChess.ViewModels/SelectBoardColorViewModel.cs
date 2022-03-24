using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MogriChess.Models;

namespace MogriChess.ViewModels;

public class SelectBoardColorViewModel : INotifyPropertyChanged
{
    public ColorScheme SelectedColorScheme { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<ColorScheme> ColorSchemes { get; } =
        new ObservableCollection<ColorScheme>();

    public SelectBoardColorViewModel(string currentColorSchemeName)
    {
        foreach (ColorScheme colorScheme in Constants.ColorSchemes)
        {
            ColorSchemes.Add(colorScheme);
        }

        SelectedColorScheme = ColorSchemes.FirstOrDefault(c =>
            c.Name.Equals(currentColorSchemeName, StringComparison.InvariantCultureIgnoreCase)) ??
                                  ColorSchemes.First();
        ;
    }
}