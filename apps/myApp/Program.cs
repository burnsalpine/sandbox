using System;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("### SuperCalc v1.0 ###");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Pick a number:");
            var number1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Pick another number:");
            var number2 = int.Parse(Console.ReadLine());

            var sum1 = (number1) + (number2);

            Console.WriteLine("Calculating sum... " + (number1) + " + " + (number2) + " = " + (number1 + number2));
            //Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();

            Console.WriteLine("Performing numerical analysis...");
            // Greater than, less than
            if (sum1 < 10) {
                Console.WriteLine("Sum is less than 10.");
            } else {
                Console.WriteLine("Sum is greater than 10.");
            }
            // Even, odd, using modulus operator
            if (sum1 % 2 == 0) {
                Console.WriteLine("Sum is Even.");
            } else {
                Console.WriteLine("Sum is odd.");
            }

            // Prime: A prime number is a natural number that is
            // 1. Greater than 1 
            // 2. Has no positive divisors other than 1 and itself.
            if (sum1 > 1) {

                

            }
        }
    }
}
