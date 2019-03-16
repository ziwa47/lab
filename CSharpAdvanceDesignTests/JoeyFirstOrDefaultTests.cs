﻿using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = employees.JoeyFirstOrDefault();

            Assert.IsNull(actual);
        }
        [Test]
        public void numbers_is_empty()
        {
            var employees = new List<int>();

            var actual = employees.JoeyFirstOrDefault();

            var expected =  0 ;

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}