using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyIntersectTests
    {
        [Test]
        public void intersect_numbers()
        {
            var first = new[] { 1, 3, 5, 3 };
            var second = new[] { 5, 7, 3, 7 };

            var actual = JoeyIntersect(first, second);

            var expected = new[] { 3, 5 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyIntersect(IEnumerable<int> first, IEnumerable<int> second)
        {
            //var hashFir= new HashSet<int>(first);
            //var hashSec = new HashSet<int>(second);
            

            //var firstEnumerator = hashFir.GetEnumerator();
            //while (firstEnumerator.MoveNext())
            //{
            //    var firstEnumeratorCurrent = firstEnumerator.Current;
            //    if (hashSec.Add(firstEnumeratorCurrent) == false)
            //        yield return firstEnumeratorCurrent;
            //}

            var hashFir = new HashSet<int>(first);
            var hashSec = new HashSet<int>(second);


            var firstEnumerator = hashFir.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var firstEnumeratorCurrent = firstEnumerator.Current;
                if (hashSec.Add(firstEnumeratorCurrent) == false)
                    yield return firstEnumeratorCurrent;
            }
        }
    }
}