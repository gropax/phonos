using Intervals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phonos.Core
{
    public class Word
    {
        public string[] Phonemes { get; }
        public Alignment<string>[] GraphicalForms { get; }
        public Dictionary<string, Alignment<string>> Fields { get; }
        public string[] Metas { get; }

        public Word(string[] phonemes, Alignment<string>[] graphicalForms,
            Dictionary<string, Alignment<string>> fields = null, string[] metas = null)
        {
            Phonemes = phonemes;
            GraphicalForms = graphicalForms;
            Fields = fields ?? new Dictionary<string, Alignment<string>>();
            Metas = metas ?? new string[0];
        }

        public Alignment<string> GetField(string name)
        {
            if (Fields.TryGetValue(name, out var field))
                return field;
            //else
            //    throw new KeyNotFoundException($"No field with name [{name}].");
            else
                return new Alignment<string>(Enumerable.Empty<Interval<string>>());
        }

        public void SetField(string name, Alignment<string> alignment)
        {
            Fields[name] = alignment;
        }
    }

    public static class Alignment
    {
        public static Alignment<string> Parse(string intervals)
        {
            return new Alignment<string>(ParseIntervals(intervals));
        }

        public static IEnumerable<Interval<string>> ParseIntervals(string intervals)
        {
            int i = 0;
            foreach (var g in intervals.Split(" "))
            {
                var parts = g.Split(":", StringSplitOptions.RemoveEmptyEntries);
                int length = parts.Length > 1 ? int.Parse(parts[1]) : 1;
                yield return new Interval<string>(i, length, parts[0]);
                i += length;
            }
        }
    }

    public class Alignment<T>
    {
        public SortedIntervals<T> Intervals { get; }
        public Alignment(IEnumerable<Interval<T>> intervals)
        {
            Intervals = intervals.Sorted();
        }
    }
}
