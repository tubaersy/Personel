using PTS.Helpers;
using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class HesapAyarlariController : Controller
    {
        PROJE db = new PROJE();
        // GET: HesapAyarlari

        //public ActionResult Index()
        //{

        //    PROJE db = new PROJE();

        //    KULLANICI k = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

        //    PERSONEL p = db.PERSONELs.Where(p1 => p1.KULLANICI_REFNO == k.KULLANICI_REFNO).FirstOrDefault();

        //    return View(k);

        //}

        public ActionResult Create()
        {
            KULLANICI k = SessionHelper<KULLANICI>.GetSessionItem("kullanici");
           
            return View(k);//model binding yapıyoruz.
        }

        [HttpPost]
        public ActionResult Create(KULLANICI k)
        {
            if (ModelState.IsValid)
            {
                if (k.KULLANICI_REFNO == 0)
                {
                    db.KULLANICIs.Add(k);
                }
                else
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index","Home");//listeleme yapılıyor.
            }
            return View(k);//model binding yapıyoruz.
        
        }

    }
}