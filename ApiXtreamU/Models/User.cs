using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtream.Models
{
    public class User
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }
        [JsonProperty("Username")]
        //[Required]
        public string Username { get; set; }
        [JsonProperty("Password")]
        //[Required]
        public string Password { get; set; }
        [JsonProperty("Reseller")]
        //[Required]
        public string Reseller { get; set; }

        //[Required]
        public bool Status { get; set; }

        //[Required]
        public bool Online { get; set; }

        //[Required]
        public bool Trial { get; set; }
        [JsonProperty("Expiration")]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [UIHint("{yyyy-MM-dd}")]
        //[Required]
        public DateTime Expiration { get; set; }

        //[Required]
        public int Active { get; set; }
        [JsonProperty("Conns")]
        //[Required]
        public int Conns { get; set; }
        [JsonProperty("AdminNotes")]
        public string AdminNotes { get; set; }
        [JsonProperty("ResellerNotes")]
        public string ResellerNotes { get; set; }
    }
}
