namespace Genetic_Algorithm___Graph_Coloring_Problem
{
    public class Genetic
    {
        private Population _population;
        
        public Matrix Matrix { get; }
        public Chromosome Best { get; private set; }
        public int BestFitness { get; private set; }

        public Genetic(Matrix matrix, int populationSize)
        {
            this.Matrix = matrix;
            this._population = new Population(populationSize, matrix);
        }

        public void Solve(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                if (i % 100 == 0)
                {
                    System.Console.WriteLine("Iteration: " + i);
                }
                this._population = _population.Evolve(0.1);

                var best = this._population.GetBest();
                var bestFitness = best.Fitness;

                if (this.Best == null)
                {
                    this.Best = best;
                    this.BestFitness = bestFitness;
                }
                else if (bestFitness < this.BestFitness)
                {
                    this.Best = best;
                    this.BestFitness = bestFitness;
                }
            }
        }
    }
}