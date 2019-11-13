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
    public class YabanciDilController : Controller
    {
        // GET: YabanciDil
        PROJE db = new PROJE();
        public ActionResult Index(string arama)
        {
            List<YABANCI_DIL> liste = db.YABANCI_DIL.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.YABANCI_DIL.ToList();
            }
            else
            {
                liste = db.YABANCI_DIL.Where(s => s.YABANCI_DIL_ADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);

        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                YABANCI_DIL y = db.YABANCI_DIL.Find(id);
                if (y != null)//id kullanıcılarda varsa
                {
                    db.YABANCI_DIL.Remove(y);
                    db.SaveChanges();
                }
            }

            
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            YABANCI_DIL y = new YABANCI_DIL();
            if (id != null)
            {
                y = db.YABANCI_DIL.Find(id);
                if (y == null)
                {
                    y = new YABANCI_DIL();
                }
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(y);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(YABANCI_DIL y)
        {

            if (ModelState.IsValid)
            {
                if (y.YABANCI_DIL_REFNO == 0)
                {
                    db.YABANCI_DIL.Add(y);
                }
                else
                {
                    db.Entry(y).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");

            ViewData["personel"] = db.PERSONELs.Where(k => k.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();

            return View(y);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "YabanciDil", new { arama = txtAra });
        }
    }
}