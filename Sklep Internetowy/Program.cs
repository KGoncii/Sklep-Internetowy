using Sklep_Internetowy.Models;
class Program
{
    static void Main()
    {
        var zalogowanyUzytkownik = Sesja.ZalogowanyUzytkownik;

        var ekranStartowy = new EkranStartowy(zalogowanyUzytkownik);

        bool running = true;
        while (running)
        {
            Console.Clear();
            ekranStartowy.Wyswietl();
            if (!int.TryParse(Console.ReadLine(), out int wybor))
            {
                Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                continue;
            }
            ekranStartowy.WykonajAkcje(wybor);
        }
    }
}
