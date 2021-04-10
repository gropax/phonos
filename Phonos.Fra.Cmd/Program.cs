using Phonos.Fra.Similarity;
using Phonos.Fra.Similarity.Distances;
using Phonos.Fra.Similarity.Lexicon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phonos.Fra.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string lexiquePath = @"C:\Users\VR1\source\repos\phonos\data\Lexique383.tsv";
            var neighborhoodBuilder = NeighborhoodBuilder.Init(lexiquePath);

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\nEnter a word:");
                var wordForm = Console.ReadLine();

                var scoredNeighbors = neighborhoodBuilder.GetNeighbors(wordForm, take: 20);

                Console.WriteLine($"Similarity\tDistance\tWord");
                foreach (var n in scoredNeighbors)
                    Console.WriteLine($"{n.Item1}\t{n.Item2}\t{n.Item3}");
            }
        }
    }
}
