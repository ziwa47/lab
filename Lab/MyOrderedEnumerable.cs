﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderedEnumerable : IOrderedEnumerable<Employee>
    {
        private IEnumerable<Employee> _employees;
        private IComparer<Employee> _comparer;

        public MyOrderedEnumerable(IEnumerable<Employee> employees, IComparer<Employee> comparer)
        {
            _comparer = comparer;
            _employees = employees;
        }

        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector, IComparer<TKey> comparer, bool @descending)
        {
            var newComparer = new CombineComparer<TKey>(keySelector, comparer);
            return new MyOrderedEnumerable(_employees, new ComboComparer(_comparer, newComparer));
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            //bubble sort
            var elements = _employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];
                    var compare = _comparer.Compare(element, minElement);
                    if (compare < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}