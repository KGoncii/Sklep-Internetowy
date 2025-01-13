using Sklep_Internetowy.Models;
using Xunit;

public class KoszykTests : IDisposable
{
    private Koszyk koszyk;

    public KoszykTests()
    {
        // Inicjalizacja nowego koszyka przed każdym testem
        koszyk = new Koszyk();
    }

    [Fact]
    public void DodajProdukt_NowyProdukt_DodajeDoKoszyka()
    {
        // Arrange - przygotowanie danych testowych
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });

        // Act - dodanie produktu do koszyka
        koszyk.DodajProdukt(produkt, 2);

        // Assert - sprawdzenie, czy produkt został dodany do koszyka
        var pozycje = koszyk.PobierzPozycje().ToList();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(2, pozycje[0].Ilosc);
    }

    [Fact]
    public void DodajProdukt_IstniejacyProdukt_ZwiekszaIlosc()
    {
        // Arrange - przygotowanie danych testowych
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        koszyk.DodajProdukt(produkt, 2);

        // Act - dodanie tego samego produktu do koszyka
        koszyk.DodajProdukt(produkt, 3);

        // Assert - sprawdzenie, czy ilość produktu w koszyku została zwiększona
        var pozycje = koszyk.PobierzPozycje().ToList();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(5, pozycje[0].Ilosc);
    }

    [Fact]
    public void PobierzPozycje_ZwracaWszystkiePozycje()
    {
        // Arrange - przygotowanie danych testowych
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act - pobranie wszystkich pozycji z koszyka
        var pozycje = koszyk.PobierzPozycje().ToList();

        // Assert - sprawdzenie, czy wszystkie pozycje zostały poprawnie pobrane
        Assert.Equal(2, pozycje.Count);
        Assert.Contains(pozycje, p => p.Produkt == produkt1 && p.Ilosc == 2);
        Assert.Contains(pozycje, p => p.Produkt == produkt2 && p.Ilosc == 3);
    }

    [Fact]
    public void ObliczCalkowitaCene_ZwracaPoprawnaCene()
    {
        // Arrange - przygotowanie danych testowych
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act - obliczenie całkowitej ceny koszyka
        var calkowitaCena = koszyk.ObliczCalkowitaCene();

        // Assert - sprawdzenie, czy całkowita cena jest poprawna
        Assert.Equal(800m, calkowitaCena);
    }

    [Fact]
    public void ToString_ZwracaPoprawnyFormat()
    {
        // Arrange - przygotowanie danych testowych
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act - pobranie reprezentacji tekstowej koszyka
        var koszykString = koszyk.ToString();

        // Assert - sprawdzenie, czy reprezentacja tekstowa jest poprawna
        var expectedString = "Produkt1 - 2 szt. - 100,00 zł za szt.\r\nProdukt2 - 3 szt. - 200,00 zł za szt.\r\nRazem: 800,00 zł\r\n";
        Assert.Equal(expectedString, koszykString);
    }

    public void Dispose()
    {
        // Usuwanie wszystkich produktów z koszyka po zakończeniu testów
        koszyk = new Koszyk();
    }
}
