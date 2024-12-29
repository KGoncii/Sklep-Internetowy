namespace Sklep_Internetowy.Models
{
    public static class Sesja
    {
        public static Uzytkownik ZalogowanyUzytkownik { get; set; }
        public static Koszyk KoszykUzytkownika { get; set; } = new Koszyk();

        public static void DodajProduktDoKoszyka(Produkt produkt, int ilosc)
        {
            KoszykUzytkownika.DodajProdukt(produkt, ilosc);
        }

        public static void FinalizujZakup()
        {
            if (ZalogowanyUzytkownik == null)
            {
                throw new InvalidOperationException("Użytkownik nie jest zalogowany.");
            }

            Console.WriteLine("Zakup został sfinalizowany.");
            Console.WriteLine(KoszykUzytkownika.ToString());

            KoszykUzytkownika = new Koszyk();
        }
    }
}
