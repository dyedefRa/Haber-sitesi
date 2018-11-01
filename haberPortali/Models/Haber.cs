using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace haberPortali
{
    public partial class Haber
    {
        public Haber()
        {
            HaberEtiket = new HashSet<HaberEtiket>();
            Resim = new HashSet<Resim>();
            Yorum = new HashSet<Yorum>();
        }
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Baslik alanı gereklidir.")]
        public string Baslik { get; set; }
        [Required(ErrorMessage = "Ozet alanı gereklidir.")]
        public string Ozet { get; set; }
        [Required(ErrorMessage = "Içerik alanı gereklidir.")]
        public string Icerik { get; set; }
        public DateTime? YayimTarihi { get; set; }
        public Guid? YazarId { get; set; }
        public int KategoriId { get; set; }
        public int TipId { get; set; }
        [Required(ErrorMessage = "Resim yükleme alanı gereklidir.")]
        public string ResimYol { get; set; }
        public string KucukResimYol { get; set; }
        public int? Goruntulenme { get; set; }
        public string VideoYol { get; set; }

        public Kategori Kategori { get; set; }
        public HaberTip Tip { get; set; }
        public AspnetUsers Yazar { get; set; }
        public ICollection<HaberEtiket> HaberEtiket { get; set; }
        public ICollection<Resim> Resim { get; set; }
        public ICollection<Yorum> Yorum { get; set; }
    }
}
