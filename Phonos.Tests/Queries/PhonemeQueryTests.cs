using Phonos.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Queries.Tests
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
    }
}
