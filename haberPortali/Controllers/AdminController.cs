using haberPortali.App_Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace haberPortali.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {



        HBContext ctx = new HBContext();
        // GET: Admin
        public ActionResult Index()
        {
           
            return View();
        }
       
        public ActionResult UyeEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeEkle(Kullanici temp)
        {
            MembershipCreateStatus durum;
            try
            {
                MembershipUser sonuye = Membership.CreateUser(temp.kullaniciAdi, temp.parola, temp.email, temp.gizliSoru, temp.gizliCevap, true, out durum);
                Roles.AddUserToRole(sonuye.UserName, "Uye");
                if (durum == MembershipCreateStatus.Success)
                {
                 
                    return RedirectToAction("UyeListele");
                }
            }
            catch 
            {

              
            }
            ViewBag.sorun = "Kayıt işlemi başarısız.Lüften bilgilerin doğruluğundan emin olunuz.";
            return View();


        }

        public  ActionResult UyeListele()
        {
          MembershipUserCollection muc= Membership.GetAllUsers();
        
            return View(muc);
        }

        public ActionResult RolEkle()
        {
            return View();
        }
        public string RolEkleme(string id)
        {
            try
            {
                Roles.CreateRole(id);

                return "basarili";

            }
            catch 
            {

                return "basarisiz";
            }
        }
        public ActionResult RolListele()
        {
           var rollistesi=  ctx.AspnetRoles.ToList();
           
            return View(rollistesi);
        }
        public string RolSil(string id)
        {
            try
            {
                ctx.AspnetRoles.Remove(ctx.AspnetRoles.FirstOrDefault(x => x.RoleName == id));
                ctx.SaveChanges();
                return "basari";
            }
            catch
            {

                return "sorunlu";
            }
           
        }

        public ActionResult RolDuzenle(string id)
        {
            
            return View(ctx.AspnetRoles.FirstOrDefault(x => x.RoleName == id));
        }

        public string RolDuzenlee(string eski,string yeni)
        {

            try
            {
                AspnetRoles rol = ctx.AspnetRoles.FirstOrDefault(x => x.RoleName == eski);
                rol.RoleName = yeni;
                ctx.SaveChanges();
                return "basari";

            }
            catch 
            {

                return "basarisiz";
            }
    
        }

        public ActionResult RolAta(string id)
        {
            string[] rolleri = Roles.GetRolesForUser(id);
            ViewBag.roller = ctx.AspnetRoles.Where(x => !rolleri.Contains(x.RoleName)).ToList();
            var kullanici = ctx.AspnetUsers.FirstOrDefault(x => x.UserName == id);
            return View(kullanici);
        }

        [HttpPost]
        public ActionResult RolAta(string kullaniciAdi,string rolAdi)
        {
            try
            {
                Roles.AddUserToRole(kullaniciAdi, rolAdi);
            }
            catch 
            {

             
            }
            return RedirectToAction("UyeListele");
        }

        public ActionResult HaberEkle()
        {
            ViewBag.KategoriId = new SelectList(ctx.Kategori, "Id", "Adi");
            ViewBag.TipId = new SelectList(ctx.HaberTip, "Id", "Adi");
            if (Session["haberEklemeDonutu"]!=null)
            {
                string asd= Session["haberEklemeDonutu"].ToString();
            }
            return View();
        }

        public (string,string) ResimKaydet(HttpPostedFileBase temp)
        {
          
                Image orj = Image.FromStream(temp.InputStream);

                Bitmap kck = new Bitmap(orj, Setting.KucukResimWH());
                Bitmap byk = new Bitmap(orj, Setting.BuyukResimWH());
                string dosyaAdi = Guid.NewGuid() + Path.GetExtension(temp.FileName);
               
                kck.Save(Server.MapPath("/Content/images/Resim/Kucuk/" + dosyaAdi));
                orj.Save(Server.MapPath("/Content/images/Resim/Buyuk/" + dosyaAdi));
            string x1 = "/Content/images/Resim/Kucuk/" + dosyaAdi;
            string x2 = "/Content/images/Resim/Buyuk/" + dosyaAdi;

            return (x1,x2);
        }


        //BURASI COK BASARILI

        public  string ResimYukleUrlAl(HttpPostedFileBase temp, Size boyut, string ContentAltiDosyaAdi)
        {
            Image orj = Image.FromStream(temp.InputStream);
            Bitmap bm = new Bitmap(orj, boyut);
            string dosyaAdi = Path.GetFileNameWithoutExtension(temp.FileName) + Guid.NewGuid() + Path.GetExtension(temp.FileName);
            string yol = $"/Content/images/" + ContentAltiDosyaAdi + "/" + dosyaAdi;

            bm.Save(Server.MapPath(yol));
            return yol;



        }

        public string VideoKaydet(HttpPostedFileBase temp)
        {
            string yol = Path.GetFileNameWithoutExtension(temp.FileName) + Guid.NewGuid() + Path.GetExtension(temp.FileName);
            FileStream fs = new FileStream(Server.MapPath("/Content/Videos/" + yol), FileMode.Create);
            byte[] buffer = new byte[temp.ContentLength];
            temp.InputStream.Read(buffer, 0, temp.ContentLength);
            fs.Write(buffer, 0, temp.ContentLength);
            fs.Close();
            return yol;
        }
       

        [HttpPost]
        public ActionResult HaberEkle(Haber temp,HttpPostedFileBase Resim,string Video)
        {

            try
            {
                if ((temp.Baslik != null && temp.Icerik != null && temp.Ozet != null && Resim!=null))
                {

                    temp.YazarId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                    //Guid onlineId = ctx.AspnetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name).UserId;
                    //temp.YazarId = onlineId;
                    if (Resim != null)
                    {

                        temp.KucukResimYol = ResimKaydet(Resim).Item1;
                        temp.ResimYol = ResimKaydet(Resim).Item2;
                    }
                    if (Video != null  /*Video.ContentLength > 0*/)
                    {
                        //temp.VideoYol = VideoKaydet(Video);
                        temp.VideoYol = Video;

                    }
                    ctx.Haber.Add(temp);
                    ctx.SaveChanges();
                    Session["haberEklemeDonutu"] = "Haber başarıyla eklendi.";
                }
                else
                {
                    Session["haberEklemeDonutu"] = "Haber eklenemedi.. Lütfen bilgilerinizi kontrol ediniz.(Video hariç her alan kesinlikle doldurulmalıdır.)";
                }

            }
            catch (Exception)
            {

                Session["haberEklemeDonutu"] = "Haber eklenemedi.. Lütfen bilgilerinizi kontrol ediniz.(Video hariç her alan kesinlikle doldurulmalıdır.)";
            }
            
            return RedirectToAction("HaberEkle");
        }


        public ActionResult HaberListele()
        {
            return View(ctx.Haber.ToList());
        }


        public ActionResult KategoriListele()
        {
            return View(ctx.Kategori.ToList());
        }


        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori temp, HttpPostedFileBase resimcik)
        {


            try
            {
                if (resimcik == null || temp == null)
                {

                    Session["kategoriCevap"] = "Kategori eklerken sorun oluştu";
                    return View();
                }
                else
                {
                    Image resim = Image.FromStream(resimcik.InputStream);

                    Bitmap bm = new Bitmap(resim, Setting.KucukResimWH());
                    string ad = Path.GetFileNameWithoutExtension(resimcik.FileName) + Guid.NewGuid() + Path.GetExtension(resimcik.FileName);
                    string yol = "/Content/images/KategoriResim/" + ad;
                    bm.Save(Server.MapPath(yol));
                    temp.ResimYol = yol;

                    ctx.Kategori.Add(temp);
                    ctx.SaveChanges();

                    Session["kategoriCevap"] = "Kategori başarıyla eklendi";
                    return RedirectToAction("KategoriEkle");
                }

            }
            catch 
            {
                Session["kategoriCevap"] = "Kategori eklerken sorun oluştu";
                return View();

            }

           
                 
            
        }

        public ActionResult KategoriDuzenle (int id)
        {
            return View(ctx.Kategori.FirstOrDefault(x=>x.Id==id));

        }

        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori temp,HttpPostedFileBase resimcikk)
        {
            try
            {
                Kategori updateKategori = ctx.Kategori.FirstOrDefault(x => x.Id == temp.Id);
                if (resimcikk!=null)
                {
                    updateKategori.ResimYol = ResimYukleUrlAl(resimcikk, Setting.KucukResimWH(), "KategoriResim");
                   
                }
                if (temp.Adi!=null)
                {
                    updateKategori.Adi = temp.Adi;

                }
                if (temp.Aciklama != null)
                {
                    updateKategori.Aciklama = temp.Aciklama;

                }
                ctx.Kategori.Update(updateKategori);
                ctx.SaveChanges();
                Session["kategoriDuzenleCevap"] = $"{updateKategori.Id} numaralı kategori başarıyla duzenlendi";
                return RedirectToAction("KategoriDuzenle", new { id = updateKategori.Id }); 
            }
            catch 
            {
                Session["kategoriDuzenleCevap"] = $"{temp.Id} numaralı kategorinin duzenlenmesinde sorun oluştu";
                return View("KategoriDuzenle", new { id = temp.Id });
               
            }
        }
    }
}