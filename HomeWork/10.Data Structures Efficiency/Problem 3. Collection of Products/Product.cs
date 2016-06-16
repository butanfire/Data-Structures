using System;
using System.Text;

namespace Problem_3.Collection_of_Products
{
    public class Product : IComparable<Product>
    {
        public Product(int Id, string title, string supplier, int price)
        {
            this.ID = Id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Supplier { get; set; }

        public int Price { get; set; }

        public int CompareTo(Product other)
        {
            return other.ID.CompareTo(this.ID); 
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendFormat("ID: {0} , Title: {1} , Supplier: {2} , Price: {3}", this.ID, this.Title, this.Supplier, this.Price);
            return output.ToString();
        }
    }
}
