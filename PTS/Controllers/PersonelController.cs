using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using PTS.Models;
using Rotativa;

namespace PTS.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        PROJE db = new PROJE();
        // GET: Personel

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            int ygr = Helpers.SessionHelper<KULLANICI>.GetSessionItem("kullanici").YETKI_GRUBU_REFNO;
            int sayfa_refno1 = db.SAYFAs.Where(s => s.SAYFA_ADI == "Personel").SingleOrDefault().SAYFA_REFNO;
            bool yetki1 = YETKI.YetkiVarmi(ygr, sayfa_refno1, YETKI.YETKI_TIPI.OKUMA);
            if (yetki1 == false) return RedirectToAction("Index", "Home");

            List<PERSONEL> liste = db.PERSONELs.ToList();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.PERSONELs.Count());
                liste = db.PERSONELs.OrderBy(u => u.PERSONEL_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else

            {
                Sayfalama(db.PERSONELs.Where(s => s.ADI_SOYADI.Contains(arama)).Count());
                liste = db.PERSONELs.Where(s => s.ADI_SOYADI.Contains(arama))
                                .OrderBy(u => u.PERSONEL_REFNO)
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

        

        public ActionResult ExportExcel()
        {
            GridView gv = new GridView();

            gv.DataSource = db.PERSONELs.ToList();
            gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=PersonelExport.xls");
            Response.Charset = "";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }

        public ActionResult ExportWord()
        {
            // get the data from database
            var data = db.PERSONELs.ToList();
            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            // set the header
            Response.AddHeader("content-disposition", "attachment; filename = personel.doc");
            Response.ContentType = "application/ms-word";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }



        public ActionResult CV(int? id)
        {
            PERSONEL p = new PERSONEL();
            if (id != null)
            {
                p = db.PERSONELs.Find(id);

                if (p == null)
                {

                    p = new PERSONEL();
                }

                ViewData["calistigi_yer"] = db.
                    CALISTIGI_YER.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();

                ViewData["yabanci_dil"] = db.
                    YABANCI_DIL.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();

                ViewData["egitim"] = db.
                    EGITIMs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();

                ViewData["kurs"] = db.
                    KURS.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();

                ViewData["sertifika"] = db.
                    SERTIFIKAs.Where(s => s.PERSONEL_REFNO.Equals(p.PERSONEL_REFNO)).ToList();
            }

            return View(p);//model binding yapıyoruz.
        }

        public ActionResult CVList(int id)
        {

            return this.CV(id);
        }

        public ActionResult PrintCV(int id)
        {
            var print = new ActionAsPdf("CVList", new { id = id });
            return print;
        }

        public ActionResult PersonelList()
        {
            List<PERSONEL> liste = db.PERSONELs.ToList();
            liste = db.PERSONELs.ToList();
            return View(liste);
        }


        public ActionResult PrintAll()
        {
            var print = new ActionAsPdf("PersonelList");
            return (print);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                PERSONEL s = db.PERSONELs.Find(id);
                if (s != null)//id kullanıcılarda varsa
                {
                    db.PERSONELs.Remove(s);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create(int? id)
        {
            PERSONEL p = new PERSONEL();
            if (id != null)
            {
                p = db.PERSONELs.Find(id);
                if (p == null)
                {
                    p = new PERSONEL();
                }
            }

            ViewData["departman"] = db.DEPARTMAN.ToList();
            ViewData["kullanici"] = db.KULLANICIs.ToList();
            return View(p);//model binding yapıyoruz.
        }
        [HttpPost]
        public ActionResult Create(PERSONEL p, HttpPostedFileBase RESIM)
        {
            ViewData["departman"] = db.DEPARTMAN.ToList();
            ViewData["kullanici"] = db.KULLANICIs.ToList();  
            if (RESIM != null)
                {
                    p.RESIM = RESIM.FileName;
                }
                if (p.PERSONEL_REFNO == 0)
                {
                    db.PERSONELs.Add(p);
                }
                else
                {
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                }
                if (RESIM != null)
                {
                    RESIM.SaveAs(Request.PhysicalApplicationPath + "/Images/" + RESIM.FileName);//resim yükleme
                }
                db.SaveChanges();
                return RedirectToAction("Index");//listeleme yapılıyor.
        }
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Personel", new { arama = txtAra });
        }
        public ActionResult Maas(int? id)
        {
            PERSONEL p = new PERSONEL();
            if (id != null)
            {
                p = db.PERSONELs.Find(id);
                if (p == null)
                {
                    return Content("Personel Bulunamadı");
                }
            }

            return Content(p.AYLIK_UCRET.ToString());
        }
        public ActionResult AyniTarih(int? id, int ay, int yil)
        {
            PERSONEL p = new PERSONEL();
            if (id != null)
            {
                var liste = db.MAAS.ToList();
                var aynitarih = liste.Where(s => s.PERSONEL_REFNO == id &&
                s.AY == ay && s.YIL == yil).FirstOrDefault<MAA>();


                if (aynitarih == null)
                {
                    return Content(p.AYLIK_UCRET.ToString());

                }

                return Content("false");
            }

            return Content("id git");
        }
    }
}

