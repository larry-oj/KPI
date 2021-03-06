using static System.Console;

namespace Lab3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tsp = new TravellingSalesmanProblem(50);

            var colony = new AntColony(tsp.Distances);

            colony.Search(1000);

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
