using Sklep_Internetowy.Models;
using Xunit;

public class KoszykTests
{
    [Fact]
    public void DodajProdukt_DodajeNowyProduktDoKoszyka()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });

        // Act
        koszyk.DodajProdukt(produkt, 2);

        // Assert
        var pozycje = koszyk.PobierzPozycje();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(2, pozycje[0].Ilosc);
    }

    [Fact]
    public void DodajProdukt_ZwiekszaIloscIstniejacegoProduktu()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        koszyk.DodajProdukt(produkt, 2);

        // Act
        koszyk.DodajProdukt(produkt, 3);

        // Assert
        var pozycje = koszyk.PobierzPozycje();
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(5, pozycje[0].Ilosc);
    }

    [Fact]
    public void UsunProdukt_UsuwaProduktZKoszyka()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        koszyk.DodajProdukt(produkt, 2);

        // Act
        koszyk.UsunProdukt(produkt);

        // Assert
        var pozycje = koszyk.PobierzPozycje();
        Assert.Empty(pozycje);
    }

    [Fact]
    public void ObliczCeneCalkowita_ZwracaPoprawnaCene()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 1);

        // Act
        var cenaCalkowita = koszyk.ObliczCeneCalkowita();

        // Assert
        Assert.Equal(400m, cenaCalkowita);
    }

    [Fact]
    public void PobierzPozycje_ZwracaListePozycji()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        koszyk.DodajProdukt(produkt, 2);

        // Act
        var pozycje = koszyk.PobierzPozycje();

        // Assert
        Assert.Single(pozycje);
        Assert.Equal(produkt, pozycje[0].Produkt);
        Assert.Equal(2, pozycje[0].Ilosc);
    }

    [Fact]
    public void ToString_ZwracaPoprawnyString()
    {
        // Arrange
        var koszyk = new Koszyk();
        var produkt1 = new Produkt("Produkt1", "Opis1", 10, 100m, new string[] { "Kategoria1" });
        var produkt2 = new Produkt("Produkt2", "Opis2", 5, 200m, new string[] { "Kategoria2" });
        koszyk.DodajProdukt(produkt1, 2);
        koszyk.DodajProdukt(produkt2, 1);

        // Act
        var koszykString = koszyk.ToString();

        // Assert
        var expectedString = "Produkt1 - 2 szt. - 100,00 zł za szt.\r\nProdukt2 - 1 szt. - 200,00 zł za szt.\r\nCena całkowita: 400,00 zł\r\n";
        Assert.Equal(expectedString, koszykString);
    }
}
