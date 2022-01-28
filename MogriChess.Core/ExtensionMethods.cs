using System;
using System.Collections.Generic;
using System.Linq;

namespace MogriChess.Core
{
    public static class ExtensionMethods
    {
        public static T RandomElement<T>(this List<T> options)
        {
            if (options == null || options.None())
            {
                return default;
            }

            return options[RngCreator.GetNumberBetween(0, options.Count - 1)];
        }

        public static bool None<T>(this IEnumerable<T> elements, Func<T, bool> func = null)
        {
            return func == null
                ? !elements.Any()
                : !elements.Any(func.Invoke);
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
}