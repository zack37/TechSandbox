using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceFX.Web.Tools
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
            return source;
        }
    }
}