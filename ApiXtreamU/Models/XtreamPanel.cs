using ApiXtream.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ApiXtreamU.Models
{
    public class XtreamPanel
    {
        [JsonProperty("user_info")]
        public User_Info User_info { get; set; }
        [JsonProperty("server_info")]
        public Server_Info Server_info { get; set; }
        [JsonProperty("categories")]
        public Categories Categories { get; set; }
        // [JsonConverter(typeof(CustomConverter))]
        //[JsonProperty("available_channels")]
        //[JsonProperty]
        //public List<Channels> Available_Channels { get; set; }
        [JsonProperty("available_channels")]
        //public Dictionary<string, Channels> Channels { get; set; }

        //public List<Channels> Available_Channels => Channels.Where(kvp => kvp.Value.stream_type == "live").Select(k => k.Value).ToList();
        
        public Dictionary<string, Channels> Channels;

        public List<KeyValuePair<string, Channels>> Available_Channels
        {            
            get { return Channels.Where(kvp => kvp.Value.stream_type.Contains("live")).Union(Available_Movies.TakeLast(1097)).ToList(); }
            set { Channels = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        [JsonProperty("available_movies")]
        public Dictionary<string, Channels> Movies;

        public List<KeyValuePair<string, Channels>> Available_Movies
        {
            get { return Channels.Where(kvp => kvp.Value.stream_type.Contains("movie")).ToList(); }
            set { Channels = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        [JsonProperty("available_series")]
        public Dictionary<string, Channels> Series;

        public List<KeyValuePair<string, Channels>> Available_Series
        {
            get { return Channels.Where(kvp => kvp.Value.stream_type.Contains("series")).ToList(); }
            set { Channels = value.ToDictionary(x => x.Key, x => x.Value); }
        }

        public string GenerateUrlFrom(Channels channel, string protocol = "http", string outputFormat = "ts")
         {
             if (channel == null)
                 return string.Empty;

             return $"{protocol}://{Server_info.url}:{Server_info.port}/live/{User_info.username}/{User_info.password}/{channel.stream_id}.{outputFormat}";
         }
        /*public class CustomConverter : JsonConverter<List<Channels>>
        {
            public override List<Channels> ReadJson(JsonReader reader, Type objectType, List<Channels> existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<Channels>>();
                }
                return null;
            }

            public override void WriteJson(JsonWriter writer, List<Channels> value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }*/
    }
}
