﻿using System;
using System.Collections;
using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySelectManyTests
    {
        [Test]
        public void flat_all_cities_and_sections()
        {
            var cities = new List<City>
            {
                new City {Name = "台北市", Sections = new List<string> {"大同", "大安", "松山"}},
                new City {Name = "新北市", Sections = new List<string> {"三重", "新莊"}},
            };

            var actual = JoeySelectMany(cities, 
                c => c.Sections,
                (city, citySection) => $"{city.Name}-{citySection}");

            

            var expected = new[]
            {
                "台北市-大同",
                "台北市-大安",
                "台北市-松山",
                "新北市-三重",
                "新北市-新莊",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<string> JoeySelectMany(IEnumerable<City> cities, Func<City, IEnumerable<string>> collectionSelector, Func<City, string, string> resultSelector)
        {
            foreach (var city in cities)
            {
                foreach (var citySection in collectionSelector(city))
                {
                    yield return resultSelector(city, citySection);
                }
            }
        }

        
    }

    public class City
    {
        public string Name { get; set; }
        public IEnumerable<string> Sections { get; set; }
    }
}