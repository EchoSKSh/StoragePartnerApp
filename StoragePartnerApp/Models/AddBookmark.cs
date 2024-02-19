using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePartnerApp.Models
{
    public class AddBookmark
    {
        [JsonProperty("user_Id")]
        public int UserId { get; set; }
        [JsonProperty("propertyId")]
        public int PropertyId { get; set; }
    }
}
