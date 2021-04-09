using System;
using Xunit;

namespace Phonos.Fra.Lexicon.Tests
{
    public class Lexique380ParserTests
    {
        [Fact]
        public void TestParseLine()
        {
            string line = "grumeleuses\tgRym°l2z\tgrumeleux\tADJ\tf\tp\t0.33\t1.89\t0.02\t0.14\t\t1\t2\t0\t11\t8\tCCVCVCVVCVC\tCCVCVCVC\t0\t0\t0\t0\tgRy-m°-l2z\t3\tCCV-CV-CVC\tsesuelemurg\tz2l°myRg\tgru-me-leu-ses\tADJ\t63\t19\t3.4\t\tgrumeleux\t1\n";

            var expected = new LexiqueEntry("grumeleuses", "grumeleux", "ADJ",
                phonemes: new[]
                {
                    new ContextualPhoneme(Phonemes.g),
                    new ContextualPhoneme(Phonemes.R),
                    new ContextualPhoneme(Phonemes.y),
                    new ContextualPhoneme(Phonemes.m),
                    new ContextualPhoneme(Phonemes._e, isElidable: true),
                    new ContextualPhoneme(Phonemes.l),
                    new ContextualPhoneme(Phonemes.eu),
                    new ContextualPhoneme(Phonemes.z),
                });

            var entry = LexiqueParser.ParseEntry(line);

            Assert.Equal(expected, entry);
        }
    }
}
