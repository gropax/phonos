using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.French.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Phonos.French.SubSystems.Tests
{
    public class Chapter6Tests : RuleSystemTests
    {
        public Chapter6Tests()
        {
            Analyzers = new IAnalyzer[]
            {
                new MemoizeAnalyzer("classical_latin"),
                new Latin.SyllableAnalyzer(),
                new Latin.AccentAnalyzer(),
            };
        }

        //[Theory]
        //public void TestRuleSystem(params string[] data)
        //{
        //    TestRules(Chapter6.Rules(), data);
        //}

        [Theory]
        [InlineData("ambulat", /*phono:*/ "amblat", /*graphs:*/ "amblat")]
        [InlineData("auricula", /*phono:*/ "au̯rikla", /*graphs:*/ "auricla")]  // @faked
        [InlineData("oculum", /*phono:*/ "oklum", /*graphs:*/ "oclum")]
        [InlineData("tabula", /*phono:*/ "tabla", /*graphs:*/ "tabla")]
        [InlineData("genita")]  // no match
        [InlineData("quaesita")]  // no match
        public void TestRule1(params string[] data)
        {
            TestRule(Chapter6.Rule1(), data);
        }

        [Theory]
        [InlineData("calidum", /*phono:*/ "kaldum", /*graphs:*/ "caldum")]
        [InlineData("viridem", /*phono:*/ "wirdem", /*graphs:*/ "virdem")]
        [InlineData("genita", /*phono:*/ "genta", /*graphs:*/ "genta")]
        [InlineData("quaesita", /*phono:*/ "kʷai̯sta", /*graphs:*/ "quaesta")]
        [InlineData("oculum")]  // no match
        [InlineData("tabula")]  // no match
        public void TestRule2(params string[] data)
        {
            TestRule(Chapter6.Rule2(), data);
        }

        [Theory]
        [InlineData("debita", /*phono:*/ "debta", /*graphs:*/ "debta")]
        [InlineData("movita", /*phono:*/ "mowta", /*graphs:*/ "movta")]
        [InlineData("hominem")]  // no match
        public void TestRule3(params string[] data)
        {
            TestRule(Chapter6.Rule3(), data);
        }

        [Theory]
        [InlineData("gaudia", /*phono:*/ "gau̯dja", /*graphs:*/ "gaudia")]
        [InlineData("nausea", /*phono:*/ "nau̯sja", /*graphs:*/ "nausea")]
        [InlineData("vidua", /*phono:*/ "widwa", /*graphs:*/ "vidua")]
        public void TestRule4(params string[] data)
        {
            TestRule(Chapter6.Rule4(), data);
        }

        [Theory]
        [InlineData("hominem", /*phono:*/ "homnem", /*graphs:*/ "homnem")]
        [InlineData("feretrum", /*phono:*/ "fertrum", /*graphs:*/ "fertrum")]
        [InlineData("comitem", /*phono:*/ "komtem", /*graphs:*/ "comtem")]
        [InlineData("cubitum", /*phono:*/ "kubtum", /*graphs:*/ "cubtum")]
        public void TestRule5(params string[] data)
        {
            TestRule(Chapter6.Rule5(), data);
        }

        [Theory]
        [InlineData("sacramentum", /*phono:*/ "sakrəmentum", /*graphs:*/ "sacramentum")]
        [InlineData("armatūra", /*phono:*/ "armətuːra", /*graphs:*/ "armatūra")]
        [InlineData("firmamente", /*phono:*/ "firməmente", /*graphs:*/ "firmamente")]
        public void TestRule6(params string[] data)
        {
            TestRule(Chapter6.Rule6(), data);
        }
    }
}
