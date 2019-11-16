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

        [Theory]
        [InlineData("ferum", /*phono:*/ "fɛrom", /*graphs:*/ "ferum")]
        [InlineData("sērum", /*phono:*/ "serom", /*graphs:*/ "sērum")]
        [InlineData("soror", /*phono:*/ "sɔrɔr", /*graphs:*/ "soror")]
        [InlineData("sōlum", /*phono:*/ "solom", /*graphs:*/ "sōlum")]
        [InlineData("subinde", /*phono:*/ "sobendɛ", /*graphs:*/ "subinde")]
        [InlineData("poena", /*phono:*/ "pena", /*graphs:*/ "poena")]
        [InlineData("praeda", /*phono:*/ "prɛda", /*graphs:*/ "praeda")]
        [InlineData("saeta", /*phono:*/ "sɛta", /*graphs:*/ "saeta")]
        [InlineData("pauper", /*phono:*/ "pɔpɛr", /*graphs:*/ "pauper")]
        [InlineData("causa", /*phono:*/ "kɔsa", /*graphs:*/ "causa")]
        [InlineData("mālum", /*phono:*/ "malom", /*graphs:*/ "mālum")]

        [InlineData("ambulat", /*phono:*/ "amblat", /*graphs:*/ "amblat")]
        [InlineData("auricula", /*phono:*/ "au̯rikla", /*graphs:*/ "auricla")]  // @faked
        [InlineData("oculum", /*phono:*/ "oklum", /*graphs:*/ "oclum")]
        [InlineData("tabula", /*phono:*/ "tabla", /*graphs:*/ "tabla")]
        [InlineData("calidum", /*phono:*/ "kaldum", /*graphs:*/ "caldum")]
        [InlineData("viridem", /*phono:*/ "wirdem", /*graphs:*/ "virdem")]
        [InlineData("genita", /*phono:*/ "genta", /*graphs:*/ "genta")]
        [InlineData("quaesita", /*phono:*/ "kʷai̯sta", /*graphs:*/ "quaesta")]
        [InlineData("debita", /*phono:*/ "debta", /*graphs:*/ "debta")]
        [InlineData("movita", /*phono:*/ "mowta", /*graphs:*/ "movta")]
        [InlineData("gaudia", /*phono:*/ "gau̯dja", /*graphs:*/ "gaudia")]
        [InlineData("nausea", /*phono:*/ "nau̯sja", /*graphs:*/ "nausea")]
        [InlineData("vidua", /*phono:*/ "widwa", /*graphs:*/ "vidua")]
        [InlineData("hominem", /*phono:*/ "homnem", /*graphs:*/ "homnem")]
        [InlineData("feretrum", /*phono:*/ "fertrum", /*graphs:*/ "fertrum")]
        [InlineData("comitem", /*phono:*/ "komtem", /*graphs:*/ "comtem")]
        [InlineData("cubitum", /*phono:*/ "kubtum", /*graphs:*/ "cubtum")]
        [InlineData("sacramentum", /*phono:*/ "sakrəmentum", /*graphs:*/ "sacramentum")]
        [InlineData("armatūra", /*phono:*/ "armətuːra", /*graphs:*/ "armatūra")]
        [InlineData("firmamente", /*phono:*/ "firməmente", /*graphs:*/ "firmamente")]
        public void TestRuleSystem1(params string[] data)
        {
            TestRules(French.Rules(), data);
        }
    }
}
