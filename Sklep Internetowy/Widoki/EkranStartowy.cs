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

        // Pętla do momentu, aż użytkownik poda prawidłową wartość
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

        // Opcja zarządzania produktami dostępna tylko dla użytkowników z rolą 2 (administratorzy)
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
                new Rejestracja().Wykonaj();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 2:
                new Logowanie().Wykonaj();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 3:
                magazyn.WyswietlProdukty();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 4:
                new Koszyk().DodajProduktDoKoszyka();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 5:
                new Koszyk().WyswietlKoszyk();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 6:
                new Koszyk().FinalizujZakup();
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 7:
                Sesja.ZalogowanyUzytkownik = null;
                Console.WriteLine("Wylogowano.");
                Console.WriteLine("Wciśnij ENTER, aby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            case 8:
                Console.WriteLine("Zamykanie aplikacji...");
                Environment.Exit(0);
                break;
            case 9:
                if (Sesja.ZalogowanyUzytkownik != null && Sesja.ZalogowanyUzytkownik.Rola == 2)
                {
                    new ZarzadzanieProduktami().Wykonaj();
                }
                else
                {
                    if (Sesja.ZalogowanyUzytkownik == null) Console.WriteLine("Nie jesteś zalogowany");
                    else if (Sesja.ZalogowanyUzytkownik.Rola != 2) Console.WriteLine("Nie masz uprawnień do zarządzania produktami");
                }
                Console.WriteLine("Wciśnij ENTER żeby kontynuować");
                Console.ReadLine();
                Console.Clear();
                break;
            default:
                Console.WriteLine("Nieznana opcja!");
                Console.ReadLine();
                break;
        }
    }
}
