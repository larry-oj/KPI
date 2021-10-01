using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Lab1_2
{
    class Node
    {
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
        public int[] Puzzle { get; set; }
        public int X { get; set; }
        public int H { get; set; }
        public int G { get; set; }
        public int F { get; set; }

        public Node(int[] puzzle, int g, Node parent)
        {
            this.Parent = parent;
            this.Children = new List<Node>();

            this.Puzzle = new int[9];
            SetPuzzle(puzzle);

            this.X = 0;

            this.G = g;
            this.H = CountH();
            this.F = H + G;
        }

        private int CountH() 
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

        private int MDCoords(List<int> x, List<int> y)
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

        private void SetPuzzle(int[] puzzle) 
        {
            for (int i = 0; i < this.Puzzle.Length; i++)
            {
                Puzzle[i] = puzzle[i];
            }
        }

        public bool IsReachedGoal() 
        {
            var goal = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

            for (int i = 0; i < Puzzle.Length; i++)
            {
                if (Puzzle[i] != goal[i]) return false;
            }

            return true;
        }

        public void Expand() 
        {
            for (int i = 0; i < Puzzle.Length; i++)
            {
                if (Puzzle[i] == 0)
                {
                    X = i;
                    break;
                }
            }

            if (X % 3 < 3 - 1)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, Puzzle);

                int tmp = childPuzzle[X + 1];
                childPuzzle[X + 1] = childPuzzle[X];
                childPuzzle[X] = tmp;

                Node child = new Node(childPuzzle, G + 1, this);
                Children.Add(child);
            }
            if (X % 3 > 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, Puzzle);

                int tmp = childPuzzle[X - 1];
                childPuzzle[X - 1] = childPuzzle[X];
                childPuzzle[X] = tmp;

                Node child = new Node(childPuzzle, G + 1, this);
                Children.Add(child);
            }
            if (X - 3 >= 0)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, Puzzle);

                int tmp = childPuzzle[X - 3];
                childPuzzle[X - 3] = childPuzzle[X];
                childPuzzle[X] = tmp;

                Node child = new Node(childPuzzle, G + 1, this);
                Children.Add(child);
            }
            if (X + 3 < Puzzle.Length)
            {
                int[] childPuzzle = new int[9];
                CopyPuzzle(ref childPuzzle, Puzzle);

                int tmp = childPuzzle[X + 3];
                childPuzzle[X + 3] = childPuzzle[X];
                childPuzzle[X] = tmp;

                Node child = new Node(childPuzzle, G + 1, this);
                Children.Add(child);
            }
        }

        public void PrintPuzzle()
        {
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
            System.Console.WriteLine();
        }

        public void PrintState()
        {
            System.Console.WriteLine($"[g = {G}; h = {H} f = {F};]");
        }

        public bool IsSamePuzzle(int[] puzzle) 
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

        public void CopyPuzzle(ref int[] a, int[] b) 
        {
            for (int i = 0; i < b.Length; i++)
            {
                a[i] = b[i];
            }
        }
    }
}