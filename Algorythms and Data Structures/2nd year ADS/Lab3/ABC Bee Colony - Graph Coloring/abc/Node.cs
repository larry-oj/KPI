using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc
{
    public class Node
    {
        public int Number { get; set; }
        public Color The_color { get; set; }
        public int Nectar_weight { get; set; }
        public List<Node> Neighbors { get; set; }
        public enum Color
        {
            Grey,
            Red,
            Green,
            Blue,
            Yellow,
            Pink,
            Viollet,
            Orange,
            Brown,
            Cyan,
            Lilac,
            Magenta,
            White,
            Black,
            Crimson,
            Golden
        }
        public Node(int num, int nectar)
        {
            this.Number = num;
            this.Nectar_weight = nectar;
            Neighbors = new List<Node>();
        }
        public static List<Node> ReadTheMatrix(int[,] matrix)
        {
            List<Node> node_list = new List<Node>();
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i, j] == 1)
                        count++;
                }
                node_list.Add(new Node(i, count));
                count = 0;
            }
            foreach(Node _node in node_list)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, _node.Number] == 1)
                    {
                        foreach(Node _neighbor in node_list)
                        {
                            if (_neighbor.Number == i)
                                _node.Neighbors.Add(_neighbor);
                        }
                    }
                }
            }
            return node_list;
        }
        
        public bool TryColor(ref List<Node.Color> color_list)
        {
            List<Color> banned_colors = new List<Color>();
            foreach (Node _neighbor in this.Neighbors)
            {
                banned_colors.Add(_neighbor.The_color);
            }
            banned_colors.Add(Color.Grey);
            banned_colors = banned_colors.Distinct().ToList();

            int compare_count = 0;
            foreach (Color _color in color_list)
            {
                compare_count = 0;
                foreach (Color banned_color in banned_colors)
                {
                    if (_color == banned_color)
                        compare_count++;
                }
                if (compare_count == 0)
                {
                    this.The_color = _color;
                    return true;
                }
            }

            for (int i = 1; i < Enum.GetNames(typeof(Color)).Length; i++)
            {
                compare_count = 0;
                foreach (Color _color in color_list)
                {
                    if(_color==(Color)i)
                    {
                        compare_count++;
                    }
                }
                if (compare_count == 0)
                {
                    this.The_color = (Color)i;
                    color_list.Add((Color)i);
                    return false;
                }
            }
            return false;
        }

        public static void TheABC(List<Node> node_list)
        {
            Random rnd = new Random();
            int iterations = 0;
            List<Color> used_colors = new List<Color>();
            used_colors.Add(Color.Grey);
            List<Color> chromatic_number = new List<Color>();
            int NectarFullWeight = 0;
            foreach (Node _node in node_list)
            {
                NectarFullWeight += _node.Nectar_weight;
            }

            List<Node> forager = new List<Node>();
            List<Node> scout = new List<Node>();
            int scout_count = 3;
            int forager_count = 32;
            int sum_of_nectars = 0;
            int current_foragers = 0;
            int rnd_hell = 0;
            int gg_nice_count = node_list.Count;
            List<Color> best_result = new List<Color>();
            int best_result_iteration = 0;

            while(NectarFullWeight > 0 && (used_colors.Contains(Color.Grey) || chromatic_number.Count <= used_colors.Count))
            {
                iterations++;
                used_colors = new List<Color>();

                foreach (Node _node in node_list)
                {
                    used_colors.Add(_node.The_color);
                }
                used_colors = used_colors.Distinct().ToList();

                Node find_node = null;
                for (int i = 0; i < scout_count; i++)
                {
                    while (find_node == null)
                    {
                        int key = rnd.Next(0, gg_nice_count);
                        find_node = node_list.Find(x => (x.Number == key));
                        rnd_hell++;
                        if (rnd_hell > 1000)
                            find_node = node_list.FirstOrDefault();
                    }                        
                    scout.Add(find_node);
                    find_node = null;
                }

                foreach (Node _scoat in scout)
                    sum_of_nectars += _scoat.Nectar_weight;

                foreach (Node _scoat in scout)
                {
                    current_foragers = (int)(forager_count * ((double)_scoat.Nectar_weight / sum_of_nectars));
                    if (current_foragers > _scoat.Neighbors.Count)
                        current_foragers = _scoat.Neighbors.Count;
                    _scoat.Nectar_weight -= current_foragers;
                    if (_scoat.Nectar_weight < 0)
                        _scoat.Nectar_weight = 0;
                    NectarFullWeight -= current_foragers;
                    for (int i = 0; i < current_foragers; i++)
                    {
                        _scoat.Neighbors[i].TryColor(ref used_colors);                        
                    }
                    if (_scoat.Nectar_weight == 0)
                    {
                        _scoat.TryColor(ref used_colors);
                        node_list.Remove(_scoat);
                    }
                }

                int another_key = scout.Count;
                for (int i = 0; i < another_key; i++)
                    scout.RemoveAt(0);

                sum_of_nectars = 0;
                chromatic_number = used_colors;
                if(used_colors.Contains(Color.Grey))
                    Console.Write(" <" + chromatic_number.Count + " (" + iterations + ") " + NectarFullWeight + "> ");
                else
                    Console.Write(" [" + chromatic_number.Count + " {" + iterations + "} " + NectarFullWeight + "] ");
                rnd_hell = 0;
                if (chromatic_number.Count < best_result.Count || (best_result.Count == 0 && !used_colors.Contains(Color.Grey)))
                {
                    best_result = chromatic_number;
                    best_result_iteration = iterations;
                }
            }

            used_colors = new List<Color>();

            foreach (Node _node in node_list)
            {
                used_colors.Add(_node.The_color);
            }
            used_colors = used_colors.Distinct().ToList();

            string mmmm = "";
            foreach (Color clr in chromatic_number)
                mmmm += " " + clr.ToString();
            string mmmm_best = "";
            foreach (Color clr in best_result)
                mmmm_best += " " + clr.ToString();
            Console.WriteLine();
            Console.WriteLine("Кiнцеве рiшення: [Кольори: "+ chromatic_number.Count + ": " + mmmm+ " Iтерацiї: " + iterations+"]");
            Console.WriteLine("Найкращий результат: [Кольори: " + best_result.Count + ": " + mmmm_best + " Iтерацiї: " + best_result_iteration + "]");
        }
    }
}
