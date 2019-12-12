﻿using Intervals;
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
    public class Part1Chapter9Tests : RuleSystemTests
    {
        public static RuleTestData RuleData
        {
            get
            {
                var parser = new YamlParser();
                var path = @".\Specs\Part1Chapter9.yaml";
                using (StreamReader reader = File.OpenText(path))
                    return new RuleTestData(parser.Parse(reader).ToList());
            }
        }

        [Theory]
        [MemberData(nameof(RuleData))]
        public void Test(RuleTest ruleTest)
        {
            TestRules(Part1Chapter9.Rules(), ruleTest);
        }
    }
}