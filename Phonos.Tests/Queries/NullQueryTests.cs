using Phonos.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Queries.Tests
{
    public class NullQueryTests
    {
        [Fact]
        public void Test()
        {
            var q = new NullQuery();

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            QueryAssert.IsMatch(q, word, 0, new string[0]);
            QueryAssert.IsMatch(q, word, 1, new string[0]);
            QueryAssert.IsMatch(q, word, 2, new string[0]);
            QueryAssert.IsMatch(q, word, 3, new string[0]);
            QueryAssert.IsMatch(q, word, 4, new string[0]);
            QueryAssert.IsMatch(q, word, 5, new string[0]);
            QueryAssert.IsMatch(q, word, 6, new string[0]);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 7));
        }
    }
}
