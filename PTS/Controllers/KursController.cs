
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
    public class KursController : Controller
    {
        // GET: Kurs
        PROJE db = new PROJE();
        public ActionResult Index(string arama)
        {
            List<KUR> liste = db.KURS.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.KURS.ToList();
            }
            else
            {
                liste = db.KURS.Where(s => s.KURS_ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);

        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                KUR k = db.KURS.Find(id);
                if (k != null)//id kullanıcılarda varsa
                {
                    db.KURS.Remove(k);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            KUR k = new KUR();
            if (id != null)
            {
                k = db.KURS.Find(id);
                if (k == null)
                {
                    k = new KUR();
                }
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(s => s.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();
            return View(k);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(KUR k)
        {

            if (ModelState.IsValid)
            {
                if (k.KURS_REFNO == 0)
                {
                    db.KURS.Add(k);
                }
                else
                {
                    db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(s => s.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();
            return View(k);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Kurs", new { arama = txtAra });
        }
    }
}