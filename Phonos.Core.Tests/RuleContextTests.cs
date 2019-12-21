using Intervals;
using Phonos.Core.Rules;
using Phonos.Core.Tests;
using Phonos.Core.Tests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Core.Queries.Tests
{
    public class RuleContextTests
    {
        [Fact]
        public void TestApply()
        {
            var l = new PhonemeQuery(new[] { "l" });

            var rule = new Rule(
                id: "test",
                group: "test",
                timeSpan: new Interval(0, 1),
                queries: new[] {
                    new ContextualQuery(new SequenceQuery(new[] { l, l })),
                },
                operation: new[] {
                    new Operation(
                        name: "Test",
                        phonological: ps => new[] { "l" },
                        graphical: new[] {
                            new GraphicalMap(ps => ps),
                        })
                });

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
                timeSpan: new Interval(0, 1),
                queries: new[] {
                    new ContextualQuery(v, lookBehind: c, lookAhead: c, scope: "syllable"),
                },
                operation: new[] {
                    new Operation(
                        name: "Test",
                        phonological: ps => new[] { "o" },
                        graphical: new[] {
                            new GraphicalMap(ps => ps),
                        })
                });

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
