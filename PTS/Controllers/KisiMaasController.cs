using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    public class KisiMaasController : Controller
    {
        // GET: KisiMaas
        PROJE db = new PROJE();
        // GET: Personel
        public ActionResult Index(string arama)
        {
            List<MAA> liste = db.MAAS.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.MAAS.ToList();
            }
            else
            {
                liste = db.MAAS.Where(s => s.PERSONEL.ADI_SOYADI.Contains(arama)).ToList();
            }


            ViewData["veri"] = arama;
            return View(liste);

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
        
        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", "Maas", new { arama = txtAra });
        }
    }
}
