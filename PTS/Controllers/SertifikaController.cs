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
    public class SertifikaController : Controller
    {
        // GET: Sertifika
        PROJE db = new PROJE();
        public ActionResult Index(string arama)
        {
            List<SERTIFIKA> liste = db.SERTIFIKAs.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.SERTIFIKAs.ToList();
            }
            else
            {
                liste = db.SERTIFIKAs.Where(s => s.SERTIFIKA_ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);

        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                SERTIFIKA s = db.SERTIFIKAs.Find(id);
                if (s!= null)//id kullanıcılarda varsa
                {
                    db.SERTIFIKAs.Remove(s);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            SERTIFIKA s = new SERTIFIKA();
            if (id != null)
            {
                s = db.SERTIFIKAs.Find(id);
                if (s == null)
                {
                    s = new SERTIFIKA();
                }
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();
            return View(s);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(SERTIFIKA s)
        {

            if (ModelState.IsValid)
            {
                if (s.SERTIFIKA_REFNO == 0)
                {
                    db.SERTIFIKAs.Add(s);
                }
                else
                {
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();
            return View(s);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Sertifika", new { arama = txtAra });
        }
    }
}