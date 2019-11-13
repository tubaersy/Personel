using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    [Authorize]
    public class YetkiGruplariController : Controller
    {
        // GET: YetkiGruplari
        PROJE db = new PROJE();
        public ActionResult Index()
        {
           int ygr= Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            //int kullanici_refno = Convert.ToInt32(Session["KULLANICI_REFNO"]);
            //int ygr = db.KULLANICIs.Where(y1 => y1.KULLANICI_REFNO == kullanici_refno).FirstOrDefault().YETKI_GRUBU_REFNO;

            int sayfa_refno = db.SAYFAs.Where(s => s.SAYFA_ADI == "YetkiGruplari").SingleOrDefault().SAYFA_REFNO;
           
            bool yetki= YETKI.YetkiVarmi(ygr, sayfa_refno, YETKI.YETKI_TIPI.OKUMA);
           
            if (yetki == false) return RedirectToAction("Index", "Home");
            
            var liste = db.YETKI_GRUBU.ToList();
            return View(liste);
        }
        public ActionResult Yetkiler(int id)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            //int kullanici_refno = Convert.ToInt32(Session["KULLANICI_REFNO"]);
            //int ygr = db.KULLANICIs.Where(y1 => y1.KULLANICI_REFNO == kullanici_refno).FirstOrDefault().YETKI_GRUBU_REFNO;

            int sayfa_refno = db.SAYFAs.Where(s => s.SAYFA_ADI == "YetkiGruplari").SingleOrDefault().SAYFA_REFNO;

            bool yetki = YETKI.YetkiVarmi(ygr, sayfa_refno, YETKI.YETKI_TIPI.OKUMA);

            if (yetki == false) return RedirectToAction("Index", "Home");

            ViewData["id"] = id;
            ViewData["sayfalar"] = db.SAYFAs.ToList();
            var liste = db.YETKIs.Where(y => y.YETKI_GRUBU_REFNO == id).ToList();
            return View(liste);
        }
        public ActionResult Kayit(FormCollection formdata)
        {   //yetki sayfası kaydet
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            //int kullanici_refno = Convert.ToInt32(Session["KULLANICI_REFNO"]);
            //int ygr = db.KULLANICIs.Where(y1 => y1.KULLANICI_REFNO == kullanici_refno).FirstOrDefault().YETKI_GRUBU_REFNO;

            int sayfa_refno = db.SAYFAs.Where(s => s.SAYFA_ADI == "YetkiGruplari").SingleOrDefault().SAYFA_REFNO;

            bool yetki = YETKI.YetkiVarmi(ygr, sayfa_refno, YETKI.YETKI_TIPI.OKUMA);

            if (yetki == false) return RedirectToAction("Index", "Home");

            //eski yetkileri silmeliyiz.
            int YETKI_GRUBU_REFNO = Convert.ToInt32(formdata["YETKI_GRUBU_REFNO"]);
            var yetkiler = db.YETKIs.RemoveRange(db.YETKIs.Where(y => y.YETKI_GRUBU_REFNO==YETKI_GRUBU_REFNO));
            db.YETKIs.RemoveRange(yetkiler);
            db.SaveChanges();

            int toplamsatir = db.SAYFAs.Count();
            for (int i = 0; i < toplamsatir; i++)
            {
                int sayfarefno = db.SAYFAs.ToList()[i].SAYFA_REFNO;
                bool OKUMA = false, KAYDET = false, SIL = false, YENI = false, ARAMA = false;
                if (formdata["OKUMA" + sayfarefno] != null) OKUMA = true;
               
                if (formdata["KAYDET" + sayfarefno] != null) KAYDET = true;

                if (formdata["SIL" + sayfarefno] != null) SIL = true;

                if (formdata["YENI" + sayfarefno] != null) YENI = true;

                if (formdata["ARAMA" + sayfarefno] != null) ARAMA = true;

                YETKI y = new YETKI()
                {
                    SAYFA_REFNO = sayfarefno,
                    YETKI_GRUBU_REFNO = YETKI_GRUBU_REFNO,
                    OKUMA = OKUMA,
                    KAYDET = KAYDET,
                    SIL = SIL,
                    YENI = YENI,
                    ARAMA = ARAMA
                };
                db.YETKIs.Add(y);
                db.SaveChanges();


            }

            return RedirectToAction("Index");
        }
    }
}