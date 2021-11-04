using System;
using System.Collections.Generic;
using Lab3_1.Ants;

namespace Lab3_1
{
    public class AntColony
    {
        #region Properties & Fields
        private int[,] _distanceMap;
        private double[,] _pheromonesMap;
        private List<IAnt> _ants;

        private double _rho;
        private int _alpha;
        private int _beta;
        
        private List<int> _bestPath;
        private int _bestPathLength;

        public int[,] DistanceMap => _distanceMap;
        public double[,] PheromonesMap => _pheromonesMap;
        public int Alpha => _alpha;
        public int Beta => _beta;
        public List<int> BestPath => _bestPath;
        public int BestPathLength => _bestPathLength;
        #endregion

        public AntColony(int[,] distanceMap) : this(distanceMap, 45, 10, 4, 2, 0.3) { }
        public AntColony(int[,] distanceMap, int antCount, int wildAntCount, int alpha, int beta, double rho)
        {
            this._distanceMap = distanceMap;
            this._pheromonesMap = InitPheromonesMap();

            this._alpha = alpha;
            this._beta = beta;
            this._rho = rho;

            this._ants = InitAnts(antCount, wildAntCount);

            this._bestPath = new List<int>();
            this._bestPathLength = int.MaxValue;
        }


        #region Initializers
        private double[,] InitPheromonesMap() // Initializes pheromones map
        {
            var citiesCount = this._distanceMap.GetLength(0);

            var map = new double[citiesCount, citiesCount];

            for (int row = 0; row < citiesCount; row++)
            {
                for (int column = 0; column < citiesCount; column++)
                {
                    map[row, column] = 1.0;
                }
            }

            return map;
        }
        private List<IAnt> InitAnts(int antCount, int wildAntCount) // Creates ants
        {
            var normalAntsCount = antCount - wildAntCount;

            var lMin = CountLMin();

            var ants = new List<IAnt>();

            IAnt ant;

            for (int i = 0; i < normalAntsCount; i++)
            {
                ant = new Ant(this, lMin);
                ants.Add(ant);
            }

            for (int i = 0; i < wildAntCount; i++)
            {
                ant = new WildAnt(this, lMin);
                ants.Add(ant);
            }

            return ants;
        }
        private int CountLMin()
        {
            var startPoint = 0;

            var openList = new List<int>(); // contains indexes of cities
            var closedList = new List<int>(); // basically contains path

            // fill openList with cities
            for (int i = 0; i < this.DistanceMap.GetLength(1); i++)
            {
                if (i == startPoint) continue;

                openList.Add(i);
            }

            var totalDistance = 0;
            var random = new Random();

            var currentCity = 0;

            // iterate though all cities
            for (int i = 0; i < this.DistanceMap.GetLength(1) - 1; i++)
            {
                if (i == 0)
                {
                    currentCity = startPoint;
                }

                var shortestDistance = int.MaxValue;
                var city = -1;
                foreach (var openCity in openList)
                {
                    if (this.DistanceMap[currentCity, openCity] < shortestDistance)
                    {
                        shortestDistance = this.DistanceMap[currentCity, openCity];
                        city = openCity;
                    }
                }

                totalDistance += this.DistanceMap[currentCity, city];

                closedList.Add(city);
                openList.Remove(city);

                currentCity = city;
            }

            closedList.Add(startPoint);

            // and add its distance
            totalDistance += this.DistanceMap[currentCity, startPoint];

            return totalDistance;
        }
        #endregion

        #region Methods
        public void Search(int iterationsCount)
        {
            var random = new Random();

            var counter = 0;
            var crutch = true;
            for (int iteration = 0; iteration < iterationsCount; iteration++) // main loop
            {
                // vaporize pheromones -> (1 - ρ) * т
                for (int row = 0; row < _pheromonesMap.GetLength(0); row++)
                {
                    for (int column = 0; column < _pheromonesMap.GetLength(1); column++)
                    {
                        _pheromonesMap[row, column] = (1 - _rho) * _pheromonesMap[row, column];
                    }
                }

                // force enslaved ants to work (xd)
                foreach (var ant in _ants)
                {
                    var startingCity = random.Next(0, _distanceMap.GetLength(0));

                    ant.Work(startingCity);

                    // set new best path legnths
                    var pathLength = PathLength(ant.LastPath);
                    // System.Console.WriteLine(pathLength);
                    if (pathLength < _bestPathLength)
                    {
                        _bestPathLength = pathLength;
                        this._bestPath.Clear();
                        foreach (var innerItem in ant.LastPath)
                        {
                            this._bestPath.Add(innerItem);
                        }
                    }
                }

                // Add pheromones based on path taken
                foreach (var ant in _ants)
                {
                    var startingCity = ant.LastPath[ant.LastPath.Count - 1];

                    for (int i = 0; i < ant.LastPath.Count; i++)
                    {
                        if (i == ant.LastPath.Count - 1) break;

                        var city = ant.LastPath[i];
                        var nextCity = ant.LastPath[i + 1];

                        // on the first iteration adds pheromone value to startPoint -> city
                        if (i == 0)
                        {
                            _pheromonesMap[startingCity, city] += ant.LastPheromoneValue;
                            _pheromonesMap[city, startingCity] += ant.LastPheromoneValue;
                        }

                        // adds pheromones to city -> next city
                        _pheromonesMap[city, nextCity] += ant.LastPheromoneValue;
                        _pheromonesMap[nextCity, city] += ant.LastPheromoneValue;
                    }
                }

                counter++;
                if (crutch)
                {
                    Console.WriteLine( $"{iteration + 1} - Length: {BestPathLength}");
                }
                if (counter == 9)
                {
                    crutch = false;
                }
                if (counter == 19)
                {
                    counter = -1;

                    Console.WriteLine($"{iteration + 2} - Length: {BestPathLength}");
                }
            }
        }

        private int PathLength(List<int> path) // calculates path length
        {
            var sum = 0;

            var startCity = path[path.Count - 1];
            var currentCity = 0;

            for (int i = 0; i < path.Count; i++)
            {
                if (i == 0)
                {
                    currentCity = startCity;
                }

                sum += _distanceMap[currentCity, path[i]];
                currentCity = path[i];
            }

            return sum;
        }
        #endregion
    }
}