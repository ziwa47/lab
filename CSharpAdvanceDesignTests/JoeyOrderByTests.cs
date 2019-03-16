using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
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
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };
            var firstCombineComparer = new CombineComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondCombineComparer = new CombineComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var actual = MyOwnLinq.JoeyOrderBy(employees, new ComboComparer(firstCombineComparer, secondCombineComparer));

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
        public void orderBy_lastName_and_firstName_and_age()
        {

            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };
            var firstCombineComparer = new CombineComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondCombineComparer = new CombineComparer<string>(element => element.FirstName, Comparer<string>.Default);
            var thirdCombineComparer = new CombineComparer<int>(element => element.Age, Comparer<int>.Default);
            var firstCombo = new ComboComparer(firstCombineComparer, secondCombineComparer);
            var finalCombo = new ComboComparer(firstCombo, thirdCombineComparer);
            var actual = MyOwnLinq.JoeyOrderBy(employees, finalCombo);


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

        [Test]
        public void lastname_thenby_firstname_thenby_age()
        {

            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };


            //var firstCombineComparer = new CombineComparer<string>(element => element.LastName, Comparer<string>.Default);
            //var secondCombineComparer = new CombineComparer<string>(element => element.FirstName, Comparer<string>.Default);
            //var thirdCombineComparer = new CombineComparer<int>(element => element.Age, Comparer<int>.Default);
            //var firstCombo = new ComboComparer(firstCombineComparer, secondCombineComparer);
            //var finalCombo = new ComboComparer(firstCombo, thirdCombineComparer);


            //var actual = MyOwnLinq.JoeyOrderBy(employees, finalCombo);

            var actual = employees.JoeyOrderByKeep(e => e.LastName, Comparer<string>.Default)
                .JoeyThenBy(e => e.FirstName, Comparer<string>.Default)
                .JoeyThenBy(e => e.Age, Comparer<int>.Default);

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
    }
}