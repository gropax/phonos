using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.French.Tests
{
    public class IntegrationTests : RuleSystemTests
    {
        public IntegrationTests()
        {
            Analyzers = new IAnalyzer[]
            {
                new MemoizeAnalyzer("classical_latin"),
                new Latin.SyllableAnalyzer(),
                new Latin.AccentAnalyzer(),
            };
        }

        //[Theory]
        //[InlineData("ferum", /*phono:*/ "fɛrom", /*graphs:*/ "ferum")]
        //[InlineData("sērum", /*phono:*/ "serom", /*graphs:*/ "sērum")]
        //[InlineData("soror", /*phono:*/ "sɔrɔr", /*graphs:*/ "soror")]
        //[InlineData("sōlum", /*phono:*/ "solom", /*graphs:*/ "sōlum")]
        //[InlineData("subinde", /*phono:*/ "sobendɛ", /*graphs:*/ "subinde")]
        //[InlineData("poena", /*phono:*/ "pena", /*graphs:*/ "poena")]
        //[InlineData("praeda", /*phono:*/ "prɛda", /*graphs:*/ "praeda")]
        //[InlineData("saeta", /*phono:*/ "sɛta", /*graphs:*/ "saeta")]
        //[InlineData("pauper", /*phono:*/ "pɔpɛr", /*graphs:*/ "pauper")]
        //[InlineData("causa", /*phono:*/ "kɔsa", /*graphs:*/ "causa")]
        //[InlineData("mālum", /*phono:*/ "malom", /*graphs:*/ "mālum")]
        //[InlineData(/*fake*/ "mūlum", /*phono:*/ "mulom", /*graphs:*/ "mūlum")]
        //[InlineData(/*fake*/ "mīlum", /*phono:*/ "milom", /*graphs:*/ "mīlum")]
        //public void TestRuleSystem1(params string[] data)
        //{
        //    TestRules(French.Rules(), data);
        //}
    }
}
