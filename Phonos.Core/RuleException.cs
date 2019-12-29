using Phonos.Core.Rules;
using System;
using System.Runtime.Serialization;

namespace Phonos.Core
{
    [Serializable]
    internal class RuleException : Exception
    {
        private IRule _rule;

        public RuleException(IRule rule, Exception innerException)
            : base($"An error occured while applying rule [{rule.Id}].", innerException)
        { }
    }
}