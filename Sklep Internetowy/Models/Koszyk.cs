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
            if (pozycja == null)
            {
                pozycje.Add(new PozycjaKoszyka(produkt, ilosc));
            }
            else
            {
                pozycja.Ilosc += ilosc;
            }
        }

        public void UsunProdukt(Produkt produkt)
        {
            var pozycja = pozycje.FirstOrDefault(p => p.Produkt.Id == produkt.Id);
            if (pozycja != null)
            {
                pozycje.Remove(pozycja);
            }
        }

        public decimal ObliczCeneCalkowita()
        {
            return pozycje.Sum(p => p.Produkt.Cena * p.Ilosc);
        }

        public List<PozycjaKoszyka> PobierzPozycje()
        {
            return pozycje;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pozycja in pozycje)
            {
                sb.AppendLine(pozycja.ToString());
            }
            sb.AppendLine($"Cena całkowita: {ObliczCeneCalkowita():C}");
            return sb.ToString();
        }
    }
}
