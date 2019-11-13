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
    public class CalistigiYerController : Controller
    {
        // GET: CalistigiYer
        PROJE db = new PROJE();
        public ActionResult Index(string arama)
        {
            List<CALISTIGI_YER> liste = db.CALISTIGI_YER.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.CALISTIGI_YER.ToList();
            }
            else
            {
                liste = db.CALISTIGI_YER.Where(s => s.ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);

        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                CALISTIGI_YER c = db.CALISTIGI_YER.Find(id);
                if (c != null)//id kullanıcılarda varsa
                {
                    db.CALISTIGI_YER.Remove(c);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            CALISTIGI_YER c = new CALISTIGI_YER();
            if (id != null)
            {
                c = db.CALISTIGI_YER.Find(id);
                if (c == null)
                {
                    c = new CALISTIGI_YER();
                }
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(c);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(CALISTIGI_YER c)
        {

            if (ModelState.IsValid)
            {
                if (c.CALISTIGI_YER_REFNO == 0)
                {
                    db.CALISTIGI_YER.Add(c);
                }
                else
                {
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(c);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "CalistigiYer", new { arama = txtAra });
        }
    }
}