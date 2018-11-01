using System;
using System.Collections.Generic;

namespace EntitiyTempp
{
    public partial class Kategori
    {
        public Kategori()
        {
            Anket = new HashSet<Anket>();
            Haber = new HashSet<Haber>();
            InverseUstKategori = new HashSet<Kategori>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public string ResimYol { get; set; }
        public int? UstKategoriId { get; set; }

        public Kategori UstKategori { get; set; }
        public ICollection<Anket> Anket { get; set; }
        public ICollection<Haber> Haber { get; set; }
        public ICollection<Kategori> InverseUstKategori { get; set; }
    }
}
