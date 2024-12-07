using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Widoki
{
    public class EkranStartowy
    {
        int wybor;
        int counter = 1; // do naprawy cała logika związana z tą zmienna bo chciałem byc mądry i zrobić menu responsywne ale chyba nie jestem to mądre
        private Uzytkownik ZalogowanyUzytkownik;
        public EkranStartowy(Uzytkownik zalogowanyUzytkownik)
        {
            ZalogowanyUzytkownik = zalogowanyUzytkownik;

            Console.WriteLine("=== Witaj w naszym sklepie internetowym ===\n");

            Console.WriteLine("Co chcesz zrobić?");

            int counter = 1;
            Wyswietl();
            while (!int.TryParse(Console.ReadLine(), out wybor) && (wybor < 1 && wybor > counter))
            {
                Console.WriteLine("Nieprawidłowa wartość. Wprowadź 1 dla Klienta lub 2 dla Admina.");
            }
            Console.Clear();
            WykonajAkcje(wybor);
        }
        public void Wyswietl() {
            counter = 1;
            Console.WriteLine($"{counter++}.Rejestracja");
            Console.WriteLine($"{counter++}.Logowanie");
            Console.WriteLine($"{counter++}.Przeglądaj produkty");
            Console.WriteLine($"{counter++}.Wyloguj");
            Console.WriteLine($"{counter++}.Zamknij");
            if (ZalogowanyUzytkownik != null && ZalogowanyUzytkownik.Rola == 2)
            {
                Console.WriteLine($"{counter++}. Zarządzaj produktami");
            }
        }

        private void WykonajAkcje(int wybor)
        {
            switch (wybor)
            {
                case 1:
                    Console.WriteLine("Rozpoczynanie rejestracji...");
                    break;
                case 2:
                    Console.WriteLine("Rozpoczynanie logowania...");
                    break;
                case 3:
                    Console.WriteLine("Przeglądanie produktów...");
                    break;
                case 4:
                    Console.WriteLine("Wylogowywanie...");
                    ZalogowanyUzytkownik = null;
                    break;
                case 5:
                    if (ZalogowanyUzytkownik != null && ZalogowanyUzytkownik.Rola == 2)
                    {
                        Console.WriteLine("Zarządzanie produktami...");
                    }
                    break;
                default:
                    Console.WriteLine("Nieznana opcja!");
                    break;
            }
        }
    }
}
