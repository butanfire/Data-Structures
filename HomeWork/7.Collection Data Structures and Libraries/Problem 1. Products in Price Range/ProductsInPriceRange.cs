namespace ProductsInPriceRange
{
    using System;
    using Wintellect.PowerCollections;

    public class ProductsInPriceRange
    {
        public static void Main(string[] args)
        {
            int inputLines = int.Parse(Console.ReadLine());
            OrderedMultiDictionary<string, string> bags = new OrderedMultiDictionary<string, string>(true);

            for (int i = 0; i < inputLines; i++)
            {
                var input = Console.ReadLine().Split(' ');
                var item = input[0];
                var price = input[1];
                bags.Add(price, item);
            }

            var rangeToFrom = Console.ReadLine().Split(' ');
            var from = rangeToFrom[0];
            var to = rangeToFrom[1];

            var result = bags.Range(from, true, to, true);

            foreach (var pair in result)
            {
                var price = pair.Key;
                var item = pair.Value.ToString();
                Console.WriteLine("{0} {1}", price, item.Substring(1, item.Length - 2));
            }
        }
    }
}
