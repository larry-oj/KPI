using System;
using System.Collections.Generic;

namespace Lab4_1.Genetic
{
    public class Generation
    {
        private List<Individual> _population;
        private double _mutationChance;
        private int _k;

        public List<Individual> Population => _population;

        public Generation(int populationSize, int k, double mutationChance, Clique clique)
        {
            this._mutationChance = mutationChance;
            this._k = k;
            this._population = InitPopulation(clique.Matrix.GetLength(0), clique);
        }
        private Generation(List<Individual> population, int k, double mutationChance)
        {
            this._mutationChance = mutationChance;
            this._k = k;
            this._population = population;
        }

        private List<Individual> InitPopulation(int populationSize, Clique clique)
        {
            var individuals = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
            {
                var individual = new Individual(populationSize, this._k, this._mutationChance, clique);

                individuals.Add(individual);
            }

            return individuals;
        }
    

        public Generation Evolve()
        {
            var random = new Random();

            var newPopulation = new List<Individual>();

            var populationCopy = new List<Individual>();

            foreach (var item in _population)
            {
                populationCopy.Add(item);
            }

            for (int i = 0; i < _population.Count; i += 2)
            {
                var tmp = TournamentSelection(populationCopy);
                var indOne = (Individual)tmp.Clone();
                populationCopy.Remove(tmp);

                tmp = TournamentSelection(populationCopy);
                var indTwo = (Individual)tmp.Clone();
                populationCopy.Remove(tmp);

                var firstPoint = _population.Count / 4; // 25%
                var secondPoint = firstPoint * 2;       // 50%
                var thirdPoint = firstPoint * 3;        // 75%
                var fourthPoint = _population.Count;    // 100%

                var buffer = 0;
                for (int j = firstPoint; j < secondPoint; j++)
                {
                    buffer = indOne.Chromosome[j];
                    indOne.Chromosome[i] = indTwo.Chromosome[i];
                    indTwo.Chromosome[i] = buffer;
                }
                for (int j = thirdPoint; j < fourthPoint; j++)
                {
                    buffer = indOne.Chromosome[j];
                    indOne.Chromosome[i] = indTwo.Chromosome[i];
                    indTwo.Chromosome[i] = buffer;
                }

                // LocalImprovement(ref indOne);
                // LocalImprovement(ref indTwo);

                newPopulation.Add(indOne);
                newPopulation.Add(indTwo);
            }

            foreach (var individual in newPopulation)
            {
                individual.MutationOne();
                individual.MutationTwo();
            }

            for (int i = 0; i < newPopulation.Count; i++)
            {
                Individual ind = newPopulation[i];
                if (ind.Fitness >= _k && ind.CountVertices() > _k)
                {
                    LocalestImprovement(ref ind);
                }
            }

            var newGeneration = new Generation(newPopulation, _k, _mutationChance);

            return newGeneration;
        }

        private Individual TournamentSelection(List<Individual> population)
        {
            while (true)
            {
                var random = new Random();

                var indOneIndex = 0;
                var indTwoIndex = 0;

                indOneIndex = random.Next(0, population.Count);

                if (population.Count == 1)
                {
                    return population[indOneIndex];
                }

                do
                {
                    indTwoIndex = random.Next(0, population.Count);
                }
                while (indOneIndex == indTwoIndex);

                var indOne = population[indOneIndex];
                var indTwo = population[indTwoIndex];

                return indOne.Fitness > indTwo.Fitness ? indOne : indTwo;
            }
        }
    
        private void LocalImprovement(ref Individual ind)
        {
            if (ind.CountVertices() == _k) return;

            while (ind.CountVertices() > _k)
            {
                ind.MutationTwo(false);
            }

            while (ind.CountVertices() < _k)
            {
                ind.MutationTwo(true);
            }
        }
    
        private void LocalestImprovement(ref Individual ind)
        {
            var verticesIndexes = new List<int>();
            for (int i = 0; i < ind.Chromosome.Count; i++)
            {
                if (ind.Chromosome[i] == 1)
                {
                    verticesIndexes.Add(i);
                }
            }


            for (int i = 0; i < verticesIndexes.Count; i++)
            {
                ind.Chromosome[verticesIndexes[i]] = 0;
                if (ind.Fitness > _k)
                {
                    continue;
                }
                else if (ind.Fitness == _k)
                {
                    if (ind.CountVertices() == _k)
                    {
                        break;
                    }
                }
                else 
                {
                    ind.Chromosome[verticesIndexes[i]] = 1;
                }
            }
        }
    }
}