using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {

            var souEnumerator = sources.GetEnumerator();
            while (souEnumerator.MoveNext())
            {
                var item = souEnumerator.Current;

                if (predicate(item))
                    yield return item;
            }
            //var result = new List<TSource>();
            //foreach (var p in sources)
            //{
            //    if (predicate(p))
            //        yield return p;
            //    //result.Add(p);
            //}
            //return result;
        }
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> sources, Func<TSource, int, bool> predicate)
        {
            //var result = new List<TSource>();
            var index = 0;
            foreach (var p in sources)
            {
                if (predicate(p, index++))
                    yield return p;
                //result.Add(p);
            }
            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources,
            Func<TSource, TResult> selector)
        {
            //var result = new List<TResult>();
            foreach (var source in sources)
            {
                //result.Add(selector(source));
                yield return selector(source);
            }

            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources,
            Func<TSource, int, TResult> selector)
        {
            //var result = new List<TResult>();
            var index = 0;
            foreach (var source in sources)
            {
                //result.Add(selector(source, ++index));
                yield return selector(source, index++);
            }

            //return result;
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> employees, int takeCount)
        {

            var employeeEnumerator = employees.GetEnumerator();
            var i = 0;
            while (employeeEnumerator.MoveNext())
            {
                var item = employeeEnumerator.Current;
                if (i++ < takeCount)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> employees, int skipCount)
        {
            var employeesEnumerator = employees.GetEnumerator();
            var count = 0;
            while (employeesEnumerator.MoveNext())
            {
                var item = employeesEnumerator.Current;
                if (count++ >= skipCount)
                    yield return item;
            }
        }
    }
}