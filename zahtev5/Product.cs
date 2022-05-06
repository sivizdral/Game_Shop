using SE_Game_Shop;
using System;

public class Product
{
    private String name;
    private int UPC;
    private float price;
    private float specialDiscount;
    private bool discountAdvantage;

    private static float discount = 15;
    private static float tax = 20;

    public Product(String name = "", int UPC = 0, float price = 0, float specialDiscount = 0, bool discountAdvantage = false)
    {
        this.name = name;
        this.UPC = UPC;
        this.price = price;
        this.specialDiscount = specialDiscount;
        this.discountAdvantage = discountAdvantage;
    }

    public int getUPC()
    {
        return UPC;
    }

    public float getSpecialDiscount()
    {
        return specialDiscount;
    }

    public float priceBeforeTaxation()
    {
        return price;
    }

    public void setUPC()
    {
        int UPC = 0;
        bool correctUPC = false;
        while (!correctUPC)
        {
            Console.WriteLine("Unesite UPC barkod proizvoda: ");
            String upcString = Console.ReadLine();
            correctUPC = Product.checkIfCorrectUPC(upcString);
            if (correctUPC) UPC = Int32.Parse(upcString);
        }
        this.UPC = UPC;
    }

    public void setName()
    {
        String name = "";
        bool correctName = false;
        while (!correctName)
        {
            Console.WriteLine("Dobrodosli u prodavnicu! Unesite zeljeni naziv proizvoda: ");
            name = Console.ReadLine();
            correctName = Product.checkIfCorrectName(name);
        }
        this.name = name;
    }

    public void setPrice()
    {
        float price = 0;
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
        this.price = price;
    }

    public void setSpecialDiscount()
    {
        float specialDiscount = 0;
        bool hasDiscount = false;
        bool correctDiscount = false;
        while (!correctDiscount)
        {
            Console.WriteLine("Unesite zeljeni procenat popusta: ");
            String discountPercentString = Console.ReadLine();
            if (discountPercentString != "")
            {
                hasDiscount = true;
                correctDiscount = Product.checkIfCorrectPercent(discountPercentString);
                if (correctDiscount)
                {
                    specialDiscount = (float)Double.Parse(Product.convertPercent(discountPercentString));
                    this.specialDiscount = specialDiscount;
                    Console.WriteLine("Da li specijalni popust ima prednost nad porezom? da/ne: ");
                    String advantage = Console.ReadLine();
                    if (advantage == "da" || advantage == "DA" || advantage == "Da") this.discountAdvantage = true;
                    else this.discountAdvantage = false;
                }
            }
            else
            {
                specialDiscount = 0;
                Console.WriteLine("Da li ste sigurni da ne zelite da uracunate popust? da/ne: ");
                String yesDiscount = Console.ReadLine();
                if (yesDiscount == "da" || yesDiscount == "DA" || yesDiscount == "Da")
                    correctDiscount = true;
            }
        }
    }

    public static void changeTaxPercent()
    {
        bool correctPercent = false;
        while (!correctPercent)
        {
            Console.WriteLine("Unesite zeljeni procenat poreza: ");
            String taxPercentString = Console.ReadLine();
            correctPercent = Product.checkIfCorrectPercent(taxPercentString);
            if (correctPercent)
            {
                Product.tax = (float)Double.Parse(Product.convertPercent(taxPercentString));
            }
        }
    }

    public static void changeDiscountPercent()
    {
        bool correctPercent = false;
        while (!correctPercent)
        {
            Console.WriteLine("Unesite zeljeni procenat popusta: ");
            String discountPercentString = Console.ReadLine();
            correctPercent = Product.checkIfCorrectPercent(discountPercentString);
            if (correctPercent)
            {
                Product.discount = (float)Double.Parse(Product.convertPercent(discountPercentString));
            }
        }
    }

    public float priceWithArbitraryTax(float taxPercent)
    {
        if (taxPercent < 0) return -1;
        float priceWithTax = (float)(price * (100.0 + taxPercent) / 100);
        return priceWithTax;
    }

    public float percentAmount(float baseSum, float percent)
    {
        return (float)(baseSum * percent / 100.0);
    }

    public float priceAfterTaxAndDiscount()
    {
        if (this.discountAdvantage == true)
        {
            float discountedPrice = (float)(price - percentAmount(price, this.specialDiscount));
            float finalPrice = (float)(discountedPrice + percentAmount(discountedPrice, Product.tax) - percentAmount(discountedPrice, Product.discount));
            return finalPrice;
        } else
        {
            float taxedPrice = (float)(price + percentAmount(price, Product.tax));
            float discountedPrice = (float)(taxedPrice - percentAmount(taxedPrice, Product.discount) - percentAmount(taxedPrice, this.specialDiscount));
            return discountedPrice;
        }
        
    }

    public float deduction()
    {
        if (this.discountAdvantage == true)
        {
            float specialDiscount = percentAmount(price, this.specialDiscount);
            float discountedPrice = (float)(price - specialDiscount);
            float additionalDiscount = percentAmount(discountedPrice, Product.discount);  
            return specialDiscount + additionalDiscount;
        }
        else
        {
            float tax = percentAmount(price, Product.tax);
            float taxedPrice = price - tax;
            float discount = percentAmount(taxedPrice, this.specialDiscount + Product.discount);
            return discount;
        }
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

        String select = "";
        Catalog catalog = new Catalog();

        while (true)
        {
            bool correctSelect = false;

            while (!correctSelect)
            {
                Console.WriteLine("1. Unos novog proizvoda u katalog");
                Console.WriteLine("2. Obracun cene proizvoda za zadati UPC");
                Console.WriteLine("3. Promena iznosa opsteg poreza");
                Console.WriteLine("4. Promena iznosa opsteg popusta");
                Console.WriteLine("Dobrodosli u prodavnicu! Izaberite opciju koju zelite: ");

                select = Console.ReadLine();

                if (select == "1" || select == "2" || select == "3" || select == "4") correctSelect = true;
                else Console.WriteLine("Unos nije ispravan! Molimo unesite broj 1 ili 2 kao izbor: ");
            }
            
            if (select == "1")
            {
                Product product = new Product();
                product.setName();
                product.setUPC();
                product.setPrice();
                product.setSpecialDiscount();
                catalog.addProduct(product);
                Console.WriteLine("Uspesno ste uneli proizvod u katalog!");
            } else if (select == "2")
            {
                int UPC = 0;
                bool correctUPC = false;
                while (!correctUPC)
                {
                    Console.WriteLine("Unesite UPC barkod proizvoda: ");
                    String upcString = Console.ReadLine();
                    correctUPC = Product.checkIfCorrectUPC(upcString);
                    if (correctUPC) UPC = Int32.Parse(upcString);
                }
                catalog.calculateAndDisplayInformation(UPC);
            } else if (select == "3")
            {
                changeTaxPercent();
            } else if (select == "4")
            {
                changeDiscountPercent();
            } 
            
            Console.WriteLine("Da li zelite da nastavite sa radom? da/ne: ");
            String additional = Console.ReadLine();
            if (additional == "ne" || additional == "NE" || additional == "Ne") break;
        }

    }

}
