using Sklep_Internetowy.Kontrolery;
using Sklep_Internetowy.Models;

public class Rejestracja
{
    public void Wykonaj()
    {
        // Wyświetlenie nagłówka rejestracji
        Console.WriteLine("=== Rejestracja ===");

        // Pobranie loginu od użytkownika
        Console.Write("Podaj login: ");
        string login = Console.ReadLine();

        // Pobranie hasła od użytkownika
        Console.Write("Podaj hasło: ");
        string haslo = Console.ReadLine();

        // Pobranie roli użytkownika (1 - użytkownik, 2 - admin)
        Console.Write("Podaj rolę (1 - użytkownik, 2 - admin): ");
        int rola;
        while (!int.TryParse(Console.ReadLine(), out rola))
        {
            // Komunikat o błędzie w przypadku nieprawidłowej wartości
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }

        // Utworzenie nowego użytkownika
        var uzytkownik = new Uzytkownik(login, haslo, rola);

        // Zapisanie użytkownika za pomocą UzytkownikManager
        UzytkownikManager.ZapiszUzytkownika(uzytkownik);

        // Komunikat o zakończeniu rejestracji
        Console.WriteLine("Rejestracja zakończona sukcesem!");
    }
}
