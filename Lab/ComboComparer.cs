using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstCombineComparer, IComparer<Employee> secondCombineComparer)
        {
            FirstCombineComparer = firstCombineComparer;
            SecondCombineComparer = secondCombineComparer;
        }

        private IComparer<Employee> FirstCombineComparer { get; set; }
        private IComparer<Employee> SecondCombineComparer { get; set; }
        public int Compare(Employee x, Employee y)
        {
            var firstCompare = FirstCombineComparer.Compare(x, y);
            if (firstCompare == 0)
                return SecondCombineComparer.Compare(x, y);
            return firstCompare;
        }
    }
}