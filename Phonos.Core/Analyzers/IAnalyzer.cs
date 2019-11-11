using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.Analyzers
{
    interface IAnalyzer
    {
        void Analyze(Word word);
    }
}
