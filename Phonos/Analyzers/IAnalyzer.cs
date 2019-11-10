using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Analyzers
{
    interface IAnalyzer
    {
        void Analyze(Word word);
    }
}
