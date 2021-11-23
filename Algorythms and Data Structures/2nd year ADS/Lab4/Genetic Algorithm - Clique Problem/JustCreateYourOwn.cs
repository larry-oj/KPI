using System.Collections.Generic;
using System;
using Lab4_1.Genetic;

namespace Lab4_1
{
    class JuStCrEaTeYoUrOwN // пидорас 
    {
        public static bool DoShit(List<int> set, Clique clique)
        {
            var size = clique.Matrix.GetLength(0);
            var something = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    something[i,j] = 0;
                }
            }

            var indexes = new List<int>();

            for (int i = 0; i < set.Count; i++)
            {
                if (set[i] == 1)
                {
                    indexes.Add(i);
                }
            }

            for (int i = 0; i < indexes.Count; i++)
            {
                for (int j = i; j < size; j++)
                {
                    something[indexes[i],j] += clique.Matrix[indexes[i],j];
                    something[j,indexes[i]] += clique.Matrix[j,indexes[i]];
                }
                
                
            }

            return /*shit*/ false;
        }
    }
}