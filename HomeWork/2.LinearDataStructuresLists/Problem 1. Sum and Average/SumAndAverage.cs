namespace Problem_1.Sum_and_Average
{
    using System;
    using System.Linq;
    using System.Text;

    public class SumAndAverage
    {
        public static void Main(string[] args)
        {
            var numberInput = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            StringBuilder output = new StringBuilder();
            output.AppendFormat(string.Format("Sum=" + numberInput.Sum() + "; Average=" + numberInput.Average()));
            Console.WriteLine(output);
        }
    }
}
