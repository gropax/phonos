using Intervals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phonos
{
    public class Word
    {
        public string[] Phonemes { get; }
        public Alignment<string[]>[] GraphicalForms { get; }
        private Dictionary<string, Alignment<string>> _fields;

        public Word(string[] phonemes, Alignment<string[]>[] graphicalForms,
            Dictionary<string, Alignment<string>> fields = null)
        {
            Phonemes = phonemes;
            GraphicalForms = graphicalForms;
            _fields = fields ?? new Dictionary<string, Alignment<string>>();
        }

        public Alignment<string> GetField(string name)
        {
            if (_fields.TryGetValue(name, out var field))
                return field;
            else
                throw new KeyNotFoundException($"No field with name [{name}].");
        }

        public void SetField(string name, Alignment<string> alignment)
        {
            _fields[name] = alignment;
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
