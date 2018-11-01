using System;
using System.Collections.Generic;

namespace EntitiyTempp
{
    public partial class HaberEtiket
    {
        public int HaberId { get; set; }
        public int EtiketId { get; set; }

        public Etiket Etiket { get; set; }
        public Haber Haber { get; set; }
    }
}
