using System;

namespace Lab3_1
{
    public class TravellingSalesmanProblem
    {
        public int[,] Distances { get; private set; }


        public TravellingSalesmanProblem(int citiesCount) : this(citiesCount, 1, 40) { }
        public TravellingSalesmanProblem(int citiesCount, int minDistance, int maxDistance)
        {
            this.Distances = new int[citiesCount, citiesCount];
            CreateCities(citiesCount, minDistance, maxDistance);
        }

        private void CreateCities(int citiesCount, int minDistance, int maxDistance)
        {
            var random = new Random();
            int rDistance;

            for (int row = 0; row < citiesCount; row++)
            {
                for (int column = row; column < citiesCount; column++)
                {
                    if (row == column)
                    {
                        this.Distances[row, column] = 0;
                    }
                    else
                    {
                        rDistance = random.Next(minDistance, maxDistance + 1);
                        this.Distances[row, column] = rDistance;
                        this.Distances[column, row] = rDistance;
                    }
                }
            }
        }
    }
}