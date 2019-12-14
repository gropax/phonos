using Intervals;
using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public class Rule
    {
        public static Func<string[], string[]> Identity = (x) => x;

        public string Name { get; }
        public string[] Metas { get; }
        public Func<string[], string[]> Phonological { get; }
        public GraphicalMap[] Graphical { get; }

        public Rule(string name, Func<string[], string[]> phonological, GraphicalMap[] graphical, string[] metas = null)
        {
            Name = name;
            Phonological = phonological;
            Graphical = graphical;
            Metas = metas ?? new string[0];
        }
    }

    public class RuleContext
    {
        public string Id { get; }
        public string Group { get; }
        public Interval TimeSpan { get; }
        public ContextualQuery[] Queries { get; }
        public Rule[] Rules { get; }

        public RuleContext(string id, string group, Interval timeSpan,
            ContextualQuery[] queries, Rule[] rules)
        {
            Id = id;
            Group = group;
            TimeSpan = timeSpan;
            Queries = queries;
            Rules = rules;
        }

        public Word[] Apply(Word word)
        {
            var matches = Queries.SelectMany(q => q.Match(word)).Sorted();
            if (matches.Count() == 0)
                return new Word[0];

            var words = Rules.Select(r => DeriveWord(r, word, matches)).ToArray();
            return words;
        }

        public Word DeriveWord(Rule rule, Word word, SortedIntervals<string[]> matches)
        {
            var replacements = matches.Map(match => rule.Phonological(match));
            var alignment = word.Phonemes.AlignReplace(replacements);

            // Compute new phoneme sequence
            var phonemes = alignment.Right;

            // Compute every graphical forms
            var graphicalForms = rule.Graphical.SelectMany(gmap =>
                word.GraphicalForms.Select(gf =>
                    DeriveGraphicalForm(word, gmap, gf, replacements, alignment))).ToArray();

            // Realign fields on new phoneme sequence
            var fields = word.Fields.ToDictionary(kv => kv.Key,
                kv => RealignField(kv.Value, replacements, alignment));

            var metas = word.Metas.Concat(rule.Metas).ToArray();

            return new Word(phonemes, graphicalForms, fields, metas);
        }

        public Alignment<string> RealignField(Alignment<string> field,
            SortedIntervals<string[]> replacements, Alignment<string, string[]> alignment)
        {
            var enumerator = field.Intervals.GetEnumerator();
            var annotations = new List<Interval<string>>();
            int shift = 0;

            foreach (var i in replacements)
            {
                var orignalAnnotations = field.Intervals
                    .IntersectingWith(i, ContainsMode.STRICT)
                    .Union(g => g.First())
                    .SingleOrDefault()
                    ?? new Interval<string>(i, string.Empty);

                var coveredPhonemes = alignment.Intervals
                    .Select(a => new Interval<IntervalAlignment<string[]>>(a.Left, a))
                    .IntersectingWith(orignalAnnotations, ContainsMode.STRICT)
                    .Values().Select(a => a.Right)
                    .AsEnumerable<IInterval>().ToList();

                var range = coveredPhonemes.Range();

                var realignedAnnotation = new Interval<string>(range, orignalAnnotations.Value);

                while (enumerator.MoveNext() && enumerator.Current.End <= orignalAnnotations.Start)
                    annotations.Add(enumerator.Current.Translate(shift));

                annotations.Add(realignedAnnotation);

                shift += realignedAnnotation.Length - orignalAnnotations.Length;
            }

            while (enumerator.MoveNext())
                annotations.Add(enumerator.Current.Translate(shift));

            return new Alignment<string>(annotations);
        }

        public Alignment<string> DeriveGraphicalForm(Word word, GraphicalMap map,
            Alignment<string> graphicalForm, SortedIntervals<string[]> replacements,
            Alignment<string, string[]> alignment)
        {
            var enumerator = graphicalForm.Intervals.GetEnumerator();
            var newGraphemes = new List<Interval<string>>();
            int shift = 0;

            foreach (var i in replacements)
            {
                var orignalGraphemes = graphicalForm.Intervals
                    .IntersectingWith(i, ContainsMode.STRICT)
                    .Union(g => string.Join(string.Empty, g))
                    .SingleOrDefault()
                    ?? new Interval<string>(i, string.Empty);

                var coveredPhonemes = alignment.Intervals
                    .Select(a => new Interval<IntervalAlignment<string[]>>(a.Left, a))
                    .IntersectingWith(orignalGraphemes, ContainsMode.STRICT)
                    .Values().Select(a => a.Right)
                    .AsEnumerable<IInterval>().ToList();

                var range = coveredPhonemes.Range();

                var replacedGraphemes = new Interval<string>(range, map.Map(orignalGraphemes.Value));

                while (enumerator.MoveNext() && enumerator.Current.End <= orignalGraphemes.Start)
                    newGraphemes.Add(enumerator.Current.Translate(shift));

                if (replacedGraphemes.Value.Length > 0)
                    newGraphemes.Add(replacedGraphemes);

                shift += replacedGraphemes.Length - orignalGraphemes.Length;
            }

            while (enumerator.MoveNext())
                newGraphemes.Add(enumerator.Current.Translate(shift));

            return new Alignment<string>(newGraphemes);
        }

        public Interval<T> RemapInterval<T>(Dictionary<int, int> mappings, Interval<T> interval)
        {
            int start = mappings[interval.Start];
            int end = mappings[interval.End];
            return new Interval<T>(start, end - start, interval.Value);
        }
    }

    public class GraphicalMap
    {
        public static GraphicalMap Identity => new GraphicalMap(_ => _);

        public Func<string, string> Map { get; }
        public GraphicalMap(Func<string, string> map)
        {
            Map = map;
        }
    }
}
