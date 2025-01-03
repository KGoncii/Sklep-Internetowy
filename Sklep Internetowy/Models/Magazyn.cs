﻿using System;
using System.Collections.Generic;
using Sklep_Internetowy.Models;

public class Magazyn
{
    public Dictionary<int, Produkt> produkty = new Dictionary<int, Produkt>();
    private const string filePath = "produkty.txt";

    public Magazyn()
    {
        Produkt.Counter = 1; // Resetowanie licznika
        WczytajProduktyZPliku();
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

    public void WczytajProdukty(List<Produkt> listaProduktow)
    {
        foreach (var produkt in listaProduktow)
        {
            DodajProdukt(produkt);
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

    public void KupProdukt(int id, Platosc metodaPłatności)
    {
        if (produkty.ContainsKey(id))
        {
            var produkt = produkty[id];
            metodaPłatności.Zaplac(produkt.Cena);
            Console.WriteLine($"Produkt {produkt.Nazwa} został zakupiony.");
        }
        else
        {
            Console.WriteLine($"Produkt o ID {id} nie istnieje.");
        }
    }
}
