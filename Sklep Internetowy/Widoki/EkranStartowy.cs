using Sklep_Internetowy.Models;

public class EkranStartowy
{
    int wybor;
    Magazyn magazyn = new Magazyn();
    public EkranStartowy(Uzytkownik zalogowanyUzytkownik)
    {
        Sesja.ZalogowanyUzytkownik = zalogowanyUzytkownik;

        Console.WriteLine("=== Witaj w naszym sklepie internetowym ===\n");

        Console.WriteLine("Co chcesz zrobić?");

        Wyswietl();
        while (!int.TryParse(Console.ReadLine(), out wybor) && (wybor < 1 && wybor > 7))
        {
            Console.WriteLine("Nieprawidłowa wartość. Wprowadź 1 dla Klienta lub 2 dla Admina.");
        }
        Console.Clear();
        WykonajAkcje(wybor);
    }
    public void Wyswietl()
    {
        Console.WriteLine($"1.Rejestracja");
        Console.WriteLine($"2.Logowanie");
        Console.WriteLine($"3.Przeglądaj produkty");
        Console.WriteLine($"4.Wyloguj");
        Console.WriteLine($"5.Zamknij");
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
                Console.WriteLine("Rozpoczynanie rejestracji...");
                Rejestracja rejestracja = new Rejestracja();
                rejestracja.ZarejestrujUzytkownika();
                break;
            case 2:
                Console.WriteLine("Rozpoczynanie logowania...");
                break;
            case 3:
                magazyn.WyswietlProdukty();
                Console.WriteLine("Wciśnij ENTER żeby kontynuować");
                Console.ReadLine();
                break;
            case 4:
                Console.WriteLine("Wylogowywanie...");
                Sesja.ZalogowanyUzytkownik = null;
                Console.WriteLine("Wciśnij ENTER żeby kontynuować");
                Console.ReadLine();
                break;
            case 5:
                Console.WriteLine("Zamykanie aplikacji...");
                Environment.Exit(0);
                break;
            case 6:
                if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
                {
                    Console.WriteLine("Zarządzanie produktami...");
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
                Console.WriteLine("Wciśnij ENTER żeby kontynuować");
                Console.ReadLine();
                break;
        }
    }
}