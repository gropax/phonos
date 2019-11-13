using Intervals;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phonos.Core.Tests
{
    public static class WordAssert
    {
        public static void Equal(Word[] expected, Word[] real)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                var e = expected[i];
                var r = real[i];
                Assert.Equal(e.Phonemes, r.Phonemes);
            }
        }

        public static void Equal(Word expected, Word real)
        {
            Assert.Equal(expected.Phonemes, real.Phonemes);
        }

        public static void Field(Word word, string fieldName, string expected)
        {
            Assert.Equal(ParseFieldData(expected), word.GetField(fieldName).Intervals);
        }

        private static IEnumerable<Interval<string>> ParseFieldData(string data)
        {
            int i = 0;
            foreach (var g in data.Split(" "))
            {
                int length;

                if (g.Contains(':'))
                {
                    var parts = g.Split(":");
                    length = parts.Length > 1 ? int.Parse(parts[1]) : 1;
                    yield return new Interval<string>(i, length, parts[0]);
                }
                else
                {
                    length = int.Parse(g);
                    yield return new Interval<string>(i, length, string.Empty);
                }

                i += length;
            }
        }
    }
}
