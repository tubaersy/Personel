using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace PTS.Controllers
{
    [Authorize]
    public class IzinOnayController : Controller
    {
        // GET: IzinOnay

        PROJE db = new PROJE();

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "IzinOnay").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            List<IZIN> liste = db.IZINs.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.IZINs.Count());
                liste = db.IZINs.OrderBy(u => u.IZIN_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.IZINs.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).Count());
                liste = db.IZINs.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama))
                                .OrderBy(u => u.IZIN_REFNO)
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
                IZIN i = db.IZINs.Find(id);
                if (i != null)//id kullanıcılarda varsa
                {
                    db.IZINs.Remove(i);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            IZIN i = new IZIN();
            if (id != null)
            {
                i = db.IZINs.Find(id);
                if (i == null && i.DURUM_REFNO == 2)
                {
                    i = new IZIN();
                }

                else if (i.DURUM_REFNO == 1)
                {
                    //MessageBox.Show("Siz onay seçemezsiniz!", "Bilgilendirme Penceresi");
                }

            }

            ViewData["personel"] = db.PERSONELs.ToList();
            ViewData["durum"] = db.DURUMs.ToList();
            return View(i);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(IZIN i)
        {
            if (ModelState.IsValid)
            {
                if (i.IZIN_REFNO == 0 && i.DURUM_REFNO == 2)
                {
                    db.IZINs.Add(i);
                }
                else if (i.DURUM_REFNO == 1)
                {
                    //MessageBox.Show("izin yetkiniz yok!", "Bilgilendirme Penceresi");
                }
                else
                {
                    db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
            }
            ViewData["personel"] = db.PERSONELs.ToList();
            ViewData["durum"] = db.DURUMs.ToList();
            return View(i);//model binding yapıyoruz.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "IzinOnay", new { arama = txtAra });
        }

        public ActionResult Onay(int? id)
        {
            IZIN p = new IZIN();
            if (id != null)
            {
                p = db.IZINs.Find(id);

                if (p.DURUM_REFNO == 2)
                {
                    return Content("false");
                }

                return Content("true");
            }

            return Content("id git");
        }
    }
}