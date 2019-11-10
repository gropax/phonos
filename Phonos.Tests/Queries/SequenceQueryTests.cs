using Phonos.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Queries.Tests
{
    public class SequenceQueryTests
    {
        [Fact]
        public void Test()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });
            var q = new SequenceQuery(new[] { c, v, c });

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            QueryAssert.IsMatch(q, word, 0, new[] { "r", "a", "t" });
            QueryAssert.NoMatch(q, word, 1);
            QueryAssert.IsMatch(q, word, 2, new[] { "t", "a", "b" });
            QueryAssert.NoMatch(q, word, 3);
            QueryAssert.NoMatch(q, word, 4);
            QueryAssert.NoMatch(q, word, 5);
            QueryAssert.NoMatch(q, word, 6);
            QueryAssert.NoMatch(q, word, 7);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 8));
        }
    }
}
