using ExpectedObjects;

using Lab.Entities;

using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CSharpAdvanceDesignTests
{
    public class MyOrderedEnumerable : IOrderedEnumerable<Employee>
    {

        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector, IComparer<TKey> comparer, bool @descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    

    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private IComparer<TKey> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element),KeySelector(minElement));
        }
    }

    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        private IComparer<Employee> FirstComparer { get; set; }
        private IComparer<Employee> SecondComparer { get; set; }

        public int Compare(Employee x, Employee y)
        {
            var compareResult = FirstComparer.Compare(x,y);
            if (compareResult == 0)
                return SecondComparer.Compare(x, y);
            return compareResult;
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderBy(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}
        [Test]
        public void orderBy_lastName_then_first_name()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);
            var firstCombo = new ComboComparer(firstComparer, secondComparer);
            var actual = JoeyOrderBy(employees, firstCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
        [Test]
        public void orderBy_lastName_then_first_name_then_age()
        {

            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var thirdComparer = new CombineKeyComparer<int>(element => element.Age, Comparer<int>.Default);

            var firstCombo = new ComboComparer(firstComparer, secondComparer);
            var finalCombo = new ComboComparer(firstCombo, thirdComparer);
            var actual = JoeyOrderBy(employees, finalCombo);


            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }


        private IEnumerable<TSource> JoeyOrderBy<TSource>(
            IEnumerable<TSource> employees, IComparer<TSource> comboComparer)
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
                    if (comboComparer.Compare(element,minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}