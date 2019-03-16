using System;
using System.Collections.Generic;

namespace Lab.Entities
{
    public class MyEmployeeEqualComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            return Tuple.Create(obj.FirstName, obj.LastName).GetHashCode();
        }
    }
    public class MyEmployeeWithPhoneEqualComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName && x.Phone == y.Phone;
        }

        public int GetHashCode(Employee obj)
        {
            return Tuple.Create(obj.FirstName, obj.LastName,obj.Phone).GetHashCode();
        }
    }

    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }
        public string Phone { get; set; }
    }
}