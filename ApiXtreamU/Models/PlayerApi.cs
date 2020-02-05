using ApiXtream.Models;
using Newtonsoft.Json;

namespace ApiXtreamU.Models
{
    public class PlayerApi
    {
        [JsonProperty("user_info")]
        public User_Info User_info { get; set; }
        [JsonProperty("server_info")]
        public Server_Info Server_info { get; set; }
    }
}
