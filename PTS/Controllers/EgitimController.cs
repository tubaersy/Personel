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
    public class EgitimController : Controller
    {
        PROJE db = new PROJE();
        // GET: Egitim
        public ActionResult Index(string arama)
        {
            List<EGITIM> liste = db.EGITIMs.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.EGITIMs.ToList();
            }
            else
            {
                liste = db.EGITIMs.Where(s => s.UNIVERSITE.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                EGITIM e = db.EGITIMs.Find(id);
                if (e != null)//id kullanıcılarda varsa
                {
                    db.EGITIMs.Remove(e);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            EGITIM e = new EGITIM();
            if (id != null)
            {
                e = db.EGITIMs.Find(id);
                if (e == null)
                {
                    e = new EGITIM();
                }
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(e);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(EGITIM e)
        {
            if (ModelState.IsValid)
            {
                if (e.EGITIM_REFNO == 0)
                {
                    db.EGITIMs.Add(e);
                }
                else
                {
                    db.Entry(e).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(e);//model binding yapıyoruz.

        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Egitim", new { arama = txtAra });
        }
    }
}
