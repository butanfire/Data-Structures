using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_6.ReversedList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ReverseArray = new ReversedList<int>(10);
            ReverseArray.Add(10);
            ReverseArray.Add(9);
            ReverseArray.Add(8);
            ReverseArray.Add(7);
            ReverseArray.Add(6);
            ReverseArray.Add(5);
            ReverseArray.Add(4);
            ReverseArray.Add(3);
            ReverseArray.Add(2);
            ReverseArray.Add(1);

            Console.WriteLine("Count after adding 10 elements :" + ReverseArray.Count());
            Console.WriteLine("Output of the 5th and 1st element in the reverse array : ");
            Console.WriteLine(ReverseArray[5]);
            Console.WriteLine(ReverseArray[1]);

            Console.WriteLine("Removing the first element two times in the reverse array :");
            ReverseArray.Remove(0);
            ReverseArray.Remove(0);
           
            foreach (var item in ReverseArray)
            {
                Console.Write(item + " ");
            }
            
            Console.WriteLine();
            Console.WriteLine(ReverseArray.Count);
            Console.WriteLine(ReverseArray.Capacity);
        }
    }
}
