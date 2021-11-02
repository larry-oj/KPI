using System;
using System.Collections.Generic;

namespace Lab3_1.Ants
{
    public class WildAnt : IAnt
    {
        #region Properties & Fields
        private AntColony _colony;


        private double _lastPheromoneValue;
        private List<int> _lastPath;


        public double LastPheromoneValue => _lastPheromoneValue;
        public List<int> LastPath => _lastPath;
        #endregion

        public WildAnt(AntColony colony)
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

            var currentCity = 0;

            // iterate though all cities
            for (int i = 0; i < _colony.DistanceMap.GetLength(1); i++)
            {
                if (i == 0)
                {
                    currentCity = startPoint;
                }
                
                var city = random.Next(0, openList.Count);

                totalDistance += _colony.DistanceMap[currentCity, city];

                closedList.Add(city);
                openList.Remove(city);

                currentCity = city;
            }

            closedList.Add(startPoint);

            // and add its distance
            totalDistance += _colony.DistanceMap[currentCity, startPoint];

            // calculate path pheromone
            var deltaTau = (double)1 / (double)totalDistance;
            this._lastPheromoneValue = deltaTau;

            this._lastPath.Clear();
            foreach (var item in closedList)
            {
                this._lastPath.Add(item);
            }
        }
    }
}