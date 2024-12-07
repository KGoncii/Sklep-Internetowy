using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Views
{
    public class Rejestracja
    {
        public Uzytkownik ZarejestrujUzytkownika()
        {
            Console.WriteLine("=== Rejestracja użytkownika ===");

            Console.Write("Podaj imię: ");
            string imie = Console.ReadLine();

            Console.Write("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();

            Console.Write("Podaj adres: ");
            string adres = Console.ReadLine();

            Console.Write("Podaj email: ");
            string email = Console.ReadLine();

            Console.Write("Podaj hasło: ");
            string haslo = Console.ReadLine();

            Console.WriteLine("Podaj rolę użytkownika: (1 - Klient, 2 - Admin)");
            int rola;
            while (!int.TryParse(Console.ReadLine(), out rola) || (rola != 1 && rola != 2))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wprowadź 1 dla Klienta lub 2 dla Admina.");
            }

            var nowyUzytkownik = new Uzytkownik(imie, nazwisko, adres, email, haslo, rola);

            Console.WriteLine("\n=== Rejestracja zakończona ===\n");
            return nowyUzytkownik;
        }
    }
}
