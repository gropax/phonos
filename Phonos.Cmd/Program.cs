using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using Phonos.French;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.WriteLine($"\t{latinWord}");
                foreach (var step in ToSteps(derivation))
                    Console.WriteLine($"\t{step.DebuggerDisplay}");
            }
        }

        private static DerivationStep[] ToSteps(WordDerivation derivation)
        {
            var steps = new List<DerivationStep>();

            var d = derivation;
            while (d.Previous != null)
            {
                steps.Add(ToStep(d));
                d = d.Previous;
            }

            return steps.AsEnumerable().Reverse().ToArray();
        }

        private static DerivationStep ToStep(WordDerivation d)
        {
            var phonemes = string.Join("", d.Derived.Phonemes);
            var graphicalForms = d.Derived.GraphicalForms
                .Select(g => string.Join("", g.Intervals
                    .OrderBy(i => i.Start)
                    .ThenBy(i => i.End)
                    .Values()))
                .ToArray();
            return new DerivationStep(
                phonemes,
                graphicalForms,
                d.Rule.Id,
                d.Rule.TimeSpan.Start,
                d.Rule.TimeSpan.End,
                string.Join(" / ", d.Rule.Operations.Select(o => o.Name)));
        }
    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    public class DerivationStep
    {
        public string Phonemes { get; }
        public string[] GraphicalForms { get; }
        public string RuleId { get; }
        public int StartDate { get; }
        public int EndDate { get; }
        public string Operations { get; }
        public string DebuggerDisplay => $"{Phonemes} ({string.Join(", ", GraphicalForms)})\t{RuleId}\t{StartDate}-{EndDate}\t{Operations}";

        public DerivationStep(string phonemes, string[] graphicalForms, string ruleId, int startDate, int endDate, string operations)
        {
            Phonemes = phonemes;
            GraphicalForms = graphicalForms;
            RuleId = ruleId;
            StartDate = startDate;
            EndDate = endDate;
            Operations = operations;
        }

        public override bool Equals(object obj)
        {
            return obj is DerivationStep step &&
                   Phonemes == step.Phonemes &&
                   Enumerable.SequenceEqual(GraphicalForms, step.GraphicalForms);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Phonemes, GraphicalForms);
        }
    }
}
