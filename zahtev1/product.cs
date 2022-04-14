using System;

public class Product
{
    private String Name;
    private int UPC;
    private float price;

    public Product(String name, int UPC, float price)
    {
        this.Name = name;
        this.UPC = UPC;
        this.price = price;
    }

    public float priceBeforeTaxation()
    {
        return price;
    }

    public float priceWithStandardTax()
    {
        float priceWithTax = (float)(price * 1.2);
        return priceWithTax;
    }

    public float priceWithArbitraryTax(int taxPercent)
    {
        if (taxPercent < 0) return -1;
        float priceWithTax = (float)(price * (100.0 + taxPercent) / 100);
        return priceWithTax;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Dobrodosli u prodavnicu! Unesite zeljeni naziv proizvoda: ");
        String name = Console.ReadLine();
        Console.WriteLine("Unesite UPC barkod proizvoda: ");
        int UPC = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Unesite cenu prozivoda: ");
        float price = (float)(Convert.ToDouble(Console.ReadLine()));
        Console.WriteLine("Unesite zeljeni procenat poreza: ");
        int taxPercent = Convert.ToInt32(Console.ReadLine());
        Product product = new Product(name, UPC, price);
        float priceWithStandardTax = product.priceWithStandardTax();
        float priceWithArbitraryTax = product.priceWithArbitraryTax(taxPercent);
        Console.WriteLine("Cena " + price.ToString("0.00") + " din pre poreza i " + priceWithStandardTax.ToString("0.00") + " din nakon 20% poreza.");
        Console.WriteLine("Cena " + price.ToString("0.00") + " din pre poreza i " + priceWithArbitraryTax.ToString("0.00") + " din nakon " + taxPercent.ToString() + "% poreza.");

    }

}
