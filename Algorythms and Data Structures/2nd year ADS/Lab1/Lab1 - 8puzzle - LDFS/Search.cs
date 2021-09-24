using System.Collections.Generic;
using System;

namespace Lab1_1
{
    class Search
    {
        public List<Node> PathToSolution;

        public Search()
        {
            this.PathToSolution = new List<Node>();
        }

        public bool LDFS(Node node, int depth, int limit) // LDFS = DLS = Depth-limited search
        {
            if (depth < limit)
            {
                if (node.IsReachedGoal())
                {
                    PathToSolution.Add(node);
                    return true;
                }

                node.Expand();

                foreach (var child in node.Children)
                {
                    if (LDFS(child, depth + 1, limit))
                    {
                        PathToSolution.Add(node);
                        return true;
                    }
                }
            }

            return false;
        } 
    }
}