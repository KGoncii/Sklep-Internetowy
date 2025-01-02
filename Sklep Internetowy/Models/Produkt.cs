using System.Diagnostics.Metrics;

namespace Sklep_Internetowy.Models
{
    public class Produkt
    {
        public static int Counter = 1;
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public int Ilosc { get; set; }
        public decimal Cena { get; set; }
        public string[] Kategorie { get; set; }
        public Produkt( string nazwa, string opis, int ilosc, decimal cena, string[] kategorie)
        {
            Id = Counter++;
            Nazwa = nazwa;
            Opis = opis;
            Ilosc = ilosc;
            Cena = cena;
            Kategorie = kategorie;
        }
    }
}
