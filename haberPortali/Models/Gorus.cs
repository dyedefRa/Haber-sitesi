using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class Gorus
    {
        public Gorus()
        {
            Yorum = new HashSet<Yorum>();
        }

        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public DateTime? GorusTarihi { get; set; }
        public Guid YazarId { get; set; }
        public int? Begeni { get; set; }
        public int? Tiksinti { get; set; }

        public AspnetUsers Yazar { get; set; }
        public ICollection<Yorum> Yorum { get; set; }
    }
}
