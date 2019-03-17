using System;
using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyExceptTests
    {
        [Test]
        public void except_numbers()
        {
            var first = new[] {1, 3, 5, 7, 3};
            var second = new[] {7, 1, 4, 1};

            var actual = first.JoeyExcept(second);
            var expected = new[] {3, 5};

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        [Test]
        public void skip_last_2()
        {
            var numbers = new[] {10, 20, 30, 40, 50, 60,70};
            var actual = numbers.JoeySkipLast(2);

            var expected = new[] {10, 20, 30, 40,50};

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}