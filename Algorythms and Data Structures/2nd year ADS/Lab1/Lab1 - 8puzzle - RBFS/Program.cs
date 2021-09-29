using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Search();

            int[] puzzle =
            {
                1, 0, 5, 
                4, 3, 2, 
                7, 8, 6
            };

            // Shuffle until solveable
            // Shuffle(puzzle);
            // while (!IsSolvable(puzzle))
            // {
            //     Shuffle(puzzle);
            // }
            
            var iterations = 0;
            var deadEnds = 0;
            var states = 1;
            var statesInMemory = 0;

            var initialNode = new Node(puzzle, 0); // root node

            var success = s.RBFS(initialNode, int.MaxValue, ref iterations, ref deadEnds, ref states);

            if (success)
            {
                s.PathToSolution.Reverse();

                initialNode.PrintPuzzle();

                // foreach (var item in s.PathToSolution)
                // {
                //     item.PrintPuzzle();
                // }

                statesInMemory = s.PathToSolution.Count;

                System.Console.WriteLine($"Success: {success}\nIterations: {iterations}\nDead ends: {deadEnds}\nTotal states: {states}\nStates in memory: {statesInMemory}");
            }
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

        static bool IsSolvable(int[] puzzle) // Checks if puzzle us solveable
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

        static int GetInvCount(int[,] arr) // Used by IsSolveable()
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
