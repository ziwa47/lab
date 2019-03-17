using NUnit.Framework;
using NUnit.Framework.Internal;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyCastTests
    {
        [Test]
        public void cast_int_exception_when_ArrayList_has_string()
        {
            var arrayList = new ArrayList { 1, "a", 3 };

            void TestDelegate() => JoeyCast<int>(arrayList).ToArray();

            Assert.Throws<JoeyCastException>(TestDelegate);
        }

        private static IEnumerable<T> JoeyCast<T>(IEnumerable source)
        {
            var sourceEnumerator = source.GetEnumerator();
            while (sourceEnumerator.MoveNext())
            {
                var current = sourceEnumerator.Current;
                yield return current is T item ? item : throw new JoeyCastException();
            }
            // T 強轉 is as cast
        }
    }

    public class JoeyCastException :Exception
    {
    }
}