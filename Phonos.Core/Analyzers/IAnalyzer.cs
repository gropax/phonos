using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Analyzers
{
    public interface IAnalyzer
    {
        void Analyze(Word word);
    }
}
