using Newtonsoft.Json;
using StoragePartnerApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Models
{
    public class ReservationList
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("statusId")]
        public int StatusId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("ownerId")]
        public int OwnerId { get; set; }
        [JsonProperty("storageId")]
        public int StorageId { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
