using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTS.Models;

namespace PTS.Controllers
{
    [Authorize]
    public class FazlaMesaiController : Controller
    {
        // GET: FazlaMesai
        PROJE db = new PROJE();

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "FazlaMesai").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            List<FAZLA_MESAI> liste = db.FAZLA_MESAI.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.FAZLA_MESAI.Count());
                liste = db.FAZLA_MESAI.OrderBy(u => u.FAZLA_MESAI_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.FAZLA_MESAI.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).Count());
                liste = db.FAZLA_MESAI.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama))
                                .OrderBy(u => u.FAZLA_MESAI_REFNO)
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
                FAZLA_MESAI f = db.FAZLA_MESAI.Find(id);
                if (f != null)//id kullanıcılarda varsa
                {
                    db.FAZLA_MESAI.Remove(f);
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            FAZLA_MESAI f = new FAZLA_MESAI();
            if (id != null)
            {
                f = db.FAZLA_MESAI.Find(id);
                if (f == null)
                {
                    f = new FAZLA_MESAI();
                }
            }
            ViewData["personel"] = db.PERSONELs.ToList();
            return View(f);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(FAZLA_MESAI f)
        {

            if (ModelState.IsValid)
            {
                if (f.FAZLA_MESAI_REFNO == 0)
                {
                    db.FAZLA_MESAI.Add(f);
                }
                else
                {
                    db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            ViewData["personel"] = db.PERSONELs.ToList();
            return View(f);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "FazlaMesai", new { arama = txtAra });
        }

        public ActionResult ToplamSaat(int? id, int ay, int yil)
        {
            if (id != null)
            {
                var fazla_mesai = db.FAZLA_MESAI.Where(s => s.PERSONEL_REFNO == id && 
                s.AY == ay && s.YIL == yil).FirstOrDefault<FAZLA_MESAI>();


                if (fazla_mesai == null)
                {
                    return Content("false");
                }

                return Content(fazla_mesai.TOPLAM_SAAT.ToString());
            }

            return Content("id git");
        }
    }
}