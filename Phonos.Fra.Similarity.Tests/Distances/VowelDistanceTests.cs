using Phonos.Fra.Lexicon;
using Phonos.Fra.Similarity.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Distances.Tests
{
    //public class VowelDistanceTests : VowelDistanceTestsBase
    //{
    //    protected override IDistance<Phoneme> _distance => new VowelDistance();
    //}

    public class NeighborhoodVowelDistanceTests : VowelDistanceTestsBase
    {
        protected override IDistance<Phoneme> _distance => PhonemeDistances.NeighborhoodVowelDistance;
    }

    public abstract class VowelDistanceTestsBase : DistanceTests<Phoneme>
    {
        protected override IEnumerable<Phoneme> _points => Phonemes.Vowels.Append(Phonemes._);

        [Theory]
        [InlineData("a", "ɑ")]
        [InlineData("ø", "ə")]
        [InlineData("œ", "ə")]
        [InlineData("ɛ̃", "œ̃")]
        public void TestMinimalPairs(string s1, string s2)
        {
            var p1 = Phonemes.BySymbol(s1);
            var p2 = Phonemes.BySymbol(s2);

            var d1 = _distance.GetDistance(p1, p2);

            foreach ((var q1, var q2) in Phonemes.Vowels.UnorderedPairs())
            {
                var d = _distance.GetDistance(q1, q2);
                Assert.True(d1 <= d, $"d(/{s1}/, /{s2}/) = {d1} should be minimal but d(/{q1.Symbol}/, /{q2.Symbol}/) = {d}.");
            }
        }

        [Theory]
        [InlineData("i", "e", "=", "y", "ø")]
        [InlineData("i", "e", "=", "u", "o")]

        [InlineData("e", "ɛ", "=", "ø", "œ")]
        [InlineData("e", "ɛ", "=", "o", "ɔ")]

        [InlineData("i", "y", "=", "e", "ø")]
        [InlineData("i", "y", "=", "ɛ", "œ")]

        [InlineData("ɔ", "ɑ", "=", "ɔ̃", "ɑ̃")]

        [InlineData("e", "ə", ">", "ɛ", "ə")]
        [InlineData("o", "ə", ">", "ɔ", "ə")]
        [InlineData("e", "ə", "=", "o", "ə")]
        //[InlineData("e", "ə", "=", "e", "ɛ")]
        //[InlineData("o", "ə", "=", "o", "ɔ")]

        [InlineData("i", "y", "<", "i", "u")]
        [InlineData("e", "ø", "<", "e", "o")]
        [InlineData("ɛ", "œ", "<", "ɛ", "ɔ")]

        [InlineData("e", "ɛ", "<", "e", "i")]
        [InlineData("e", "ɛ", "<", "ɛ", "a")]
        [InlineData("ø", "œ", "<", "ø", "y")]
        [InlineData("ø", "œ", "<", "œ", "a")]
        [InlineData("o", "ɔ", "<", "o", "u")]
        [InlineData("o", "ɔ", "=", "ɔ", "ɑ")]

        [InlineData("i", "e", "<", "i", "ɛ")]
        [InlineData("i", "ɛ", "<", "i", "a")]
        [InlineData("y", "ø", "<", "y", "œ")]
        [InlineData("y", "œ", "<", "y", "a")]
        [InlineData("u", "o", "<", "u", "ɔ")]
        [InlineData("u", "ɔ", "<", "u", "ɑ")]

        [InlineData("ɔ", "ɑ", "<", "ɛ", "a")]

        //[InlineData("ɛ", "œ", "=", "ɛ̃", "œ̃")]
        [InlineData("ɛ", "ɔ", "=", "ɛ̃", "ɔ̃")]
        [InlineData("ɛ", "ɑ", "=", "ɛ̃", "ɑ̃")]
        [InlineData("œ", "ɔ", "=", "œ̃", "ɔ̃")]
        [InlineData("œ", "ɑ", "=", "œ̃", "ɑ̃")]
        [InlineData("ɑ", "ɔ", "=", "ɑ̃", "ɔ̃")]
        public void TestParallelPairs(string s1, string s2, string op, string s3, string s4)
        {
            var p1 = Phonemes.BySymbol(s1);
            var p2 = Phonemes.BySymbol(s2);
            var p3 = Phonemes.BySymbol(s3);
            var p4 = Phonemes.BySymbol(s4);

            var d1 = _distance.GetDistance(p1, p2);
            var d2 = _distance.GetDistance(p3, p4);

            if (op == "=")
                Assert.True(d1 == d2, $"d(/{s1}/, /{s2}/) = {d1} ≠ {d2} = d(/{s3}/, /{s4}/)");
            else if (op == "<")
                Assert.True(d1 < d2, $"d(/{s1}/, /{s2}/) = {d1} ≮ {d2} = d(/{s3}/, /{s4}/)");
            else if (op == ">")
                Assert.True(d1 > d2, $"d(/{s1}/, /{s2}/) = {d1} ≯ {d2} = d(/{s3}/, /{s4}/)");
            else
                throw new NotImplementedException();
        }
    }
}
