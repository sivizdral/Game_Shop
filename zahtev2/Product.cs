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

    public float priceWithArbitraryTax(float taxPercent)
    {
        if (taxPercent < 0) return -1;
        float priceWithTax = (float)(price * (100.0 + taxPercent) / 100);
        return priceWithTax;
    }

    public float percentAmount(float percent)
    {
        return (float)(price * percent / 100.0);
    }

    public float priceAfterTaxAndDiscount(float taxPercent, float discountPercent)
    {
        return (float)(price + percentAmount(taxPercent) - percentAmount(discountPercent));
    }

    public static bool checkIfCorrectName(String name)
    {
        if (name == "")
        {
            Console.WriteLine("Naziv mora sadrzati bar jedan karakter!");
            return false;
        }
        else if (name.Length > 100)
        {
            Console.WriteLine("Naziv ne moze biti duzi od 100 karaktera!");
            return false;
        }
        else return true;
    }

    public static bool checkIfCorrectUPC(String upc)
    {
        if (upc == "")
        {
            Console.WriteLine("UPC ne moze biti prazan!");
            return false;
        }
        else
        {
            try
            {
                int upcint = Int32.Parse(upc);
                if (upcint < 0)
                {
                    Console.WriteLine("UPC ne moze biti negativan broj!");
                    return false;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("UPC moze sadrzati samo numerike i mora biti ceo pozitivan broj!");
                return false;
            }
        }
        return true;
    }

    public static bool checkIfCorrectPrice(String price)
    {
        if (price == "")
        {
            Console.WriteLine("Cena ne moze biti ostavljena prazna!");
            return false;
        }
        else
        {
            try
            {
                if (price.Contains(','))
                {
                    price = price.Replace(',', '.');
                }
                float priceFloat = (float)Double.Parse(price);
                if (priceFloat < 0)
                {
                    Console.WriteLine("Cena ne moze biti negativan broj!");
                    return false;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Cena moze sadrzati samo numerike i decimalnu tacku!");
                return false;
            }
        }
        return true;
    }

    public static String convertPercent(String percent)
    {
        if (percent.EndsWith('%')) percent = percent.Remove(percent.Length - 1);
        if (percent.Contains(','))
        {
            percent = percent.Replace(',', '.');
        }
        return percent;
    }

    public static bool checkIfCorrectPercent(String percent)
    {
        if (percent == "")
        {
            Console.WriteLine("Procenat ne moze biti ostavljen prazan!");
            return false;
        }
        else
        {
            try
            {
                percent = convertPercent(percent);
                float percentFloat = (float)Double.Parse(percent);
                if (percentFloat < 0)
                {
                    Console.WriteLine("Procenat ne moze biti negativan broj!");
                    return false;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Procenat moze sadrzati samo numerike i decimalnu tacku, kao i znak % na kraju!");
                return false;
            }
        }
        return true;
    }

    public static void Main(string[] args)
    {
        String name = "";
        int UPC = 0;
        float price = 0;
        float taxPercent = 0;
        float discountPercent = 0;

        while (true)
        {
            bool correctName = false;
            while (!correctName)
            {
                Console.WriteLine("Dobrodosli u prodavnicu! Unesite zeljeni naziv proizvoda: ");
                name = Console.ReadLine();
                correctName = Product.checkIfCorrectName(name);
            }

            bool correctUPC = false;
            while (!correctUPC)
            {
                Console.WriteLine("Unesite UPC barkod proizvoda: ");
                String upcString = Console.ReadLine();
                correctUPC = Product.checkIfCorrectUPC(upcString);
                if (correctUPC) UPC = Int32.Parse(upcString);
            }

            bool correctPrice = false;
            while (!correctPrice)
            {
                Console.WriteLine("Unesite cenu prozivoda: ");
                String priceString = Console.ReadLine();
                correctPrice = Product.checkIfCorrectPrice(priceString);
                if (correctPrice)
                {
                    if (priceString.Contains(','))
                    {
                        priceString = priceString.Replace(',', '.');
                    }
                    price = (float)Double.Parse(priceString);
                }
            }

            bool correctPercent = false;
            while (!correctPercent)
            {
                Console.WriteLine("Unesite zeljeni procenat poreza: ");
                String taxPercentString = Console.ReadLine();
                correctPercent = Product.checkIfCorrectPercent(taxPercentString);
                if (correctPercent)
                {
                    taxPercent = (float)Double.Parse(Product.convertPercent(taxPercentString));
                }
            }

            bool correctDiscount = false;
            while (!correctDiscount)
            {
                Console.WriteLine("Unesite zeljeni procenat popusta: ");
                String discountPercentString = Console.ReadLine();
                correctDiscount = Product.checkIfCorrectPercent(discountPercentString);
                if (correctDiscount)
                {
                    discountPercent = (float)Double.Parse(Product.convertPercent(discountPercentString));
                }
            }

            Product product = new Product(name, UPC, price);
            float priceWithArbitraryTax = product.priceWithArbitraryTax(taxPercent);
            Console.WriteLine("Na osnovnu cenu od " + price.ToString("0.00") + " din, apsolutni iznos poreza je " + product.percentAmount(taxPercent).ToString("0.00") + " din nakon " + taxPercent.ToString() + "% poreza.");
            Console.WriteLine("Apsolutni iznos popusta na cenu je " + product.percentAmount(discountPercent).ToString("0.00") + " din za " + discountPercent.ToString() + "% popusta.");
            Console.WriteLine("Cena nakon sracunatog popusta i poreza iznosi: " + product.priceAfterTaxAndDiscount(taxPercent, discountPercent).ToString("0.00") + " din");
            Console.WriteLine("Da li zelite da nastavite sa radom? da/ne: ");
            String additional = Console.ReadLine();
            if (additional == "ne" || additional == "NE" || additional == "Ne") break;
        }

    }

}
