using PTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PTS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        PROJE db = new PROJE();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckUser(string KULLANICI_ADI, string PAROLA)
        {
            KULLANICI kullanici = db.KULLANICIs.Where(k => k.KULLANICI_ADI == KULLANICI_ADI && k.PAROLA == PAROLA).SingleOrDefault();
            if (kullanici != null)
            {
                Helpers.SessionHelper<KULLANICI>.SetSessionItem("kullanici", kullanici);
                
                PERSONEL p = db.PERSONELs.Where(p1 => p1.KULLANICI_REFNO == kullanici.KULLANICI_REFNO).FirstOrDefault();

                Helpers.SessionHelper<PERSONEL>.SetSessionItem("personel", p);


                FormsAuthentication.SetAuthCookie(KULLANICI_ADI, false);//session da değilde cookie de tutuyor false olanda pc bunu tutsun mu diye 
                
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.LoginError = "Kullanıcı Adı veya Parola Yanlış";
            }
            return View("Index");
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}