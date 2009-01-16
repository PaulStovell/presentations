using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Data;

namespace ScratchPad
{

    class Program
    {
        static void Main(string[] args)
        {
            var i = new List<string>();

            i.Add("SUhs");
            i.Add("SUhs");
            i.Add("SUhs");

            i.FilterItems(s => s.Contains("H"));

            Console.ReadKey();
        }
    }

    static class Foo
    {
        public delegate bool DoFilterCallback<T>(T item);

        public static IEnumerable<T> FilterItems<T>(this IEnumerable<T> items, Expression<DoFilterCallback<T>> callback)
        {
            foreach (var item in items)
            {
                var invokable = callback.Compile();
                if (invokable(item))
                {
                    yield return item;
                }
            }
        }
    }
}
