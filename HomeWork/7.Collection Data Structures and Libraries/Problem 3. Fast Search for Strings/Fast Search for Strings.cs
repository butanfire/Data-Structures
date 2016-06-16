namespace Problem_3.Fast_Search_for_Strings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class FastSearchForStrings
    {
        public static void Main(string[] args)
        {
            // Read the input list of words
            var inputCycles = int.Parse(Console.ReadLine());
            List<string> inputWords = new List<string>();

            for (int i = 0; i < inputCycles; i++)
            {
                inputWords.Add(Console.ReadLine());
            }

            string[] wordsLowercase = inputWords.Select(w => w.ToLower()).ToArray();

            int[] occurrences = new int[wordsLowercase.Length];
            StringBuilder buffer = new StringBuilder();

            //input for the text
            int numberOfLines = int.Parse(Console.ReadLine());
            List<string> textCompare = new List<string>();

            for (int k = 0; k < numberOfLines; k++)
            {
                textCompare.Add(Console.ReadLine());
            }

            //matching the text to the words
            for (int q = 0; q < textCompare.Count; q++)
            {
                for (int w = 0; w < textCompare[q].Length; w++)
                {
                    char ch = textCompare[q][w];
                    ch = char.ToLower(ch);
                    buffer.Append(ch);

                    for (int i = 0; i < wordsLowercase.Length; i++)
                    {
                        string word = wordsLowercase[i];
                        if (buffer.EndsWith(word))
                        {
                            occurrences[i]++;
                        }
                    }
                }
            }

            // Print the result

            for (int i = 0; i < inputWords.Count; i++)
            {
                Console.WriteLine("{0} --> {1}", inputWords[i], occurrences[i]);
            }
        }

        public static bool EndsWith(this StringBuilder buffer, string str)
        {
            if (buffer.Length < str.Length)
            {
                return false;
            }

            for (int bufIndex = buffer.Length - str.Length, strIndex = 0; strIndex < str.Length; bufIndex++, strIndex++)
            {
                if (buffer[bufIndex] != str[strIndex])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
