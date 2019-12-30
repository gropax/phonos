using Intervals;
using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Rules
{
    public class Rule : IRule
    {
        public string Id { get; }
        public string Group { get; }
        public Interval TimeSpan { get; }
        public ContextualQuery[] Queries { get; }
        public Operation[] Operations { get; }
        public string[] Analyzers { get; }
        public bool Optional { get; }

        public Rule(string id, string group, Interval timeSpan,
            ContextualQuery[] queries, Operation[] operation,
            string[] analyzers = null, bool optional = false)
        {
            Id = id;
            Group = group;
            TimeSpan = timeSpan;
            Queries = queries;
            Operations = operation;
            Analyzers = analyzers ?? new string[0];
            Optional = optional;
        }

        public WordDerivation[] Derive(ExecutionContext context,
            WordDerivation derivation)
        {
            return Apply(context, derivation.Derived)
                .Select(w => new WordDerivation(this, derivation.Derived, w, derivation))
                .ToArray();
        }

        public Word[] Apply(ExecutionContext context, Word word)
        {
            foreach (var analyzer in Analyzers)
                context.RunAnalyzer(analyzer, word);

            var words = new List<Word>();
            if (Optional)
                words.Add(word);

            var matches = new List<Interval<string[]>>();

            foreach (var query in Queries)
                foreach (var match in query.Match(word))
                    if (matches.IntersectingWith(match).Count() == 0)
                        matches.Add(match);

            if (matches.Count() == 0)
                return words.ToArray();

            var derived = Operations.Select(r => DeriveWord(r, word, matches.Sorted())).ToArray();
            words.AddRange(derived);

            return words.ToArray();
        }

        public Word DeriveWord(Operation operation, Word word, SortedIntervals<string[]> matches)
        {
            var replacements = matches.Map(match => operation.Phonological(match));
            var alignment = word.Phonemes.AlignReplace(replacements);

            // Compute new phoneme sequence
            var phonemes = alignment.Right;

            // Compute every graphical forms
            var graphicalForms = operation.Graphical.SelectMany(gmap =>
                word.GraphicalForms.Select(gf =>
                    DeriveGraphicalForm(word, gmap, gf, replacements, alignment))).ToArray();

            // Realign fields on new phoneme sequence
            var fields = word.Fields.ToDictionary(kv => kv.Key,
                kv => RealignField(kv.Value, replacements, alignment));

            var metas = word.Metas.Concat(operation.Metas).ToArray();

            return new Word(phonemes, graphicalForms, fields, metas);
        }

        public Alignment<string> RealignField(Alignment<string> field,
            SortedIntervals<string[]> replacements, Alignment<string, string[]> alignment)
        {
            var enumerator = field.Intervals
                .OrderBy(i => i.Start).ThenBy(i => i.End)
                .GetEnumerator();

            var annotations = new List<Interval<string>>();
            int shift = 0;
            int lastPosition = 0;

            foreach (var i in replacements)
            {
                var intersected = field.Intervals
                    .IntersectingWith(i, ContainsMode.STRICT)
                    .OrderBy(j => j.Start)
                    .ThenBy(j => j.End)
                    .ToArray();

                var originalAnnotations = intersected.Length > 0
                    ? intersected.Range(g => string.Join(string.Empty, g))
                    : new Interval<string>(i, string.Empty);

                var coveredPhonemes = alignment.Intervals
                    .Select(a => new Interval<IntervalAlignment<string[]>>(a.Left, a))
                    .IntersectingWith(originalAnnotations, ContainsMode.STRICT)
                    .Values().Select(a => a.Right)
                    .AsEnumerable<IInterval>().ToList();

                var range = coveredPhonemes.Range();

                var realignedAnnotation = new Interval<string>(range, originalAnnotations.Value);

                while (enumerator.MoveNext() && enumerator.Current.End <= originalAnnotations.Start)
                    if (enumerator.Current.Start >= lastPosition)
                        annotations.Add(enumerator.Current.Translate(shift));

                annotations.Add(realignedAnnotation);

                lastPosition = originalAnnotations.End;
                shift += realignedAnnotation.Length - originalAnnotations.Length;
            }

            do
            {
                if (enumerator.Current.Start + shift >= annotations.Last().End)
                    annotations.Add(enumerator.Current.Translate(shift));
            } while (enumerator.MoveNext());

            return new Alignment<string>(annotations);
        }

        public Alignment<string> DeriveGraphicalForm(Word word, GraphicalMap map,
            Alignment<string> graphicalForm, SortedIntervals<string[]> replacements,
            Alignment<string, string[]> alignment)
        {
            var enumerator = graphicalForm.Intervals
                .OrderBy(i => i.Start).ThenBy(i => i.End)
                .GetEnumerator();

            var newGraphemes = new List<Interval<string>>();
            Interval<string> originalGraphemes = null;
            int shift = 0;
            int lastPosition = 0;

            foreach (var i in replacements)
            {
                var intersected = graphicalForm.Intervals
                    .IntersectingWith(i, ContainsMode.STRICT)
                    .OrderBy(j => j.Start)
                    .ThenBy(j => j.End)
                    .ToArray();

                originalGraphemes = intersected.Length > 0
                    ? intersected.Range(g => string.Join(string.Empty, g))
                    : new Interval<string>(i, string.Empty);

                var coveredPhonemes = alignment.Intervals
                    .Select(a => new Interval<IntervalAlignment<string[]>>(a.Left, a))
                    .IntersectingWith(originalGraphemes, ContainsMode.STRICT)
                    .Values().Select(a => a.Right)
                    .AsEnumerable<IInterval>().ToList();

                var range = coveredPhonemes.Range();

                var before = string.Join("", graphicalForm.Intervals
                    .Where(j => j.End <= originalGraphemes.Start)
                    .Values());
                var after = string.Join("", graphicalForm.Intervals
                    .Where(j => j.Start >= originalGraphemes.End)
                    .Values());

                var replacedGraphemes = map
                    .Map(before, originalGraphemes.Value, after, i.Value.Length)
                    .Translate(range.Start)
                    .ToArray();

                while (enumerator.MoveNext() && enumerator.Current.End <= originalGraphemes.Start)
                    if (enumerator.Current.Start >= lastPosition)
                        newGraphemes.Add(enumerator.Current.Translate(shift));

                int replacementLength = 0;
                if (replacedGraphemes.Length > 0)
                {
                    newGraphemes.AddRange(replacedGraphemes);
                    replacementLength = replacedGraphemes.Range().Length;
                }

                lastPosition = originalGraphemes.End;
                shift += replacementLength - originalGraphemes.Length;
            }

            do
            {
                if (enumerator.Current.Start + shift >= newGraphemes.Last().End)
                    newGraphemes.Add(enumerator.Current.Translate(shift));
            } while (enumerator.MoveNext());

            return new Alignment<string>(newGraphemes);
        }

        public Interval<T> RemapInterval<T>(Dictionary<int, int> mappings, Interval<T> interval)
        {
            int start = mappings[interval.Start];
            int end = mappings[interval.End];
            return new Interval<T>(start, end - start, interval.Value);
        }
    }
}
