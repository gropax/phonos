using Intervals;
using Phonos.Core.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class MaybeQueryTests
    {
        [Fact]
        public void Test()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });
            var q = new SequenceQuery(new IQuery[] { c, new MaybeQuery(c), v });

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            QueryAssert.NoMatch(q, word, 0);
            QueryAssert.IsMatch(q, word, 1, new[] { "t", "a" });
            QueryAssert.NoMatch(q, word, 2);
            QueryAssert.IsMatch(q, word, 3, new[] { "b", "l", "e" });
            QueryAssert.IsMatch(q, word, 4, new[] { "l", "e" });
            QueryAssert.NoMatch(q, word, 5);
            QueryAssert.NoMatch(q, word, 6);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 7));
        }

        [Fact]
        public void TestScope()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "s", "c", "b", "l" });
            var q = new SequenceQuery(new IQuery[] { c, new MaybeQuery(c), v });

            var word = new Word(
                phonemes: new[] { "s", "c", "r", "a", "b", "l", "e", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            var scope = new Interval(2, 6);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 1, scope));
            QueryAssert.IsMatch(q, word, 2, new[] { "r", "a" }, scope);
            QueryAssert.NoMatch(q, word, 3, scope);
            QueryAssert.IsMatch(q, word, 4, new[] { "b", "l", "e" }, scope);
            QueryAssert.IsMatch(q, word, 5, new[] { "l", "e" }, scope);
            QueryAssert.NoMatch(q, word, 6, scope);
            QueryAssert.NoMatch(q, word, 7, scope);
            QueryAssert.NoMatch(q, word, 8, scope);
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 9, scope));
        }
    }
}
