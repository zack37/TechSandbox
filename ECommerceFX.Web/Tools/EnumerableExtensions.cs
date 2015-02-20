using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceFX.Web.Tools
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            return source.Select(x =>
            {
                action(x);
                return x;
            });
        }

        public static HashSet<TElement> ToHashSet<TElement>(this IEnumerable<TElement> source, IEqualityComparer<TElement> compare = null)
        {
            return new HashSet<TElement>(source, compare);
        }
    }
}