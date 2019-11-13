using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        PROJE db = new PROJE();
        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "Kullanici").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            List<KULLANICI> liste = db.KULLANICIs.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.KULLANICIs.Count());
                liste = db.KULLANICIs.OrderBy(u => u.KULLANICI_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.KULLANICIs.Where(s => s.KULLANICI_ADI.Contains(arama)).Count());
                liste = db.KULLANICIs.Where(s => s.KULLANICI_ADI.Contains(arama))
                                .OrderBy(u => u.KULLANICI_REFNO)
                                .Skip(aktifsayfa * sayfadakisatirsayisi).
                                 Take(sayfadakisatirsayisi).ToList();

            }
            ViewData["veri"] = arama;
            ViewData["aktifsayfa"] = aktifsayfa;
            return View(liste);

        }

        public void Sayfalama(int satirsayisi)
        {
            int toplamsatir = satirsayisi;
            int toplamsayfa = toplamsatir / sayfadakisatirsayisi;

            if (toplamsatir % sayfadakisatirsayisi != 0)
            {
                toplamsayfa++;
            }
            ViewData["toplamsatir"] = toplamsatir;
            ViewData["toplamsayfa"] = toplamsayfa;
        }

        
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                KULLANICI k = db.KULLANICIs.Find(id);
                if (k != null)//id kullanıcılarda varsa
                {
                    db.KULLANICIs.Remove(k);
                    db.SaveChanges();
                }
            }

    
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            KULLANICI k = new KULLANICI();
            if (id != null)
            {
                k = db.KULLANICIs.Find(id);
                if (k == null)
                {
                    k = new KULLANICI();
                }
            }
            ViewData["yetkigrubu"] = db.YETKI_GRUBU.ToList();
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
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            ViewData["yetkigrubu"] = db.YETKI_GRUBU.ToList();
            return View(k);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "kullanici", new { arama = txtAra });
        }
    }
}