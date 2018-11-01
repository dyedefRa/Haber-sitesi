using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class HaberTip
    {
        public HaberTip()
        {
            Haber = new HashSet<Haber>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }

        public ICollection<Haber> Haber { get; set; }
    }
}
