using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.logic
{
    public class TheGameVerwaltung
    {
        public static List<Benutzer> AlleCLient()
        {
            Debug.WriteLine("TheGameVerwaltung - AlleCLient");
            Debug.Indent();
            List<Benutzer> daten = new List<Benutzer>();

            try
            {
                using (var context = new MmorpgTheGameEntities())
                {
                    daten = context.AlleBenutzer.ToList();
                    Debug.WriteLine(daten.Count + " Benutzer Gefunden");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Fehler - AlleCLient");
                Debug.WriteLine(ex);
                Debugger.Break();
            }
            
            Debug.Unindent();
            return daten;
        }



        public static Benutzer AktClient(string username)
        {
            Debug.WriteLine("TheGameVerwaltung - AktClient");
            Debug.Indent();
            Benutzer cl = new Benutzer();

            try
            {
                using (var context = new MmorpgTheGameEntities())
                {
                    cl = context.AlleBenutzer.Where(m => m.Username == username).FirstOrDefault();
                    if (cl != null)
                        Debug.WriteLine("Benutzer Gefunden");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Fehler - AktClient");
                Debug.WriteLine(ex);
                Debugger.Break();
            }
            
            Debug.Unindent();
            return cl;
        }

        public static Benutzer AktClient(string username,string email)
        {
            Debug.WriteLine("TheGameVerwaltung - AktClient");
            Debug.Indent();
            Benutzer cl = new Benutzer();

            try
            {
                using (var context = new MmorpgTheGameEntities())
                {
                    cl = context.AlleBenutzer.Where(m => m.Username == username || m.Email == email).FirstOrDefault();
                    if (cl != null)
                        Debug.WriteLine("Benutzer Gefunden");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Fehler - AktClient");
                Debug.WriteLine(ex);
                Debugger.Break();
            }

            Debug.Unindent();
            return cl;
        }



        public static bool PlayerCreate(string playerName, string email, string password,string password2)
        {
            Debug.WriteLine("TheGameVerwaltung - PlayerCreate");
            Debug.Indent();
            bool erfolgreich = false;

            if (password != password2)
                return erfolgreich;

            password = PasswordHash(password2);

            try
            {
                using (var contex = new MmorpgTheGameEntities())
                {
                    Benutzer benutzer = contex.AlleBenutzer.Where(x => x.Email == email || x.Username == playerName).FirstOrDefault();
                    if(benutzer == null)
                    {
                        benutzer = new Benutzer();
                        benutzer.Username = playerName;
                        benutzer.Passwort = password;
                        benutzer.Email = email;
                        benutzer.IstFreigeschalten = false;

                        contex.AlleBenutzer.Add(benutzer);

                        int zeilen = contex.SaveChanges();
                        erfolgreich = zeilen > 0;
                        Debug.WriteLine(zeilen + " wurden erfolgreich Hinzugefügt");
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Fehler - PlayerCreate");
                Debug.WriteLine(ex);
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;
        }

        public static string PasswordHash(string ppassword)
        {
            Debug.WriteLine("TheGameVerwaltung - PasswordHash");
            Debug.Indent();
            var bytes = new UTF8Encoding().GetBytes(ppassword);
            var hashBytes = MD5.Create().ComputeHash(bytes);
            Debug.Unindent();
            return Convert.ToBase64String(hashBytes);
        }

    }
}
