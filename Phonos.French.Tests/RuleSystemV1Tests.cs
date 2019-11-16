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
        public Latin.AccentAnalyzer AccentAnalyzer { get; }

        public RuleSystemV1Tests()
        {
            RuleSystem = new RuleSystemV1();
            WordParser = new Latin.WordParser();
            SyllableAnalyzer = new Latin.SyllableAnalyzer();
            AccentAnalyzer = new Latin.AccentAnalyzer();
        }

        [Theory]
        [InlineData("ambulat", /*phono:*/ "amblat", /*graphs:*/ "amblat")]
        [InlineData("auricula", /*phono:*/ "au̯rikla", /*graphs:*/ "auricla")]  // @faked
        [InlineData("oculum", /*phono:*/ "oklum", /*graphs:*/ "oclum")]
        [InlineData("tabula", /*phono:*/ "tabla", /*graphs:*/ "tabla")]
        [InlineData("genita")]  // no match
        [InlineData("quaesita")]  // no match
        public void TestRule1(params string[] data)
        {
            TestRule(RuleSystem.Rule1(), data);
        }

        [Theory]
        [InlineData("calidum", /*phono:*/ "kaldum", /*graphs:*/ "caldum")]
        [InlineData("viridem", /*phono:*/ "wirdem", /*graphs:*/ "virdem")]
        [InlineData("genita", /*phono:*/ "genta", /*graphs:*/ "genta")]
        [InlineData("quaesita", /*phono:*/ "kʷai̯sta", /*graphs:*/ "quaesta")]
        [InlineData("oculum")]  // no match
        [InlineData("tabula")]  // no match
        public void TestRule2(params string[] data)
        {
            TestRule(RuleSystem.Rule2(), data);
        }

        [Theory]
        [InlineData("debita", /*phono:*/ "debta", /*graphs:*/ "debta")]
        [InlineData("movita", /*phono:*/ "mowta", /*graphs:*/ "movta")]
        [InlineData("hominem")]  // no match
        public void TestRule3(params string[] data)
        {
            TestRule(RuleSystem.Rule3(), data);
        }

        [Theory]
        [InlineData("gaudia", /*phono:*/ "gau̯dja", /*graphs:*/ "gaudia")]
        [InlineData("nausea", /*phono:*/ "nau̯sja", /*graphs:*/ "nausea")]
        [InlineData("vidua", /*phono:*/ "widwa", /*graphs:*/ "vidua")]
        public void TestRule4(params string[] data)
        {
            TestRule(RuleSystem.Rule4(), data);
        }

        [Theory]
        [InlineData("hominem", /*phono:*/ "homnem", /*graphs:*/ "homnem")]
        [InlineData("feretrum", /*phono:*/ "fertrum", /*graphs:*/ "fertrum")]
        [InlineData("comitem", /*phono:*/ "komtem", /*graphs:*/ "comtem")]
        [InlineData("cubitum", /*phono:*/ "kubtum", /*graphs:*/ "cubtum")]
        public void TestRule5(params string[] data)
        {
            TestRule(RuleSystem.Rule5(), data);
        }

        [Theory]
        [InlineData("sacramentum", /*phono:*/ "sakrəmentum", /*graphs:*/ "sacramentum")]
        [InlineData("armatūra", /*phono:*/ "armətuːra", /*graphs:*/ "armatūra")]
        [InlineData("firmamente", /*phono:*/ "firməmente", /*graphs:*/ "firmamente")]
        public void TestRule6(params string[] data)
        {
            TestRule(RuleSystem.Rule6(), data);
        }

        [Theory]
        [InlineData("ferum", /*phono:*/ "fɛrom", /*graphs:*/ "ferum")]
        [InlineData("sērum", /*phono:*/ "serom", /*graphs:*/ "sērum")]
        [InlineData("soror", /*phono:*/ "sɔrɔr", /*graphs:*/ "soror")]
        [InlineData("sōlum", /*phono:*/ "solom", /*graphs:*/ "sōlum")]
        [InlineData("subinde", /*phono:*/ "sobendɛ", /*graphs:*/ "subinde")]
        [InlineData("poena", /*phono:*/ "pena", /*graphs:*/ "poena")]
        [InlineData("praeda", /*phono:*/ "prɛda", /*graphs:*/ "praeda")]
        [InlineData("saeta", /*phono:*/ "sɛta", /*graphs:*/ "saeta")]
        [InlineData("pauper", /*phono:*/ "pɔpɛr", /*graphs:*/ "pauper")]
        [InlineData("causa", /*phono:*/ "kɔsa", /*graphs:*/ "causa")]
        public void TestRuleSystem1(params string[] data)
        {
            TestRules(RuleSystem.RuleSystem1(), data);
        }


        private void TestRules(Rule[] rules, string[] data, Func<Rule[], IRuleSequencer> sequencerBuilder = null)
        {
            var testData = ParseData(data);

            var word = WordParser.Parse(testData.Latin);
            SyllableAnalyzer.Analyze(word);
            AccentAnalyzer.Analyze(word);

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

        private void TestRule(Rule rule, string[] data)
        {
            var testData = ParseData(data);

            var word = WordParser.Parse(testData.Latin);
            SyllableAnalyzer.Analyze(word);
            AccentAnalyzer.Analyze(word);

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
