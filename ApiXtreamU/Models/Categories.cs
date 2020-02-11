using System.Collections.Generic;

namespace ApiXtreamU.Models
{
    public class Categories
    {
        public List<Serie> serie { get; set; }
        public List<Movie> movie { get; set; }
        public List<Live> live { get; set; }
    }
}
