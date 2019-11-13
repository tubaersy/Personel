
using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class DegerlendirmeController : Controller
    {
        PROJE db = new PROJE();
        // GET: Degerlendirme

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "Degerlendirme").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            List<DEGERLENDIRME> liste = db.DEGERLENDIRMEs.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.DEGERLENDIRMEs.Count());
                liste = db.DEGERLENDIRMEs.OrderBy(u => u.DEGERLENDIRME_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.DEGERLENDIRMEs.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).Count());
                liste = db.DEGERLENDIRMEs.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama))
                                .OrderBy(u => u.DEGERLENDIRME_REFNO)
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

        
        public ActionResult GetData()
        {
            PROJE db = new PROJE();

            var data = db.DEGERLENDIRMEs.Include("Product")
                   .GroupBy(p => p.PERSONEL.ADI_SOYADI)
                   .Select(g => new {
                       name = g.Key,
                       count = g.Average(w => w.KAVRAMA) + g.Average(d => d.INISIYATIF) + g.Average(d => d.INISIYATIF) + g.Average(d => d.EKIP_CALISMASI) + g.Average(i => i.IS_BILGISI) + g.Sum(i => i.IS_KALITESI)
                    + g.Average(i => i.OZVERI) + g.Average(i => i.SORUMLULUK) + g.Average(i => i.YARATICILIK) + g.Average(i => i.ZAMANLAMA)
                   }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                DEGERLENDIRME d = db.DEGERLENDIRMEs.Find(id);
                if (d != null)//id kullanıcılarda varsa
                {
                    db.DEGERLENDIRMEs.Remove(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            DEGERLENDIRME d = new DEGERLENDIRME();
            if (id != null)
            {
                d = db.DEGERLENDIRMEs.Find(id);
                if (d == null)
                {
                    d = new DEGERLENDIRME();
                }
            }

            ViewData["personel"] = db.PERSONELs.ToList();
            return View(d);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(DEGERLENDIRME d)
        {

            if (d.DEGERLENDIRME_REFNO == 0)
            {
                db.DEGERLENDIRMEs.Add(d);
            }
            else
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            ViewData["personel"] = db.PERSONELs.ToList();
            return RedirectToAction("Index");//listeleme yapılıyor.
           
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Degerlendirme", new { arama = txtAra });
        }
    }
}