using NUnit.Framework;

using System.Collections.Generic;
using Lab;
using Lab.Entities;
using NUnit.Framework.Interfaces;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal_0()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1, 0 };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3, };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_2()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal_3()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 0 };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal_2()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = first.JoeySequenceEqual(second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen",},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };
            var second = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };
            var actual = first.JoeySequenceEqual(second,new MyEmployeeEqualComparer());
            Assert.IsTrue(actual);
        }


        //[Test]
        //public void dict_add()
        //{
        //    var dic = new Dictionary<Employee,int>(new MyEmployeeEqualComparer());
        //    var a = new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"};
        //    var b = new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "234"};
        //    var c = new Employee() {FirstName = "123", LastName = "Chen", Phone = "123"};
        //    dic.Add(a,1);
        //    dic.Add(b,2);
        //    dic.Add(c,3);
        //}
    }
}