using Intervals;
using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phonos.Core.Tests.Queries
{
    public static class QueryAssert
    {
        public static void IsMatch(IQuery q, Word word, int index, string[] value = null, Interval scope = null)
        {
            var match = q.Match(word, index, scope);
            Assert.NotNull(match);
            Assert.Equal(index, match.Start);
            Assert.Equal(match.Value.Length, match.Length);

            if (value != null)
                Assert.Equal(value, match.Value);
        }

        public static void NoMatch(IQuery q, Word word, int index, Interval scope = null)
        {
            var match = q.Match(word, index, scope);
            Assert.Null(match);
        }
    }
}
