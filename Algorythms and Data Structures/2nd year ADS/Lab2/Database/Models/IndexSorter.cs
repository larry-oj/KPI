using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab2.Database.Models
{
    public class IndexSorter : IComparer<Index>
    {
        public int Compare(Index a, Index b)
        {
            if (a.Key == b.Key) return 0;
            else if (a.Key < b.Key) return -1;
            else return 1;
        }
    }
}