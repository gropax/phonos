using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using Phonos.French;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\nPlease enter a latin word:");
                var latinWord = Console.ReadLine();

                DeriveWord(latinWord);
            }
        }

        static void DeriveWord(string latinWord)
        {
            latinWord = latinWord.Replace(':', '\u0304');

            var latinParser = new Latin.WordParser();
            var word = latinParser.Parse(latinWord);

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

            var context = new ExecutionContext(new Dictionary<string, IAnalyzer>()
            {
                { "syllable", new Latin.SyllableAnalyzer() },
            });

            var derivations = sequencer.Derive(context, word);
            var sequences = derivations.Select(d => ToSteps(d)).ToArray();

            foreach (var derivation in derivations)
            {
                string output = derivation.Derived.DebuggerDisplay;

                Console.WriteLine($"\nOutput: {output}\n");
                Console.WriteLine($"Steps:");
                foreach (var step in ToSteps(derivation))
                    Console.WriteLine($"\t{step.DebuggerDisplay}");
            }
        }

        private static WhiteBoxStep[] ToSteps(WordDerivation derivation)
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

        private static WhiteBoxStep ToStep(Word word)
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
