using Phonos.Fra.Similarity.Lexicon;
using Phonos.Fra.Similarity.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Distances.Tests
{
    public class NeighborhoodConsonantDistanceTests : ConsonantDistanceTests
    {
        protected override IDistance<Phoneme> _distance => Distances.NeighborhoodConsonantDistance;
    }


    public abstract class ConsonantDistanceTests : DistanceTests<Phoneme>
    {
        protected override IEnumerable<Phoneme> _points => Phonemes.Consonants.Append(Phonemes._);

        [Theory]
        [InlineData("p", "b")]
        [InlineData("t", "d")]
        [InlineData("k", "g")]
        [InlineData("s", "z")]
        [InlineData("f", "v")]
        [InlineData("ʃ", "ʒ")]
        [InlineData("χ", "ʁ")]
        public void TestMinimalPairs(string s1, string s2)
        {
            var p1 = Phonemes.BySymbol(s1);
            var p2 = Phonemes.BySymbol(s2);

            var d1 = _distance.GetDistance(p1, p2);

            foreach ((var q1, var q2) in _points.UnorderedPairs())
            {
                var d = _distance.GetDistance(q1, q2);
                Assert.True(d1 <= d, $"d({p1}, {p2}) = {d1} should be minimal but d({q1}, {q2}) = {d}.");
            }
        }

        [Theory]
        [InlineData("p", "t", "=", "t", "k")]
        [InlineData("b", "d", "=", "d", "g")]
        [InlineData("f", "s", "=", "s", "ʃ")]
        [InlineData("v", "z", "=", "z", "ʒ")]

        [InlineData("p", "m", "=", "t", "n")]
        [InlineData("p", "m", "=", "k", "ŋ")]
        [InlineData("b", "m", "=", "d", "n")]
        [InlineData("b", "m", "=", "g", "ŋ")]
        public void TestParallelPairs(string s1, string s2, string op, string s3, string s4)
        {
            var p1 = Phonemes.BySymbol(s1);
            var p2 = Phonemes.BySymbol(s2);
            var p3 = Phonemes.BySymbol(s3);
            var p4 = Phonemes.BySymbol(s4);

            var d1 = _distance.GetDistance(p1, p2);
            var d2 = _distance.GetDistance(p3, p4);

            if (op == "=")
                Assert.True(d1 == d2, $"d({p1}, {p2}) = {d1} ≠ {d2} = d({p3}, {p4})");
            else if (op == "<")
                Assert.True(d1 < d2, $"d({p1}, {p2}) = {d1} ≮ {d2} = d({p3}, {p4})");
            else if (op == ">")
                Assert.True(d1 > d2, $"d({p1}, {p2}) = {d1} ≯ {d2} = d({p3}, {p4})");
            else
                throw new NotImplementedException();
        }
    }
}
