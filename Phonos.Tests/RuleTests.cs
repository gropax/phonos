using Intervals;
using Phonos.Tests;
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

        [Fact]
        public void TestApply()
        {
            var l = new PhonemeQuery(new[] { "l" });

            var rule = new Rule(
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { l, l }),
                maps: new[] {
                    new PhonologicalMap(
                        ps => new[] { "l" },
                        graphicalMaps: new[] {
                            new GraphicalMap(ps => ps),
                        })
                },
                lookBehind: null,
                lookAhead: null);

            var word = new Word(
                phonemes: new[] { "b", "e", "l", "l", "a" },
                graphicalForms: new[] {
                    new Alignment<string[]>(new[] {
                        new Interval<string[]>(0, 1, new [] { "B" }),
                        new Interval<string[]>(1, 1, new [] { "E" }),
                        new Interval<string[]>(2, 1, new [] { "L" }),
                        new Interval<string[]>(3, 1, new [] { "L" }),
                        new Interval<string[]>(4, 1, new [] { "A" }),
                    })
                },
                fields: null);

            var newWords = rule.Apply(word);
            var expected = new[]
            {
                new Word(
                    phonemes: new[] { "b", "e", "l", "a" },
                    graphicalForms: new[] {
                        new Alignment<string[]>(new[] {
                            new Interval<string[]>(0, 1, new [] { "B" }),
                            new Interval<string[]>(1, 1, new [] { "E" }),
                            new Interval<string[]>(2, 1, new [] { "L", "L" }),
                            new Interval<string[]>(3, 1, new [] { "A" }),
                        })
                    },
                    fields: null)
            };

            WordAssert.Equal(expected, newWords);
        }
    }
}
