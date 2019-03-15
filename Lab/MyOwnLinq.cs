using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var p in sources)
            {
                if (predicate(p))
                    result.Add(p);
            }
            return result;
        }
    }
}