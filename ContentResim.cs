using System;
using System.Collections.Generic;

#nullable disable

namespace SmartSense
{
    public partial class ContentResim
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ContentId { get; set; }

        public virtual Content Content { get; set; }
    }
}
