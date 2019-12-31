using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtreamU.Models
{
    public class ApiSuccess
    {
        //[JsonProperty("result")]
        public bool Result { get; set; }
        //[JsonProperty("created_id")]
        public int Created_Id { get; set; }
        //[JsonProperty("username")]
        public string Username { get; set; }
        //[JsonProperty("password")]
        public string Password { get; set; }
    }
}
