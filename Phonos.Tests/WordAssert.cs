using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phonos.Tests
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
    }
}
