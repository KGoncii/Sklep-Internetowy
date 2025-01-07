using Sklep_Internetowy.Models;

public class EkranStartowy
{
    int wybor;
    Magazyn magazyn = Magazyn.Instance;

    public EkranStartowy()
    {
        Console.WriteLine("=== Witaj w naszym sklepie internetowym ===\n");

        Console.WriteLine("Co chcesz zrobić?");
        Wyswietl();

        // Zakres wyboru dla użytkownika
        int maxOpcji = Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2 ? 9 : 8;

        while (!int.TryParse(Console.ReadLine(), out wybor) || wybor < 1 || wybor > maxOpcji)
        {
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }
        Console.Clear();
        WykonajAkcje(wybor);
    }

    public void Wyswietl()
    {
        Console.WriteLine($"1. Rejestracja");
        Console.WriteLine($"2. Logowanie");
        Console.WriteLine($"3. Przeglądaj produkty");
        Console.WriteLine($"4. Dodaj produkt do koszyka");
        Console.WriteLine($"5. Wyświetl koszyk");
        Console.WriteLine($"6. Finalizuj zakup");
        Console.WriteLine($"7. Wyloguj");
        Console.WriteLine($"8. Zamknij");

        if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
        {
            Console.WriteLine($"9. Zarządzaj produktami");
        }
    }

    public void WykonajAkcje(int wybor)
    {
        switch (wybor)
        {
            case 1:
                Rejestracja();
                break;
            case 2:
                Logowanie();
                break;
            case 3:
                magazyn.WyswietlProdukty();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                break;
            case 4:
                DodajProduktDoKoszyka();
                break;
            case 5:
                WyswietlKoszyk();
                break;
            case 6:
                FinalizujZakup();
                break;
            case 7:
                Sesja.ZalogowanyUzytkownik = null;
                Console.WriteLine("Wylogowano.");
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                break;
            case 8:
                Console.WriteLine("Zamykanie aplikacji...");
                Environment.Exit(0);
                break;
            case 9:
                if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
                {
                    ZarzadzanieProduktami();
                }
                else
                {
                    if (Sesja.ZalogowanyUzytkownik == null) Console.WriteLine("Nie jesteś zalogowany");
                    else if (Sesja.ZalogowanyUzytkownik.Rola != 2) Console.WriteLine("Nie masz uprawnień do zarządzania produktami");
                }
                break;
            default:
                Console.WriteLine("Nieznana opcja!");
                Console.ReadLine();
                break;
        }
    }

    private void Rejestracja()
    {
        Console.WriteLine("=== Rejestracja ===");
        Console.Write("Podaj login: ");
        string login = Console.ReadLine();
        Console.Write("Podaj hasło: ");
        string haslo = Console.ReadLine();
        Console.Write("Podaj rolę (1 - użytkownik, 2 - admin): ");
        int rola;
        while (!int.TryParse(Console.ReadLine(), out rola))
        {
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }

        var uzytkownik = new Uzytkownik(login, haslo, rola);
        UzytkownikManager.ZapiszUzytkownika(uzytkownik);

        Console.WriteLine("Rejestracja zakończona sukcesem!");
        Console.WriteLine("Wciśnij ENTER, aby kontynuować");
        Console.ReadLine();
    }

    private void Logowanie()
    {
        Console.WriteLine("=== Logowanie ===");
        Console.Write("Podaj login: ");
        string login = Console.ReadLine();
        Console.Write("Podaj hasło: ");
        string haslo = Console.ReadLine();

        var uzytkownik = UzytkownikManager.Zaloguj(login, haslo);
        if (uzytkownik != null)
        {
            Sesja.ZalogowanyUzytkownik = uzytkownik;
            Console.WriteLine($"Zalogowano jako {uzytkownik.Login}");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy login lub hasło.");
        }
        Console.WriteLine("Wciśnij ENTER, aby kontynuować");
        Console.ReadLine();
    }

    private void DodajProduktDoKoszyka()
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
        Console.WriteLine("Wciśnij ENTER, aby kontynuować");
        Console.ReadLine();
    }

    private void WyswietlKoszyk()
    {
        Console.WriteLine("=== Twój koszyk ===");
        Console.WriteLine(Sesja.KoszykUzytkownika.ToString());
        Console.WriteLine("Wciśnij ENTER, aby kontynuować");
        Console.ReadLine();
    }

    private void FinalizujZakup()
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

        Platosc metodaPłatności = wyborMetody switch
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
        Console.WriteLine("Wciśnij ENTER, aby kontynuować");
        Console.ReadLine();
    }

    private void ZarzadzanieProduktami()
    {
        Console.WriteLine($"1. Dodawanie produktu");
        Console.WriteLine($"2. Usuwanie produktu");

        while (!int.TryParse(Console.ReadLine(), out wybor) || wybor < 1 || wybor > 2)
        {
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }
        Console.Clear();
        WykonajAkcjeZarzadzania(wybor);
    }

    private void WykonajAkcjeZarzadzania(int wybor)
    {
        switch (wybor)
        {
            case 1:
                DodawanieProduktow();
                break;
            case 2:
                UsuwanieProduktow();
                break;
            default:
                Console.WriteLine("Nieznana opcja!");
                Console.ReadLine();
                break;
        }
    }

    private void DodawanieProduktow()
    {
        Console.Clear();
        Console.WriteLine("Dodawanie produktów...");
        Console.WriteLine("Podaj nazwę produktu:");
        string nazwa = Console.ReadLine();
        Console.WriteLine("Podaj opis produktu:");
        string opis = Console.ReadLine();
        Console.WriteLine("Podaj ilość produktu:");
        int ilosc;
        while (!int.TryParse(Console.ReadLine(), out ilosc))
        {
            Console.WriteLine("Nieprawidłowa wartość. Podaj ilość produktu:");
        }
        Console.WriteLine("Podaj cenę produktu:");
        decimal cena;
        while (!decimal.TryParse(Console.ReadLine(), out cena))
        {
            Console.WriteLine("Nieprawidłowa wartość. Podaj cenę produktu:");
        }
        Console.WriteLine("Podaj kategorie produktu (oddzielone przecinkami):");
        string[] kategorie = Console.ReadLine().Split(',');

        magazyn.DodajProdukt(new Produkt(nazwa, opis, ilosc, cena, kategorie));
        Console.WriteLine("Wciśnij ENTER żeby kontynuować");
        Console.ReadLine();
    }

    private void UsuwanieProduktow()
    {
        Console.Clear();
        Console.WriteLine("Usuwanie produktów...");
        Console.WriteLine("Podaj ID produktu:");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || !magazyn.produkty.ContainsKey(id))
        {
            Console.WriteLine("Nieprawidłowe ID produktu. Spróbuj ponownie.");
        }
        magazyn.UsunProdukt(id);
        Console.WriteLine("Wciśnij ENTER żeby kontynuować");
        Console.ReadLine();
    }
}
