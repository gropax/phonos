using Intervals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phonos.Core.Tests.TestData
{
    public class YamlParser
    {
        public IEnumerable<WhiteBoxTest> ParseWhiteBoxTests(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var result = deserializer.Deserialize(reader);

            foreach (var kv in (Dictionary<object, dynamic>)result)
            {
                string id = (string)kv.Key;

                var stepsObj = (List<object>)kv.Value;
                var steps = new List<WhiteBoxStep>();

                var latin = (string)stepsObj[0];

                foreach (var stepObj in stepsObj.Skip(1))
                {
                    var parts = ((string)stepObj).Split(new string[] { "=>" }, StringSplitOptions.None);
                    string phono = parts[0].Trim();
                    var graphicalForms = parts[1].Split(',').Select(g => g.Trim()).ToArray();

                    steps.Add(new WhiteBoxStep(phono, graphicalForms));
                }

                yield return new WhiteBoxTest(latin, steps.ToArray());
            }
        }

        public IEnumerable<BlackBoxTest> ParseBlackBoxTests(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var result = deserializer.Deserialize(reader);

            foreach (var kv in (Dictionary<object, dynamic>)result)
            {
                string latin = (string)kv.Key;

                var fieldsKv = (Dictionary<object, dynamic>)kv.Value;
                //var metas = new string[0];
                var outputs = new List<SampleOutput>();

                foreach (var fieldKv in fieldsKv)
                {
                    string fieldName = (string)fieldKv.Key;

                    var outputPhono = ParsePhonemes((string)fieldKv.Key);
                    var graphicalForms = new Alignment<string>[0];
                    var metas = new string[0];
                    var wordFields = new Dictionary<string, Alignment<string>>();

                    if (fieldKv.Value.GetType() == typeof(string))
                    {
                        graphicalForms = ((string)fieldKv.Value)
                            .Split(',').Select(s => ParseAlignment(s.Trim()))
                            .ToArray();
                    }
                    else
                    {
                        foreach (var fieldKv2 in (Dictionary<object, dynamic>)fieldKv.Value)
                        {
                            string fieldName2 = (string)fieldKv2.Key;

                            if (fieldName2 == "_graph")
                            {
                                if (fieldKv2.Value.GetType() == typeof(List<object>))
                                    graphicalForms = ((List<dynamic>)fieldKv2.Value)
                                        .Select(g => ParseAlignment((string)g))
                                        .ToArray();
                                else
                                    graphicalForms = new[]
                                    {
                                        ParseAlignment((string)fieldKv2.Value),
                                    };
                            }
                            else if (fieldName2 == "_meta")
                            {
                                metas = ((string)fieldKv2.Value)
                                    .Split(',').Select(m => m.Trim())
                                    .ToArray();
                            }
                            else if (fieldName2.StartsWith("_"))
                                wordFields.Add(fieldName2.Substring(1), ParseAlignment(fieldKv2.Value));
                        }
                    }

                    outputs.Add(new SampleOutput(
                        new Word(outputPhono, graphicalForms, wordFields, metas)));
                }

                yield return new BlackBoxTest(latin, outputs.ToArray());
            }
        }

        public IEnumerable<RuleContextTest> ParseRuleTests(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var result = deserializer.Deserialize(reader);

            foreach (var kv in (Dictionary<object, dynamic>)result)
            {
                string id = (string)kv.Key;
                var fields = (Dictionary<object, dynamic>)kv.Value;

                var src = (string)fields["src"];
                var context = (string)fields["context"];

                var rules = new List<string>();
                var rulesField = fields["rules"];
                if (rulesField.GetType() == typeof(List<object>))
                    foreach (var rule in rulesField)
                        rules.Add((string)rule);
                else
                    rules.Add((string)rulesField);

                var from = ParseDate((string)fields["from"]);
                var to = ParseDate((string)fields["to"]);

                var samples = new List<RuleTestSample>();
                var wordsKv = (Dictionary<object, dynamic>)fields["samples"];

                foreach (var wordKv in wordsKv)
                {
                    var inputPhono = (string)wordKv.Key;
                    var fieldsKv = (Dictionary<object, dynamic>)wordKv.Value;

                    var phonemes = ParsePhonemes(inputPhono);
                    var graphicalForms = new Alignment<string>[0];
                    var metas = new string[0];
                    var wordFields = new Dictionary<string, Alignment<string>>();
                    var outputs = new List<SampleOutput>();

                    foreach (var fieldKv in fieldsKv)
                    {
                        string fieldName = (string)fieldKv.Key;

                        if (fieldName == "_graph")
                        {
                            if (fieldKv.Value.GetType() == typeof(List<object>))
                                graphicalForms = ((List<dynamic>)fieldKv.Value)
                                    .Select(g => ParseAlignment((string)g))
                                    .ToArray();
                            else
                                graphicalForms = new[]
                                {
                                    ParseAlignment((string)fieldKv.Value),
                                };
                        }
                        else if (fieldName == "_meta")
                        {
                            metas = ((string)fieldKv.Value)
                                .Split(',').Select(m => m.Trim())
                                .ToArray();
                        }
                        else if (fieldName.StartsWith("_"))
                            wordFields.Add(fieldName.Substring(1), ParseAlignment(fieldKv.Value));
                        else
                        {
                            var outputPhono = ParsePhonemes((string)fieldKv.Key);
                            var graphicalForms2 = new Alignment<string>[0];
                            var metas2 = new string[0];
                            var wordFields2 = new Dictionary<string, Alignment<string>>();

                            if (fieldKv.Value.GetType() == typeof(string))
                            {
                                graphicalForms2 = ((string)fieldKv.Value)
                                    .Split(',').Select(s => ParseAlignment(s.Trim()))
                                    .ToArray();
                            }
                            else
                            {
                                foreach (var fieldKv2 in (Dictionary<object, dynamic>)fieldKv.Value)
                                {
                                    string fieldName2 = (string)fieldKv2.Key;

                                    if (fieldName2 == "_graph")
                                    {
                                        if (fieldKv2.Value.GetType() == typeof(List<object>))
                                            graphicalForms2 = ((List<dynamic>)fieldKv2.Value)
                                                .Select(g => ParseAlignment((string)g))
                                                .ToArray();
                                        else
                                            graphicalForms2 = new[]
                                            {
                                                ParseAlignment((string)fieldKv2.Value),
                                            };
                                    }
                                    else if (fieldName2 == "_meta")
                                    {
                                        metas2 = ((string)fieldKv2.Value)
                                            .Split(',').Select(m => m.Trim())
                                            .ToArray();
                                    }
                                    else if (fieldName2.StartsWith("_"))
                                        wordFields2.Add(fieldName2.Substring(1), ParseAlignment(fieldKv2.Value));
                                }
                            }

                            outputs.Add(new SampleOutput(
                                new Word(outputPhono, graphicalForms2, wordFields2, metas2)));
                        }
                    }

                    var inputWord = new Word(phonemes, graphicalForms, wordFields, metas);

                    samples.Add(new RuleTestSample(inputWord, outputs.ToArray()));
                }

                yield return new RuleContextTest(
                    id: id,
                    source: src,
                    rules: rules.ToArray(),
                    from: from,
                    to: to,
                    samples: samples.ToArray()
                    );
            }
        }

        private DateInfo ParseDate(string dateInfo)
        {
            string dateStr = dateInfo;
            bool certain = true;

            if (dateInfo.EndsWith('?'))
            {
                dateStr = dateStr.Substring(0, dateInfo.Length - 1);
                certain = false;
            }

            var date = int.Parse(dateStr);
            return new DateInfo(date, certain);
        }

        public char[] Range1 = new[] { 'ʷ', 'ʰ', 'ʲ', 'ː', '̃', '̊' };
        public char[] Range2 = new[] { '̯' };
        public string[] ParsePhonemes(string phonStr)
        {
            var phonemes = new List<string>();
            for (int i = phonStr.Length - 1; i >= 0; i--)
            {
                char current = phonStr[i];
                if (Range1.Contains(current))
                {
                    phonemes.Add(phonStr.Substring(i - 1, 2));
                    i--;
                }
                else if (Range2.Contains(current))
                {
                    if (Range1.Contains(phonStr[i - 1]))
                    {
                        if (Range1.Contains(phonStr[i - 3]))
                        {
                            phonemes.Add(phonStr.Substring(i - 4, 5));
                            i -= 4;
                        }
                        else
                        {
                            phonemes.Add(phonStr.Substring(i - 3, 4));
                            i -= 3;
                        }
                    }
                    else
                    {
                        phonemes.Add(phonStr.Substring(i - 2, 3));
                        i -= 2;
                    }
                }
                else
                    phonemes.Add(phonStr[i].ToString());
            }

            return phonemes.AsEnumerable().Reverse().ToArray();
        }

        public Alignment<string> ParseAlignment(string alignStr)
        {
            var intervals = new List<Interval<string>>();
            int i = 0;

            var parts = alignStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var fields = part.Split(':');
                string value = fields[0];
                int start = i;
                int length;

                if (fields.Length == 3)
                {
                    start = int.Parse(fields[1]);
                    length = int.Parse(fields[2]);
                    i = start;
                }
                else if (fields.Length == 2)
                    length = int.Parse(fields[1]);
                else
                    length = 1;

                intervals.Add(new Interval<string>(start, length, value));
                i += length;
            }

            return new Alignment<string>(intervals);
        }
    }
}
