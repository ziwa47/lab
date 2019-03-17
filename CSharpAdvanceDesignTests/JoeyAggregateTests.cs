using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ExpectedObjects;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList, balance, 
                (draw, myBalance) => draw > myBalance ? myBalance : myBalance - draw, 
                myBalance1 => new Employee
                {
                    Saving = 10.91m
                });

            //var expected = 10.91m;
            var expected = new Employee
            {p
                Saving = 10.91m
            };

           expected.ToExpectedObject().ShouldEqual(actual);
        }

        private TResut JoeyAggregate<TResut>(IEnumerable<int> drawlingList, decimal balance, Func<int, decimal, decimal> func, Func<decimal, TResut> resultSelector)
        {
            var drawEnumerator = drawlingList.GetEnumerator();
            var myBalance = balance;
            while (drawEnumerator.MoveNext())
            {
                var draw = drawEnumerator.Current;
                myBalance = func(draw, myBalance);
            }

            //foreach (var draw in drawlingList)
            //{
            //    myBalance = draw > myBalance ? myBalance : myBalance - draw;
            //}

            return resultSelector(myBalance);

            //return myBalance;

            
        }
    }
}