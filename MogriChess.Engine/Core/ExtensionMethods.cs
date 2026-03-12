using System;
using System.Collections.Generic;
using System.Linq;
using MogriChess.Engine.Models;
using System.Security.Cryptography;

namespace MogriChess.Engine.Core;

public static class ExtensionMethods
{
    public static Color OppositeColor(this Color color)
    {
        return color == Color.Light ? Color.Dark : Color.Light;
    }

    public static T RandomElement<T>(this List<T> options)
    {
        if (options == null || options.None())
        {
            return default;
        }

        // Need to add one to options.Count, because otherwise,
        // this method will never generate a value that matches options.Count - 1.
        // For example: to get a value from 0 to 9 (inclusive),
        // the code must (effectively) call: RandomNumberGenerator.GetInt32(0, 10);
        int index = RandomNumberGenerator.GetInt32(0, options.Count);
        return options[index];
    }

    public static bool None<T>(this IEnumerable<T> elements, Func<T, bool> func = null)
    {
        return func == null ? !elements.Any() : !elements.Any(func.Invoke);
    }

    public static bool IsEven(this int val)
    {
        return val % 2 == 0;
    }

    public static bool IsOdd(this int val)
    {
        return !val.IsEven();
    }

    public static void ApplyToEach<T>(this IEnumerable<T> elements, Action<T> func)
    {
        foreach (T element in elements)
        {
            func(element);
        }
    }
}