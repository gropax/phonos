using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class RuleSystemTests
    {
        public Latin.WordParser WordParser => new Latin.WordParser();
        public IAnalyzer[] Analyzers { get; protected set; }

        protected void TestRules(RuleContext[] rules, string[] data, Func<RuleContext[], IRuleSequencer> sequencerBuilder = null)
        {
            var testData = ParseData(data);

            var word = WordParser.Parse(testData.Latin);

            foreach (var analyzer in Analyzers)
                analyzer.Analyze(word);

            sequencerBuilder = sequencerBuilder ?? (rx => new LinearRuleSequencer(rules));

            var sequencer = sequencerBuilder(rules);
            var derived = sequencer.Apply(word).FinalWords().ToArray();

            Assert.Equal(testData.PhonologicalForms.Length, derived.Length);

            for (int i = 0; i < testData.PhonologicalForms.Length; i++)
            {
                var expected = testData.PhonologicalForms[i];
                var real = derived[i];

                Assert.Equal(expected.Phonemes, string.Join(string.Empty, real.Phonemes));
                Assert.Equal(expected.GraphicalForms.Length, real.GraphicalForms.Length);

                for (int j = 0; j < expected.GraphicalForms.Length; j++)
                {
                    var expectedG = expected.GraphicalForms[j];
                    var realG = real.GraphicalForms[j];
                    var realStr = string.Join(string.Empty, realG.Intervals.SelectMany(k => k.Value));

                    Assert.Equal(expectedG, realStr);
                }
            }
        }

        protected void TestRules(RuleContext[] rules, RuleContextTest ruleTest)
        {
            var dict = rules.ToDictionary(r => r.Id, r => r);
            Assert.True(dict.TryGetValue(ruleTest.Id, out var rule),
                $"Rule not found [{ruleTest.Id}].");
            TestRule(rule, ruleTest);
        }

        protected void TestRule(RuleContext rule, RuleContextTest ruleTest)
        {
            foreach (var sample in ruleTest.Samples)
                TestSample(rule, ruleTest, sample);
        }


        protected void TestSample(RuleContext rule, RuleContextTest ruleTest, RuleTestSample sample)
        {
            var word = sample.Input;
            var derived = rule.Apply(word);

            Assert.Equal(sample.Outputs.Length, derived.Length);

            for (int i = 0; i < sample.Outputs.Length; i++)
                TestSampleOutput(sample.Outputs, derived, i);
        }

        private void TestSampleOutput(RuleTestSampleOutput[] outputs,
            Word[] derived, int index)
        {
            var output = outputs[index];
            var expected = output.Word;
            var real = derived[index];

            Assert.Equal(expected.Phonemes, real.Phonemes);

            Assert.Equal(expected.GraphicalForms.Length, real.GraphicalForms.Length);
            for (int j = 0; j < expected.GraphicalForms.Length; j++)
                TestGraphicalForm(expected.GraphicalForms, real.GraphicalForms, j);

            Assert.Equal(expected.Metas, real.Metas);

            foreach (var key in expected.Fields.Keys)
            {
                var expectedField = expected.Fields[key];
                Assert.True(real.Fields.TryGetValue(key, out var realField),
                    $"Missing field [{key}].");
                Assert.Equal(DumpField(expectedField), DumpField(realField));
            }
        }

        private string[] DumpField(Core.Alignment<string> alignment)
        {
            return alignment.Intervals.Select(i => $"{i.Value}:{i.Start}:{i.End}").ToArray();
        }

        private void TestGraphicalForm(Core.Alignment<string>[] expectedForms,
            Core.Alignment<string>[] derived, int index)
        {
            var expected = expectedForms[index];
            var real = derived[index];

            Assert.Equal(
                expected.Intervals.Select(i => i.Value).ToArray(),
                real.Intervals.Select(i => i.Value).ToArray());
        }

        protected void TestRule(RuleContext rule, string[] data)
        {
            var testData = ParseData(data);

            var word = WordParser.Parse(testData.Latin);

            foreach (var analyzer in Analyzers)
                analyzer.Analyze(word);

            var derived = rule.Apply(word);

            Assert.Equal(testData.PhonologicalForms.Length, derived.Length);

            for (int i = 0; i < testData.PhonologicalForms.Length; i++)
            {
                var expected = testData.PhonologicalForms[i];
                var real = derived[i];

                Assert.Equal(expected.Phonemes, string.Join(string.Empty, real.Phonemes));
                Assert.Equal(expected.GraphicalForms.Length, real.GraphicalForms.Length);

                for (int j = 0; j < expected.GraphicalForms.Length; j++)
                {
                    var expectedG = expected.GraphicalForms[j];
                    var realG = real.GraphicalForms[j];
                    var realStr = string.Join(string.Empty, realG.Intervals.SelectMany(k => k.Value));

                    Assert.Equal(expectedG, realStr);
                }
            }
        }

        private WordData ParseData(string[] data)
        {
            string latin = data[0];
            var phono = data.Skip(1).ToArray();

            var phonoData = new List<PhonologicalFormData>();
            string phonemes = null;
            var graphicalForms = new List<string>();

            for (int i = 0; i < phono.Length; i++)
            {
                string segment = phono[i];
                if (phonemes == null)
                    phonemes = segment;
                else if (segment == "||")
                {
                    phonoData.Add(new PhonologicalFormData(phonemes, graphicalForms.ToArray()));
                    phonemes = null;
                    graphicalForms.Clear();
                }
                else
                    graphicalForms.Add(segment);
            }

            if (phonemes != null)
                phonoData.Add(new PhonologicalFormData(phonemes, graphicalForms.ToArray()));

            return new WordData(latin, phonoData.ToArray());
        }
    }

    public class WordData
    {
        public string Latin { get; }
        public PhonologicalFormData[] PhonologicalForms { get; }

        public WordData(string latin, PhonologicalFormData[] phonologicalForms)
        {
            Latin = latin;
            PhonologicalForms = phonologicalForms;
        }
    }

    public class PhonologicalFormData
    {
        public string Phonemes { get; }
        public string[] GraphicalForms { get; }

        public PhonologicalFormData(string phonemes, string[] graphicalForms)
        {
            Phonemes = phonemes;
            GraphicalForms = graphicalForms;
        }
    }
}
