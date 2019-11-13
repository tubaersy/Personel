using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class YazilimGrafikController : Controller
    {
        // GET: YazilimGrafik
        PROJE db = new PROJE();
        public ActionResult Index()
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "YazilimGrafik").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            var liste = db.PERSONELs.Where(x => x.DEPARTMAN_REFNO == 1).ToList();

            return View(liste);
        }
        public ActionResult GetData()
        {
            var data = db.PERSONELs.Include("Maas").Where(t => t.DEPARTMAN_REFNO == 1)
                   .GroupBy(p => p.ADI_SOYADI)
                   .Select(g => new {
                       name = g.Key,
                       count = g.Average(w => w.AYLIK_UCRET)
                   }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}