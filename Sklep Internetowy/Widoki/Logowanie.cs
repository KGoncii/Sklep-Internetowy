using Sklep_Internetowy.Kontrolery;
using Sklep_Internetowy.Models;

public class Logowanie
{
    public void Wykonaj()
    {
        // Wyświetlenie nagłówka logowania
        Console.WriteLine("=== Logowanie ===");

        // Pobranie loginu od użytkownika
        Console.Write("Podaj login: ");
        string login = Console.ReadLine();

        // Pobranie hasła od użytkownika
        Console.Write("Podaj hasło: ");
        string haslo = Console.ReadLine();

        // Próba zalogowania użytkownika
        var uzytkownik = UzytkownikManager.Zaloguj(login, haslo);
        if (uzytkownik != null)
        {
            // Jeśli logowanie się powiodło, ustawienie zalogowanego użytkownika w sesji
            Sesja.ZalogowanyUzytkownik = uzytkownik;
            Console.WriteLine($"Zalogowano jako {uzytkownik.Login}");
        }
        else
        {
            // Jeśli logowanie się nie powiodło, wyświetlenie komunikatu o błędzie
            Console.WriteLine("Nieprawidłowy login lub hasło.");
        }
    }
}
