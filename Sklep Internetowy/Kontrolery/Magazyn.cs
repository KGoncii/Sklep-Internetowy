using System;
using System.Collections.Generic;
using Sklep_Internetowy.Models;

public class Magazyn
{
    private static Magazyn instance;
    private static readonly object lockObj = new object();

    public Dictionary<int, Produkt> produkty = new Dictionary<int, Produkt>();
    private const string filePath = "produkty.txt";

    private Magazyn()
    {
        Produkt.Counter = 1; // Resetowanie licznika
        WczytajProduktyZPliku();
    }
    //Singleton - wzorzec projektowy, który zapewnia, że dana klasa ma tylko jedną instancję i zapewnia globalny punkt dostępu do tej instancji.
    public static Magazyn Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new Magazyn();
                }
                return instance;
            }
        }
    }

    public void DodajProdukt(Produkt produkt)
    {
        if (!produkty.ContainsKey(produkt.Id))
        {
            produkty[produkt.Id] = produkt;
            Console.WriteLine($"Produkt {produkt.Nazwa} został dodany.");
            ZapiszProduktyDoPliku();
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
            ZapiszProduktyDoPliku();
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

    private void WczytajProduktyZPliku()
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 6)
                {
                    var produkt = new Produkt(parts[1], parts[2], int.Parse(parts[3]), decimal.Parse(parts[4]), parts[5].Split(','));
                    produkt.Id = int.Parse(parts[0]);
                    produkty[produkt.Id] = produkt;
                }
            }
        }
    }

    public void ZapiszProduktyDoPliku()
    {
        var lines = new List<string>();
        foreach (var produkt in produkty.Values)
        {
            var line = $"{produkt.Id};{produkt.Nazwa};{produkt.Opis};{produkt.Ilosc};{produkt.Cena};{string.Join(",", produkt.Kategorie)}";
            lines.Add(line);
        }
        File.WriteAllLines(filePath, lines);
    }
}
