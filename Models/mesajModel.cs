using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense.Models
{
    public class MesajModel
    {
        [Required(ErrorMessage = "mail alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage ="geçerli formatta bir adres girin")]
        public String Email { get; set; }
        public String Konu { get; set; }

        [Required(ErrorMessage = "Mesaj alanı boş bırakılamaz")]
        public String Mesaj { get; set; }
    }
}
