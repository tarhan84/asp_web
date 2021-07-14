using System;
using System.Collections.Generic;

#nullable disable

namespace SmartSense
{
    public partial class Mesajlar
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public int Okundu { get; set; }
        public string Tarih { get; set; }
    }
}
