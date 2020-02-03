using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiXtreamU.Models
{
    
    public class Channels
    {

        
        public int num { get; set; }
        public string name { get; set; }
        public string stream_type { get; set; }
        public string type_name { get; set; }
        public string stream_id { get; set; }
        public int streamId => Convert.ToInt32(stream_id);
        public string stream_icon { get; set; }
        public string epg_channel_id { get; set; }
        public string added { get; set; }
        public string category_name { get; set; }
        public string category_id { get; set; }
        public int categoryId => Convert.ToInt32(category_id);
        public string series_no { get; set; }
        public string live { get; set; }

        public MediaType MediaType
        {
            get
            {
                if (string.IsNullOrEmpty(stream_type))
                    return MediaType.LiveTv;
                switch (live)
                {
                    case "Live":
                        return MediaType.LiveTv;
                    case "Vod":
                        return MediaType.Video;
                    default:
                        return MediaType.LiveTv;
                }
            }
        }

        public string container_extension { get; set; }
        public string custom_sid { get; set; }
        public int tv_archive { get; set; }
        public string direct_source { get; set; }
        public int tv_archive_duration { get; set; }
    }

    public enum MediaType : Byte
    {
        LiveTv = 0,
        Radio,
        /// <summary>
        /// video file
        /// </summary>
        Video,
        /// <summary>
        /// audio file
        /// </summary>
        Audio,
        Other
    }
}
