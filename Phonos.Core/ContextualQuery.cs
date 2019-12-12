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
        public string Scope { get; }

        public ContextualQuery(IQuery query, IQuery lookBehind = null,
            IQuery lookAhead = null, string scope = null)
        {
            Query = query;
            LookBehind = lookBehind ?? new NullQuery();
            LookAhead = lookAhead ?? new NullQuery();
            Scope = scope;
        }

        public Interval<string[]>[] Match(Word word)
        {
            var scopes = Scope != null
                ? word.GetField(Scope).Intervals.ToArray()
                : new[] { new Interval(0, word.Phonemes.Length) };

            var matches = scopes.SelectMany(s => MatchScope(word, s)).ToArray();
            return matches;
        }

        public SortedIntervals<string[]> MatchScope(Word word)
        {
            var matches = new List<Interval<string[]>>();

            foreach (var interval in word.GetField(Scope).Intervals)
            {
                int index = interval.Start;

                while (index < interval.End)
                {
                    var behind = LookBehind.Match(word, index, interval);
                    if (behind == null)
                    {
                        index++;
                        continue;
                    }

                    var match = Query.Match(word, behind.End, interval);
                    if (match == null)
                    {
                        index++;
                        continue;
                    }

                    var ahead = LookAhead.Match(word, match.End, interval);
                    if (ahead == null)
                        index++;
                    else
                    {
                        matches.Add(match);
                        index = match.End;
                    }
                }
            }

            return matches.AssumeSorted();
        }

        public IEnumerable<Interval<string[]>> MatchScope(Word word, Interval scope)
        {
            int index = scope.Start;

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
                    index++;
                else
                {
                    yield return match;
                    index = match.End;
                }
            }
        }
    }
}
