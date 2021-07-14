using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Models
{
    public class ClientModel 
    {
        [Required]
        public int id { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public string IPaddress { get; set; }
        public string Os { get; set; }
        public string userID { get; set; }
        public DateTime date { get; set; }

    }
}
