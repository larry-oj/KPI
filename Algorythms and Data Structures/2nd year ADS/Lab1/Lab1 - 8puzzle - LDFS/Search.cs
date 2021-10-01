using System.Collections.Generic;
using System;

namespace Lab1_1
{
    class Search
    {
        public List<Node> PathToSolution { get; set; }
        public int Iterations { get; private set; }
        public int DeadEnds { get; private set; }
        public int States { get; private set; }

        public Search()
        {
            this.PathToSolution = new List<Node>();
            this.Iterations = 0;
            this.DeadEnds = 0;
            this.States = 1;
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
                Iterations++;

                States += node.Children.Count;

                foreach (var child in node.Children)
                {
                    if (LDFS(child, depth + 1, limit))
                    {
                        PathToSolution.Add(node);
                        return true;
                    }
                }
            }
            else
            {
                DeadEnds++;
            }

            return false;
        } 
    }
}