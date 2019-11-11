using Intervals;
using Phonos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class RuleSystemV1Tests
    {
        public RuleSystemV1 RuleSystem { get; }
        public RuleSystemV1Tests()
        {
            RuleSystem = new RuleSystemV1();
        }

        [Fact]
        public void TestRule1()
        {
            var original = new Word(
                phonemes: new string[] { "a", "m", "b", "u", "l", "a", "t" },
                graphicalForms: new[] {
                    new Core.Alignment<string[]>(new []
                    {
                        new Interval<string[]>(0, 1, new [] { "a" }),
                        new Interval<string[]>(1, 1, new [] { "m" }),
                        new Interval<string[]>(2, 1, new [] { "b" }),
                        new Interval<string[]>(3, 1, new [] { "u" }),
                        new Interval<string[]>(4, 1, new [] { "l" }),
                        new Interval<string[]>(5, 1, new [] { "a" }),
                        new Interval<string[]>(6, 1, new [] { "t" }),
                    })
                },
                fields: new Dictionary<string, Core.Alignment<string>>()
                {
                    { "accent", new Core.Alignment<string>(new []
                    {
                        new Interval<string>(0, 1, "tonic"),
                        new Interval<string>(3, 1, "post-tonic"),
                        new Interval<string>(5, 1, "final"),
                    }) },
                });

            var rule = RuleSystem.Rule1();
            var derived = rule.Apply(original);
            Assert.Single(derived);
            Assert.Equal(new[] { "a", "m", "b", "l", "a", "t" }, derived[0].Phonemes);
        }
    }
}
