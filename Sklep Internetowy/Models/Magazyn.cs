using System;
using System.Collections.Generic;
using Sklep_Internetowy.Models;

public class Magazyn
{
    private Dictionary<int, Produkt> produkty = new Dictionary<int, Produkt>();

    public Magazyn()
    {
        DodajPrzykladoweProdukty();
    }

    public void DodajProdukt(Produkt produkt)
    {
        if (!produkty.ContainsKey(produkt.Id))
        {
            produkty[produkt.Id] = produkt;
            Console.WriteLine($"Produkt {produkt.Nazwa} został dodany.");
        }
        else
        {
            Console.WriteLine($"Produkt o ID {produkt.Id} już istnieje.");
        }
    }

    public void UsunProdukt(int id)
    {
        if (produkty.ContainsKey(id))
        {
            produkty.Remove(id);
            Console.WriteLine($"Produkt o ID {id} został usunięty.");
        }
        else
        {
            Console.WriteLine($"Produkt o ID {id} nie istnieje.");
        }
    }

    public void WyswietlProdukty()
    {
        Console.WriteLine("Lista produktów:");
        if (produkty.Count == 0)
        {
            Console.WriteLine("Brak produktów w magazynie.");
        }
        foreach (var produkt in produkty.Values)
        {
            Console.WriteLine($"ID: {produkt.Id}, Nazwa: {produkt.Nazwa}, Cena: {produkt.Cena:C}, Ilość: {produkt.Ilosc}");
        }
    }

    public void WczytajProdukty(List<Produkt> listaProduktow)
    {
        foreach (var produkt in listaProduktow)
        {
            DodajProdukt(produkt);
        }
    }

    public void DodajPrzykladoweProdukty()
    {
        var produktyPrzykladowe = new List<Produkt>
        {
            new Produkt("Produkt1", "Opis1", 10, 99.99m, new string[] { "Kategoria1" }),
            new Produkt("Produkt2", "Opis2", 5, 199.99m, new string[] { "Kategoria2" }),
            new Produkt("Produkt3", "Opis3", 20, 299.99m, new string[] { "Kategoria3" })
        };

        WczytajProdukty(produktyPrzykladowe);
    }
}
