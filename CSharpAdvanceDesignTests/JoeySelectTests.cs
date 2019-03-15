﻿using System;
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
                url=>url.Replace("http://", "https://")+"/joey");
            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private IEnumerable<string> JoeySelect(IEnumerable<string> urls, Func<string,string> mapper)
        {
            var result = new List<string>();
            foreach (var url in urls)
            {
                var mapResult = mapper(url);
                result.Add(mapResult);
            }

            return result;
        }


        //private IEnumerable<string> JoeySelectAppend(IEnumerable<string> urls, Func<string,string> mapper)
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