using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Identity
{
    public class User:IdentityUser
    {
        public string ad { get; set; }
        public string soyad { get; set; }
    }
}
