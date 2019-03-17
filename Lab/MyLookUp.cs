using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyGrouping : IGrouping<string, Employee>
    {
        private readonly IEnumerable<Employee> _collection;

        public MyGrouping(string key, IEnumerable<Employee> collection)
        {
            Key = key;
            _collection = collection;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }
    }

    public class MyLookUp : IEnumerable<IGrouping<string, Employee>>
    {
        private Dictionary<string, List<Employee>> _lookup = new Dictionary<string, List<Employee>>();

        public IEnumerator<IGrouping<string, Employee>> GetEnumerator()
        {
            return _lookup.Select(r => new MyGrouping(r.Key, r.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddElement(string groupKeySelector, Employee employee)
        {
            if (_lookup.ContainsKey(groupKeySelector))
            {
                _lookup[groupKeySelector].Add(employee);
            }
            else
            {
                _lookup.Add(groupKeySelector, new List<Employee>() { employee });
            }
        }
    }
}