using System;
using System.Linq;
using System.Collections.Generic;
using Lab2.Models;
using Lab2.Database;

namespace Lab2
{
    class Program
    {
        private static Random random => new Random();

        static void Main(string[] args)
        {
            var path = @"D:\KPI\AnDS\Lab2_Data";

            var context = new DbContext<Player>(path);

            Randomize(ref context);

            // . . .

            System.Console.WriteLine(context.Select(1));
            System.Console.WriteLine(context.Comparisons);
        }

        static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void Shuffle(IList<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    
        static void Randomize(ref DbContext<Player> context)
        {
            List<int> randomKeys = new List<int>();

            for (int i = 0; i < 6500; i++)
            {
                randomKeys.Add(i);
            }

            Shuffle(randomKeys);

            for (int i = 0; i < 6500; i++)
            {
                var player = new Player
                {
                    Id = randomKeys[i],
                    Nickname = RandomString(10),
                    Level = random.Next(5, 100),
                    Money = random.Next(0, 1000000),
                    Clan = RandomString(20),
                };
                var succ = context.Insert(player);
                // System.Console.WriteLine($"#{i + 1} = {succ} - {player.Id}");
            }
        }
    }
}
