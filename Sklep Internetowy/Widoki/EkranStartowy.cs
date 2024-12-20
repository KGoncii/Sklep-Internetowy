using Sklep_Internetowy.Models;
public class EkranStartowy
{
    int wybor;
    Magazyn magazyn = new Magazyn();

    public EkranStartowy()
    {
        Console.WriteLine("=== Witaj w naszym sklepie internetowym ===\n");

        Console.WriteLine("Co chcesz zrobić?");
        Wyswietl();

        // Zakres wyboru dla użytkownika
        int maxOpcji = Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2 ? 6 : 5;

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
        Console.WriteLine($"4. Wyloguj");
        Console.WriteLine($"5. Zamknij");

        if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
        {
            Console.WriteLine($"6. Zarządzaj produktami");
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
                Sesja.ZalogowanyUzytkownik = null;
                Console.WriteLine("Wylogowano.");
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                break;
            case 5:
                Console.WriteLine("Zamykanie aplikacji...");
                Environment.Exit(0);
                break;
            case 6:
                if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Zarządzanie produktami...");
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
                else
                {
                    if (Sesja.ZalogowanyUzytkownik == null) Console.WriteLine("Nie jesteś zalogowany");
                    else if (Sesja.ZalogowanyUzytkownik.Rola != 2) Console.WriteLine("Nie jesteś zalogowany");
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
        Console.Write("Podaj rolę (1 - urzytkownik, 2 - admin): ");
        int rola;
        while (!int.TryParse(Console.ReadLine(), out rola))
        {
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }

        var uzytkownik = new Uzytkownik(login, haslo, rola); // Domyślnie rola = klient
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
}
