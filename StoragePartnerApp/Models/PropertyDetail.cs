using Newtonsoft.Json;
using StoragePartnerApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Models
{
    public class PropertyDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string FullImageUrl => AppSettings.BaseApiUrl + ImageUrl;
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("bookmark")]
        public Bookmark Bookmark { get; set; }
    }
}
