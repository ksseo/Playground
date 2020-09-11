using System;
using System.Linq;

namespace ArraySkip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] myArray = { 0, 1, 2, 3, 4, 5, 6 };
            //MyFunc(myArray.Skip(2).ToArray());

            myArray = myArray.Skip(2).ToArray();
            Console.WriteLine(myArray);
        }

        private static int[] MyFunc(int[] array)
        {
            Console.WriteLine(array);

            return array;
        }
    }
}
