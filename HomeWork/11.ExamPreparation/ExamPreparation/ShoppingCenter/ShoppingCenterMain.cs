﻿namespace ShoppingCenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingCenterSlow
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        private readonly List<Product> products = new List<Product>();

        private string AddProduct(string name, string price, string producer)
        {
            Product product = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };
            this.products.Add(product);
            return PRODUCT_ADDED;
        }

        private string FindProductsByName(string name)
        {
            var products = this.products
                .Where(p => p.Name == name)
                .OrderBy(p => p);
            return PrintProducts(products);
        }

        private string FindProductsByProducer(string producer)
        {
            var products = this.products
                .Where(p => p.Producer == producer)
                .OrderBy(p => p);
            return PrintProducts(products);
        }

        private string FindProductsByPriceRange(string from, string to)
        {
            decimal rangeStart = decimal.Parse(from);
            decimal rangeEnd = decimal.Parse(to);
            var products = this.products
                .Where(p => p.Price >= rangeStart && p.Price <= rangeEnd)
                .OrderBy(p => p);
            return PrintProducts(products);
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var builder = new StringBuilder();
                foreach (var product in products)
                {
                    builder.AppendLine(product.ToString());
                }
                string formattedProducts = builder.ToString().TrimEnd();
                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            int countOfRemovedProducts =
                this.products.RemoveAll(p => p.Name == name && p.Producer == producer);
            if (countOfRemovedProducts > 0)
            {
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByProducer(string producer)
        {
            int countOfRemovedProducts =
                this.products.RemoveAll(p => p.Producer == producer);
            if (countOfRemovedProducts > 0)
            {
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
                parameterValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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
