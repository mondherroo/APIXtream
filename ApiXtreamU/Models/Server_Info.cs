using System;

namespace ApiXtreamU.Models
{
    public class Server_Info
    {
        public string url { get; set; }
        public string port { get; set; }
        public string https_port { get; set; }
        public string server_protocol { get; set; }
        public string rtmp_port { get; set; }
        public string timezone { get; set; }
        public DateTime time_now { get; set; }
    }
}
