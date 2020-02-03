using System.Collections.Generic;

namespace ApiXtreamU.Models
{
    public class Categories
    {
        public List<Serie> series { get; set; }
        public List<Movie> movie { get; set; }
        public List<Live> live { get; set; }
    }
}
