namespace Problem_3.Longest_Subsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestSubsequence
    {
        public static void Main(string[] args)
        {
            var inputNumbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            List<int> outputNumbers = new List<int>();
            int count = 0, tempCount = 0;

            for (int i = 0; i < inputNumbers.Count; i++)
            {
                tempCount = 0;
                for(int j = 0; j < inputNumbers.Count; j++)
                {
                    if(inputNumbers[j] == inputNumbers[i])
                    {
                        tempCount++;
                    }

                    if(tempCount > count)
                    {
                        count = tempCount;
                        outputNumbers.Clear(); //we remove all existing numbers
                        outputNumbers.Add(inputNumbers[i]); //we add the longest subsequence number
                    }
                }
            }

            for(int i = 0; i < count; i++)
            {
                Console.Write(outputNumbers[0] + " "); //we output the subsequence number x count
            }

            Console.WriteLine();
        }
    }
}
