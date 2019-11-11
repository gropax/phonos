using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos
{
    public interface IRuleSequencer
    {
        WordDerivation Apply(Word word);
    }
}
