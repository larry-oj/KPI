using System.Linq;
using System;
using System.Collections.Generic;
namespace Genetic_Algorithm___Graph_Coloring_Problem
{
    public class Chromosome : ICloneable
    {
        private int _vertices;
        private Matrix _matrix;
        private Random _random => new Random();

        public List<int> Genes { get; set; }
        public int Fitness => CalculateFitness();

        public Chromosome(Matrix matrix, bool? randomize = null)
        {
            this._vertices = matrix.Adjacency.GetLength(0);
            this._matrix = matrix;
            if (randomize != null)
            {
                this.Genes = new List<int>();
                Randomize();
            }
        }

        private void Randomize()
        {
            var random = new Random();

            for (int i = 0; i < _vertices; i++)
            {
                var rnd = random.Next(1, _vertices + 1);
                this.Genes.Add(rnd);
            }
        }

        private int CalculateFitness()
        {
            var fitness = 0;

            for (int i = 0; i < _vertices; i++)
            {
                var currentColor = Genes[i];
                for (int j = 0; j < _vertices; j++)
                {
                    if (_matrix.Adjacency[i,j] == 1)
                    {
                        if (Genes[j] == currentColor)
                        {
                            fitness++;
                        }
                    }
                }
            }

            var closedList = new List<int>();

            for (int i = 0; i < Genes.Count; i++)
            {
                if (!closedList.Contains(Genes[i]))
                {
                    fitness++;
                    closedList.Add(Genes[i]);
                }
            }

            return fitness;
        }

        public void Mutate(double chance)
        {
            var roulette = _random.NextDouble();

            if (roulette < chance)
            {
                var geneIndex = _random.Next(0, Genes.Count);

                if (Genes[geneIndex] != 1)
                {
                    Genes[geneIndex]--;
                }
            }

            if (roulette < chance / 2)
            {
                var geneIndex = _random.Next(0, Genes.Count);

                if (Genes[geneIndex] != 1)
                {
                    Genes[geneIndex] = Genes.Min();
                }
            }
        }

        public void Improve()
        {
            ImproveCrutch();

            var loneWolves = new List<int>();

            var someGene = -1;
            var condition = false;
            for (int i = 0; i < Genes.Count; i++)
            {
                someGene = Genes[i];
                for (int j = 0; j < Genes.Count; j++)
                {
                    if (i == j) continue;
                    if (someGene == Genes[j])
                    {
                        condition = true;
                        break;
                    }
                }

                if (!condition)
                {
                    loneWolves.Add(i);
                }

                condition = false;
            }

            foreach (var wolf in loneWolves)
            {
                ImproveCrutch(wolf);
            }
        }

        private void ImproveCrutch(int? h = null)
        {
            var highest = 0;

            if (h != null)
            {
                highest = Convert.ToInt32(h);
            }

            for (int i = 0; i < Genes.Count; i++)
            {
                if (Genes[i] > highest)
                {
                    highest = i;
                }
            }

            var neighbors = new List<int>();

            for (int i = 0; i < _matrix.Adjacency.GetLength(0); i++)
            {
                if (_matrix.Adjacency[highest, i] == 1)
                {
                    neighbors.Add(i);
                }
            }

            var condition = true;
            for (int i = 0; i < _vertices; i++)
            {
                foreach (var neighbor in neighbors)
                {
                    if (Genes[neighbor] == i + 1)
                    {
                        condition = false;
                        break;
                    }
                }
                if (condition)
                {
                    Genes[highest] = i + 1;
                    break;
                }
            }
        }

        public int CountColors()
        {
            var colors = new List<int>();
            foreach (var color in Genes)
            {
                if (!colors.Contains(color))
                {
                    colors.Add(color);
                }
            }

            return colors.Count;
        }

        public Chromosome Copy() => (Chromosome)Clone();
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}