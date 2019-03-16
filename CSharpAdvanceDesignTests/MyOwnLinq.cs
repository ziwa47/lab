using System;
using System.Collections.Generic;
using System.Linq;
using Lab;
using Lab.Entities;

static internal class MyOwnLinq
{
    public static IOrderedEnumerable<Employee> JoeyThenBy<TKey>(this IOrderedEnumerable<Employee> source, Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
    {
        return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
    }

    public static IOrderedEnumerable<Employee> JoeyOrderByKeep<TKey>(this IEnumerable<Employee> employees, Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
    {
        return new MyOrderedEnumerable(employees, new CombineComparer<TKey>(keySelector, comparer));
    }

    public static IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees,
        ComboComparer comboComparer)
    {
        //bubble sort
        var elements = employees.ToList();
        while (elements.Any())
        {
            var minElement = elements[0];
            var index = 0;
            for (int i = 1; i < elements.Count; i++)
            {
                var element = elements[i];
                var lastNameCompare = comboComparer.Compare(element, minElement);
                if (lastNameCompare < 0)
                {
                    minElement = element;
                    index = i;
                }
                else if (lastNameCompare == 0)
                {
                    var firstNameCompare = comboComparer.Compare(element, minElement);
                    if (firstNameCompare < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }
            }

            elements.RemoveAt(index);
            yield return minElement;
        }
    }
}