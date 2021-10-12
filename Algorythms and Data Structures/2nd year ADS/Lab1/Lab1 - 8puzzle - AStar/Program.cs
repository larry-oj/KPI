using System;
using System.Collections.Generic;

namespace Lab1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] puzzle =
            {
                1, 2, 3, 
                8, 5, 6,
                4, 0, 7
            };

            {
                /* 
                Shuffle(puzzle);

                while (!IsSolvable(puzzle))
                {
                    Shuffle(puzzle);
                } 
                */
            }            

            var initialNode = new Node(puzzle, 0, null);

            var s = new Search();

            var success = s.AStar(initialNode);

            s.PathToSolution.Reverse();
            foreach (var item in s.PathToSolution)
            {
                item.PrintPuzzle();
            }
            System.Console.WriteLine($"\nSuccess: {success}\nIterations: {s.Iterations}\nDead ends: {s.DeadEnds}\nTotal states: {s.States}\nStates in memory: {s.PathToSolution.Count}");
        }

        public static void Shuffle(IList<int> list, Random rng = null) // Shuffles current puzzle
        {
            rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static bool IsSolvable(int[] puzzle) 
        {
            var matrix = new int[3, 3];

            var counter = 0;
            var rowIndex = 0;
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (counter == 3)
                {
                    rowIndex++;
                    counter = 0;
                    if (rowIndex == 3)
                    {
                        break;
                    }
                }
                matrix[rowIndex, counter] = puzzle[i];
                counter++;
            }

            int invCount = GetInvCount(matrix);

            return (invCount % 2 == 0);
        }

        static int GetInvCount(int[,] arr)
        {
            int inv_count = 0;
            for (int i = 0; i < 3 - 1; i++)
                for (int j = i + 1; j < 3; j++)
                    if (arr[j, i] > 0 && arr[j, i] > arr[i, j])
                        inv_count++;

            return inv_count;
        }
    }
}
