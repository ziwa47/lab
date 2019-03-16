using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    [Ignore("not yet")]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = employees.JoeyLastOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void get_last_employee()
        {
            var employees = new List<Employee>()
            {
                new Employee(){FirstName = "Ziwa",LastName="i",Role = Role.Engineer},
                new Employee(){FirstName = "Cash",LastName="i",Role = Role.Engineer},
                new Employee(){FirstName = "Eason",LastName="i",Role = Role.Engineer},
            };
            var actual = employees.JoeyLastOrDefault();

            var expected = new Employee() { FirstName = "Eason", LastName = "i", Role = Role.Engineer };
            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}