using System;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pick a number:");
            var number1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Pick another number:");
            var number2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Adding the two numbers: " + (number1 + number2));
            //Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();
        }
    }
}
