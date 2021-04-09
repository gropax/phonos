using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Linq;
using Xunit;

namespace Phonos.Fra.Similarity.Tests
{
    public class ExperimentalTests
    {
        [Fact]
        public void TestParseLine()
        {
            string lexiquePath = @"C:\Users\VR1\source\repos\phonos\data\Lexique383.tsv";
            var nouns = LexiqueParser.ParseLexique(lexiquePath)
                .Where(e => e.POS == "NOM")
                .ToArray();
        }
    }
}
