using Sklep_Internetowy.Kontrolery;
using Sklep_Internetowy.Models;

public class Rejestracja
{
    public void Wykonaj()
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
    }
}
