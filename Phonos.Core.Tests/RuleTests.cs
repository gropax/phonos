using Intervals;
using Phonos.Core.Tests;
using Phonos.Core.Tests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class RuleTests
    {
        [Fact]
        public void TestMatches()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { c, v, c }),
                maps: null,
                lookBehind: null,
                lookAhead: null);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(0, 3, "rat"),
                new Interval<string>(5, 3, "ler"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookBehind()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: null);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(2, 1, "t"),
                new Interval<string>(4, 1, "b"),
                new Interval<string>(7, 1, "r"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookAhead()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: null,
                lookAhead: v);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(0, 1, "r"),
                new Interval<string>(2, 1, "t"),
                new Interval<string>(5, 1, "l"),
                new Interval<string>(7, 1, "r"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithLookBehindAndLookAhead()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: v);

            var word = new Word(
                phonemes: new[] { "r", "a", "t", "a", "b", "l", "e", "r", "a" },
                graphicalForms: null,
                fields: null);

            var matches = r.Match(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(2, 1, "t"),
                new Interval<string>(7, 1, "r"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestApply()
        {
            var l = new PhonemeQuery(new[] { "l" });

            var rule = new Rule(
                name: "Test",
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
                    new Alignment<string>(new[] {
                        new Interval<string>(0, 1, "B"),
                        new Interval<string>(1, 1, "E"),
                        new Interval<string>(2, 1, "L"),
                        new Interval<string>(3, 1, "L"),
                        new Interval<string>(4, 1, "A"),
                    })
                },
                fields: new Dictionary<string, Alignment<string>>()
                {
                    { "type", new Alignment<string>(new[] {
                        new Interval<string>(0, 1, "C"),
                        new Interval<string>(1, 1, "V"),
                        new Interval<string>(2, 1, "C"),
                        new Interval<string>(3, 1, "C"),
                        new Interval<string>(4, 1, "V"),
                    }) },
                }); 

            var newWords = rule.Apply(word);
            var expected = new[]
            {
                new Word(
                    phonemes: new[] { "b", "e", "l", "a" },
                    graphicalForms: new[] {
                        new Alignment<string>(new[] {
                            new Interval<string>(0, 1, "B"),
                            new Interval<string>(1, 1, "E"),
                            new Interval<string>(2, 1, "LL"),
                            new Interval<string>(3, 1, "A"),
                        })
                    },
                    fields: new Dictionary<string, Alignment<string>>()
                    {
                        { "type", new Alignment<string>(new[] {
                            new Interval<string>(0, 1, "C"),
                            new Interval<string>(1, 1, "V"),
                            new Interval<string>(2, 1, "C"),
                            new Interval<string>(3, 1, "V"),
                        }) },
                    })
            };

            WordAssert.Equal(expected, newWords);
        }
    }
}
