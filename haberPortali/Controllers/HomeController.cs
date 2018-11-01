using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haberPortali.Controllers
{
    using haberPortali.App_Classes;
    using System.Web.Security;
   
    
    public class HomeController : Controller
    {
        HBContext ctx = new HBContext();
        
      
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SliderGetir()
        {
         
            List<Haber> data = ctx.Haber.Where(x => x.Tip.Adi == "Manset").OrderByDescending(x => x.YayimTarihi).Take(10).ToList();
           

            return View(data);
        }
        public ActionResult EnSonHaberler()
        {
            var haberler = ctx.Haber.OrderByDescending(x => x.YayimTarihi).Take(7).ToList();
            return View(haberler);
        }
        public ActionResult EglenceGundemi()
        {
            return View();
        }

        public ActionResult block_Video ()
        {
          var videolu=  ctx.Haber.Where(x => x.VideoYol != null).OrderByDescending(x => x.YayimTarihi).Take(6).ToList();

            return View(videolu);
        }
        public ActionResult block_Option()
        {
           var gorusler =ctx.Gorus.OrderByDescending(x => x.GorusTarihi).Take(2).ToList();
            return View(gorusler);
        }
        public ActionResult block_Extra()
        {
            return View();
        }
        public ActionResult block_Anket()
        {
            if (Session["oylanananket"]!=null)
            {
                
                Anket oylanmis = (Anket)Session["oylanananket"];
                return View("block_AnketSonucGetir",oylanmis);
            }
            else
            { 
            HttpCookie cooker = Request.Cookies["anketler"];
            if (cooker == null) cooker = new HttpCookie("anketler");
           

            string anketCookie = cooker.Value;
            if (anketCookie == null)
                anketCookie = "0";
         
            int[] yapilanAnketlerinIDleri = anketCookie.Split(',').Select(x=>Convert.ToInt32(x)).ToArray();
            List<Anket> anketler = ctx.Anket.Where(x => x.AktifMi == true &&x.SonOyTarihi>=DateTime.Now && !yapilanAnketlerinIDleri.Contains(x.Id)).ToList();
            if (anketler.Any())
            {
                Random rand = new Random();

                return View(anketler[rand.Next(0, anketler.Count)]);
            }
            return View("AnketYok");
            }
        }

        public ActionResult OyVer(int id)
        {
            
                int secenekID = Convert.ToInt32(Request.Form["choice"]);
                AnketSecenek anketSecenek = ctx.AnketSecenek.FirstOrDefault(x => x.Id == secenekID);
            Anket anket = ctx.Anket.FirstOrDefault(x => x.Id == id);
            if (anketSecenek!=null)
            {
                anketSecenek.OySayisi++;
            
               
                anket.KatilimciSayisi++;
                ctx.SaveChanges();
            }



            HttpCookie hc = Request.Cookies["anketler"];
            if (hc != null)
            {
                hc.Value += "," + id.ToString();
               
            }
            else
            {
                hc = new HttpCookie("anketler");
                hc.Value = "0";
                hc.Value += "," + id.ToString();
            }
           Response.Cookies.Add( hc);
            //SUAN SIKINTI YOK 

            //OYLAMA YAPABILDIYSE BUNU BELIRTEBILMEMIZ GEREK
            Session["oylanananket"] = anket;
            return View("Index");

        }
        public ActionResult FotoSliderGetir()
        {
            var fotohaber = ctx.Haber.Where(x => x.TipId == 3 && x.ResimYol!=null).OrderByDescending(x=>x.YayimTarihi).Take(6).ToList();
            return View(fotohaber);
        }
        [AllowAnonymous]
        public ActionResult Giris()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Giris(Kullanici temp,string ReturnUrl="")
        {
            if (Membership.ValidateUser(temp.kullaniciAdi,temp.parola))
            {
                FormsAuthentication.RedirectFromLoginPage(temp.kullaniciAdi, temp.beniHatirla);
                //HttpCookie hc = new HttpCookie("uyeCookie");
                //hc.Expires = DateTime.Now.AddMonths(2);
                //hc.Value = temp.kullaniciAdi;
                HttpContext.Session["uye"] = temp.kullaniciAdi;
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                ViewBag.girisSorun = "Giriş başarısız";
                return View();
            }
          
        }
        [AllowAnonymous]
        public ActionResult HesapOlustur()
        {
           
                return View();
           
           
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult HesapOlustur(Kullanici temp)
        {
            MembershipCreateStatus durum;

            Membership.CreateUser(temp.kullaniciAdi, temp.parola, temp.email, temp.gizliSoru, temp.gizliCevap, true, out durum);

            string mesaj="";

            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += " Geçersiz kullanıcı adı hatası";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += " Geçersiz parola hatası";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += " Geçersiz gizli soru hatası";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += " Geçersiz gizli cevap hatası";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += " Geçersiz email hatası ";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += " Bu kullanıcı adı önceden kullanılmış ";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += " Bu email önceden kullanılmış ";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += " Kullanıcı engel hatası ";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                   mesaj += " Kullanıcı Key Sağlayıcısı  hatası ";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış Kullanıcı Key hatası ";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += " Sağlayıcı hatası ";
                    break;
                default:
                    break;
            }
            ViewBag.mesaj = mesaj;
            if (durum==MembershipCreateStatus.Success)
            {
                return RedirectToAction("Giris", "Home");
            }
            else
               

                return View();
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            Session["uye"] = null;
            return RedirectToAction("Giris", "Home");
        }
    }
}