using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Models
{
    public class Bookmark
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("user_Id")]
        public int UserId { get; set; }
        [JsonProperty("propertyId")]
        public int PropertyId { get; set; }
    }
}
