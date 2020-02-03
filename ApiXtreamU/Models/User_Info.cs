using System;
using System.Collections.Generic;

namespace ApiXtream.Models
{
    public class User_Info
    {
        public string username { get; set; }
        public string password { get; set; }
        public int auth { get; set; }
        public string status { get; set; }
        public string exp_date { get; set; }

        public DateTime? expirationDate { get { return exp_date?.UnixTimeStampToDateTime(); } }

        public string is_trial { get; set; }
        public bool isTrial { get { return !string.IsNullOrEmpty(is_trial) && active_cons.Equals("1"); } }
        public string active_cons { get; set; }

        public bool active { get { return !string.IsNullOrEmpty(active_cons) && active_cons.Equals("1"); } }

        public string created_at { get; set; }
        public DateTime? createdDate { get { return created_at?.UnixTimeStampToDateTime(); } }

        public string max_connections { get; set; }
        public List<string> allowed_output_formats { get; set; }
    }
}
