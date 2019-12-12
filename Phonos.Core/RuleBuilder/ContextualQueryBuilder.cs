using Phonos.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonos.Core.RuleBuilder
{
    public class ContextualQueryBuilder
    {
        private string _scope;
        private IQuery _query;
        private IQuery _lookBehind;
        private IQuery _lookAhead;

        public ContextualQuery Build()
        {
            if (_query == null)
                throw new QueryBuilderException("Match query must be set before building.");

            return new ContextualQuery(_query, _lookBehind, _lookAhead, _scope);
        }

        public ContextualQueryBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        public ContextualQueryBuilder Match(Action<MatchQueryBuilder> queryDefinition)
        {
            var queryBuilder = new MatchQueryBuilder();
            queryDefinition(queryBuilder);
            _query = queryBuilder.Build();
            return this;
        }

        public ContextualQueryBuilder Before(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _lookBehind = queryBuilder.Build();
            return this;
        }

        public ContextualQueryBuilder After(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _lookAhead = queryBuilder.Build();
            return this;
        }
    }
}
