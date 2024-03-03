using Newtonsoft.Json;
using StoragePartnerApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Models
{
    public class TrendingProperty
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.BaseApiUrl + ImageUrl;
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("isTrending")]
        public bool IsTrending { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("bookmarks")]
        public object Bookmark { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
    }
}
