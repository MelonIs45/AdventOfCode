using System;
using System.IO;

namespace Day3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int ones = 0;
            int zeros = 0;
            string gamma = "";
            string epsilon = "";

            for (int i = 0; i < input[0].Length; i++)
            {
                foreach (string line in input)
                {
                    if (line.Substring(i, 1) == "1")
                        ones++;
                    else
                        zeros++;
                }

                if (ones > zeros)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }

                ones = 0;
                zeros = 0;
            }

            int product = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);

            Console.WriteLine(product);

        }
    }
}
