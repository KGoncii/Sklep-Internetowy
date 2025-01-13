using Sklep_Internetowy.Kontrolery;
using Sklep_Internetowy.Models;

public class Logowanie
{
    public void Wykonaj()
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
    }
}
