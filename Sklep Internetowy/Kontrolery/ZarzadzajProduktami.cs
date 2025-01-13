using Sklep_Internetowy.Models;

public class ZarzadzanieProduktami
{
    int wybor;
    Magazyn magazyn = Magazyn.Instance;

    public void Wykonaj()
    {
        Console.WriteLine($"1. Dodawanie produktu");
        Console.WriteLine($"2. Usuwanie produktu");

        while (!int.TryParse(Console.ReadLine(), out wybor) || wybor < 1 || wybor > 2)
        {
            Console.WriteLine("Nieprawidłowa wartość. Spróbuj ponownie.");
        }
        Console.Clear();
        WykonajAkcjeZarzadzania(wybor);
    }

    private void WykonajAkcjeZarzadzania(int wybor)
    {
        switch (wybor)
        {
            case 1:
                DodawanieProduktow();
                break;
            case 2:
                UsuwanieProduktow();
                break;
            default:
                Console.WriteLine("Nieznana opcja!");
                Console.ReadLine();
                break;
        }
    }

    private void DodawanieProduktow()
    {
        Console.Clear();
        Console.WriteLine("Dodawanie produktów...");
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

    private void UsuwanieProduktow()
    {
        Console.Clear();
        Console.WriteLine("Usuwanie produktów...");
        Console.WriteLine("Podaj ID produktu:");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || !magazyn.produkty.ContainsKey(id))
        {
            Console.WriteLine("Nieprawidłowe ID produktu. Spróbuj ponownie.");
        }
        magazyn.UsunProdukt(id);
        Console.WriteLine("Wciśnij ENTER żeby kontynuować");
        Console.ReadLine();
    }
}
