namespace ShoppingCenter
{
    using System;

    public class Product : IComparable<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }

        public int CompareTo(Product other)
        {
            int resultOfCompare = this.Name.CompareTo(other.Name);
            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Producer.CompareTo(other.Producer);
            }
            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Price.CompareTo(other.Price);
            }
            return resultOfCompare;
        }

        public override string ToString()
        {
            string toString =
                "{" +
                this.Name + ";" +
                this.Producer + ";" +
                this.Price.ToString("0.00") +
                "}";
            return toString;
        }
    }
}
