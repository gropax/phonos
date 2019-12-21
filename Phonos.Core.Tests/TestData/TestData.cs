using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Phonos.Core.Tests.TestData
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class WhiteBoxTest
    {
        public string Latin { get; }
        public WhiteBoxStep[] Steps { get; }
        public string DebuggerDisplay => Latin;

        public WhiteBoxTest(string latin, WhiteBoxStep[] steps)
        {
            Latin = latin;
            Steps = steps;
        }
    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    public class WhiteBoxStep
    {
        public string Phonemes { get; }
        public string[] GraphicalForms { get; }
        public string DebuggerDisplay => $"{Phonemes} ({string.Join(", ", GraphicalForms)})";

        public WhiteBoxStep(string phonemes, string[] graphicalForms)
        {
            Phonemes = phonemes;
            GraphicalForms = graphicalForms;
        }
    }

    public class BlackBoxTest
    {
        public string Latin { get; }
        public SampleOutput[] Outputs { get; }
        public BlackBoxTest(string latin, SampleOutput[] outputs)
        {
            Latin = latin;
            Outputs = outputs;
        }
    }

    public class RuleContextTest
    {
        public string Id { get; }
        public string Source { get; }
        public DateInfo From { get; }
        public DateInfo To { get; }
        //public string[] PostProcessing { get; }
        public string[] PostProcessing { get; }
        public string[] Rules { get; }
        public RuleTestSample[] Samples { get; }

        public RuleContextTest(string id, string source, string[] rules, DateInfo from, DateInfo to, RuleTestSample[] samples)
        {
            Id = id;
            Source = source;
            Rules = rules;
            From = from;
            To = to;
            Samples = samples;
        }
    } 

    public class RuleTestSample
    {
        public Word Input { get; }
        public SampleOutput[] Outputs { get; }

        public RuleTestSample(Word input, SampleOutput[] outputs)
        {
            Input = input;
            Outputs = outputs;
        }
    }

    public class SampleOutput
    {
        public Word Word { get; }
        public SampleOutput(Word word)
        {
            Word = word;
        }
    }

    public struct DateInfo
    {
        public int Date { get; }
        public bool Certain { get; }
        public DateInfo(int date, bool certain)
        {
            Date = date;
            Certain = certain;
        }
    }
}
