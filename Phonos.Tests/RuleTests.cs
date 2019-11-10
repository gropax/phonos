using Intervals;
using Phonos.Tests.Queries;
using System;
using System.Linq;
using Xunit;

namespace Phonos.Queries.Tests
{
    public class RuleTests
    {
        [Fact]
        public void TestMatches()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { c, v, c }),
                maps: null,
                lookBehind: null,
                lookAhead: null);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).ToIntervals().ToArray();
            var expected = new[]
            {
                new Interval<string[]>(0, 3, new[] { "r", "a", "t" }),
                new Interval<string[]>(5, 3, new[] { "l", "e", "r" }),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookBehind()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: null);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).ToIntervals().ToArray();
            var expected = new[]
            {
                new Interval<string[]>(2, 1, new[] { "t" }),
                new Interval<string[]>(4, 1, new[] { "b" }),
                new Interval<string[]>(7, 1, new[] { "r" }),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookAhead()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: null,
                lookAhead: v);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).ToIntervals().ToArray();
            var expected = new[]
            {
                new Interval<string[]>(0, 1, new[] { "r" }),
                new Interval<string[]>(2, 1, new[] { "t" }),
                new Interval<string[]>(5, 1, new[] { "l" }),
                new Interval<string[]>(7, 1, new[] { "r" }),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookBehindAndLookAhead()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: v);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).ToIntervals().ToArray();
            var expected = new[]
            {
                new Interval<string[]>(2, 1, new[] { "t" }),
                new Interval<string[]>(7, 1, new[] { "r" }),
            };

            Assert.Equal(expected, matches);
        }

    }
}
