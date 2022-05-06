using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Game_Shop
{
    class Catalog
    {

        private List<Product> products;
        private int numberOfProducts;

        public Catalog()
        {
            numberOfProducts = 0;
            products = new List<Product>();
            Product p1 = new Product("LEGO kocke", 11111, (float)(1299.99), 7, true);
            Product p2 = new Product("Slagalica", 22222, (float)(1999.99), 10, true);
            Product p3 = new Product("Plisani meda", 33333, (float)(800), 5, false);
            Product p4 = new Product("Probni", 44444, (float)(20.25), 7, true);
            this.addProduct(p1);
            this.addProduct(p2);
            this.addProduct(p3);
            this.addProduct(p4);
        }

        public void addProduct(Product product)
        {
            products.Add(product);
        }

        public Product getProduct(int UPC)
        {
            foreach (Product p in products)
            {
                if (p.getUPC() == UPC) return p;
            }
            return null;
        }

        public bool contains(int UPC)
        {
            foreach (Product p in products)
            {
                if (p.getUPC() == UPC) return true;
            }
            return false;
        }

        public void calculateAndDisplayInformation(int UPC)
        {
            if (!this.contains(UPC))
            {
                Console.WriteLine("Proizvod sa datim barkodom ne postoji u katalogu!");
            } else
            {
                Product product = this.getProduct(UPC);
                float finalPrice = product.priceAfterTaxAndDiscount();
                float deduction = product.deduction();
                Console.WriteLine("Cena proizvoda iznosi " + finalPrice.ToString("0.00") + " din");
                Console.WriteLine("Odbijeno je " + deduction.ToString("0.00") + " din");
            }
        }

    }
}
