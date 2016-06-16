namespace Problem_5.Count_of_Occurrences
{
    using System;
    using System.Linq;

    public class CountOfOccurences
    {
        public static void Main(string[] args)
        {
            var inputNumbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            int numberToDelete = 0;
            int count = 0;

            inputNumbers.Sort();

            for (int iterator = 0; iterator < inputNumbers.Count; iterator++)
            {
                count = 0;
                for (int j = 0; j < inputNumbers.Count; j++)
                {
                    if (inputNumbers[j] == inputNumbers[iterator])
                    {
                        count++;
                    }
                }

                numberToDelete = inputNumbers[iterator];
                Console.WriteLine("{0} -> {1} times", numberToDelete, count);

                inputNumbers.RemoveAll(s => s == numberToDelete);
                iterator = -1; //we have to go back to the start of the array via -1. 
                               //because iterator is incremented after the end of the first loop run, otherwise it will skip the newly arranged [0] element.
            }

            Console.WriteLine(string.Join(" ", inputNumbers));
        }
    }
}
