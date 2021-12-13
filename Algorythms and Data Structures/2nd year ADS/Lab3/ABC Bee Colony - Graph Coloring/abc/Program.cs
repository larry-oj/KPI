using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,]  {{0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                                        { 1, 0, 1, 1, 1, 1, 0, 0, 0, 0 },
                                        { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
                                        { 0, 1, 1, 0, 0, 0, 1, 0, 0, 0 },
                                        { 0, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                                        { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0 },
                                        { 0, 0, 0, 1, 1, 0, 0, 1, 0, 1 },
                                        { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1 },
                                        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                                        { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 }};

            int matrix_length = 250;
            int max_degree = 25;
            int full_nectar_weight = matrix_length * max_degree / 10;
            int[,] mat = new int[matrix_length, matrix_length];
            mat = MatrixGenerete(matrix_length, max_degree, full_nectar_weight);

            DrawMatrix(mat, matrix_length);

            List<Node> node_list = new List<Node>();
            node_list = Node.ReadTheMatrix(mat);

            Node.TheABC(node_list);

            Console.ReadKey();
        }
        static int[,] MatrixGenerete(int count, int max_degree, int full_nectar_weight)
        {
            Random position = new Random();
            int[,] matrix = new int[count, count];
            int degree_x = 0;
            int degree_y = 0;
            int x = 0;
            int y = 0;
            for (int i = 0; i < count; i++)
            {
                x = position.Next(0, count);
                y = position.Next(0, count);
                if (x != y)
                {
                    matrix[i, x] = 1;
                    matrix[i, y] = 1;
                    matrix[x, i] = 1;
                    matrix[y, i] = 1;
                }
            }

            for (int i = 0; i < full_nectar_weight; i++)
            {
                degree_x = 0;
                degree_y = 0;
                x = position.Next(0, count);
                y = position.Next(0, count);
                for (int j = 0; j < count; j++)
                {
                    if (matrix[x, j] == 1)
                        degree_x++;
                    if (matrix[j, y] == 1)
                        degree_y++;
                }
                if (degree_x < max_degree && degree_y < max_degree && x != y)
                {
                    matrix[x, y] = 1;
                    matrix[y, x] = 1;
                }
            }

            for (int i = 0; i < count; i++)
            {
                matrix[i, i] = 0;
            }

            return matrix;
        }

        static void DrawMatrix(int[,] mat, int matrix_length)
        {
            for (int i = 0; i < matrix_length; i++)
            {
                for (int j = 0; j < matrix_length; j++)
                {
                    Console.Write(mat[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
