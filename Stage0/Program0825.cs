using System;
namespace Targil0
{
    partial class Program
    {
        static void Main(string[]args)
        {
            Welcome0825();
            Welcome4210();
            Console.ReadKey();
        }

        private static void Welcome0825()
        {
            Console.WriteLine("Enter your name: ");
            string ?username = Console.ReadLine();
            Console.WriteLine("{0} ,welcome to my first console application", username);
            Console.ReadKey( );
        }
        static partial void Welcome4210();
    }
}