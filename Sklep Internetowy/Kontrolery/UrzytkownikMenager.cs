using Sklep_Internetowy.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sklep_Internetowy.Kontrolery
{
    public static class UzytkownikManager
    {
        private static readonly string PlikUzytkownikow = "uzytkownicy.txt";

        // Pobierz wszystkich użytkowników z pliku
        public static List<Uzytkownik> PobierzUzytkownikow()
        {
            var uzytkownicy = new List<Uzytkownik>();
            if (File.Exists(PlikUzytkownikow))
            {
                var lines = File.ReadAllLines(PlikUzytkownikow);
                foreach (var line in lines)
                {
                    uzytkownicy.Add(Uzytkownik.FromString(line));
                }
            }
            return uzytkownicy;
        }

        // Zapisz użytkownika do pliku
        public static void ZapiszUzytkownika(Uzytkownik uzytkownik)
        {
            using (var writer = new StreamWriter(PlikUzytkownikow, true))
            {
                writer.WriteLine(uzytkownik);
            }
        }

        // Sprawdź logowanie
        public static Uzytkownik Zaloguj(string login, string haslo)
        {
            var uzytkownicy = PobierzUzytkownikow();
            foreach (var uzytkownik in uzytkownicy)
            {
                if (uzytkownik.Login == login && uzytkownik.Haslo == haslo)
                {
                    return uzytkownik;
                }
            }
            return null;
        }
    }
}
