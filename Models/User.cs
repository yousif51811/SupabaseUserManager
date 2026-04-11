using System;
using System.Collections.Generic;
using System.Text;

namespace SupabaseUserManager.Models
{
    class User
    {
        public string UID { get; set; }
        public string Display_Name { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
    }
}
