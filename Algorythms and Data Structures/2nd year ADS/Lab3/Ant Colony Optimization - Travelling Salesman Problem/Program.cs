using static System.Console;
using Lab3_1.Ants;

namespace Lab3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tsp = new TravellingSalesmanProblem(50);
            // DrawDistances(tsp.Distances);
            // WriteLine("\n");

            // var tmp = new int[,] {
            //     { 0,  14, 22, 17 },
            //     { 14, 0,  3,  21 },
            //     { 22, 3,  0,  9 },
            //     { 17, 21, 9,  0 }
            // };
            // var colony = new AntColony(tmp);

            var colony = new AntColony(tsp.Distances);

            colony.Search(1000);

            // Write(colony.BestPath[colony.BestPath.Count - 1] + " -> ");
            // foreach (var item in colony.BestPath)
            // {
            //     if (colony.BestPath.IndexOf(item) != colony.BestPath.Count - 1)
            //     {
            //         Write(item + " -> ");
            //     }
            //     else
            //     {
            //         WriteLine(item);
            //     }
                
            // }
            WriteLine("Length: " + colony.BestPathLength);
        }

        static void DrawDistances(int[,] distances)
        {
            for (var i = 0; i < distances.GetLength(0); i++)
            {
                for (var j = 0; j < distances.GetLength(1); j++)
                {
                    Write(distances[i, j] + "\t");
                }
                WriteLine();
            }
        }
    }
}
