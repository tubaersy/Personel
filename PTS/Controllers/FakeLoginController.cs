using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PTS.Models;

namespace PTS.Controllers
{
    public class FakeLoginController : Controller
    {
        // GET: FakeLogin
        PROJE db = new PROJE();
        public ActionResult Index()
        {
            KULLANICI kullanici = new KULLANICI();
            return View(kullanici);
        }
        public ActionResult Giris(KULLANICI y)
        {
            KULLANICI kullanici = db.KULLANICIs.Where(y1 => y1.KULLANICI_ADI == y.KULLANICI_ADI && y1.PAROLA == y.PAROLA).SingleOrDefault();
            if (kullanici==null)
            {
                TempData["mesaj"] = "Kullanıcı adı ve şifre yanlış";
                FormsAuthentication.SignOut();
                return RedirectToAction("Index");
            }
            FormsAuthentication.SetAuthCookie(kullanici.KULLANICI_ADI, false);
            Session["KULLANICI_REFNO"] = kullanici.KULLANICI_REFNO;

            return RedirectToAction("Index", "Home");
        }
       
    }
}