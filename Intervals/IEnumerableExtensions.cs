using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intervals
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> ts, SortedIntervals<T[]> intervals)
        {
            var enumerator = ts.GetEnumerator();
            var idx = 0;
            Interval<T[]> lastInterval = null;

            foreach (var interval in intervals)
            {
                if (lastInterval != null && interval.Start < lastInterval.End)
                    throw new ArgumentException("Intervals should not overlap.");

                // Yield elements before interval starts
                for (int i = 0; i < interval.Start - idx; i++)
                {
                    enumerator.MoveNext();
                    yield return enumerator.Current;
                }

                // Skip elements covered by interval
                for (int i = 0; i < interval.Length; i++)
                    enumerator.MoveNext();

                // Yield elements in interval value
                foreach (var elem in interval.Value())
                    yield return elem;

                idx = interval.End;
                lastInterval = interval;
            }

            // Yield elements after last interval end
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static Alignment<T, T[]> AlignReplace<T>(this T[] ts, SortedIntervals<T[]> intervals)
        {
            var builder = new List<T>();

            string toString;
            var intervalAlignments = new List<IntervalAlignment<T[]>>();
            var mappings = new Dictionary<int, int>();

            try
            {
                var leftIdx = 0;
                var rightIdx = 0;
                mappings.Add(0, 0);

                Interval<T[]> lastInterval = null;
                foreach (var interval in intervals)
                {
                    if (lastInterval != null && interval.Start < lastInterval.End)
                        throw new ArgumentException("Intervals should not overlap.");

                    // Append string segment before interval
                    T[] before = ts.SubArray(leftIdx, interval.Start - leftIdx);
                    builder.AddRange(before);

                    var beforeInterval = new Interval<T[]>(leftIdx, before.Length, before);
                    intervalAlignments.Add(new IntervalAlignment<T[]>(beforeInterval,
                        beforeInterval.Translate(rightIdx - leftIdx), true));

                    rightIdx += before.Length;

                    var leftContent = ts.SubArray(interval);

                    // Append string replacement
                    var rightContent = interval.Value;
                    builder.AddRange(rightContent);

                    // Store from and to intervals
                    var rightInterval = new Interval<T[]>(start: rightIdx, length: rightContent.Length, value: interval.Value);
                    intervalAlignments.Add(new IntervalAlignment<T[]>(interval, rightInterval, false));

                    // Store mappings for intervals start and end positions
                    mappings[interval.Start] = rightInterval.Start;
                    mappings[interval.End] = rightInterval.End;

                    leftIdx = interval.End;
                    rightIdx = rightInterval.End;

                    lastInterval = interval;
                }

                // Append string segment after last interval
                var final = ts.SubArray(leftIdx, ts.Length - leftIdx);
                builder.AddRange(final);

                if (final.Length > 0)
                {
                    var finalInterval = new Interval<T[]>(leftIdx, final.Length, final);
                    intervalAlignments.Add(new IntervalAlignment<T[]>(finalInterval,
                        finalInterval.Translate(rightIdx - leftIdx), true));
                }

                // Finalize TO string and store mapping for end of strings
                toString = builder.ToString();
                mappings[ts.Length] = toString.Length;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException("Interval exceeding string dimensions.", e);
            }

            return new Alignment<T, T[]>(
                left: ts,
                right: builder.ToArray(),
                intervals: intervalAlignments.ToArray(),
                mappings: mappings);
        }

    }
}
