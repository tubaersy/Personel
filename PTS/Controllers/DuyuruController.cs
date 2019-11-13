using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        PROJE db = new PROJE();
        // GET: Duyuru
        public ActionResult Index(string arama)
        {
            List<DUYURU> liste = db.DUYURUs.ToList();
            // GET: Admin/Sayfaalr

            if (arama == null)
            {
                arama = "";
                liste = db.DUYURUs.ToList();
            }
            else
            {
                liste = db.DUYURUs.Where(s => s.DUYURU_BASLIK.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            return View(liste);

        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                DUYURU d = db.DUYURUs.Find(id);
                if (d != null)//id kullanıcılarda varsa
                {
                    db.DUYURUs.Remove(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            DUYURU d = new DUYURU();
            if (id != null)
            {
                d = db.DUYURUs.Find(id);
                if (d == null)
                {
                    d = new DUYURU();
                }
            }
            return View(d);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(DUYURU sayfa)
        {
            if (ModelState.IsValid)
            {
                if (sayfa.DUYURU_REFNO == 0)
                {
                    sayfa.DUYURU_ICERIK = HttpUtility.HtmlDecode(sayfa.DUYURU_ICERIK);
                    db.DUYURUs.Add(sayfa);
                }
                else
                {
                    sayfa.DUYURU_ICERIK = HttpUtility.HtmlDecode(sayfa.DUYURU_ICERIK);
                    db.Entry(sayfa).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sayfa);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Duyuru", new { arama = txtAra });
        }
    }
}