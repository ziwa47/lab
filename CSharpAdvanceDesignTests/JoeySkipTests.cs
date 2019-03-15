using ExpectedObjects;

using Lab;
using Lab.Entities;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipTests
    {
        [Test]
        public void skip_2_employees()
        {
            var employees = (IEnumerable<Employee>)new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            var actual = employees.JoeySkip(2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        [Test]
        public void num_skip_3()
        {
            var employees = new[]
            {
                10,20,30,40
            };

            var actual = employees.JoeySkip(3);

            var expected = new[]
            {
               40
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }
    }
}