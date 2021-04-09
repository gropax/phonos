﻿using System;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Lexicon.Tests
{
    public class SyllabifierTests
    {
        [Fact]
        public void TestCompute()
        {
            var syllabifier = new Syllabifier();

            var phonemes = "stʁil.bjuʁ.goz.dʁɥi.fwastχ";
            var input = string.Join("", phonemes.Split('.')).ToCharArray().Select(c => Phonemes.BySymbol(c.ToString())).ToArray();

            var expected = new[]
            {
                new Syllable(
                    onset: new [] { Phonemes.s, Phonemes.t, Phonemes.R, },
                    nucleus: Phonemes.i,
                    coda: new [] { Phonemes.l, }),
                new Syllable(
                    onset: new [] { Phonemes.b, Phonemes.j, },
                    nucleus: Phonemes.u,
                    coda: new [] { Phonemes.R, }),
                new Syllable(
                    onset: new [] { Phonemes.g, },
                    nucleus: Phonemes.o,
                    coda: new [] { Phonemes.z, }),
                new Syllable(
                    onset: new [] { Phonemes.d, Phonemes.R, Phonemes.Y, },
                    nucleus: Phonemes.i,
                    coda: new Phoneme[0]),
                new Syllable(
                    onset: new [] { Phonemes.f, Phonemes.w, },
                    nucleus: Phonemes.a,
                    coda: new [] { Phonemes.s, Phonemes.t, Phonemes.X, }),
            };

            var result = syllabifier.Compute(input).ToArray();

            Assert.Equal(expected.Length, result.Length);
        }
    }
}
