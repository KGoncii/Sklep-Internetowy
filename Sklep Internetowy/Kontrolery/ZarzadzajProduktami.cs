using Sklep_Internetowy.Models;

public class ZarzadzanieProduktami
{
    int wybor;
    Magazyn magazyn = Magazyn.Instance; // Singleton - zapewnia jedną instancję klasy Magazyn

    public void Wykonaj()
    {
        Console.WriteLine($"1. Dodawanie produktu");
        Console.WriteLine($"2. Usuwanie produktu");

        // Pętla do momentu, aż użytkownik poda prawidłową wartość
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
                DodawanieProduktow(); // Wywołanie metody dodawania produktów
                break;
            case 2:
                UsuwanieProduktow(); // Wywołanie metody usuwania produktów
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
        // Pętla do momentu, aż użytkownik poda prawidłową ilość
        while (!int.TryParse(Console.ReadLine(), out ilosc))
        {
            Console.WriteLine("Nieprawidłowa wartość. Podaj ilość produktu:");
        }
        Console.WriteLine("Podaj cenę produktu:");
        decimal cena;
        // Pętla do momentu, aż użytkownik poda prawidłową cenę
        while (!decimal.TryParse(Console.ReadLine(), out cena))
        {
            Console.WriteLine("Nieprawidłowa wartość. Podaj cenę produktu:");
        }
        Console.WriteLine("Podaj kategorie produktu (oddzielone przecinkami):");
        string[] kategorie = Console.ReadLine().Split(',');

        // Dodanie produktu do magazynu
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
        // Pętla do momentu, aż użytkownik poda prawidłowe ID produktu
        while (!int.TryParse(Console.ReadLine(), out id) || !magazyn.produkty.ContainsKey(id))
        {
            Console.WriteLine("Nieprawidłowe ID produktu. Spróbuj ponownie.");
        }
        // Usunięcie produktu z magazynu
        magazyn.UsunProdukt(id);
        Console.WriteLine("Wciśnij ENTER żeby kontynuować");
        Console.ReadLine();
    }
}
