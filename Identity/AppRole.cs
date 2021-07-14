using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Identity
{
    public class AppRole: IdentityRole<int>
    {
        public string rolName { get; set; }
    }
}
