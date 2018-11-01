using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace haberPortali.Controllers
{
    public class GaleriController : Controller
    {
        HBContext ctx = new HBContext();
        // GET: Galeri
        public ActionResult Index()
        {
            var data = ctx.Haber.Where(x => x.Tip.Id == 3).OrderByDescending(x => x.YayimTarihi).ToList();
            return View(data);
        }

        public ActionResult GaleriGoruntule(int id)
        {
            var haber = ctx.Haber.FirstOrDefault(x => x.Id == id);
            return View(haber);
        }
    }
}