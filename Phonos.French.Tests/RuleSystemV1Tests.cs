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
        public Latin.WordParser WordParser { get; }
        public Latin.SyllableAnalyzer SyllableAnalyzer { get; }

        public RuleSystemV1Tests()
        {
            RuleSystem = new RuleSystemV1();
            WordParser = new Latin.WordParser();
            SyllableAnalyzer = new Latin.SyllableAnalyzer();
        }

        [Fact]
        public void TestRule1()
        {
            var latin = "ambulat";
            var word = WordParser.Parse(latin);
            SyllableAnalyzer.Analyze(word);

            var rule = RuleSystem.Rule1();
            var derived = rule.Apply(word);

            Assert.Single(derived);
            Assert.Equal(new[] { "a", "m", "b", "l", "a", "t" }, derived[0].Phonemes);
        }
    }
}
