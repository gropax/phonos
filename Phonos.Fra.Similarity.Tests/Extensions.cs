using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Similarity.Tests
{
    public static class Extensions
    {
        public static IEnumerable<Tuple<T, T>> UnorderedPairs<T>(this IEnumerable<T> ts)
        {
            var ary = ts.ToArray();
            for (int i = 0; i < ary.Length; i++)
                for (int j = 0; j < i; j++)
                    yield return Tuple.Create(ary[i], ary[j]);
        }
    }
}
