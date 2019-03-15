using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = JoeySelect(urls,
                url => url.Replace("http://", "https://"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void replace_http_to_https_and_append_joey()
        {
            var urls = GetUrls();

            var actual = JoeySelect(urls,
                url => url.Replace("http://", "https://") + "/joey");
            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void get_full_name()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var expected = new[]
            {
                "Joey-Chen",
                "Tom-Li",
                "David-Chen",
            };

            var actual = JoeySelect(employees, e => $"{e.FirstName}-{e.LastName}");
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_full_length()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var expected = new[]
            {
                8,5,9
            };

            var actual = JoeySelect(employees, e => $"{e.FirstName}{e.LastName}".Length);
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_full_name_with_no()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
            var expected = new[]
            {
                "1.Joey-Chen",
                "2.Tom-Li",
                "3.David-Chen",
            };

            var actual = JoeySelect(employees, (e, i) => $"{i}.{e.FirstName}-{e.LastName}");
            expected.ToExpectedObject().ShouldMatch(actual);
        }



        //private IEnumerable<string> JoeySelect(IEnumerable<string> urls, Func<string, string> selector)
        //{
        //    var result = new List<string>();
        //    foreach (var url in urls)
        //    {
        //        var mapResult = selector(url);
        //        result.Add(mapResult);
        //    }

        //    return result;
        //}

        //private IEnumerable<string> JoeySelect(IEnumerable<Employee> emps, Func<Employee, string> selector)
        //{
        //    var result = new List<string>();
        //    foreach (var emp in emps)
        //    {
        //        var mapResult = selector(emp);
        //        result.Add(mapResult);
        //    }

        //    return result;
        //}

        private IEnumerable<TResult> JoeySelect<TSource, TResult>(IEnumerable<TSource> sources,
            Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var source in sources)
            {
                result.Add(selector(source));
            }

            return result;
        }

        private IEnumerable<TResult> JoeySelect<TSource, TResult>(IEnumerable<TSource> sources,
            Func<TSource, int, TResult> selector)
        {
            //var result = new List<TResult>();
            var index = 0;
            foreach (var source in sources)
            {
                //result.Add(selector(source, ++index));
                yield return selector(source, ++index);
            }

            //return result;
        }


        //private IEnumerable<string> JoeySelectAppend(IEnumerable<string> urls, Func<string,string> selector)
        //{
        //    var result = new List<string>();
        //    foreach (var url in urls)
        //    {
        //        var mapResult = url.Replace("http://", "https://") + "/joey";
        //        result.Add(mapResult);
        //    }

        //    return result;
        //}


        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}