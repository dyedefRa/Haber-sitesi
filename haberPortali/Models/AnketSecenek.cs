using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class AnketSecenek
    {
        public int Id { get; set; }
        public int AnketId { get; set; }
        public string Metin { get; set; }
        public int OySayisi { get; set; }
        public int Sira { get; set; }

        public Anket Anket { get; set; }
    }
}
