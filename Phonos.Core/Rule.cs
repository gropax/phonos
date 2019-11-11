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
        public string Name { get; }
        public Interval TimeSpan { get; }
        public IQuery Query { get; }
        public IQuery LookBehind { get; }
        public IQuery LookAhead { get; }
        public PhonologicalMap[] Maps { get; }

        public Rule(string name, Interval timeSpan, IQuery query, PhonologicalMap[] maps,
            IQuery lookBehind = null, IQuery lookAhead = null)
        {
            Name = name;
            TimeSpan = timeSpan;
            Query = query;
            Maps = maps;
            LookBehind = lookBehind ?? new NullQuery();
            LookAhead = lookAhead ?? new NullQuery();
        }

        public Word[] Apply(Word word)
        {
            var matches = Match(word);

            if (matches.ToIntervals().Count() == 0)
                return new Word[0];

            return Maps.Select(map => DeriveWord(word, map, matches)).ToArray();
        }

        public Word DeriveWord(Word word, PhonologicalMap map, SortedIntervals<string[]> matches)
        {
            var replacements = matches.Map(match => map.Map(match));
            var alignment = word.Phonemes.AlignReplace(replacements);

            // Compute new phoneme sequence
            var phonemes = alignment.Right;

            // Compute every graphical forms
            var graphicalForms = map.GraphicalMaps.SelectMany(gmap =>
                word.GraphicalForms.Select(gf =>
                    DeriveGraphicalForm(word, gmap, gf, replacements, alignment))).ToArray();

            // Realign fields on new phoneme sequence
            var fields = word.Fields.ToDictionary(kv => kv.Key,
                kv => RealignField(kv.Value, replacements, alignment));

            return new Word(phonemes, graphicalForms, fields);
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
                    .Single();

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
                    .Single();

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

        public SortedIntervals<string[]> Match(Word word)
        {
            int index = 0;
            var matches = new List<Interval<string[]>>();

            while (index < word.Phonemes.Length)
            {
                var behind = LookBehind.Match(word, index);
                if (behind == null)
                {
                    index++;
                    continue;
                }

                var match = Query.Match(word, behind.End);
                if (match == null)
                {
                    index++;
                    continue;
                }

                var ahead = LookAhead.Match(word, match.End);
                if (ahead == null)
                    index++;
                else
                {
                    matches.Add(match);
                    index = match.End;
                }
            }

            return matches.AssumeSorted();
        }
    }

    public class PhonologicalMap
    {
        public Func<string[], string[]> Map { get; }
        public GraphicalMap[] GraphicalMaps { get; }

        public PhonologicalMap(Func<string[], string[]> map, GraphicalMap[] graphicalMaps)
        {
            Map = map;
            GraphicalMaps = graphicalMaps;
        }
    }

    public class GraphicalMap
    {
        public Func<string, string> Map { get; }
        public GraphicalMap(Func<string, string> map)
        {
            Map = map;
        }
    }
}
