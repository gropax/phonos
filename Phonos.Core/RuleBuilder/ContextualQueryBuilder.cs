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
        //private IQuery _negLookBehind;
        private IQuery _negLookAhead;
        private IQuery _next;
        private bool _last = false;

        public ContextualQuery Build()
        {
            if (_query == null)
                throw new QueryBuilderException("Match query must be set before building.");
            else if (_last && _scope == null)
                throw new QueryBuilderException("Scope must be set if using Last.");
            else if (_next != null && _scope == null)
                throw new QueryBuilderException("Scope must be set if using Next.");

            return new ContextualQuery(_query, _lookBehind, _lookAhead, _negLookAhead, _scope, _next, _last);
        }

        public ContextualQueryBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        public ContextualQueryBuilder Last()
        {
            _last = true;
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

        //public ContextualQueryBuilder BeforeNot(Action<ContextQueryBuilder> queryDefinition)
        //{
        //    var queryBuilder = new ContextQueryBuilder();
        //    queryDefinition(queryBuilder);
        //    _negLookBehind = queryBuilder.Build();
        //    return this;
        //}

        public ContextualQueryBuilder AfterNot(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _negLookAhead = queryBuilder.Build();
            return this;
        }

        public ContextualQueryBuilder Next(Action<ContextQueryBuilder> queryDefinition)
        {
            var queryBuilder = new ContextQueryBuilder();
            queryDefinition(queryBuilder);
            _next = queryBuilder.Build();
            return this;
        }
    }
}
