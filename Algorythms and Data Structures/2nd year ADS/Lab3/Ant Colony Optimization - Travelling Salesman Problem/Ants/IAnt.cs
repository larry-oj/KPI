using System.Collections.Generic;

namespace Lab3_1.Ants
{
    /// Used as a type to store both <see cref="Ant"> and <see cref="WildAnt"> in one collection
    public interface IAnt
    {
        double LastPheromoneValue { get; }
        List<int> LastPath { get; }
        int Lmin { get; }
        
        void Work(int startPoint);
    }
}