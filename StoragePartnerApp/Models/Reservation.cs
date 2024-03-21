using Newtonsoft.Json;

namespace StoragePartnerApp.Models
{
    public class Reservation
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("statusId")]
        public int StatusId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("ownerId")]
        public int OwnerId { get; set; }
        [JsonProperty("storageId")]
        public int StorageId { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
