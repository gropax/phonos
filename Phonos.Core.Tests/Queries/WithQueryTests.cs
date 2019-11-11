using Intervals;
using Phonos.Core.Tests.Queries;
using System;
using System.Collections.Generic;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class WithQueryTests
    {
        [Fact]
        public void Test()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var q = new WithQuery(v, "field", new[] { "val1", "val2" });

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: new Dictionary<string, Alignment<string>>()
                {
                    { "field", new Alignment<string>(new []
                    {
                        new Interval<string>(1, 1, "val1"),
                        new Interval<string>(2, 1, "val1"),
                        new Interval<string>(3, 1, "val3"),
                        new Interval<string>(4, 1, "val3"),
                        new Interval<string>(6, 1, "val2"),
                        new Interval<string>(8, 1, "val1"),
                    }) },
                });

            QueryAssert.NoMatch(q, word, 0);
            QueryAssert.IsMatch(q, word, 1, new[] { "a" });
            QueryAssert.NoMatch(q, word, 2);
            QueryAssert.NoMatch(q, word, 3);
            QueryAssert.NoMatch(q, word, 4);
            QueryAssert.NoMatch(q, word, 5);
            QueryAssert.IsMatch(q, word, 6, new[] { "e" });
            QueryAssert.NoMatch(q, word, 7);
            QueryAssert.IsMatch(q, word, 8, new[] { "a" });
            QueryAssert.NoMatch(q, word, 9);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 10));
        }
    }
}
