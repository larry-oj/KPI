using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab2.Database.Models
{
    public class Index 
    {
        [JsonPropertyName("key")]
        public int Key { get; set; }

        [JsonPropertyName("location")]
        public int Location { get; set; }

        public Index(int key, int location)
        {
            this.Key = key;
            this.Location = location;
        }
    }
}