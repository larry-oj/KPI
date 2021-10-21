using System;
using Lab2.Database.Models;
using System.Text.Json.Serialization;

namespace Lab2.Models
{
    public class Player : IDataModel, ICloneable
    {
        [JsonIgnore]
        public int Key {
            get => Id;
            set => Id = value;
        }
        
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }
        
        [JsonPropertyName("level")]
        public int Level { get; set; }
        
        [JsonPropertyName("money")]
        public int Money { get; set; }
        
        [JsonPropertyName("clan")]
        public string Clan { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}