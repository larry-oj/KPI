using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Lab1_3
{
    class Node
    {
        public List<Node> Children { get; set; }
        public int[] Puzzle { get; set; }
        public int X { get; set; } // empty space index
        public int H { get; set; } // heuristic function value
        public int G { get; set; } // depth of tree
        public int F { get; set; } // G + H

        public Node(int[] puzzle, int g)
        {
            this.Children = new List<Node>();

            this.Puzzle = new int[9];
            SetPuzzle(puzzle);

            this.X = 0;

            this.G = g;
            this.H = CountH();
            this.F = H + G;
        }

        private int CountH() // manhatten distance
        {
            int h = 0;

            var matrix = new int[3, 3];
            var goal = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 }
            };

            var counter = 0;
            var rowIndex = 0;
            for (int i = 0; i < Puzzle.Length; i++)
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
                matrix[rowIndex, counter] = Puzzle[i];
                counter++;
            }

            var x = new List<int>();
            var y = new List<int>();

            bool end = false;

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (matrix[row, column] == 9) continue;

                    if (matrix[row, column] != goal[row, column])
                    {
                        x.Add(row);
                        y.Add(column);

                        for (int row1 = 0; row1 < 3; row1++)
                        {
                            for (int column1 = 0; column1 < 3; column1++)
                            {
                                if (row1 == row && column1 == column) continue;

                                if (goal[row1, column1] == matrix[row, column])
                                {
                                    x.Add(row1);
                                    y.Add(column1);

                                    goal[row1, column1] = 9;
                                    matrix[row, column] = 9;
                                    end = true;
                                }
                                if (end) break;
                            }
                            if (end) break;
                        }
                        h += MDCoords(x, y);
                        x.Clear();
                        y.Clear();
                        end = false;
                    }
                }
            }

            return h;
        }

        private int MDCoords(List<int> x, List<int> y) // manhatten distance between two points
        {
            var n = x.Count;
            int sum = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    sum += Math.Abs(x[i] - x[j]) + Math.Abs(y[i] - y[j]);
                }
            }

            return sum;
        }

        private void SetPuzzle(int[] puzzle) // sets puzzle (avoids pointers)
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

        public void Expand() // create children
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

            Children = Children.OrderBy(c => c.F).ToList();
        }

        public void MoveRight(int[] puzzle, int index)
        {
            if (index % 3 < 3 - 1)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index + 1];
                childPuzzle[index + 1] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle, G + 1);
                Children.Add(child);
            }
        }
        public void MoveLeft(int[] puzzle, int index)
        {
            if (index % 3 > 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index - 1];
                childPuzzle[index - 1] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle, G + 1);
                Children.Add(child);
            }
        }
        public void MoveUp(int[] puzzle, int index)
        {
            if (index - 3 >= 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index - 3];
                childPuzzle[index - 3] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle, G + 1);
                Children.Add(child);
            }
        }
        public void MoveDown(int[] puzzle, int index)
        {
            if (index + 3 < puzzle.Length)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, puzzle);

                int tmp = childPuzzle[index + 3];
                childPuzzle[index + 3] = childPuzzle[index];
                childPuzzle[index] = tmp;

                Node child = new Node(childPuzzle, G + 1);
                Children.Add(child);
            }
        }

        public void PrintPuzzle() // print puzzle
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

        public void PrintState() // print current heuristic value
        {
            System.Console.WriteLine($"g = {G}; f = {F};");
        }

        public bool IsSamePuzzle(int[] puzzle) // check if two puzzles are the same
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (Puzzle[i] != puzzle[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void CopyPuzzle(ref int[] a, int[] b) // copy puzzle
        {
            for (int i = 0; i < b.Length; i++)
            {
                a[i] = b[i];
            }
        }
    }
}