using Intervals;
using Phonos.Core.Tests;
using Phonos.Core.Tests.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Phonos.Core.Tests.TestData
{
    public class YamlParserTests
    {
        [Fact]
        public void TestMatches()
        {
            var parser = new YamlParser();
            var path = @".\TestData\rules.yaml";

            using (StreamReader reader = File.OpenText(path))
            {
                var result = parser.Parse(reader).ToList();
            }
        }
    }
}
