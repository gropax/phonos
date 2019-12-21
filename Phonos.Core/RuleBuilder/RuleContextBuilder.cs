using Intervals;
using Phonos.Core.Queries;
using Phonos.Core.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phonos.Core.RuleBuilder
{

    public class RuleContextBuilder
    {
        private string _id = null;
        private string _group = "Unnamed";
        private int _start = 0;
        private int _end = 0;
        private ContextualQuery[] _queries;
        private Operation[] _rules;

        public Rule Build()
        {
            //if (_id == null)
            //    throw new QueryBuilderException("Rule must have an ID.");
            //else
            if (_queries.Length == 0)
                throw new QueryBuilderException("RuleContext must have at least one ContextualQuery.");
            else if (_rules == null)
                throw new QueryBuilderException("RuleContext must have at least one Rule.");

            return new Rule(_id, _group, new Interval(_start, _end - _start),
                _queries, _rules);
        }

        public RuleContextBuilder Id(string id)
        {
            _id = id;
            return this;
        }

        public RuleContextBuilder Group(string group)
        {
            _group = group;
            return this;
        }

        public RuleContextBuilder From(int start)
        {
            _start = start;
            return this;
        }

        public RuleContextBuilder To(int end)
        {
            _end = end;
            return this;
        }

        public RuleContextBuilder Query(params Action<ContextualQueryBuilder>[] queryDefinitions)
        {
            var queries = queryDefinitions.Select(md =>
            {
                var builder = new ContextualQueryBuilder();
                md(builder);
                return builder.Build();
            });

            _queries = queries.ToArray();
            return this;
        }

        public RuleContextBuilder Rules(params Action<RuleBuilder>[] ruleDefinitions)
        {
            var rules = ruleDefinitions.Select(md =>
            {
                var builder = new RuleBuilder();
                md(builder);
                return builder.Build();
            });

            _rules = rules.ToArray();
            return this;
        }
    }





    public class QueryBuilderException : Exception
    {
        public QueryBuilderException(string message) : base(message)
        {
        }
    }
}
