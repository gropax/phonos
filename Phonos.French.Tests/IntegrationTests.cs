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
        [InlineData("ambulat", /*phono:*/ "amblat", /*graphs:*/ "amblat")]
        [InlineData("armatūra", /*phono:*/ "armətura", /*graphs:*/ "armatūra")]
        [InlineData("auricula", /*phono:*/ "ɔrekla", /*graphs:*/ "auricla")]  // @faked
        [InlineData("calidum", /*phono:*/ "kaldom", /*graphs:*/ "caldum")]
        [InlineData("causa", /*phono:*/ "kɔsa", /*graphs:*/ "causa")]
        [InlineData("comitem", /*phono:*/ "kɔmtɛm", /*graphs:*/ "comtem")]
        [InlineData("cubitum", /*phono:*/ "kobtom", /*graphs:*/ "cubtum")]
        [InlineData("debita", /*phono:*/ "dɛbta", /*graphs:*/ "debta")]
        [InlineData("feretrum", /*phono:*/ "fɛrtrom", /*graphs:*/ "fertrum")]
        [InlineData("ferum", /*phono:*/ "fɛrom", /*graphs:*/ "ferum")]
        [InlineData("firmamente", /*phono:*/ "ferməmɛntɛ", /*graphs:*/ "firmamente")]
        [InlineData("gaudia", /*phono:*/ "gɔdja", /*graphs:*/ "gaudia")]
        [InlineData("genita", /*phono:*/ "gɛnta", /*graphs:*/ "genta")]
        [InlineData("hominem", /*phono:*/ "hɔmnɛm", /*graphs:*/ "homnem")]
        [InlineData("mālum", /*phono:*/ "malom", /*graphs:*/ "mālum")]
        [InlineData("movita", /*phono:*/ "mɔwta", /*graphs:*/ "movta")]
        [InlineData("nausea", /*phono:*/ "nɔsja", /*graphs:*/ "nausea")]
        [InlineData("oculum", /*phono:*/ "ɔklom", /*graphs:*/ "oclum")]
        [InlineData("pauper", /*phono:*/ "pɔpɛr", /*graphs:*/ "pauper")]
        [InlineData("poena", /*phono:*/ "pena", /*graphs:*/ "poena")]
        [InlineData("praeda", /*phono:*/ "prɛda", /*graphs:*/ "praeda")]
        [InlineData("quaesita", /*phono:*/ "kʷɛsta", /*graphs:*/ "quaesta")]
        [InlineData("sacramentum", /*phono:*/ "sakrəmɛntom", /*graphs:*/ "sacramentum")]
        [InlineData("saeta", /*phono:*/ "sɛta", /*graphs:*/ "saeta")]
        [InlineData("sērum", /*phono:*/ "serom", /*graphs:*/ "sērum")]
        [InlineData("sōlum", /*phono:*/ "solom", /*graphs:*/ "sōlum")]
        [InlineData("soror", /*phono:*/ "sɔrɔr", /*graphs:*/ "soror")]
        [InlineData("subinde", /*phono:*/ "sobendɛ", /*graphs:*/ "subinde")]
        [InlineData("tabula", /*phono:*/ "tabla", /*graphs:*/ "tabla")]
        [InlineData("vidua", /*phono:*/ "wedwa", /*graphs:*/ "vidua")]
        [InlineData("viridem", /*phono:*/ "werdɛm", /*graphs:*/ "virdem")]
        public void TestRuleSystem1(params string[] data)
        {
            TestRules(French.Rules(), data);
        }
    }
}
