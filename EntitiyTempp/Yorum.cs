using System;
using System.Collections.Generic;

namespace EntitiyTempp
{
    public partial class Yorum
    {
        public int Id { get; set; }
        public int? HaberId { get; set; }
        public int? GorusId { get; set; }
        public string Baslik { get; set; }
        public string Ip { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public DateTime? Tarih { get; set; }
        public string Icerik { get; set; }
        public bool? Onayli { get; set; }
        public int? Begeni { get; set; }
        public int? Tiksinti { get; set; }

        public Gorus Gorus { get; set; }
        public Haber Haber { get; set; }
    }
}
