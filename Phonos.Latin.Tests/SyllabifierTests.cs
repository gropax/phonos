using System;
using System.Linq;
using Xunit;

namespace Phonos.Latin.Tests
{
    public class SyllabifierTests
    {
        //[Theory]
        //[InlineData("maximus", "ˈmak.si.mus")]
        //// Consonant clusters
        //[InlineData("dextra", "ˈdeks.tra")]
        //[InlineData("exclāmans", "eks.ˈklaː.mans")]
        //// diphtongues
        //[InlineData("aenigma", "ai̯.ˈnig.ma")]
        //// Close vowels
        //[InlineData("cloāca", "klo.ˈaː.ka")]
        //// Semi-vowels
        //[InlineData("judex", "ˈju.deks")]
        //[InlineData("ejectio", "ej.ˈjek.ti.o")]
        ////[InlineData("proavunculus", "ˌpro.a.ˈwuŋ.ku.lus")]
        //// Secondary stress
        //[InlineData("architectūra", "ˌar.kʰi.tek.ˈtuː.ra")]
        //[InlineData("exclamatūrūs", "ˌeks.kla.ma.ˈtuː.ruːs")]
        //// word beginnig with qu and gu
        //[InlineData("gubernātōrem", "gu.ˌber.naː.ˈtoː.rem")]
        //public void GetSyllableTheory(string graphical, string phonetic)
        //{
        //    var analyzer = new GraphemeToPhonemeAnalyzer();
        //    var syllabifier = new Syllabifier();
        //    var formatter = new PhonologicalFormatter();

        //    var alignment = analyzer.Analyze(graphical);
        //    var phonemes = alignment.Right;
        //    var syllables = syllabifier.GetSyllables(phonemes);
        //    var formatted = formatter.Format(phonemes, syllables);

        //    Assert.Equal(phonetic, formatted);
        //}
    }
}
