using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class Etiket
    {
        public Etiket()
        {
            HaberEtiket = new HashSet<HaberEtiket>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }

        public ICollection<HaberEtiket> HaberEtiket { get; set; }
    }
}
