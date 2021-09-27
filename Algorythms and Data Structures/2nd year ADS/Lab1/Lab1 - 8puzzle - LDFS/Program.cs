using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            // initialNode.PrintPuzzle();

            var s = new Search();

            var succ = false;

            while (!succ)
            {
                int[] puzzle = // starting puzzle
                {
                    1, 2, 3,
                    4, 5, 6,
                    0, 7, 8
                };

                Shuffle(puzzle);


                // Shuffle until solveable
                while (!IsSolvable(puzzle))
                {
                    Shuffle(puzzle);
                }

                var initialNode = new Node(puzzle); // root node
                succ = s.LDFS(initialNode, 0, 10); // start search, 20 - depth limit
            }
            
            

            // Print solution to console
            s.PathToSolution.Reverse();
            foreach(var node in s.PathToSolution)
            {
                node.PrintPuzzle();
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
            var matrix = new int[3,3];

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
