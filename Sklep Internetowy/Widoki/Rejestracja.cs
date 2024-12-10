using Sklep_Internetowy.Models;

public class Rejestracja
{
    public void ZarejestrujUzytkownika()
    {
        Console.WriteLine("Podaj imię:");
        string imie = Console.ReadLine();

        Console.WriteLine("Podaj nazwisko:");
        string nazwisko = Console.ReadLine();

        Console.WriteLine("Podaj adres:");
        string adres = Console.ReadLine();

        Console.WriteLine("Podaj email:");
        string email = Console.ReadLine();

        Console.WriteLine("Podaj hasło:");
        string haslo = Console.ReadLine();

        Console.WriteLine("Podaj rolę (1 dla Klienta, 2 dla Admina):");
        int rola;
        while (!int.TryParse(Console.ReadLine(), out rola) || (rola != 1 && rola != 2))
        {
            Console.WriteLine("Nieprawidłowa wartość. Wprowadź 1 dla Klienta lub 2 dla Admina.");
        }

        Uzytkownik nowyUzytkownik = new Uzytkownik(imie, nazwisko, adres, email, haslo, rola);

        // Ustaw nowego użytkownika jako zalogowanego w sesji
        Sesja.ZalogowanyUzytkownik = nowyUzytkownik;

        Console.WriteLine("Rejestracja zakończona pomyślnie. Użytkownik został zalogowany.");
    }
}