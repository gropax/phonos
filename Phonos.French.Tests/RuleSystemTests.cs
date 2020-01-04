using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Rules;
using Phonos.Core.Tests.TestData;
using Phonos.Latin;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class RuleSystemTests
    {
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

        protected void TestRules(IRule[] rules, RuleContextTest ruleTest)
        {
            var dict = rules.ToDictionary(r => r.Id, r => r);
            Assert.True(dict.TryGetValue(ruleTest.Id, out var rule),
                $"Rule not found [{ruleTest.Id}].");
            TestRule(rule, ruleTest);
        }

        protected void TestRule(IRule rule, RuleContextTest ruleTest)
        {
            foreach (var sample in ruleTest.Samples)
                TestSample(rule, ruleTest, sample);
        }


        protected void TestSample(IRule rule, RuleContextTest ruleTest, RuleTestSample sample)
        {
            var word = sample.Input;
            var initialDerivation = WordDerivation.Origin(word);
            var derivation = rule.Derive(ExecutionContext, initialDerivation);
            var derived = derivation.Select(d => d.Derived).ToArray();

            int expectedNb = sample.Outputs.Length;

            Assert.True(expectedNb == derived.Length,
                $"Expect [{word.PrettyPhonemes}] to have [{expectedNb}] derivations ({string.Join(", ", sample.Outputs.Select(o => $"[{o.Word.PrettyPhonemes}]"))}) but only have [{derived.Length}].");

            for (int i = 0; i < sample.Outputs.Length; i++)
                TestSampleOutput(sample.Outputs, derived, i);
        }

        protected void TestBlackBoxSample(SampleOutput[] outputs,
            Word[] derived, int index)
        {
            var output = outputs[index];
            var expected = output.Word;
            var real = derived[index];

            Assert.Equal(expected.Phonemes, real.Phonemes);
            Assert.Equal(expected.Liaison, real.Liaison);

            Assert.Equal(expected.GraphicalForms.Length, real.GraphicalForms.Length);
            for (int j = 0; j < expected.GraphicalForms.Length; j++)
                TestGraphicalForm(expected.GraphicalForms, real.GraphicalForms, j, detailed: false);

            Assert.Equal(expected.Metas, real.Metas);

            foreach (var key in expected.Fields.Keys)
            {
                var expectedField = expected.Fields[key];
                Assert.True(real.Fields.TryGetValue(key, out var realField),
                    $"Missing field [{key}].");
                Assert.Equal(DumpField(expectedField), DumpField(realField));
            }
        }

        protected void TestSampleOutput(SampleOutput[] outputs,
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
            Core.Alignment<string>[] derived, int index, bool detailed = true)
        {
            var expected = expectedForms[index].Intervals.OrderBy(i => i.Start).ThenBy(i => i.End).Values().ToArray();
            var real = derived[index].Intervals.OrderBy(i => i.Start).ThenBy(i => i.End).Values().ToArray();

            if (detailed)
                Assert.Equal(expected, real);
            else
                Assert.Equal(string.Join("", expected), string.Join("", real));
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
