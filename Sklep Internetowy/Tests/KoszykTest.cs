using Sklep_Internetowy.Models;
using Xunit;

public class KoszykTests : IDisposable
{
    private Koszyk koszyk;

    public KoszykTests()
    {
        koszyk = new Koszyk();
    }

    [Fact]
    public void DodajProdukt_NowyProdukt_DodajeDoKoszyka()
    {
        // Arrange
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });

        // Act
        koszyk.DodajProdukt(produkt, 2);

        // Assert
        var pozycje = koszyk.PobierzPozycje().ToList();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(2, pozycje[0].Ilosc);
    }

    [Fact]
    public void DodajProdukt_IstniejacyProdukt_ZwiekszaIlosc()
    {
        // Arrange
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        koszyk.DodajProdukt(produkt, 2);

        // Act
        koszyk.DodajProdukt(produkt, 3);

        // Assert
        var pozycje = koszyk.PobierzPozycje().ToList();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(5, pozycje[0].Ilosc);
    }

    [Fact]
    public void PobierzPozycje_ZwracaWszystkiePozycje()
    {
        // Arrange
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act
        var pozycje = koszyk.PobierzPozycje().ToList();

        // Assert
        Assert.Equal(2, pozycje.Count);
        Assert.Contains(pozycje, p => p.Produkt == produkt1 && p.Ilosc == 2);
        Assert.Contains(pozycje, p => p.Produkt == produkt2 && p.Ilosc == 3);
    }

    [Fact]
    public void ObliczCalkowitaCene_ZwracaPoprawnaCene()
    {
        // Arrange
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act
        var calkowitaCena = koszyk.ObliczCalkowitaCene();

        // Assert
        Assert.Equal(800m, calkowitaCena);
    }

    [Fact]
    public void ToString_ZwracaPoprawnyFormat()
    {
        // Arrange
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 3);

        // Act
        var koszykString = koszyk.ToString();

        // Assert
        var expectedString = "Produkt1 - 2 szt. - 100,00 zł za szt.\r\nProdukt2 - 3 szt. - 200,00 zł za szt.\r\nRazem: 800,00 zł\r\n";
        Assert.Equal(expectedString, koszykString);
    }

    public void Dispose()
    {
        // Usuwanie wszystkich produktów z koszyka po zakończeniu testów
        koszyk = new Koszyk();
    }
}
