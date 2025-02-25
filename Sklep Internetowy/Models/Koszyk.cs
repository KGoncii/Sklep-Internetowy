﻿using System.Text;

namespace Sklep_Internetowy.Models
{
    public class PozycjaKoszyka
    {
        public Produkt Produkt { get; set; }
        public int Ilosc { get; set; }

        public PozycjaKoszyka(Produkt produkt, int ilosc)
        {
            Produkt = produkt;
            Ilosc = ilosc;
        }
    }

    public class Koszyk
    {
        private List<PozycjaKoszyka> pozycje = new List<PozycjaKoszyka>();

        // Dodaje produkt do koszyka
        public void DodajProdukt(Produkt produkt, int ilosc)
        {
            var pozycja = pozycje.FirstOrDefault(p => p.Produkt.Id == produkt.Id);
            if (pozycja != null)
            {
                pozycja.Ilosc += ilosc;
            }
            else
            {
                pozycje.Add(new PozycjaKoszyka(produkt, ilosc));
            }
        }

        // Pobiera wszystkie pozycje z koszyka
        public IEnumerable<PozycjaKoszyka> PobierzPozycje()
        {
            return pozycje;
        }

        // Oblicza całkowitą cenę koszyka
        public decimal ObliczCalkowitaCene()
        {
            return pozycje.Sum(p => p.Produkt.Cena * p.Ilosc);
        }

        // Zwraca tekstową reprezentację koszyka
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pozycja in pozycje)
            {
                sb.AppendLine($"{pozycja.Produkt.Nazwa} - {pozycja.Ilosc} szt. - {pozycja.Produkt.Cena:C} za szt.");
            }
            sb.AppendLine($"Razem: {ObliczCalkowitaCene():C}");
            return sb.ToString();
        }

        Magazyn magazyn = Magazyn.Instance;

        // Dodaje produkt do koszyka z interfejsem użytkownika
        public void DodajProduktDoKoszyka()
        {
            Console.WriteLine("=== Dodaj produkt do koszyka ===");
            Console.Write("Podaj ID produktu: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || !magazyn.produkty.ContainsKey(id))
            {
                Console.WriteLine("Nieprawidłowe ID produktu. Spróbuj ponownie.");
            }

            var produkt = magazyn.produkty[id];
            Console.Write("Podaj ilość: ");
            int ilosc;
            while (!int.TryParse(Console.ReadLine(), out ilosc) || ilosc <= 0 || ilosc > produkt.Ilosc)
            {
                Console.WriteLine("Nieprawidłowa ilość. Spróbuj ponownie.");
            }

            Sesja.DodajProduktDoKoszyka(produkt, ilosc);

            Console.WriteLine($"Produkt {produkt.Nazwa} został dodany do koszyka w ilości {ilosc} szt.");
        }

        // Wyświetla zawartość koszyka
        public void WyswietlKoszyk()
        {
            Console.WriteLine("=== Twój koszyk ===");
            Console.WriteLine(Sesja.KoszykUzytkownika.ToString());
        }

        // Finalizuje zakup
        public void FinalizujZakup()
        {
            Console.WriteLine("=== Finalizacja zakupu ===");
            Console.WriteLine("Wybierz metodę płatności:");
            Console.WriteLine("1. PayPal");
            Console.WriteLine("2. Karta Płatnicza");
            Console.WriteLine("3. MasterCard");

            int wyborMetody;
            while (!int.TryParse(Console.ReadLine(), out wyborMetody) || wyborMetody < 1 || wyborMetody > 3)
            {
                Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
            }

            Platnosc metodaPłatności = wyborMetody switch
            {
                1 => new PayPal(),
                2 => new KartaPłatnicza(),
                3 => new MasterCard(),
                _ => throw new InvalidOperationException("Nieznana metoda płatności")
            };

            try
            {
                foreach (var pozycja in Sesja.KoszykUzytkownika.PobierzPozycje())
                {
                    var produkt = magazyn.produkty[pozycja.Produkt.Id];
                    produkt.Ilosc -= pozycja.Ilosc;
                }

                metodaPłatności.Zaplac(Sesja.KoszykUzytkownika.ObliczCalkowitaCene());
                Sesja.FinalizujZakup();
                magazyn.ZapiszProduktyDoPliku();
                Console.WriteLine("Zakup został sfinalizowany.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
