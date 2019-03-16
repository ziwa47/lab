using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineComparer<TKey> : IComparer<Employee>
    {
        public CombineComparer(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private IComparer<TKey> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }
}