namespace Sklep_Internetowy.Models
{
    public static class Sesja
    {
        // Przechowuje aktualnie zalogowanego użytkownika
        public static Uzytkownik ZalogowanyUzytkownik { get; set; }

        // Przechowuje koszyk użytkownika
        public static Koszyk KoszykUzytkownika { get; set; } = new Koszyk();

        // Dodaje produkt do koszyka użytkownika
        public static void DodajProduktDoKoszyka(Produkt produkt, int ilosc)
        {
            KoszykUzytkownika.DodajProdukt(produkt, ilosc);
        }

        // Finalizuje zakup, jeśli użytkownik jest zalogowany
        public static void FinalizujZakup()
        {
            if (ZalogowanyUzytkownik == null)
            {
                throw new InvalidOperationException("Użytkownik nie jest zalogowany.");
            }

            Console.WriteLine("Zakup został sfinalizowany.");
            Console.WriteLine(KoszykUzytkownika.ToString());

            // Resetuje koszyk po sfinalizowaniu zakupu
            KoszykUzytkownika = new Koszyk();
        }
    }
}
