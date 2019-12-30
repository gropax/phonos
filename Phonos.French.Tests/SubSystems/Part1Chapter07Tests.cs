using Intervals;
using Phonos.Core;
using Phonos.Core.Analyzers;
using Phonos.Core.Tests.TestData;
using Phonos.French.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Phonos.French.SubSystems.Tests
{
    public class Part1Chapter07Tests : RuleSystemTests
    {
        public static RuleTestData RuleData
        {
            get
            {
                var parser = new YamlParser();
                var path = @".\Specs\Part1Chapter07.yaml";
                using (StreamReader reader = File.OpenText(path))
                    return new RuleTestData(parser.ParseRuleTests(reader).ToList());
            }
        }

        [Theory]
        [MemberData(nameof(RuleData))]
        public void Test(RuleContextTest ruleTest)
        {
            TestRules(Part1Chapter07.RuleComponents(), ruleTest);
        }
    }
}
