using System;
namespace ApiXtreamU.Models
{
    public class Live
    {
        public string category_id { get; set; }
        public int categoryId { get { return Convert.ToInt32(category_id); } }
        public string category_name { get; set; }
        public int parent_id { get; set; }
    }
}
