
using PTS.Helpers;
using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;

namespace PTS.Controllers
{
    [Authorize]
    public class IzinIstegiController : Controller
    {
        // GET: IzinIstegi
        PROJE db = new PROJE();

        public ActionResult Index(string arama)
        {
            PERSONEL p = new PERSONEL();
            List<IZIN> liste = db.IZINs.ToList();
            if (arama == null)
            {
                arama = "";
                liste = db.IZINs.ToList();
            }
            else
            {
                liste = db.IZINs.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).ToList();
            }
            ViewData["veri"] = arama;
            ViewData["onay"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO) && s.DURUM_REFNO == 2).ToList();
            return View(liste);

        }
        public ActionResult KalanGun(int? id)
        {
            PERSONEL p = new PERSONEL();
           
            if (id != null)
            {
                p = db.PERSONELs.Find(id);

                if (p == null)
                {

                    p = new PERSONEL();
                }
                
                ViewData["izin"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();

                ViewData["onayyoksa"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO) && s.DURUM_REFNO == 1).Count();

                var onayyoksa = ViewData["onayyoksa"];

                if (Convert.ToInt32(onayyoksa) >= 1)
                {
                    MessageBox.Show("Son izniniz henüz onaylanmadı. !", "Bilgilendirme Penceresi");

                    return RedirectToAction("Index");
                }



                ViewData["toplamgun"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO) && s.DURUM_REFNO == 2).Sum(s1 => s1.GUN).ToString();
                
                var toplamgun = ViewData["toplamgun"];
                var sayi = 15;
                

                if (Convert.ToInt32(toplamgun) > 15)
                {
                    MessageBox.Show("Yıllık izniniz dolduğu için izniniz kabul edilmemiştir. !", "Bilgilendirme Penceresi");

                    return RedirectToAction("Index");
                }
                if (Convert.ToInt32(toplamgun) <= 15)
                {
                    MessageBox.Show("Kullandığınız izin sayısı:" + toplamgun + " Kalan izin sayısı: " + (sayi - Convert.ToInt32(toplamgun)), "Bilgilendirme Penceresi");

                    return RedirectToAction("Index");
                }

            }
            return View(p);//model binding yapıyoruz.
        }
        public ActionResult IzinKarti(int? id)
        {
            PERSONEL p = new PERSONEL();
            if (id != null)
            {
                p = db.PERSONELs.Find(id);

                if (p == null)
                {

                    p = new PERSONEL();
                }

                ViewData["izin"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();


                ViewData["onayyoksa"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO) && s.DURUM_REFNO == 1).Count();

                var onayyoksa = ViewData["onayyoksa"];

                if (Convert.ToInt32(onayyoksa) >= 1)
                {
                    MessageBox.Show("Son izniniz henüz onaylanmadı. !", "Bilgilendirme Penceresi");
                    return RedirectToAction("Index");
                }


                ViewData["toplamgun"] = db.IZINs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO) && s.DURUM_REFNO == 2).Sum(s1 => s1.GUN).ToString();
                var toplamgun = ViewData["toplamgun"];

                TimeSpan zaman = new TimeSpan();
                DateTime d1, d2 = new DateTime();
                d1 = p.ISE_BASLAMA_TARIHI;
                d2 = DateTime.Today;
                zaman = d2 - d1;
                int isebaslama = Math.Abs(zaman.Days);
                if (isebaslama < 365)
                {
                    MessageBox.Show("Bir yıllık çalışma süreniz dolmadığı için izniniz kabul edilmemiştir. !", "Bilgilendirme Penceresi");

                    return RedirectToAction("Index");
                }
                if (Convert.ToInt32(toplamgun) > 15)
                {
                    MessageBox.Show("Yıllık izniniz dolduğu için izniniz kabul edilmemiştir. !", "Bilgilendirme Penceresi");

                    return RedirectToAction("Index");
                }


                ViewData["isebaslama"] = isebaslama.ToString();

            }

            return View(p);//model binding yapıyoruz.
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
                if (i == null && i.DURUM_REFNO == 1)
                {
                    i = new IZIN();
                }

                else if (i.DURUM_REFNO == 2)
                {
                    //MessageBox.Show("Siz onay seçemezsiniz!", "Bilgilendirme Penceresi");
                }

            }
            KULLANICI p = SessionHelper<KULLANICI>.GetSessionItem("kullanici");
            ViewData["personel"] = db.PERSONELs.Where(s => s.KULLANICI_REFNO == p.KULLANICI_REFNO).ToList();
            ViewData["durum"] = db.DURUMs.ToList();
            return View(i);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(IZIN i)
        {
            if (ModelState.IsValid)
            {

                if (i.IZIN_REFNO == 0 && i.DURUM_REFNO == 1)
                {
                    TimeSpan zaman = new TimeSpan();
                    DateTime d1, d2 = new DateTime();
                    d1 = i.BITIS_TARIHI;
                    d2 = i.BASLANGIC_TARIHI;
                    zaman = d1 - d2;
                    i.GUN += Math.Abs(zaman.Days);
                    db.IZINs.Add(i);
                }

                else if (i.DURUM_REFNO == 2)
                {
                    //MessageBox.Show("siz onay veremezsiniz!", "Bilgilendirme Penceresi");
                }

                else
                {
                    TimeSpan zaman = new TimeSpan();
                    DateTime d1, d2 = new DateTime();
                    d1 = i.BITIS_TARIHI;
                    d2 = i.BASLANGIC_TARIHI;
                    zaman = d1 - d2;
                    i.GUN += Math.Abs(zaman.Days);
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
            return RedirectToAction("Index", "IzinIstegi", new { arama = txtAra });
        }
    }
}