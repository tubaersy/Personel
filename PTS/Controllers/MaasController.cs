
using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class MaasController : Controller
    {
        PROJE db = new PROJE();
        // GET: Personel

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "Maas").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");


            List<MAA> liste = db.MAAS.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.MAAS.Count());
                liste = db.MAAS.OrderBy(u => u.MAAS_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.MAAS.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).Count());
                liste = db.MAAS.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama))
                                .OrderBy(u => u.MAAS_REFNO)
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
                MAA s = db.MAAS.Find(id);
                if (s != null)//id kullanıcılarda varsa
                {
                    db.MAAS.Remove(s);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            MAA m = new MAA();
            if (id != null)
            {
                m = db.MAAS.Find(id);
                if (m == null)
                {
                    m = new MAA();
                }
            }

            ViewData["personel"] = db.PERSONELs.ToList();
            ViewData["aylik_ucret"] = db.
                PERSONELs.Where(s => s.PERSONEL_REFNO.Equals(m.PERSONEL_REFNO)).ToList();
            return View(m);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(MAA m)
        {
            if (ModelState.IsValid)
            {
                if (m.MAAS_REFNO == 0)
                {
                    db.MAAS.Add(m);
                }
                else
                {
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();

                ViewData["personel"] = db.PERSONELs.ToList();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }

            return View(m);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Maas", new { arama = txtAra });
        }
    }
}
