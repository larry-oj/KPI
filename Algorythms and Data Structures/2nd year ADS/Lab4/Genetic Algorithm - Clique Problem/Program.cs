using System;
using Lab4_1.Genetic;

namespace Lab4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var k = 4;

            var clique = new Clique(8);

            // clique.CreateGraph();

            var genetic = new GeneticAlgorithm(clique, k, 0.1);

            genetic.Progress(1000);


            if (genetic.Best.CountVertices() == k && genetic.Best.Fitness == k)
            {
                System.Console.WriteLine("Iteration - " + genetic.BestIteration);
                System.Console.WriteLine("Fitness - " + genetic.Best.Fitness);
                foreach (var gene in genetic.Best.Chromosome)
                {
                    System.Console.Write(gene);
                }
            }
            else
            {
                System.Console.WriteLine("Not found");
            }
        }
    }
}
