using Intervals;
using Phonos.Core.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class PhonemeQueryTests
    {
        [Fact]
        public void Test()
        {
            var q = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            QueryAssert.IsMatch(q, word, 0, new[] { "a" });
            QueryAssert.NoMatch(q, word, 1);
            QueryAssert.IsMatch(q, word, 2, new[] { "a" });
            QueryAssert.NoMatch(q, word, 3);
            QueryAssert.NoMatch(q, word, 4);
            QueryAssert.IsMatch(q, word, 5, new[] { "e" });
            QueryAssert.NoMatch(q, word, 6);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 7));
        }

        [Fact]
        public void TestScope()
        {
            var q = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var scope = new Interval(2, 4);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 1, scope));
            QueryAssert.IsMatch(q, word, 2, new[] { "a" }, scope);
            QueryAssert.NoMatch(q, word, 3, scope);
            QueryAssert.NoMatch(q, word, 4, scope);
            QueryAssert.IsMatch(q, word, 5, new[] { "e" }, scope);
            QueryAssert.NoMatch(q, word, 6, scope);
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 7, scope));
        }
    }
}
