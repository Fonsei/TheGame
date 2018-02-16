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
    public class TheGameController : Controller
    {
        List<Benutzer> AlleBenutzer = new List<Benutzer>();

        // GET: TheGame
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Player()
        {
            Debug.WriteLine("GET - TheGameController - Player");
            Debug.Indent();
            List<PlayerModel> model = new List<PlayerModel>();
            AlleBenutzer = TheGameVerwaltung.AlleCLient();
            List<BenutzerProfil> alleBenutzerProfile = null;
            foreach (var spieler in AlleBenutzer)
            {
                alleBenutzerProfile = TheGameVerwaltung.AlleProfile(spieler.ID);

                PlayerModel player = new PlayerModel();
                player.ID = spieler.ID;
                player.PlayerName = spieler.Username;
                player.Email = spieler.Email;
                player.IsFreigeschalten = (bool)spieler.IstFreigeschalten;
                player.Profile = alleBenutzerProfile;
                model.Add(player);
            }

            Debug.Unindent();
            return View(model);
        }

        [HttpGet]
        public ActionResult PlayerDetails(int id)
        {
            Debug.WriteLine("GET - TheGameController - PlayerDetails");
            Debug.Indent();

            List<BenutzerProfil> alleBenutzerProfile = TheGameVerwaltung.AlleProfile(id);
            Benutzer spieler = TheGameVerwaltung.AktClient(id);
            PlayerModel model = new PlayerModel();
            model.ID = spieler.ID;
            model.PlayerName = spieler.Username;
            model.Email = spieler.Email;
            model.IsFreigeschalten = (bool)spieler.IstFreigeschalten;
            model.Profile = alleBenutzerProfile;

            Debug.Unindent();
            return View(model);
        }

        [HttpGet]
        public ActionResult PlayerCharDetails(string name)
        {
            Debug.WriteLine("GET - TheGameController - PlayerCharDetails");
            Debug.Indent();
            BenutzerProfil aktprofil = new BenutzerProfil();

            aktprofil = TheGameVerwaltung.AktuellesProfile(name);

            Debug.Unindent();
            return View(aktprofil);
        }

        [HttpGet]
        public ActionResult CreatePlayer()
        {
            Debug.WriteLine("GET - TheGameController - CreatePlayer");
            Debug.Indent();

            Debug.Unindent();
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlayer(PlayerModel model)
        {
            Debug.WriteLine("POST - TheGameController - CreatePlayer");
            Debug.Indent();

            if (ModelState.IsValid)
            {
                Benutzer benutzer = TheGameVerwaltung.AktClient(model.PlayerName, model.Email);
                if (model.Password != model.Password2)
                {
                    ModelState.AddModelError("Password2", "Passwörter müssen sind nicht gleich!");
                }
                else if (TheGameVerwaltung.PlayerCreate(model.PlayerName, model.Email, model.Password, model.Password2) && benutzer == null)
                {
                    Debug.WriteLine("Erfolgreich Erstellt");
                    return RedirectToAction("Player");
                }
                else
                {
                    ModelState.AddModelError("Password2", "Username oder Email Schon Vergeben");
                    Debug.WriteLine("Registrierung Fehlgeschlagen");
                }
            }
            else
            {
                ModelState.AddModelError("Password2", "Bitte ALle Felder Ausfüllen");
            }

            Debug.Unindent();
            return View();
        }

    }
}