using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using Phonos.Latin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class BlackBoxTests : RuleSystemTests
    {
        public static BlackBoxTestData TestData
        {
            get
            {
                var parser = new YamlParser();
                var path = @".\Specs\BlackBox.yaml";
                using (StreamReader reader = File.OpenText(path))
                    return new BlackBoxTestData(parser.ParseBlackBoxTests(reader).ToList());
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(BlackBoxTest integrationTest)
        {
            var latinParser = new Latin.WordParser();
            var word = latinParser.Parse(integrationTest.Latin);

            var analyzers = new IAnalyzer[]
            {
                new MemoizeAnalyzer("classical_latin"),
                new Latin.SyllableAnalyzer(),
                new Latin.AccentAnalyzer(),
            };

            foreach (var analyzer in analyzers)
                analyzer.Analyze(word);

            var rules = French2.Rules();
            var sequencer = new LinearRuleSequencer(rules, new Dictionary<string, IAnalyzer>()
            {
                { "syllable", new Latin.SyllableAnalyzer() },
            });

            var derived = sequencer.Derive(ExecutionContext, word).Select(d => d.Derived).ToArray();

            var expected = integrationTest.Outputs;
            Assert.Equal(expected.Length, derived.Length);

            for (int i = 0; i < expected.Length; i++)
                TestBlackBoxSample(expected, derived, i);
        }

        public ExecutionContext ExecutionContext
        {
            get
            {
                return new ExecutionContext(new Dictionary<string, IAnalyzer>()
                {
                    { "syllable", new SyllableAnalyzer() },
                });
            }
        }
    }
}
