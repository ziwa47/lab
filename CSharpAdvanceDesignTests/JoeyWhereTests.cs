using System;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void find_products_that_price_between_200_and_500()
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

            var actual = products.JoeyWhere(p => p.Price > 200 && p.Price < 500);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_more_than_30()
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

            //var actual = JoeyWhereFilterCost(sources);
            //var actual = MyOwnLinq.JoeyWhere(products, 
            //    p => p.Price > 200 && p.Price < 500 & p.Cost > 30);
            var actual = products.JoeyWhere(p => p.Price > 200 && p.Price < 500 & p.Cost > 30);

            var expected = new List<Product>
            {
                //new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
        [Test]
        public void where_and_select()
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

            //var actual = JoeyWhereFilterCost(sources);
            //var actual = MyOwnLinq.JoeyWhere(products, 
            //    p => p.Price > 200 && p.Price < 500 & p.Cost > 30);
            var actual = products
                .JoeyWhere(p => p.Price > 200 && p.Price < 500 & p.Cost > 300)
                .JoeySelect(r => r.Price);

            foreach (var i in actual)
            {
                Console.WriteLine(i);
            }

            //var expected = new[]
            //{
            //    310,410
            //};

            //expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_short_names()
        {
            var names = new List<string> { "Joey", "Cash", "William", "Sam", "Brian", "Jessica" };
            var actual = names.JoeyWhere(n => n.Length < 5);
            var expected = new[] { "Joey", "Cash", "Sam" };
            expected.ToExpectedObject().ShouldMatch(actual);
        }


        [Test]
        public void find_odd_names()
        {
            var names = new List<string> { "Joey", "Cash", "William", "Sam", "Brian", "Jessica" };
            var actual = names.JoeyWhere((n, i) => i % 2 == 0);
            var expected = new[] { "Joey", "William", "Brian" };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_group_count_3_cum_cost()
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
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"},
            };
            var actual = GroupSum(products, p => p.Id, 5);
            var expected = new[] { 15, 21 };
            expected.ToExpectedObject().ShouldMatch(actual);
            
        }


        private IEnumerable<int> GroupSum<TSource>(IEnumerable<TSource> products, Func<TSource, int> selector, int groupSize)
        {
            var pageSize = groupSize;
            var pageIndex = 0;
            var count = products.Count();
            while (count > pageSize * pageIndex)
            {
                yield return products.Skip(pageIndex * pageSize).Take(pageSize).Sum(selector);
                pageIndex++;
            }
            
        }

        //private List<Product> JoeyWhereFilterCost(List<Product> sources)
        //{
        //    var result = new List<Product>();
        //    foreach (var product in sources)
        //    {
        //        if (product.Price > 200 & product.Price < 500 && product.Cost > 30)
        //            result.Add(product);
        //    }

        //    return result;
        //}

        //private List<string> JoeyWhere(List<string> names, Func<string, bool> predicate)
        //{
        //    var result = new List<string>();
        //    foreach (var n in names)
        //    {
        //        if (predicate(n))
        //            result.Add(n);
        //    }
        //    return result;
        //}
    }
}