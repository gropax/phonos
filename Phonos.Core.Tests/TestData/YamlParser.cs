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
        public IEnumerable<RuleTest> Parse(TextReader reader)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var result = deserializer.Deserialize(reader);

            foreach (var kv in (Dictionary<object, dynamic>)result)
            {
                string id = (string)kv.Key;
                var fields = (Dictionary<object, dynamic>)kv.Value;

                var src = fields["src"];
                var desc = fields["desc"];
                var from = int.Parse(fields["from"]);
                var to = int.Parse(fields["to"]);

                //var string[] post = kv2.Value["post"].Select(p => p.ToString());

                var samples = new List<RuleTestSample>();
                var wordsKv = (Dictionary<object, dynamic>)fields["samples"];

                foreach (var wordKv in wordsKv)
                {
                    var inputPhono = (string)wordKv.Key;
                    var fieldsKv = (Dictionary<object, dynamic>)wordKv.Value;

                    var phonemes = ParsePhonemes(inputPhono);
                    var graphicalForms = new List<Alignment<string>>();
                    var wordFields = new Dictionary<string, Alignment<string>>();
                    var outputs = new List<RuleTestSampleOutput>();

                    foreach (var fieldKv in fieldsKv)
                    {
                        string fieldName = (string)fieldKv.Key;

                        if (fieldName == "_graph")
                        {
                            //graphicalForms = new string[0];
                        }
                        else if (fieldName.StartsWith("_"))
                            wordFields.Add(fieldName.Substring(1), ParseAlignment(fieldKv.Value));
                        else
                        {
                            var outputPhono = fieldKv.Key;
                            var outputGraph = fieldKv.Value;
                        }
                    }

                    var inputWord = new Word(phonemes, graphicalForms.ToArray(), wordFields);

                    samples.Add(new RuleTestSample(inputWord, outputs.ToArray()));
                }

                yield return new RuleTest(
                    id: id,
                    source: src,
                    description: desc,
                    from: from,
                    to: to,
                    samples: samples.ToArray()
                    );
            }
        }

        public char[] Range1 = new[] { 'ʷ', 'ʰ', 'ː' };
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
                    phonemes.Add(phonStr.Substring(i - 2, 3));
                    i -= 2;
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

    public class RuleTest
    {
        public string Id { get; }
        public string Source { get; }
        public string Description { get; }
        public int From { get; }
        public int To { get; }
        //public string[] PostProcessing { get; }
        public string[] PostProcessing { get; }
        public RuleTestSample[] Samples { get; }

        public RuleTest(string id, string source, string description, int from, int to, RuleTestSample[] samples)
        {
            Id = id;
            Source = source;
            Description = description;
            From = from;
            To = to;
            Samples = samples;
        }
    } 

    public class RuleTestSample
    {
        public Word Input { get; }
        public RuleTestSampleOutput[] Outputs { get; }

        public RuleTestSample(Word input, RuleTestSampleOutput[] outputs)
        {
            Input = input;
            Outputs = outputs;
        }
    }

    public class RuleTestSampleOutput
    {
        public Word Word { get; }
        public Word[] Outputs { get; }
    }
}
