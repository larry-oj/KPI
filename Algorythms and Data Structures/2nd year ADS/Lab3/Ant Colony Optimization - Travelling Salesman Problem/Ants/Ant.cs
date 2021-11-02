using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab3_1.Ants
{
    public class Ant : IAnt
    {
        #region Properties & Fields
        private AntColony _colony;

        private double _lastPheromoneValue;
        private List<int> _lastPath;

        public double LastPheromoneValue => _lastPheromoneValue;
        public List<int> LastPath => _lastPath;
        #endregion

        public Ant(AntColony colony)
        {
            this._colony = colony;
            this._lastPath = new List<int>();
        }

        public void Work(int startPoint)
        {
            var openList = new List<int>(); // contains indexes of cities
            var closedList = new List<int>(); // basically contains path

            // fill openList with cities
            for (int i = 0; i < _colony.DistanceMap.GetLength(1); i++)
            {
                if (i == startPoint) continue;

                openList.Add(i);
            }

            var totalDistance = 0;
            var random = new Random();

            var distance = 0;

            var probabilities = new Dictionary<int, double>();
            var currentCity = 0;

            // iterate though all cities
            for (int i = 0; i < _colony.DistanceMap.GetLength(1); i++)
            {
                if (i == 0)
                {
                    currentCity = startPoint;
                }

                probabilities = CalculateProbabilities(ref openList, currentCity);

                probabilities = CalculateCumulativeSum(probabilities);

                var chooser = random.NextDouble(); // roulette

                // find matching city
                foreach (var city in probabilities)
                {
                    if (city.Value > chooser) 
                    {
                        currentCity = city.Key; // set city for referencing in the next iteration

                        openList.Remove(city.Key);
                        closedList.Add(city.Key);

                        // increase distance
                        distance = _colony.DistanceMap[currentCity, city.Key];
                        totalDistance += distance;
                        break;
                    }
                }
            }

            // add start point at the end of path for cycle closing
            closedList.Add(startPoint);

            // and add its distance
            distance = _colony.DistanceMap[currentCity, startPoint];
            totalDistance += distance;

            // calculate path pheromone
            var deltaTau = (double)1 / (double)totalDistance;
            this._lastPheromoneValue = deltaTau;

            this._lastPath.Clear();
            foreach (var item in closedList)
            {
                this._lastPath.Add(item);
            }
        }

        private Dictionary<int, double> CalculateProbabilities(ref List<int> openList, int startCity) // calculates probabilities for all possible next cities
        {
            var probabilityList = new Dictionary<int, double>();

            var sum = 0.0;

            // calculate probabilities for all cities
            foreach (var city in openList) 
            {
                sum = CalculateSum(openList, startCity);

                // upper part of equasion
                var p1 = Math.Pow(_colony.PheromonesMap[startCity, city], _colony.Alpha);
                var p2 = Math.Pow(((double)1 / (double)_colony.DistanceMap[startCity, city]), _colony.Beta);
                var upper = p1 * p2; 

                // equasion
                var probability = upper / sum;
                
                probabilityList.Add(city, probability);
            }

            // oder rpobabilities by descending (for roulette)
            probabilityList = probabilityList.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);

            return probabilityList;
        }

        private Dictionary<int, double> CalculateCumulativeSum(Dictionary<int, double> probabilities)
        {
            var newProbabilities = new Dictionary<int, double>();

            var cities = new List<int>();
            var commulativeSums = new List<double>();

            foreach (var pair in probabilities)
            {
                cities.Add(pair.Key);
                commulativeSums.Add(pair.Value);
            }

            for (int i = 0; i < commulativeSums.Count; i++)
            {
                var sum = 0.0;

                sum += commulativeSums[i];
                for (int j = i; j < commulativeSums.Count; j++)
                {
                    sum += commulativeSums[j];
                }

                commulativeSums[i] = sum;
            }

            for (int i = 0; i < cities.Count; i++)
            {
                newProbabilities.Add(cities[i], commulativeSums[i]);
            }

            return newProbabilities;
        }

        private double CalculateSum(List<int> openList, int startCity) // calculates sum for probability formula
        {
            var cityList = new List<int>();
            foreach (var city in openList)
            {
                cityList.Add(city);
            }

            var sum = 0.0;

            foreach (var city in cityList)
            {
                var p1 = Math.Pow(_colony.PheromonesMap[startCity, city], _colony.Alpha);
                var p2 = Math.Pow(((double)1 / (double)_colony.DistanceMap[startCity, city]), _colony.Beta);
                var probability = p1 * p2;
                sum += probability;
            }

            return sum;
        }
    }
}