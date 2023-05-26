using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTGCollectionManager.Models.API
{
    public class MTGAPImodel
    {
        /*[JsonProperty("name")]*/
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /*[JsonProperty("manaCost")]*/
        [JsonPropertyName("manaCost")]
        public string ManaCost { get; set; }


       /*[JsonProperty("colors")]*/
       [JsonPropertyName("colors")]
        public List<string> Colors { get; set; }

        /*[JsonProperty("type")]*/
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /*[JsonProperty("rarity")]*/
        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }

        /*[JsonProperty("set")]*/
        [JsonPropertyName("set")]
        public string Set { get; set; }

        /*[JsonProperty("setName")]*/
        [JsonPropertyName("setName")]
        public string SetName { get; set; }

        /*[JsonProperty("text")]*/
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /*[JsonProperty("number")]*/
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /*[JsonProperty("power")]*/
        [JsonPropertyName("power")]
        public string Power { get; set; }

        /*[JsonProperty("toughness")]*/
        [JsonPropertyName("toughness")]
        public string Toughness { get; set; }

        /*[JsonProperty("layout")]*/
        [JsonPropertyName("layout")]
        public string Layout { get; set; }

        /*[JsonProperty("imageUrl")]*/
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        /*[JsonProperty("originalText")]*/
        [JsonPropertyName("originalText")]
        public string OriginalText { get; set; }

       /* [JsonProperty("id")]*/
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
