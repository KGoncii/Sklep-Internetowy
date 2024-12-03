namespace Sklep_Internetowy.Models
{
    public class Uzytkownik
    {
        private static int Counter = 1;
        public int Id { get; private set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Hasło { get; set; }
        public int Rola { get; set; }
        public Uzytkownik(string imie, string nazwisko, string adres, string email, string hasło, int rola)
        {
            Id = Counter++;
            Imie = imie;
            Nazwisko = nazwisko;
            Adres = adres;
            Email = email;
            Hasło = hasło;
            Rola = rola;
        }
    }
}
