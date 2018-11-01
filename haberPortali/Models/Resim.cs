using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class Resim
    {
        public int Id { get; set; }
        public string ResimYol { get; set; }
        public int HaberId { get; set; }
        public string Ozet { get; set; }
        public string KucukYol { get; set; }

        public Haber Haber { get; set; }
    }
}
