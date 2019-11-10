﻿using Intervals;
using Phonos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos
{
    public class Rule
    {
        public Interval TimeSpan { get; }
        public IQuery Query { get; }
        public IQuery LookBehind { get; }
        public IQuery LookAhead { get; }
        public PhonologicalMap[] Maps { get; }

        public Rule(Interval timeSpan, IQuery query, PhonologicalMap[] maps,
            IQuery lookBehind = null, IQuery lookAhead = null)
        {
            TimeSpan = timeSpan;
            Query = query;
            Maps = maps;
            LookBehind = lookBehind ?? new NullQuery();
            LookAhead = lookAhead ?? new NullQuery();
        }

        public Word[] Apply(Word word)
        {
            var matches = Match(word);
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
            // @todo

            return new Word(phonemes, graphicalForms, null);
        }

        public Alignment<string[]> DeriveGraphicalForm(Word word, GraphicalMap map,
            Alignment<string[]> graphicalForm, SortedIntervals<string[]> replacements,
            Alignment<string, string[]> alignment)
        {
            var enumerator = graphicalForm.Intervals.GetEnumerator();
            var newGraphemes = new List<Interval<string[]>>();
            int shift = 0;

            foreach (var i in replacements)
            {
                var orignalGraphemes = graphicalForm.Intervals
                    .IntersectingWith(i, ContainsMode.STRICT)
                    .Union(g => g.SelectMany(k => k).ToArray())
                    .Single();

                var coveredPhonemes = alignment.Intervals
                    .Select(a => new Interval<IntervalAlignment<string[]>>(a.Left, a))
                    .IntersectingWith(orignalGraphemes, ContainsMode.STRICT)
                    .Values().Select(a => a.Right)
                    .AsEnumerable<IInterval>().ToList();

                var range = coveredPhonemes.Range();

                var replacedGraphemes = new Interval<string[]>(range, map.Map(orignalGraphemes.Value));

                while (enumerator.MoveNext() && enumerator.Current.End <= orignalGraphemes.Start)
                    newGraphemes.Add(enumerator.Current.Translate(shift));

                newGraphemes.Add(replacedGraphemes);

                // Skip the replaced interval
                enumerator.MoveNext();
                shift += replacedGraphemes.Length - orignalGraphemes.Length;
            }

            while (enumerator.MoveNext())
                newGraphemes.Add(enumerator.Current.Translate(shift));

            return new Alignment<string[]>(newGraphemes);
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
        public Func<string[], string[]> Map { get; }
        public GraphicalMap(Func<string[], string[]> map)
        {
            Map = map;
        }
    }
}
