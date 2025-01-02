using System.Text;

namespace Sklep_Internetowy.Models
{
    public class PozycjaKoszyka
    {
        public Produkt Produkt { get; set; }
        public int Ilosc { get; set; }

        public PozycjaKoszyka(Produkt produkt, int ilosc)
        {
            Produkt = produkt;
            Ilosc = ilosc;
        }

        public override string ToString()
        {
            return $"{Produkt.Nazwa} - {Ilosc} szt. - {Produkt.Cena:C} za szt.";
        }
    }

    public class Koszyk
    {
        private List<PozycjaKoszyka> pozycje = new List<PozycjaKoszyka>();

        public void DodajProdukt(Produkt produkt, int ilosc)
        {
            var pozycja = pozycje.FirstOrDefault(p => p.Produkt.Id == produkt.Id);
            if (pozycja != null)
            {
                pozycja.Ilosc += ilosc;
            }
            else
            {
                pozycje.Add(new PozycjaKoszyka(produkt, ilosc));
            }
        }

        public IEnumerable<PozycjaKoszyka> PobierzPozycje()
        {
            return pozycje;
        }

        public decimal ObliczCalkowitaCene()
        {
            return pozycje.Sum(p => p.Produkt.Cena * p.Ilosc);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pozycja in pozycje)
            {
                sb.AppendLine($"{pozycja.Produkt.Nazwa} - {pozycja.Ilosc} szt. - {pozycja.Produkt.Cena:C} za szt.");
            }
            sb.AppendLine($"Razem: {ObliczCalkowitaCene():C}");
            return sb.ToString();
        }
    }
}
