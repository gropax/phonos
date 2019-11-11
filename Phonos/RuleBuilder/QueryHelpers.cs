using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.RuleBuilder
{
    public static class Q
    {
        public static Action<ContextQueryBuilder> Start => qb => qb.Start();
        public static Action<ContextQueryBuilder> End => qb => qb.End();
    }
}
