using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
namespace Genetic_Algorithm___Graph_Coloring_Problem
{
    public class Population
    {
        private List<Chromosome> _chromosomes;
        private Matrix _matrix;
        private Random _random => new Random();

        public Population(int size, Matrix matrix)
        {
            this._matrix = matrix;
            this._chromosomes = InitPopulation(size);
        }

        public Population(List<Chromosome> pupulation, Matrix matrix)
        {
            this._matrix = matrix;
            this._chromosomes = pupulation;
        }

        private List<Chromosome> InitPopulation(int size)
        {
            var population = new List<Chromosome>();
            for (int i = 0; i < size; i++)
            {
                population.Add(new Chromosome(_matrix, true));
            }
            return population;
        }
    
        public Population Evolve(double mutationChance)
        {
            var newChromosomes = new List<Chromosome>();
            var chromosomesCopy = ChromosomesCopy();

            for (int i = 0; i < _chromosomes.Count / 2; i++)
            {
                (Chromosome One, Chromosome Two) pair = GetBest(chromosomesCopy);

                (Chromosome One, Chromosome Two) newPair = Crossover(pair);

                newPair.One.Mutate(mutationChance);
                newPair.Two.Mutate(mutationChance);

                newPair.One.Improve();
                newPair.Two.Improve();

                newChromosomes.Add(newPair.One);
                newChromosomes.Add(newPair.Two);
            }

            do 
            {
                newChromosomes.Add(new Chromosome(_matrix, true));
            }
            while (newChromosomes.Count < _chromosomes.Count);

            return new Population(newChromosomes, _matrix);
        }

        public Chromosome GetBest()
        {
            var bestFitness = int.MaxValue;
            Chromosome best = default(Chromosome);

            for (int i = 0; i < _chromosomes.Count; i++)
            {
                var fitness = _chromosomes[i].Fitness;
                if (fitness < bestFitness)
                {
                    best = _chromosomes[i].Copy();
                    bestFitness = fitness;
                }
            }

            return best;
        }
        
        private List<Chromosome> ChromosomesCopy()
        {
            var copy = new List<Chromosome>();

            for (int i = 0; i < _chromosomes.Count; i++)
            {
                copy.Add(_chromosomes[i].Copy());
            }

            copy = copy.OrderBy(c => c.Fitness).ToList();
            
            return copy;
        }
    
        private (Chromosome, Chromosome) GetBest(List<Chromosome> chromosomesCopy)
        {
            var one = chromosomesCopy.First();
            chromosomesCopy.Remove(one);
            var two = chromosomesCopy.First();
            chromosomesCopy.Remove(two);

            return (one, two);
        }
    
        private (Chromosome, Chromosome) Crossover((Chromosome One, Chromosome Two) pair, int? points = null)
        {
            var buffer = 0;

            switch (points)
            {
                default:
                case 1:
                    var point = _random.Next(0, _matrix.Adjacency.GetLength(0));
                    for (int i = 0; i < point; i++)
                    {
                        buffer = pair.One.Genes[i];
                        pair.One.Genes[i] = pair.Two.Genes[i];
                        pair.Two.Genes[i] = buffer;
                    }
                    break;

                case 2:
                    var pointOne = _random.Next(0, _matrix.Adjacency.GetLength(0));
                    var pointTwo = _random.Next(pointOne + 1, _matrix.Adjacency.GetLength(0));
                    for (int i = 0; i < pointOne; i++)
                    {
                        buffer = pair.One.Genes[i];
                        pair.One.Genes[i] = pair.Two.Genes[i];
                        pair.Two.Genes[i] = buffer;
                    }
                    for (int i = pointTwo; i < _matrix.Adjacency.GetLength(0); i++)
                    {
                        buffer = pair.One.Genes[i];
                        pair.One.Genes[i] = pair.Two.Genes[i];
                        pair.Two.Genes[i] = buffer;
                    }
                    break;

                case 3:
                    var pOne = _random.Next(0, _matrix.Adjacency.GetLength(0));
                    var pTwo = _random.Next(pOne + 1, _matrix.Adjacency.GetLength(0));
                    var pThree = _random.Next(pTwo + 1, _matrix.Adjacency.GetLength(0));
                    for (int i = 0; i < pOne; i++)
                    {
                        buffer = pair.One.Genes[i];
                        pair.One.Genes[i] = pair.Two.Genes[i];
                        pair.Two.Genes[i] = buffer;
                    }
                    for (int i = pTwo; i < pThree; i++)
                    {
                        buffer = pair.One.Genes[i];
                        pair.One.Genes[i] = pair.Two.Genes[i];
                        pair.Two.Genes[i] = buffer;
                    }
                    break;
            }

            return pair;
        }
    }
}