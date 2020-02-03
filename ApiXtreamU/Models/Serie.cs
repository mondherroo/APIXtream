using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXtreamU.Models
{
    public class Serie
    {
        public string category_id { get; set; }
        public int categoryId { get { return Convert.ToInt32(category_id); } }
        public string category_name { get; set; }
        public int parent_id { get; set; }
    }
}
