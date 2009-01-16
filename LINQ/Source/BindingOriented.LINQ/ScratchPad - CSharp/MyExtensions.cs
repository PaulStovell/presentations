using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ScratchPad
{
    static class MyExtensions
    {
        public static T FindPaul<T>(this IEnumerable<T> items,
            Expression<Func<T, bool>> filter)
        {
            T result = default(T);

            foreach (T item in items)
            {
                if (filter.Compile().Invoke(item))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
    }
}
