using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core
{
    public interface IRuleSequencer
    {
        //WordDerivation Apply(Word word);
        WordDerivation[] Derive(ExecutionContext context, Word word);
    }
}
