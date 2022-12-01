using System;

namespace Day1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            string[] split = string.Join(",", input).Split(",,");
            int highest = 0;

            foreach (string s in split)
            {
                int total = 0;

                foreach (string num in s.Split(","))
                    total += int.Parse(num);

                if (total > highest) 
                    highest = total;
            }

            Console.WriteLine(highest);
        }
    }
}