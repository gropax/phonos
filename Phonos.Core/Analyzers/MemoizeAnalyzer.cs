using Intervals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.Analyzers
{
    public class MemoizeAnalyzer : IAnalyzer
    {
        public string Field { get; }

        public MemoizeAnalyzer(string field)
        {
            Field = field;
        }

        public void Analyze(Word word)
        {
            var intervals = word.Phonemes.Select((p, i) =>
                new Interval<string>(i, 1, p)).ToArray();
            word.SetField(Field, new Alignment<string>(intervals));
        }
    }
}
