using Sklep_Internetowy;
using Sklep_Internetowy.Models;
class Program
{
    static void Main()
    {
        var produkt1 = new Produkt("Telefon", "Smartfon", 10, 1999.99m, new string[] { "Elektronika", "Telefony" });
        var produkt2 = new Produkt("Laptop", "Ultrabook", 5, 4999.99m, new string[] { "Elektronika", "Komputery" });
        var Magazyn = new Magazyn();
        Magazyn.DodajProdukt(produkt1);
        Magazyn.DodajProdukt(produkt2);
        Magazyn.WyswietlProdukty();
    }
}