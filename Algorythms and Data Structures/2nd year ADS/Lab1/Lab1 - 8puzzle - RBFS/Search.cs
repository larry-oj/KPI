using System.Net.NetworkInformation;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Lab1_3
{
    class Search
    {
        public List<Node> PathToSolution;

        public Search()
        {
            this.PathToSolution = new List<Node>();
        }

        public bool RBFS(Node node, int fLimit, ref int iterations, ref int deadEnds, ref int states)
        {
            if (node.IsReachedGoal())
            {
                // PathToSolution.Add(node);
                return true;
            }

            node.Expand();
            states += node.Children.Count;

            for (int i = 0; i < node.Children.Count; i++)
            {
                node.Children[i].F = Math.Max(node.Children[i].F, node.F);
            }

            node.Children = node.Children.OrderBy(n => n.F).ToList();
            
            // node.PrintPuzzle();

            var tmp = node.Children.Count;
            for (int i = 0; i < tmp; i++)
            {
                iterations++;
                var bestNode = node.Children[0];
                if (bestNode.F > fLimit) return false;

                Node alternativeNode = null;
                if (node.Children.Count > 1) alternativeNode = node.Children[1];

                var result = RBFS(bestNode, alternativeNode == null ? fLimit : Math.Min(fLimit, alternativeNode.F), ref iterations, ref deadEnds, ref states);

                if (result)
                {
                    PathToSolution.Add(bestNode);
                    return true;
                }
                else
                {
                    deadEnds++;
                }
                node.Children.RemoveAt(0);
            }
            return false;
        }

        // public Node RBFS(Node node, int fLimit)
        // {
        //     if (node.IsReachedGoal())
        //     {
        //         return node;
        //     }
        //     else
        //     {
        //         node.Expand();
        //         if (node.Children[0].F > fLimit)
        //         {
        //             return node.Children[0];
        //         }
        //         else
        //         {
        //             closedList.Add(node);
        //             foreach (var child in node.Children)
        //             {
        //                 if (closedList.Contains(child))
        //                 {
        //                     openList.Add(child);
        //                 }
        //             }

        //             openList = openList.OrderBy(n => n.F).ToList();

        //             var bestNode = openList[0];
        //             openList.RemoveAt(0);

        //             var alternativeNode = openList[0];
        //             openList.RemoveAt(0);

        //             while (bestNode.IsReachedGoal())
        //             {
        //                 bestNode = RBFS(bestNode, Math.Min(fLimit, alternativeNode.F));
        //                 openList.Add(bestNode);
        //                 openList = openList.OrderBy(n => n.F).ToList();
        //                 bestNode = openList[0];
        //                 openList.RemoveAt(0);
        //                 alternativeNode = openList[0];
        //                 openList.RemoveAt(0);
        //             }
        //             return bestNode;
        //         }
        //     }
        // }
    }
}
