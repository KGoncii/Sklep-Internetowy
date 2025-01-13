public class PayPal : Platnosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność PayPal na kwotę {kwota:C} została zrealizowana.");
    }
}

public class KartaPłatnicza : Platnosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność kartą płatniczą na kwotę {kwota:C} została zrealizowana.");
    }
}

public class MasterCard : Platnosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność MasterCard na kwotę {kwota:C} została zrealizowana.");
    }
}
