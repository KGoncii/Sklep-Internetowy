namespace Sklep_Internetowy.Models
{
    public class Uzytkownik
    {
        public string Login { get; set; }
        public string Haslo { get; set; }
        public int Rola { get; set; } // 1 = klient, 2 = admin

        public Uzytkownik(string login, string haslo, int rola)
        {
            Login = login;
            Haslo = haslo;
            Rola = rola;
        }

        public static Uzytkownik FromString(string line)
        {
            var parts = line.Split('|');
            return new Uzytkownik(parts[0], parts[1], int.Parse(parts[2]));
        }
    }
}
