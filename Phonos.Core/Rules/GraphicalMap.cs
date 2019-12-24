using Intervals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Rules
{
    public class GraphicalMap
    {
        public static GraphicalMap Identity =>
            new GraphicalMap((b, m, a) => m);

        public Func<string, string, string, int, Interval<string>[]> Map { get; }

        public GraphicalMap(Func<string, string, string, Interval<string>[]> map)
        {
            Map = (b, m, a, i) =>
            {
                var intervals = map(b, m, a);

                if (intervals.Length == 0)
                {
                    if (i != 0)
                        throw new Exception("Grapheme intervals should cover phonemes exactly.");
                }
                else
                {
                    if (intervals.Range().Length != i)
                        throw new Exception("Grapheme intervals should cover phonemes exactly.");
                }

                return intervals;
            };
        }

        public GraphicalMap(Func<string, string, string, string> map)
        {
            Map = (b, m, a, i) => new[] {
                new Interval<string>(0, i, map(b, m, a))
            };
        }
    }
}
