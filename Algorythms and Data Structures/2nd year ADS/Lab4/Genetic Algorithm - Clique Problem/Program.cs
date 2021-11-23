using System;
using Lab4_1.Genetic;

namespace Lab4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var clique = new Clique(300);

            // clique.CreateGraph();

            var genetic = new GeneticAlgorithm(clique, 3, 0.1);

            genetic.Progress(1000);

            System.Console.WriteLine("Iteration - " + genetic.BestIteration);
            System.Console.WriteLine("Fitness - " + genetic.Best.Fitness);
            foreach (var gene in genetic.Best.Chromosome)
            {
                System.Console.Write(gene);
            }
        }
    }
}
