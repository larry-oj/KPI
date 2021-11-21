using System;
using static System.Console;

namespace Genetic_Algorithm___Graph_Coloring_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            var vertices = 12;
            var iterationsCount = 20000;
            var populationSize = 40;

            var matrix = new Matrix(vertices);

            var ga = new Genetic(matrix, populationSize);

            ga.Solve(iterationsCount);

            WriteLine("Colors: " + ga.Best.CountColors());
            for (int i = 0; i < ga.Best.Genes.Count; i++)
            {
                Write(ga.Best.Genes[i]);
                if (i != ga.Best.Genes.Count - 1)
                {
                    Write("-");
                }
            }
            WriteLine();
        }
    }
}
