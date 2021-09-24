using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Lab1_1
{
    class Node
    {
        public List<Node> Children { get; set; } // Child nodes
        public int[] Puzzle { get; set; } // Currnet state
        public int X { get; set; } // Index of empty space

        public Node(int[] puzzle)
        {
            this.Children = new List<Node>();
            this.Puzzle = new int[9];
            this.X = 0;
            SetPuzzle(puzzle);
        }

        private void SetPuzzle(int[] puzzle) // sets current state. (avoids pointers)
        {
            for (int i = 0; i < this.Puzzle.Length; i++)
            {
                Puzzle[i] = puzzle[i];
            }
        }

        public bool IsReachedGoal() // check if goal is reached
        {
            var goal = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

            for (int i = 0; i < Puzzle.Length; i++)
            {
                if (Puzzle[i] != goal[i]) return false;
            }

            return true;
        }

        public void Expand() // generate child nodes
        {
            for (int i = 0; i < Puzzle.Length; i++)
            {
                if (Puzzle[i] == 0)
                {
                    X = i;
                    break;
                }
            }

            MoveRight(Puzzle, X);
            MoveLeft(Puzzle, X);
            MoveUp(Puzzle, X);
            MoveDown(Puzzle, X);
        }


        #region Move Empty Space
        private void MoveRight(int[] puzzle, int index) 
        {
            if (index % 3 < 3 - 1)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index + 1];
                childPuzzle[index + 1] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle);
                Children.Add(child);
            }
        }
        private void MoveLeft(int[] puzzle, int index)
        {
            if (index % 3 > 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index - 1];
                childPuzzle[index - 1] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle);
                Children.Add(child);
            }
        }
        private void MoveUp(int[] puzzle, int index)
        {
            if (index - 3 >= 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index - 3];
                childPuzzle[index - 3] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle);
                Children.Add(child);
            }
        }
        private void MoveDown(int[] puzzle, int index)
        {
            if (index + 3 < puzzle.Length)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index + 3];
                childPuzzle[index + 3] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle);
                Children.Add(child);
            }
        }
        #endregion

        private void CopyPuzzle(ref int[] a, int[] b) // copies puzzle (avoids pointers)
        {
            for (int i = 0; i < b.Length; i++)
            {
                a[i] = b[i];
            }
        }

        public void PrintPuzzle() // prints puzzle to console
        {
            System.Console.WriteLine();
            var m = 0;

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    System.Console.Write(Puzzle[m] + " ");
                    m++;
                }
                System.Console.WriteLine();
            }
        }
    }
}
