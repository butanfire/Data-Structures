namespace Problem_2.String_Editor
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class StringEditor
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ');
            BigList<char> output = new BigList<char>();
            while (true)
            {
                string command = input[0];

                if (command == "END")
                {
                    break;
                }

                switch (command)
                {
                    case "APPEND":
                        var put = input[1].ToCharArray();
                        output.AddRange(put);
                        PrintSuccess();
                        break;
                    case "INSERT":
                        var startIndex = int.Parse(input[1]);
                        var insert = input[2];
                        if (startIndex < 0 || startIndex > output.Count)
                        {
                            PrintError();
                        }
                        else
                        {
                            output.InsertRange(startIndex, insert.ToList());
                            PrintSuccess();
                        }
                        break;
                    case "DELETE":
                        var deleteStartIndex = int.Parse(input[1]);
                        var deleteEndIndex = int.Parse(input[2]);
                        if (Validate(deleteStartIndex, deleteEndIndex, output.Count))
                        {
                            PrintError();
                        }
                        else
                        {
                            output.RemoveRange(deleteStartIndex, deleteEndIndex);
                            PrintSuccess();
                        }
                        break;
                    case "REPLACE":
                        var replaceStartIndex = int.Parse(input[1]);
                        var replaceEndIndex = int.Parse(input[2]);
                        var replaceText = input[3];
                        if (Validate(replaceStartIndex, replaceEndIndex, output.Count))
                        {
                            PrintError();
                        }
                        else
                        {
                            int leftover = replaceEndIndex;
                            for (int i = 0; i < replaceText.Length; i++)
                            {
                                output[replaceStartIndex++] = replaceText[i];
                                leftover--;
                            }

                            output.RemoveRange(replaceText.Length, leftover);
                            PrintSuccess();
                        }
                        break;
                    case "PRINT":
                        foreach (var letter in output)
                        {
                            Console.Write(letter);
                        }

                        Console.WriteLine();
                        break;
                }

                input = Console.ReadLine().Split(' ');
            }
        }

        private static void PrintError()
        {
            Console.WriteLine("ERROR");
        }

        private static void PrintSuccess()
        {
            Console.WriteLine("OK");
        }

        private static bool Validate(int start, int end, int count)
        {
            if (start < 0 || start > end || end < 0 || end > count || start > count)
            {
                return true;
            }

            return false;
        }
    }
}
