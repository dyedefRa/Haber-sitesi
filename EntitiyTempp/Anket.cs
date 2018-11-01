using System;
using System.Collections.Generic;

namespace EntitiyTempp
{
    public partial class Anket
    {
        public Anket()
        {
            AnketSecenek = new HashSet<AnketSecenek>();
        }

        public int Id { get; set; }
        public string Baslik { get; set; }
        public int KategoriId { get; set; }
        public int? KatilimciSayisi { get; set; }
        public DateTime? YayimTarihi { get; set; }
        public DateTime? SonOyTarihi { get; set; }
        public bool? AktifMi { get; set; }

        public Kategori Kategori { get; set; }
        public ICollection<AnketSecenek> AnketSecenek { get; set; }
    }
}
