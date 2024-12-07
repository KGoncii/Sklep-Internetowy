using Sklep_Internetowy.Models;
using Sklep_Internetowy.Views;
using Sklep_Internetowy.Widoki;
class Program
{
    static void Main()
    {
        Uzytkownik zalogowanyUzytkownik = null;

        var ekranStartowy = new EkranStartowy(zalogowanyUzytkownik);
        Rejestracja rejestracja = null;

        if (zalogowanyUzytkownik == null)
        {
            rejestracja = new Rejestracja();
            Uzytkownik nowyUzytkownik = rejestracja.ZarejestrujUzytkownika();

            Console.WriteLine($"ID: {nowyUzytkownik.Id}");
            Console.WriteLine($"Imię: {nowyUzytkownik.Imie}");
            Console.WriteLine($"Nazwisko: {nowyUzytkownik.Nazwisko}");
            Console.WriteLine($"Adres: {nowyUzytkownik.Adres}");
            Console.WriteLine($"Email: {nowyUzytkownik.Email}");
            Console.WriteLine($"Rola: {(nowyUzytkownik.Rola == 1 ? "Klient" : "Admin")}");
        }

        var produkt1 = new Produkt("Telefon", "Smartfon", 10, 1999.99m, new string[] { "Elektronika", "Telefony" });
        var produkt2 = new Produkt("Laptop", "Ultrabook", 5, 4999.99m, new string[] { "Elektronika", "Komputery" });
        var Magazyn = new Magazyn();
        Magazyn.DodajProdukt(produkt1);
        Magazyn.DodajProdukt(produkt2);

        bool running = true;
        while (running)
        {
            ekranStartowy.Wyswietl();
            string wybor = Console.ReadLine();

            switch (wybor.ToLower())
            {
                case "1":
                    Magazyn.WyswietlProdukty();
                    break;
                case "2":
                    // Dodaj inne akcje tutaj
                    break;
                case "zamknij":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                    break;
            }
        }
    }
}
