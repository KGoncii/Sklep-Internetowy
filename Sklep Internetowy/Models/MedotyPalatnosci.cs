public class PayPal : Platosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność PayPal na kwotę {kwota:C} została zrealizowana.");
    }
}

public class KartaPłatnicza : Platosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność kartą płatniczą na kwotę {kwota:C} została zrealizowana.");
    }
}

public class MasterCard : Platosc
{
    public void Zaplac(decimal kwota)
    {
        Console.WriteLine($"Płatność MasterCard na kwotę {kwota:C} została zrealizowana.");
    }
}
