using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "eposta alanı boş bırakılamaz!")]
        [EmailAddress(ErrorMessage ="Geçerli bir eposta adresi girin!")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string sifre { get; set; }
    }
}
