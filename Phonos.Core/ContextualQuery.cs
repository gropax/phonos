using Intervals;
using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core
{
    public class ContextualQuery
    {
        public IQuery Query { get; }
        public IQuery LookBehind { get; }
        public IQuery LookAhead { get; }
        //public IQuery NegativeLookBehind { get; }
        public IQuery NegativeLookAhead { get; }
        public string Scope { get; }
        public IQuery NextScopeQuery { get; }
        public bool LastScope { get; }

        public ContextualQuery(IQuery query,
            IQuery lookBehind = null, IQuery lookAhead = null,
            //IQuery negLookBehind = null,
            IQuery negLookAhead = null,
            string scope = null, IQuery nextScopeQuery = null, bool lastScope = false)
        {
            Query = query;
            LookBehind = lookBehind ?? new NullQuery();
            LookAhead = lookAhead ?? new NullQuery();
            //NegativeLookBehind = negLookBehind ?? new FalseQuery();
            NegativeLookAhead = negLookAhead ?? new FalseQuery();
            Scope = scope;
            NextScopeQuery = nextScopeQuery;
            LastScope = lastScope;
        }

        public Interval<string[]>[] Match(Word word)
        {
            var scopes = Scope != null
                ? word.GetField(Scope).Intervals.ToArray()
                : new[] { new Interval(0, word.Phonemes.Length) };

            var matches = new List<Interval<string[]>>();
            for (int i = 0; i < scopes.Length; i++)
                matches.AddRange(MatchScope(word, scopes, i));

            return matches.ToArray();
        }

        public IEnumerable<Interval<string[]>> MatchScope(Word word, Interval[] scopes, int scopeIdx)
        {
            var scope = scopes[scopeIdx];
            int index = scope.Start;

            if (LastScope && scopeIdx < scopes.Length - 1)
                yield break;

            if (NextScopeQuery != null)
            {
                if (scopeIdx == scopes.Length - 1)
                    yield break;

                var nextScope = scopes[scopeIdx + 1];

                var next = NextScopeQuery.Match(word, nextScope.Start, nextScope);
                if (next == null)
                    yield break;
            }

            while (index < scope.End)
            {
                var behind = LookBehind.Match(word, index, scope);
                if (behind == null)
                {
                    index++;
                    continue;
                }

                var match = Query.Match(word, behind.End, scope);
                if (match == null)
                {
                    index++;
                    continue;
                }

                var ahead = LookAhead.Match(word, match.End, scope);
                if (ahead == null)
                {
                    index++;
                    continue;
                }

                var negAhead = NegativeLookAhead.Match(word, match.End, scope);
                if (negAhead != null)
                {
                    index++;
                    continue;
                }

                yield return match;

                index = match.End;

                // Prevent infinite loop when empty match
                if (match.Length == 0)
                    index++;
            }
        }
    }
}
