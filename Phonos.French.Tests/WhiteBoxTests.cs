using Intervals;
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
    public class WhiteBoxTests : RuleSystemTests
    {
        public static WhiteBoxTestData TestData
        {
            get
            {
                var parser = new YamlParser();
                var path = @".\Specs\WhiteBox.yaml";
                using (StreamReader reader = File.OpenText(path))
                    return new WhiteBoxTestData(parser.ParseWhiteBoxTests(reader).ToList());
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(WhiteBoxTest whiteBoxTest)
        {
            var latinParser = new Latin.WordParser();
            var word = latinParser.Parse(whiteBoxTest.Latin);

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

            var derivations = sequencer.Derive(word);
            var sequences = derivations.Select(d => ToSteps(d)).ToArray();

            var seqs = sequences.ToList();
            var possibleSteps = new List<WhiteBoxStep>();

            for (int i = 1; i < whiteBoxTest.Steps.Length; i++)
            {
                var expected = whiteBoxTest.Steps[i];
                var newSeqs = new List<WhiteBoxStep[]>();

                for (int j = 0; j < seqs.Count; j++)
                {
                    var seq = seqs[j];

                    if (i >= seq.Length)
                        continue;
                    else if (expected.Equals(seq[i]))
                        newSeqs.Add(seq);
                    else
                        possibleSteps.Add(seq[i]);
                }

                Assert.True(newSeqs.Count > 0,
                    $"Could not find any derivation matching [{expected.Phonemes}] at step [{i}] : {string.Join(", ", possibleSteps.Select(s => "[" + s.Phonemes + "]"))}");

                seqs = newSeqs;
                possibleSteps.Clear();
            }
        }

        private WhiteBoxStep[] ToSteps(WordDerivation derivation)
        {
            var steps = new List<WhiteBoxStep>();

            steps.Add(ToStep(derivation.Derived));

            var d = derivation;
            while (d.Previous != null)
            {
                steps.Add(ToStep(d.Original));
                d = d.Previous;
            }

            return steps.AsEnumerable().Reverse().ToArray();
        }

        private WhiteBoxStep ToStep(Word word)
        {
            var phonemes = string.Join("", word.Phonemes);
            var graphicalForms = word.GraphicalForms
                .Select(g => string.Join("", g.Intervals
                    .OrderBy(i => i.Start)
                    .ThenBy(i => i.End)
                    .Values()))
                .ToArray();
            return new WhiteBoxStep(phonemes, graphicalForms);
        }
    }
}
