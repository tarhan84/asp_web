using System;
using System.Collections.Generic;

#nullable disable

namespace SmartSense
{
    public partial class Content
    {
        public Content()
        {
            ContentResims = new HashSet<ContentResim>();
        }

        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public int Id { get; set; }

        public virtual ICollection<ContentResim> ContentResims { get; set; }
    }
}
