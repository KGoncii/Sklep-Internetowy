using System;
using System.Collections.Generic;
using Xunit;
using Sklep_Internetowy.Models;

public class MagazynTests
{
    [Fact]
    public void DodajProdukt_ProduktNieIstnieje_ProduktDodany()
    {
        // Arrange
        var magazyn = new Magazyn();
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });

        // Act
        magazyn.DodajProdukt(produkt);

        // Assert
        Assert.Contains(produkt, magazyn.produkty.Values);
    }

    [Fact]
    public void DodajProdukt_ProduktIstnieje_WyswietlaKomunikat()
    {
        // Arrange
        var magazyn = new Magazyn();
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.DodajProdukt(produkt));

        // Assert
        Assert.Contains("Produkt o ID", consoleOutput);
    }

    [Fact]
    public void UsunProdukt_ProduktIstnieje_ProduktUsuniety()
    {
        // Arrange
        var magazyn = new Magazyn();
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);

        // Act
        magazyn.UsunProdukt(produkt.Id);

        // Assert
        Assert.DoesNotContain(produkt, magazyn.produkty.Values);
    }

    [Fact]
    public void UsunProdukt_ProduktNieIstnieje_WyswietlaKomunikat()
    {
        // Arrange
        var magazyn = new Magazyn();

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.UsunProdukt(999));

        // Assert
        Assert.Contains("Produkt o ID", consoleOutput);
    }

    [Fact]
    public void WyswietlProdukty_BrakProduktow_WyswietlaKomunikat()
    {
        // Arrange
        var magazyn = new Magazyn();

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.WyswietlProdukty());

        // Assert
        Assert.Contains("Brak produktów w magazynie", consoleOutput);
    }

    [Fact]
    public void WyswietlProdukty_ProduktyIstnieja_WyswietlaProdukty()
    {
        // Arrange
        var magazyn = new Magazyn();
        var produkt = new Produkt("NowyProdukt", "Opis", 10, 50.0m, new string[] { "Kategoria" });
        magazyn.DodajProdukt(produkt);

        // Act
        var consoleOutput = CaptureConsoleOutput(() => magazyn.WyswietlProdukty());

        // Assert
        Assert.Contains("ID: ", consoleOutput);
    }

    [Fact]
    public void WczytajProdukty_ListaProduktow_ProduktyDodane()
    {
        // Arrange
        var magazyn = new Magazyn();
        var produkty = new List<Produkt>
        {
            new Produkt("Produkt1", "Opis1", 10, 99.99m, new string[] { "Kategoria1" }),
            new Produkt("Produkt2", "Opis2", 5, 199.99m, new string[] { "Kategoria2" })
        };

        // Act
        magazyn.WczytajProdukty(produkty);

        // Assert
        foreach (var produkt in produkty)
        {
            Assert.Contains(produkt, magazyn.produkty.Values);
        }
    }

    private string CaptureConsoleOutput(Action action)
    {
        var consoleOutput = new System.IO.StringWriter();
        Console.SetOut(consoleOutput);
        action();
        return consoleOutput.ToString();
    }
}
