using System;
using System.Collections.Generic;
using System.Linq;
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
                    yield return item;
                else
                    yield break;
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

        public static IEnumerable<TSource> JoeyTakeWhile<TSource>(this IEnumerable<TSource> cards, Func<TSource, bool> predicate)
        {
            var cardsEnumerator = cards.GetEnumerator();
            while (cardsEnumerator.MoveNext())
            {
                var card = cardsEnumerator.Current;
                if (predicate(card))
                {
                    yield return card;
                }
                else
                {
                    yield break;
                }


            }
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            return sources.Any() == false;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var source = sources.GetEnumerator();
            while (source.MoveNext())
            {
                var e = source.Current;
                if (predicate(e))
                    return true;
            }
            return false;
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();
            return enumerator.MoveNext()
                ? enumerator.Current
                : default(TSource);

        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();
            var last = default(TSource);
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }
            return last;
        }

        public static IEnumerable<TSource> JoeyReverse<TSource>(this IEnumerable<TSource> sources)
        {
            return new Stack<TSource>(sources);


            //var enumerator = sources.GetEnumerator();
            //var stack = new Stack<TSource>();
            //while (enumerator.MoveNext())
            //{
            //    stack.Push(enumerator.Current);
            //}
            //return stack;
        }

        public static IEnumerable<TResult> JoeyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> selector)
        {
            using (var firstEnumerator = first.GetEnumerator())
            using (var secondEnumerator = second.GetEnumerator())

                while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
                {
                    var t1 = firstEnumerator.Current;
                    var t2 = secondEnumerator.Current;
                    yield return selector(t1, t2);
                }
        }

        public static bool JoeySequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return JoeySequenceEqual(first, second, EqualityComparer<TSource>.Default);
        }

        public static bool JoeySequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();

                if (IsLengthDifferent(firstFlag, secondFlag))
                    return false;

                if (IsEnd(firstFlag))
                    return true;

                if (!IsValueEqual(comparer, firstEnumerator, secondEnumerator))
                    return false;

            }
            //var isFirst = firstEnumerator.MoveNext();
            //var isSecond = secondEnumerator.MoveNext();
            //while (isFirst && isSecond)
            //{
            //    TSource f, s;

            //    f = firstEnumerator.Current;
            //    s = secondEnumerator.Current;
            //    if (f.Equals(s) == false)
            //        return false;

            //    isFirst = firstEnumerator.MoveNext();
            //    isSecond = secondEnumerator.MoveNext();

            //    if (isFirst != isSecond)
            //    {
            //        return false;
            //    }

            //    f = firstEnumerator.Current;
            //    s = secondEnumerator.Current;
            //    if (f.Equals(s) == false)
            //        return false;
            //}

            //return true;
        }

        private static bool IsValueEqual<TSource>(IEqualityComparer<TSource> comparer, IEnumerator<TSource> firstEnumerator, IEnumerator<TSource> secondEnumerator)
        {
            return comparer.Equals(firstEnumerator.Current,secondEnumerator.Current);
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }

        public static IOrderedEnumerable<Employee> JoeyThenBy<TKey>(this IOrderedEnumerable<Employee> source, Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
        }

        public static IOrderedEnumerable<Employee> JoeyOrderByKeep<TKey>(this IEnumerable<Employee> employees, Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
        {
            return new MyOrderedEnumerable(employees, new CombineComparer<TKey>(keySelector, comparer));
        }

        public static IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees,
            ComboComparer comboComparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];
                    var lastNameCompare = comboComparer.Compare(element, minElement);
                    if (lastNameCompare < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                    else if (lastNameCompare == 0)
                    {
                        var firstNameCompare = comboComparer.Compare(element, minElement);
                        if (firstNameCompare < 0)
                        {
                            minElement = element;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        public static IEnumerable<TSource> JoeyDistinct<TSource>(this IEnumerable<TSource> source)
        {
            return source.JoeyDistinctWithEqualityComparer(EqualityComparer<TSource>.Default);
            //return new HashSet<int>(source);
            //var hashSet = new HashSet<TSource>();
            //var enumerator = source.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    var item = enumerator.Current;
            //    if (hashSet.Add(item))
            //    {
            //        yield return item;
            //    }
            //}
        }

        public static IEnumerable<TSource> JoeyDistinctWithEqualityComparer<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            //return new HashSet<int>(source);
            var hashSet = new HashSet<TSource>(comparer);
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (hashSet.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<int> JoeyIntersect(this IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSec = new HashSet<int>(second);
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var firstEnumeratorCurrent = firstEnumerator.Current;
                if (hashSec.Remove(firstEnumeratorCurrent))
                    yield return firstEnumeratorCurrent;
            }

            //var hashFir = new HashSet<int>(first);
            //var hashSec = new HashSet<int>(second);


            //var firstEnumerator = hashFir.GetEnumerator();
            //while (firstEnumerator.MoveNext())
            //{
            //    var firstEnumeratorCurrent = firstEnumerator.Current;
            //    if (hashSec.Add(firstEnumeratorCurrent) == false)
            //        yield return firstEnumeratorCurrent;
            //}
        }

        public static IEnumerable<int> JoeyExcept(this IEnumerable<int> first, IEnumerable<int> second)
        {
            var hash = new HashSet<int>(second);
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var current = firstEnumerator.Current;
                if (hash.Add(current))
                {
                    yield return current;
                }
            }
        }

        public static IEnumerable<int> JoeySkipLast(this IEnumerable<int> numbers, int count)
        {
            //var stack = new Stack<int>(numbers);

            //for (int i = 0; i < count; i++)
            //{
            //    stack.Pop();
            //}

            //return new Stack<int>(stack);

            //var queue = new Queue<int>(numbers);
            //var queueCount = queue.Count;
            //for (int i = 0; i < queueCount - count; i++)
            //{
            //    yield return queue.Dequeue();
            //}

            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (queue.Count == count)
                    yield return queue.Dequeue();

                queue.Enqueue(current);
            }
        }
    }
}