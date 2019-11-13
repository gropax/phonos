using Intervals;
using Phonos.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Latin.Tests
{
    public class SyllableAnalyzerTests
    {
        [Theory]
        [InlineData("maximus", "long:3 short:2 long:3")]  // ˈmak.si.mus
        // Consonant clusters
        [InlineData("dextra", "long:4 short:3")]  // ˈdeks.tra
        [InlineData("exclāmans", "long:3 long:3 long:4")]  // eks.ˈklaː.mans
        [InlineData("feretrum", "short:2 short:2 long:4")]  // ˈmak.si.mus
        // diphtongues
        [InlineData("aenigma", "long:1 long:3 short:2")]  // ai̯.ˈnig.ma
        // Close vowels
        [InlineData("cloāca", "short:3 long:1 short:2")]  // klo.ˈaː.ka
        // Semi-vowels
        [InlineData("judex", "short:2 long:4")]  // ˈju.deks
        [InlineData("ejectio", "long:2 long:3 short:2 short:1")]  // ej.ˈjek.ti.o
        //[InlineData("proavunculus", "ˌpro.a.ˈwuŋ.ku.lus")]
        // Secondary stress
        [InlineData("architectūra", "long:2 short:2 long:3 long:2 short:2")]  // ˌar.kʰi.tek.ˈtuː.ra
        [InlineData("exclamatūrūs", "long:3 short:3 short:2 long:2 long:3")]  // ˌeks.kla.ma.ˈtuː.ruːs
        // word beginnig with qu and gu
        [InlineData("gubernātōrem", "short:2 long:3 long:2 long:2 long:3")]  // gu.ˌber.naː.ˈtoː.rem
        public void GetSyllableTheory(string latin, string syllable)
        {
            var parser = new WordParser();
            var word = parser.Parse(latin);
            new SyllableAnalyzer().Analyze(word);
            new AccentAnalyzer().Analyze(word);

            WordAssert.Field(word, "syllable", syllable);
        }
    }
}
