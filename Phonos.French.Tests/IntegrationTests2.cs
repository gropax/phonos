﻿using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class Integration2Tests : RuleSystemTests
    {
        public static IntegrationTestData TestData
        {
            get
            {
                var parser = new YamlParser();
                var path = @".\Specs\Integration.yaml";
                using (StreamReader reader = File.OpenText(path))
                    return new IntegrationTestData(parser.ParseIntegrationTests(reader).ToList());
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(IntegrationTest integrationTest)
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
            var sequencer = new LinearRuleSequencer(rules);

            var derived = sequencer.Derive(word).Select(d => d.Derived).ToArray();

            var expected = integrationTest.Outputs;
            Assert.Equal(expected.Length, derived.Length);

            for (int i = 0; i < expected.Length; i++)
                TestSampleOutput(expected, derived, i);
        }
    }
}
