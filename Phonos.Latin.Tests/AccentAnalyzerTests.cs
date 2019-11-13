using Intervals;
using Phonos.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.Latin.Tests
{
    public class AccentAnalyzerTests
    {
        [Theory]
        [InlineData("maximus", "tonic:3 post-tonic:2 final:3")]  // ˈmak.si.mus
        // Monosyllabic
        [InlineData("lex", "tonic:4")]  // 'leks
        // Consonant clusters
        [InlineData("dextra", "tonic:4 final:3")]  // ˈdeks.tra
        [InlineData("exclāmans", "initial:3 tonic:3 final:4")]  // eks.ˈklaː.mans
        [InlineData("feretrum", "tonic:2 post-tonic:2 final:4")]  // ˈmak.si.mus
        // diphtongues
        [InlineData("aenigma", "initial:1 tonic:3 final:2")]  // ai̯.ˈnig.ma
        // Close vowels
        [InlineData("cloāca", "initial:3 tonic:1 final:2")]  // klo.ˈaː.ka
        // Semi-vowels
        [InlineData("judex", "tonic:2 final:4")]  // ˈju.deks
        [InlineData("ejectio", "initial:2 tonic:3 post-tonic:2 final:1")]  // ej.ˈjek.ti.o
        //[InlineData("proavunculus", "ˌpro.a.ˈwuŋ.ku.lus")]
        // Secondary stress
        [InlineData("architectūra", "tonic:2 pre-tonic:2 pre-tonic:3 tonic:2 final:2")]  // ˌar.kʰi.tek.ˈtuː.ra
        [InlineData("exclamatūrūs", "tonic:3 pre-tonic:3 pre-tonic:2 tonic:2 final:3")]  // ˌeks.kla.ma.ˈtuː.ruːs
        // word beginnig with qu and gu
        [InlineData("gubernātōrem", "initial:2 tonic:3 pre-tonic:2 tonic:2 final:3")]  // gu.ˌber.naː.ˈtoː.rem
        public void GetAccentTheory(string latin, string accent)
        {
            var parser = new WordParser();
            var word = parser.Parse(latin);
            new SyllableAnalyzer().Analyze(word);
            new AccentAnalyzer().Analyze(word);

            WordAssert.Field(word, "accent", accent);
        }
    }
}
