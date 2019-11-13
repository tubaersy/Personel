using PTS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PTS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        PROJE db = new PROJE();
        public ActionResult Index()
        {

            var liste = db.PERSONELs.Where(a => a.DOGUM_GUNU_TARIHI != null).
                 OrderBy(a => EntityFunctions.DiffDays(DateTime.Today, EntityFunctions.AddYears(a.DOGUM_GUNU_TARIHI, EntityFunctions.DiffYears(a.DOGUM_GUNU_TARIHI, DateTime.Today) +
                 ((a.DOGUM_GUNU_TARIHI.Month < DateTime.Today.Month ||
                 (a.DOGUM_GUNU_TARIHI.Day <= DateTime.Today.Day && a.DOGUM_GUNU_TARIHI.Month == DateTime.Today.Month)) ? 1 : 0)))).Take(3).ToList();
            ViewData["yuksek"] = db.EGITIMs.Where(s => s.OGRETIM_TIPI == "Yüksek Lisans").Count();
            ViewData["lisans"] = db.EGITIMs.Where(s => s.OGRETIM_TIPI == "Lisans").Count();
            ViewData["onlisans"] = db.EGITIMs.Where(s => s.OGRETIM_TIPI == "Ön Lisans").Count();
            ViewData["duyuru"] = db.DUYURUs.ToList();
            return View(liste);
        }

        public ActionResult GetData()
        {
            int yazilim = db.PERSONELs.Where(x => x.DEPARTMAN_REFNO == 1).Count();
            int muhasebe = db.PERSONELs.Where(x => x.DEPARTMAN_REFNO == 2).Count();
            int idari = db.PERSONELs.Where(x => x.DEPARTMAN_REFNO == 3).Count();
            Ratio obj = new Ratio();
            obj.Yazılım = yazilim;
            obj.Muhasebe = muhasebe;
            obj.İdari = idari;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Yazılım { get; set; }
            public int Muhasebe { get; set; }
            public int İdari { get; set; }
        }
        public ActionResult Duyuru()
        {
            return RedirectToAction("Create", "DUYURU");
        }
        public ActionResult DuyuruSil()
        {
            return RedirectToAction("Index", "DUYURU");
        }
    }
}