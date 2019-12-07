using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Phonos.Core.Tests.TestData
{
    public class RuleTestData : TheoryData<RuleTest>
    {
        public RuleTestData(IEnumerable<RuleTest> ruleTests)
        {
            foreach (var ruleTest in ruleTests)
                Add(ruleTest);
        }
    }
}
