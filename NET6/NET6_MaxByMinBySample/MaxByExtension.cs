using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6_MaxByMinBySample
{
    public static class MaxByExtension
    {
        /// <summary>
        /// Before .NET 6, OrderByDescending then First
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallOrderByDescThenFirst<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.OrderByDescending(keySelector).First();
        }

        /// <summary>
        /// Before .NET 6, Max then First
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallMaxThenFirst<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var max = source.Max(keySelector);
            return source.First(p => keySelector(p).Equals(max));
        }

        /// <summary>
        /// After .NET6, MaxBy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static T CallMaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.MaxBy(keySelector);
        }
    }
}
