using Phonos.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phonos.Tests.Queries
{
    public static class QueryAssert
    {
        public static void IsMatch(IQuery q, Word word, int index, string[] value = null)
        {
            var match = q.Match(word, index);
            Assert.NotNull(match);
            Assert.Equal(index, match.Start);
            Assert.Equal(match.Value.Length, match.Length);

            if (value != null)
                Assert.Equal(value, match.Value);
        }

        public static void NoMatch(IQuery q, Word word, int index)
        {
            var match = q.Match(word, index);
            Assert.Null(match);
        }
    }
}
