﻿using Lab.Entities;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        //[Test]
        //public void three_employees()
        //{
        //    var emptyEmployees = new Employee[]
        //    {
        //        new Employee(),
        //        new Employee(),
        //        new Employee(),
        //    };

        //    var actual = JoeyAny(emptyEmployees);
        //    Assert.IsTrue(actual);
        //}

        //[Test]
        //public void empty_employees()
        //{
        //    var emptyEmployees = new Employee[]
        //    {
        //    };

        //    var actual = JoeyAny(emptyEmployees);
        //    Assert.IsFalse(actual);
        //}

        [Test]
        public void price_more_than_500()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var joeyAny = products.JoeyAny(p => p.Price > 500);
            Assert.IsTrue(joeyAny);
        }

        [Test]
        public void test()
        {
            var names = new String[]{};
            var ids = new[] {1, 2};

            if (names.IsEmpty() && ids.IsEmpty())
            {
                Console.WriteLine();
            }
        }
    }
}