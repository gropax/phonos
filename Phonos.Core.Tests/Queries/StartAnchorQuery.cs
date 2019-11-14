using Intervals;
using Phonos.Core.Tests.Queries;
using System;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class StartAnchorQueryTests
    {
        [Fact]
        public void Test()
        {
            var q = new StartAnchorQuery();

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            QueryAssert.IsMatch(q, word, 0, new string[0]);
            QueryAssert.NoMatch(q, word, 1);
            QueryAssert.NoMatch(q, word, 2);
            QueryAssert.NoMatch(q, word, 3);
            QueryAssert.NoMatch(q, word, 4);
            QueryAssert.NoMatch(q, word, 5);
            QueryAssert.NoMatch(q, word, 6);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 7));
        }

        [Fact]
        public void TestScope()
        {
            var q = new StartAnchorQuery();

            var word = new Word(
                phonemes: new[] { "a", "t", "a", "b", "l", "e" },
                graphicalForms: null,
                fields: null);

            var scope = new Interval(1, 3);

            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 0, scope));
            QueryAssert.IsMatch(q, word, 1, new string[0], scope);
            QueryAssert.NoMatch(q, word, 2, scope);
            QueryAssert.NoMatch(q, word, 3, scope);
            QueryAssert.NoMatch(q, word, 4, scope);
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Match(word, 5, scope));
        }
    }
}
