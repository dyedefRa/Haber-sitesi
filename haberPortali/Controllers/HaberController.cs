using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haberPortali.Controllers
{
    public class HaberController : Controller
    {
        HBContext ctx = new HBContext();
        // GET: Haber
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Goster(int id)
        {
          var haber=  ctx.Haber.FirstOrDefault(x => x.Id == id);
            return View(haber);
        }

        public ActionResult YorumYap(int id,string txtAdSoyad,string txtEmail,string txtIcerik)
        {
            Yorum yeniYorum = new Yorum();
            yeniYorum.AdSoyad = txtAdSoyad;
            yeniYorum.Email = txtEmail;
            yeniYorum.Icerik = txtIcerik;
            yeniYorum.HaberId = id;
            yeniYorum.Baslik = "";
            yeniYorum.Ip = Request.ServerVariables["REMOTE_ADDR"];
            yeniYorum.Onayli = true;

            ctx.Yorum.Add(yeniYorum);
            ctx.SaveChanges();
            return RedirectToAction("Goster",new  {id =id });
        }

        public int YorumBegen(int id)
        {
            Yorum yrm = ctx.Yorum.FirstOrDefault(x => x.Id == id);
            yrm.Begeni++;
            ctx.SaveChanges();
            return (int)yrm.Begeni -(int)yrm.Tiksinti;
        }
        public int YorumTiksin(int id)
        {
            Yorum yrm = ctx.Yorum.FirstOrDefault(x => x.Id == id);
            yrm.Tiksinti++;
            ctx.SaveChanges();
            return Convert.ToInt32(yrm.Begeni) - Convert.ToInt32(yrm.Tiksinti);

        }
    }
}