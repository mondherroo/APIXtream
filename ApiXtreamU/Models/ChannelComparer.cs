using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtreamU.Models
{
    public class ChannelComparer : IEqualityComparer<Channels>
    {
        public bool Equals(Channels p1, Channels p2)
        {
            return p1.stream_type == p2.stream_type;
        }

        public int GetHashCode(Channels p)
        {
            return p.streamId;
        }
    }
}
