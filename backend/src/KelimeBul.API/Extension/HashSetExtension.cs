using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelimeBul.API
{
    public static class HashSetExtension
    {
        private static readonly Random rnd = new Random();
        private static readonly object sync = new object();

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable is null");

            var count = enumerable.Count();

            var ndx = 0;
            lock (sync)
                ndx = rnd.Next(count);

            return enumerable.ElementAt(ndx);
        }
    }
}
