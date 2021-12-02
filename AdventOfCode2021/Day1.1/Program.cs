using System;
using System.IO;

namespace Day1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int inc = 0;

            for (int i = 0; i < input.Length - 1; i++)
                if (Convert.ToInt32(input[i + 1]) > Convert.ToInt32(input[i]))
                    inc++;

            Console.WriteLine(inc); // Answer
        }
    }
}
