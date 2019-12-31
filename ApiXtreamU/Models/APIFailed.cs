using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtreamU.Models
{
    public class APIFailed
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
