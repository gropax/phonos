using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Latin.Tests
{
    public class WordParserTests
    {
        [Theory]
        // x -> ks
        [InlineData("maximus", "m a k s i m u s", "m a x:2 i m u s")]
        [InlineData("dextra", "d e k s t r a", "d e x:2 t r a")]
        // greek aspirated consonents
        [InlineData("philosophus", "pʰ i l o s o pʰ u s", "ph i l o s o ph u s")]
        [InlineData("architectūra", "a r kʰ i t e k t uː r a", "a r ch i t e c t ū r a")]
        [InlineData("pyrrhus", "p y r r u s", "p y r rh u s")]
        // diphtongues
        [InlineData("aenigma", "ai̯ n i g m a", "ae n i g m a")]
        // semi-vowels
        [InlineData("judex", "j u d e k s", "j u d e x:2")]
        [InlineData("ejectio", "e j j e k t i o", "e j:2 e c t i o")]
        [InlineData("vacatio", "w a k a t i o", "v a c a t i o")]
        [InlineData("avia", "a w i a", "a v i a")]
        public void AnalyzeTheory(string graphical, string phonemic, string graphemic)
        {
            var analyzer = new WordParser();

            var latinWord = analyzer.Parse(graphical);

            var phonemes = ParsePhonemic(phonemic);
            var graphemes = Core.Alignment.ParseIntervals(graphemic);

            Assert.Equal(phonemes, latinWord.Phonemes);
            Assert.Equal(graphemes, latinWord.GraphicalForms[0].Intervals);
        }

        private string[] ParsePhonemic(string phonemic)
        {
            return phonemic.Split(" ");
        }
    }
}
