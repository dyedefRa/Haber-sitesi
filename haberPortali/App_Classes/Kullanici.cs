using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haberPortali.App_Classes
{
    public class Kullanici
    {
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string email { get; set; }
        public DateTime dogumTarihi { get; set; }
        public bool erkekMi { get; set; }
        public string gizliSoru { get; set; }
        public string gizliCevap { get; set; }
        public string kullaniciAdi { get; set; }
        public string parola { get; set; }
        public string parola2 { get; set; }
        public bool beniHatirla { get; set; }
        public bool sozlesmeOlayi { get; set; }
    }
}