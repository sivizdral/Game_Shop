using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Game_Shop
{
    class Catalog
    {

        struct SpecialDiscountElement
        {
            public int UPC;
            public float specialDiscount;
            public SpecialDiscountElement(int UPC, float specialDiscount)
            {
                this.UPC = UPC;
                this.specialDiscount = specialDiscount;
            }
        }

        private List<SpecialDiscountElement> products;
        private int numberOfProducts;

        public Catalog()
        {
            numberOfProducts = 0;
            products = new List<SpecialDiscountElement>();
        }

        public void addProduct(int UPC, float specialDiscount)
        {
            SpecialDiscountElement sde = new SpecialDiscountElement(UPC, specialDiscount);
            products.Add(sde);
        }

        public bool contains(int UPC)
        {
            foreach (SpecialDiscountElement p in products)
            {
                if (p.UPC == UPC) return true;
            }
            return false;
        }

        public float specialDiscount(int UPC)
        {
            foreach (SpecialDiscountElement p in products)
            {
                if (p.UPC == UPC) return p.specialDiscount;
            }
            return -1;
        }

        public void discountEnterProcedure()
        {
            int UPC = 0;
            float specialDiscountPercent = 0;

            bool correctUPC = false;
            while (!correctUPC)
            {
                Console.WriteLine("Unesite UPC barkod proizvoda: ");
                String upcString = Console.ReadLine();
                correctUPC = Product.checkIfCorrectUPC(upcString);
                if (correctUPC) UPC = Int32.Parse(upcString);
            }

            bool correctPercent = false;
            while (!correctPercent)
            {
                Console.WriteLine("Unesite zeljeni procenat specijalnog popusta: ");
                String percentString = Console.ReadLine();
                correctPercent = Product.checkIfCorrectPercent(percentString);
                if (correctPercent)
                {
                    specialDiscountPercent = (float)Double.Parse(Product.convertPercent(percentString));
                }
            }

            this.addProduct(UPC, specialDiscountPercent);
        }

    }
}
