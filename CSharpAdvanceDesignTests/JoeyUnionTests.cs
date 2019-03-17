using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5 ,3,1};
            var second = new[] { 5, 3, 7,7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            return JoeyUnionWithComparer(first, second, EqualityComparer<int>.Default);
        }
        private static IEnumerable<TSource> 
            JoeyUnionWithComparer<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second,IEqualityComparer<TSource> comparer)
        {
            
            var hashSet = new HashSet<TSource>(comparer);
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var firstCurrent = firstEnumerator.Current;
                if (hashSet.Add(firstCurrent))
                    yield return firstCurrent;
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                var secondEnumeratorCurrent = secondEnumerator.Current;
                if (hashSet.Add(secondEnumeratorCurrent))
                {
                    yield return secondEnumeratorCurrent;
                }
            }
        }
    }
}