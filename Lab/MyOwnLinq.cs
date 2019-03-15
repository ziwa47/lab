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
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource,int, bool> predicate)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var p in sources)
            {
                if (predicate(p,index++))
                    result.Add(p);
            }
            return result;
        }
    }
}