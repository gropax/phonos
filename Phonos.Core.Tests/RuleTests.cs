using Intervals;
using Phonos.Core.Tests;
using Phonos.Core.Tests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class YamlParserTests
    {
        [Fact]
        public void TestMatches()
        {
            var v = new PhonemeQuery(new[] { "a", "e", "i", "o", "u" });
            var c = new PhonemeQuery(new[] { "r", "t", "b", "l" });

            var r = new Rule(
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { c, v, c }),
                maps: null,
                lookBehind: null,
                lookAhead: null);

            var word = new Word(Phonemes("r", "a", "t", "a", "b", "l", "e", "r", "a"), null, null); 

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
                group: "test",
                id: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: null);

            var word = new Word(Phonemes("r", "a", "t", "a", "b", "l", "e", "r", "a"), null, null); 

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
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: null,
                lookAhead: v);

            var word = new Word(Phonemes("r", "a", "t", "a", "b", "l", "e", "r", "a"), null, null); 

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
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: c,
                maps: null,
                lookBehind: v,
                lookAhead: v);

            var word = new Word(Phonemes( "r", "a", "t", "a", "b", "l", "e", "r", "a"), null, null); 

            var matches = r.Match(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(2, 1, "t"),
                new Interval<string>(7, 1, "r"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithScope()
        {
            var t = new PhonemeQuery(new[] { "t" });
            var a = new PhonemeQuery(new[] { "a" });

            var rule = new Rule(
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { t, a, t }),
                maps: new[] {
                    new PhonologicalMap(
                        ps => new[] { "l" },
                        graphicalMaps: new[] {
                            new GraphicalMap(ps => ps),
                        })
                },
                lookBehind: null,
                lookAhead: null,
                scope: "syllable");

            var word = new Word(
                Phonemes("t", "a", "t", "a", "t", "t", "a", "a", "t", "a", "t"), null,
                Fields(Field("syllable", Alignment.Parse("short:2 long:3 short:2 short:1 long:3")))); 

            var matches = rule.MatchScope(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(2, 3, "tat"),
                new Interval<string>(8, 3, "tat"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithScopeAndLookBehind()
        {
            var t = new PhonemeQuery(new[] { "t" });
            var a = new PhonemeQuery(new[] { "a" });

            var rule = new Rule(
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { a, t }),
                maps: new[] {
                    new PhonologicalMap(
                        ps => new[] { "l" },
                        graphicalMaps: new[] {
                            new GraphicalMap(ps => ps),
                        })
                },
                lookBehind: t,
                lookAhead: null,
                scope: "syllable");

            var word = new Word(
                Phonemes("t", "a", "t", "a", "t", "t", "a", "a", "t", "a", "t"), null,
                Fields(Field("syllable", Alignment.Parse("short:2 long:3 short:2 short:1 long:3")))); 

            var matches = rule.MatchScope(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(3, 2, "at"),
                new Interval<string>(9, 2, "at"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestMatchesWithScopeAndLookAhead()
        {
            var t = new PhonemeQuery(new[] { "t" });
            var a = new PhonemeQuery(new[] { "a" });

            var rule = new Rule(
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: new SequenceQuery(new[] { t, a }),
                maps: new[] {
                    new PhonologicalMap(
                        ps => new[] { "l" },
                        graphicalMaps: new[] {
                            new GraphicalMap(ps => ps),
                        })
                },
                lookBehind: null,
                lookAhead: t,
                scope: "syllable");

            var word = new Word(
                Phonemes("t", "a", "t", "a", "t", "t", "a", "a", "t", "a", "t"), null,
                Fields(Field("syllable", Alignment.Parse("short:2 long:3 short:2 short:1 long:3")))); 

            var matches = rule.MatchScope(word).Map(px => string.Join(string.Empty, px)).ToArray();
            var expected = new[]
            {
                new Interval<string>(2, 2, "ta"),
                new Interval<string>(8, 2, "ta"),
            };

            Assert.Equal(expected, matches);
        }

        [Fact]
        public void TestApply()
        {
            var l = new PhonemeQuery(new[] { "l" });

            var rule = new Rule(
                id: "test",
                group: "test",
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
                Phonemes("b", "e", "l", "l", "a"),
                GraphicalForms(Alignment.Parse("B E L L A")),
                Fields(Field("type", Alignment.Parse("C V C C V")))); 

            var newWords = rule.Apply(word);
            var expected = new[]
            {
                new Word(
                    Phonemes("b", "e", "l", "a"),
                    GraphicalForms(Alignment.Parse("B E LL:2 A")),
                    Fields(Field("type", Alignment.Parse("C V C V"))))
            };

            WordAssert.Equal(expected, newWords);
        }

        [Fact]
        public void TestApplyWithScope()
        {
            var c = new PhonemeQuery(new[] { "b", "l" });
            var v = new PhonemeQuery(new[] { "a", "e" });

            var rule = new Rule(
                id: "test",
                group: "test",
                name: "Test",
                timeSpan: new Interval(0, 1),
                query: v,
                maps: new[] {
                    new PhonologicalMap(
                        ps => new[] { "o" },
                        graphicalMaps: new[] {
                            new GraphicalMap(ps => ps),
                        })
                },
                lookBehind: c,
                lookAhead: c,
                scope: "syllable");

            var word = new Word(
                Phonemes("b", "e", "l", "l", "a", "b", "e", "l"), 
                GraphicalForms(Alignment.Parse("B E L L A B E L")),
                Fields(Field("syllable", Alignment.Parse("long:3 short:2 long:3")))); 

            var newWords = rule.Apply(word);
            var expected = new[]
            {
                new Word(
                    Phonemes("b", "o", "l", "l", "a", "b", "o", "l"),
                    GraphicalForms(Alignment.Parse("B E L L A B E L")),
                    Fields(Field("syllable", Alignment.Parse("long:3 short:2 long:3")))), 
            };

            WordAssert.Equal(expected, newWords);
        }




        private string[] Phonemes(params string[] phonemes)
        {
            return phonemes;
        }

        private Alignment<string>[] GraphicalForms(params Alignment<string>[] graphicalForms)
        {
            return graphicalForms;
        }

        private Dictionary<string, Alignment<string>> Fields(params (string, Alignment<string>)[] fields)
        {
            var dict = new Dictionary<string, Alignment<string>>();

            foreach (var (name, alignment) in fields)
                dict[name] = alignment;

            return dict;
        }

        private (string, Alignment<string>) Field(string name, Alignment<string> alignment)
        {
            return (name, alignment);
        }
    }
}
