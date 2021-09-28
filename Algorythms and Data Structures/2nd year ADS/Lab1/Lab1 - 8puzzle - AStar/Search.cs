using System.Collections.Generic;
using System.Linq;

namespace Lab1_2
{
    class Search
    {
        public List<Node> PathToSolution;

        public Search()
        {
            this.PathToSolution = new List<Node>();
        }

        public bool AStar(Node root)
        {
            var openList = new List<Node>();
            var closedList = new List<Node>();

            openList.Add(root);

            var goalFound = false;

            while (openList.Count > 0 && !goalFound)
            {
                openList = openList.OrderBy(n => n.F).ToList();
                
                var currentNode = openList[0];
                closedList.Add(openList[0]);
                openList.RemoveAt(0);

                currentNode.Expand();

                foreach (var child in currentNode.Children)
                {
                    if (child.IsReachedGoal())
                    {
                        goalFound = true;

                        PathTrace(ref PathToSolution, child);

                        return true;
                    }

                    if (!Contains(openList, child) && !Contains(closedList, child))
                    {
                        openList.Add(child);
                    }
                }
            }

            return false;
        }

        public void PathTrace(ref List<Node> path, Node childNode)
        {
            var currentNode = childNode;

            path.Add(currentNode);

            while(currentNode.Parent != null)
            {
                 currentNode = currentNode.Parent;
                 path.Add(currentNode);
            }
        }

        public static bool Contains(List<Node> list, Node childNode)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsSamePuzzle(childNode.Puzzle)) return true;
            }
            return false;
        }
    }
}