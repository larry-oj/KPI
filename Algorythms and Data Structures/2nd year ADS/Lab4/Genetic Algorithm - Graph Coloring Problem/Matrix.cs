using System;
namespace Genetic_Algorithm___Graph_Coloring_Problem
{
    public class Matrix
    {
        public int[,] Adjacency { get; }

        public Matrix(int size)
        {
            this.Adjacency = InitMatrix(size);
        }

        private int[,] InitMatrix(int size)
        {
            var random = new Random();
            var matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    var rnd = random.Next(0, 2);
                    matrix[i, j] = rnd;
                    matrix[j, i] = rnd;
                }
            }

            return matrix;
        }
    }
}