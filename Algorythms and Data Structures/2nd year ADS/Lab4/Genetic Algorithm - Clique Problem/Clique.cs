using System.Numerics;
using System;

namespace Lab4_1
{
    public class Clique
    {
        private int[,] _matrix;

        public int[,] Matrix => _matrix;

        public Clique(int size)
        {
            this._matrix = CreateGraph(size);
        }

        private int[,] CreateGraph(int size)
        {
            var random = new Random();
            var graph = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    graph[i, j] = 0;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    if (i == j) continue;
                    var decider = random.Next(0, 2);
                    graph[i, j] = decider;
                    graph[j, i] = decider;
                }
            }

            return graph;
        }

        public void CreateGraph()
        {
            var graph = new int[,] 
            {
                { 0, 1, 1, 1, 0, 1, 0, 0 },
                { 1, 0, 1, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 1, 0, 1, 1, 1 },
                { 1, 0, 1, 0, 1, 1, 1, 1 },
                { 0, 0, 0, 1, 0, 1, 0, 0 },
                { 1, 0, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 1, 1, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0, 0, 1, 0 }
            };

            this._matrix = graph;
        }
    }
}