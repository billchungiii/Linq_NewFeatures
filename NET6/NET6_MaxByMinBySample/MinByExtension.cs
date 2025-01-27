using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6_MaxByMinBySample
{
    public static class MinByExtension
    {
        /// <summary>
        /// Before .NET 6, OrderByDescending then First
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallOrderByThenFirst<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.OrderBy(keySelector).First();
        }

        /// <summary>
        /// Before .NET 6, Max then First
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallMinThenFirst<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var min = source.Min(keySelector);
            return source.First(p => keySelector(p).Equals(min));
        }

        /// <summary>
        /// After .NET6, MaxBy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallMinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.MinBy(keySelector);
        }
    }
}
