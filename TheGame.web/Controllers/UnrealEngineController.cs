using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheGame.logic;
using TheGame.web.Models;

namespace TheGame.web.Controllers
{
    public class UnrealEngineController : Controller
    {
        // GET: UnrealEngine
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult Login()
        {
            Debug.WriteLine("JsonResult - UnrealEngineController - Login");
            Debug.Indent();
            Benutzer cl = new Benutzer();
            bool ergebnis = false;
            string stuff = Request.Params["stuff"];
            if (stuff == "login")
            {
                string username = Request.Params["username"];
                string passwort = Request.Params["password"];
                cl = TheGameVerwaltung.AktClient(username);
                string hashpw = TheGameVerwaltung.PasswordHash(passwort);

                if (username == cl.Username && hashpw == cl.Passwort)
                {
                    Debug.WriteLine("Username und Password Richtig");
                    ergebnis = true;
                    ErgebnisModel em = new ErgebnisModel()
                    {
                        result = ergebnis.ToString()
                    };

                    return Json(em, JsonRequestBehavior.AllowGet);
                }
                Debug.WriteLine("Username oder Password Falsch");
            }
            Debug.Unindent();
            return Json("result", "false", JsonRequestBehavior.AllowGet);
        }
    }
}
//http://localhost:60663/Pokemon/Login?stuff=login&username=test&password=test