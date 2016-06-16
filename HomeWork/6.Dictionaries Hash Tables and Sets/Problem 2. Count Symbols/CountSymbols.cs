namespace Problem_2.Count_Symbols
{
    using Problem_1.Dictionary;
    using System;
    using System.Linq;

    public class CountSymbols
    {
        static void Main(string[] args)
        {
            while (true)
            {
                HashTable<char, int> SymbolDict = new HashTable<char, int>();

                var input = Console.ReadLine().ToCharArray();

                foreach (var symbol in input)
                {
                    if (!SymbolDict.ContainsKey(symbol))
                    {
                        SymbolDict.Add(symbol, 1);
                    }
                    else
                    {
                        SymbolDict[symbol] += 1;
                    }
                }

                var outputList = SymbolDict.OrderBy(s => s.Key).ToList(); //order alphabetically

                foreach (var k in outputList)
                {
                    Console.WriteLine(string.Format(k.Key + ": " + k.Value + " time/s"));
                }

                Console.WriteLine();
            }
        }
    }
}
