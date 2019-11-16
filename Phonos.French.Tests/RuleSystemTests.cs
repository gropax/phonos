using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
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

        protected void TestRules(Rule[] rules, string[] data, Func<Rule[], IRuleSequencer> sequencerBuilder = null)
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

        protected void TestRule(Rule rule, string[] data)
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
