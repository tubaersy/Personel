using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class YazilimController : Controller
    {
        // GET: Yazilim
        PROJE db = new PROJE();
        public ActionResult Index()
        {
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