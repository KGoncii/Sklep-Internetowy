using Xunit;
using Sklep_Internetowy.Models;

public class MagazynTests
{
    private Magazyn magazyn;
    private List<Produkt> addedProducts;

    public MagazynTests()
    {
        Magazyn magazyn = Magazyn.Instance;
        addedProducts = new List<Produkt>();
    }

    [Fact]
    public void DodajProdukt_ProduktNieIstnieje_ProduktDodany()
    {
        // Arrange
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });

        // Act
        magazyn.DodajProdukt(produkt);
        addedProducts.Add(produkt);

        // Assert
        Assert.Contains(produkt, magazyn.produkty.Values);
        Dispose();
    }

    [Fact]
    public void DodajProdukt_ProduktIstnieje_WyswietlaKomunikat()
    {
        // Arrange
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);
        addedProducts.Add(produkt);

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.DodajProdukt(produkt));

        // Assert
        Assert.Contains("Produkt o ID", consoleOutput);
        Dispose();
    }

    [Fact]
    public void UsunProdukt_ProduktIstnieje_ProduktUsuniety()
    {
        // Arrange
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);
        addedProducts.Add(produkt);

        // Act
        magazyn.UsunProdukt(produkt.Id);
        addedProducts.Remove(produkt);

        // Assert
        Assert.DoesNotContain(produkt, magazyn.produkty.Values);
        Dispose();
    }

    [Fact]
    public void UsunProdukt_ProduktNieIstnieje_WyswietlaKomunikat()
    {
        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.UsunProdukt(999));

        // Assert
        Assert.Contains("Produkt o ID", consoleOutput);
        Dispose();
    }

    [Fact]
    public void WyswietlProdukty_ProduktyIstnieja_WyswietlaProdukty()
    {
        // Arrange
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);
        addedProducts.Add(produkt);

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.WyswietlProdukty());

        // Assert
        Assert.Contains("ID: ", consoleOutput);
        Dispose();
    }

    [Fact]
    public void WczytajProdukty_ListaProduktow_ProduktyDodane()
    {
        // Arrange
        var produkty = new List<Produkt>
        {
            new Produkt("Produkt1", "Opis1", 10, 99.99m, new string[] { "Kategoria1" }),
            new Produkt("Produkt2", "Opis2", 5, 199.99m, new string[] { "Kategoria2" })
        };

        // Act
        magazyn.WczytajProdukty(produkty);
        addedProducts.AddRange(produkty);

        // Assert
        foreach (var produkt in produkty)
        {
            Assert.Contains(produkt, magazyn.produkty.Values);
        }
        Dispose();
    }

    private string CaptureConsoleOutput(Action action)
    {
        var consoleOutput = new System.IO.StringWriter();
        Console.SetOut(consoleOutput);
        action();
        return consoleOutput.ToString();
    }
    public void Dispose()
    {
        foreach (var produkt in addedProducts)
        {
            magazyn.UsunProdukt(produkt.Id);
        }
        addedProducts.Clear();
    }
}
