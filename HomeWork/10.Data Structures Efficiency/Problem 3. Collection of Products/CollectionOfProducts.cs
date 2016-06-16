using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Problem_3.Collection_of_Products
{
    public class ProductIDComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.ID.CompareTo(y.ID);
        }
    }

    public class CollectionOfProducts
    { 
        OrderedDictionary<int, Product> productByID = new OrderedDictionary<int, Product>(); //helper structure
        Dictionary<string, OrderedSet<Product>> productByTitle = new Dictionary<string, OrderedSet<Product>>();
        OrderedDictionary<int, OrderedSet<Product>> productByPrice = new OrderedDictionary<int, OrderedSet<Product>>();
        Dictionary<string, OrderedDictionary<int, OrderedSet<Product>>> productByTitleAndPrice = new Dictionary<string, OrderedDictionary<int, OrderedSet<Product>>>();
        Dictionary<string, OrderedDictionary<int, OrderedSet<Product>>> productBySupplierAndPrice = new Dictionary<string, OrderedDictionary<int, OrderedSet<Product>>>();

        public static void Main(string[] args)
        {
            CollectionOfProducts newCollection = new CollectionOfProducts();
            newCollection.AddProduct(110, "Nonstop", "OCB", 100);
            newCollection.AddProduct(1110, "Nonstop", "Fars", 3500);
            newCollection.AddProduct(1411, "Nonstop", "Lars", 3500);
            newCollection.AddProduct(142, "Nonstop", "Mars", 3500);
            newCollection.AddProduct(143, "Nonstop", "Sars", 3500);
            newCollection.AddProduct(2, "Beans", "M&CO", 350);
            newCollection.AddProduct(131, "Beans", "C&CO", 3500);
            newCollection.AddProduct(333, "Onions", "M&CO", 500);
            newCollection.AddProduct(444, "Wood", "M&CO", 700);
            newCollection.AddProduct(332, "Shoes", "C&CO", 1000);
            newCollection.AddProduct(123, "Dress", "C&CO", 900);
            newCollection.AddProduct(115, "Car", "M&CO", 5500);
            newCollection.AddProduct(600, "Car Lambo", "M&CO", 5500);
            newCollection.AddProduct(7, "Car Peugeot", "M&CO", 5500);
            newCollection.AddProduct(8, "Car Lancia", "M&CO", 5500);

            var outputUnique = newCollection.FindProductByPriceRange(90, 1000);
            Console.WriteLine("Find Product by Price Range");
            Console.WriteLine(string.Join("\n", outputUnique));

            Console.WriteLine();

            Console.WriteLine("Find Product by Title");
            newCollection.AddProduct(3440, "Nonstop", "Zaz", 1200);
            var outputTitle = newCollection.FindProductByTitle("Nonstop");
            Console.WriteLine(string.Join("\n", outputTitle));

            Console.WriteLine();

            Console.WriteLine("Find Product By Title + Price");
            var outputTitlePrice = newCollection.FindProductByTitleAndPrice("Nonstop", 3500);
            Console.WriteLine(string.Join("\n", outputTitlePrice));

            Console.WriteLine();

            Console.WriteLine("Find Product By Title + PriceRange");
            var outputTitlePriceRange = newCollection.FindProductByTitleAndPriceRange("Beans", 300, 31000);
            Console.WriteLine(string.Join("\n", outputTitlePriceRange));

            Console.WriteLine();

            Console.WriteLine("Find Product By Supplier + Price");
            var outputBySupplierPrice = newCollection.FindProductBySupplierAndPrice("M&CO", 5500);
            Console.WriteLine(string.Join("\n", outputBySupplierPrice));

            Console.WriteLine();

            Console.WriteLine("Find Product By Supplier + PriceRange");
            var outputBySupplierPriceRange = newCollection.FindProductBySupplierAndPriceRange("C&CO", 300, 10000);
            Console.WriteLine(string.Join("\n", outputBySupplierPriceRange));

            Console.WriteLine();
            Console.WriteLine("Removing product ID 1110 :" + newCollection.RemoveProduct(1110));
            Console.WriteLine();

            Console.WriteLine("Find Product By Title + Price");
            outputTitlePrice = newCollection.FindProductByTitleAndPrice("Nonstop", 3500);
            Console.WriteLine(string.Join("\n", outputTitlePrice));
            
            Console.WriteLine();

            Console.WriteLine("Find Product by Title");
            newCollection.AddProduct(3440, "Nonstop", "Zaz", 1200);
             outputTitle = newCollection.FindProductByTitle("Nonstop");
            Console.WriteLine(string.Join("\n", outputTitle));


        }

        public void AddProduct(int id, string title, string supplier, int price)
        {
            var newProduct = new Product(id, title, supplier, price);

            //checking whether a product with such an ID exists
            if (productByID.ContainsKey(id))
            {
                this.RemoveProduct(id);
            }
            productByID[id] = newProduct;

            if (!productByTitle.ContainsKey(title))
            {
                productByTitle.Add(title, new OrderedSet<Product>(new ProductIDComparer()));
            }
            productByTitle[title].Add(newProduct);

            //productByPrice
            if (!productByPrice.ContainsKey(price))
            {
                productByPrice[price] = new OrderedSet<Product>(new ProductIDComparer());
            }
            productByPrice[price].Add(newProduct);

            //productByTitleAndPrice
            if (!productByTitleAndPrice.ContainsKey(title))
            {
                productByTitleAndPrice.Add(title, new OrderedDictionary<int, OrderedSet<Product>>());
            }
            if (!productByTitleAndPrice[title].ContainsKey(price))
            {
                productByTitleAndPrice[title].Add(price, new OrderedSet<Product>(new ProductIDComparer()));
            }

            productByTitleAndPrice[title][price].Add(newProduct);



            //productBySupplierAndPrice

            if (!productBySupplierAndPrice.ContainsKey(supplier))
            {
                productBySupplierAndPrice.Add(supplier, new OrderedDictionary<int, OrderedSet<Product>>());
            }
            if (!productBySupplierAndPrice[supplier].ContainsKey(price))
            {
                productBySupplierAndPrice[supplier].Add(price, new OrderedSet<Product>(new ProductIDComparer()));
            }

            productBySupplierAndPrice[supplier][price].Add(newProduct);
        }

        public bool RemoveProduct(int id)
        {
            if (productByID.ContainsKey(id))
            {
                var product = productByID[id];
                productByTitle[product.Title].Remove(product);
                productByPrice[product.Price].Remove(product);
                productByTitleAndPrice[product.Title][product.Price].Remove(product);
                productBySupplierAndPrice[product.Supplier][product.Price].Remove(product);
                return true;
            }

            return false;
        }

        private Product FindProductByID(int id)
        {
            Product foundItem = null;

            foreach (var products in productByTitle)
            {
                foundItem = products.Value.FirstOrDefault(s => s.ID == id);
            }

            return foundItem;
        } //helper function

        public IEnumerable<Product> FindProductByPriceRange(int priceFrom, int priceTo) //sort by ID
        {
            var output = productByPrice.Range(priceFrom, true, priceTo, true);
            OrderedSet<Product> result = new OrderedSet<Product>(new ProductIDComparer());

            foreach (var productList in output)
            {
                foreach (var product in productList.Value)
                {
                    result.Add(product);
                }
            }

            return result;
        }

        public IEnumerable<Product> FindProductByTitle(string title) //sort by ID
        {
            if (!productByTitle.ContainsKey(title))
            {
                return new List<Product>();
            }

            return productByTitle[title];
        }

        public IEnumerable<Product> FindProductByTitleAndPrice(string title, int price) //sort by ID
        {
            if (!productByTitleAndPrice.ContainsKey(title))
            {
                return new List<Product>();
            }

            if (!productByTitleAndPrice[title].ContainsKey(price))
            {
                return new List<Product>();
            }

            return productByTitleAndPrice[title][price];
        }

        public IEnumerable<Product> FindProductByTitleAndPriceRange(string title, int priceFrom, int priceTo) //sort by ID
        {
            if (!productByTitleAndPrice.ContainsKey(title))
            {
                return new List<Product>();
            }

            var output = productByTitleAndPrice[title].Range(priceFrom, true, priceTo, true);

            List<Product> result = new List<Product>();

            foreach (var productList in output)
            {
                foreach (var product in productList.Value)
                {
                    result.Add(product);
                }
            }

            return result;
        }

        public IEnumerable<Product> FindProductBySupplierAndPrice(string supplier, int price) //sort by ID
        {
            if (productBySupplierAndPrice.ContainsKey(supplier) && productBySupplierAndPrice[supplier].ContainsKey(price))
            {
                return productBySupplierAndPrice[supplier][price];
            }

            return new List<Product>();
        }

        public IEnumerable<Product> FindProductBySupplierAndPriceRange(string supplier, int priceFrom, int priceTo) //sort by ID
        {
            if (!productBySupplierAndPrice.ContainsKey(supplier))
            {
                return new List<Product>();
            }

            var output = productBySupplierAndPrice[supplier].Range(priceFrom, true, priceTo, true);

            OrderedSet<Product> result = new OrderedSet<Product>(new ProductIDComparer());

            foreach (var productList in output)
            {
                foreach (var product in productList.Value)
                {
                    result.Add(product);
                }
            }

            return result;
        }
    }
}
