namespace ShoppingCenter
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Text;
    using Wintellect.PowerCollections;

    public class ShoppingCenter
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var center = new ShoppingCenterFast();

            int commandsCount = int.Parse(Console.ReadLine());
            for (int i = 1; i <= commandsCount; i++)
            {
                string command = Console.ReadLine();
                string commandResult = center.ProcessCommand(command);
                Console.WriteLine(commandResult);
            }
        }
    }

    public class ShoppingCenterFast
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        private readonly MultiDictionary<string, Product> productsByName =
            new MultiDictionary<string, Product>(true);
        private readonly MultiDictionary<string, Product> productsByNameAndProducer =
            new MultiDictionary<string, Product>(true);
        private readonly OrderedMultiDictionary<decimal, Product> productsByPrice =
            new OrderedMultiDictionary<decimal, Product>(true);
        private readonly MultiDictionary<string, Product> productsByProducer =
            new MultiDictionary<string, Product>(true);

        private string AddProduct(string name, string price, string producer)
        {
            Product product = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };

            this.productsByName.Add(name, product);
            string nameAndProducerKey = this.CombineKeys(name, producer);
            this.productsByNameAndProducer.Add(nameAndProducerKey, product);
            this.productsByPrice.Add(product.Price, product);
            this.productsByProducer.Add(producer, product);

            return PRODUCT_ADDED;
        }

        private string CombineKeys(string name, string producer)
        {
            string key = name + ";" + producer;
            return key;
        }

        private string FindProductsByName(string name)
        {
            return SortAndPrintProducts(this.productsByName[name]);
        }

        private string SortAndPrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var sortedProducts = new List<Product>(products);
                sortedProducts.Sort();
                var builder = new StringBuilder();
                foreach (var product in sortedProducts)
                {
                    builder.AppendLine(product.ToString());
                }

                // Remove the undneeded last "new line"
                builder.Length -= Environment.NewLine.Length;

                string formattedProducts = builder.ToString();
                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string FindProductsByProducer(string producer)
        {
            return SortAndPrintProducts(this.productsByProducer[producer]);
        }

        private string FindProductsByPriceRange(string from, string to)
        {
            decimal rangeStart = decimal.Parse(from);
            decimal rangeEnd = decimal.Parse(to);
            var productsFound = productsByPrice.Range(rangeStart, true, rangeEnd, true).Values;
            return SortAndPrintProducts(productsFound);
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            string nameAndProducerKey = this.CombineKeys(name, producer);
            var productsToBeRemoved = productsByNameAndProducer[nameAndProducerKey];
            if (productsToBeRemoved.Any())
            {
                int countOfRemovedProducts = productsToBeRemoved.Count;
                foreach (var product in productsToBeRemoved)
                {
                    productsByName.Remove(product.Name, product);
                    productsByProducer.Remove(product.Producer, product);
                    productsByPrice.Remove(product.Price, product);
                }
                productsByNameAndProducer.Remove(nameAndProducerKey);
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByProducer(string producer)
        {
            var productsToBeRemoved = productsByProducer[producer];
            if (productsToBeRemoved.Any())
            {
                foreach (var product in productsToBeRemoved)
                {
                    productsByName.Remove(product.Name, product);
                    string nameAndProducerKey = this.CombineKeys(product.Name, producer);
                    productsByNameAndProducer.Remove(nameAndProducerKey, product);
                    productsByPrice.Remove(product.Price, product);
                }
                int countOfRemovedProducts = productsByProducer[producer].Count;
                productsByProducer.Remove(producer);
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        public string ProcessCommand(string command)
        {
            int indexOfFirstSpace = command.IndexOf(' ');
            string method = command.Substring(0, indexOfFirstSpace);
            string parameterValues = command.Substring(indexOfFirstSpace + 1);
            string[] parameters =
                parameterValues.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            switch (method)
            {
                case "AddProduct":
                    return AddProduct(parameters[0], parameters[1], parameters[2]);
                case "DeleteProducts":
                    return parameters.Length == 1 ?
                        DeleteProductsByProducer(parameters[0]) :
                        DeleteProductsByNameAndProducer(parameters[0], parameters[1]);
                case "FindProductsByName":
                    return FindProductsByName(parameters[0]);
                case "FindProductsByPriceRange":
                    return FindProductsByPriceRange(parameters[0], parameters[1]);
                case "FindProductsByProducer":
                    return FindProductsByProducer(parameters[0]);
                default:
                    return INCORRECT_COMMAND;
            }
        }
    }
}