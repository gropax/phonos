using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Phonos.Core.Tests.TestData
{
    public class RuleTestData : TheoryData<RuleContextTest>
    {
        public RuleTestData(IEnumerable<RuleContextTest> ruleTests)
        {
            foreach (var ruleTest in ruleTests)
                Add(ruleTest);
        }
    }

    public class BlackBoxTestData : TheoryData<BlackBoxTest>
    {
        public BlackBoxTestData(IEnumerable<BlackBoxTest> ruleTests)
        {
            foreach (var ruleTest in ruleTests)
                Add(ruleTest);
        }
    }

    public class WhiteBoxTestData : TheoryData<WhiteBoxTest>
    {
        public WhiteBoxTestData(IEnumerable<WhiteBoxTest> ruleTests)
        {
            foreach (var ruleTest in ruleTests)
                Add(ruleTest);
        }
    }
}
