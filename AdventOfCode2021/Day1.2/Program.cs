using System;
using System.IO;

namespace Day1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            (int, int, int)[] windows = new (int, int, int)[2000];
            int[] sums = new int[2000];

            int count = 0;

            for (int i = 0; i < windows.Length - 2; i++)
            {
                windows[i] = (Convert.ToInt32(input[count]), Convert.ToInt32(input[count + 1]), Convert.ToInt32(input[count + 2]));
                sums[i] = windows[i].Item1 + windows[i].Item2 + windows[i].Item3;
                Console.WriteLine(windows[i]);
                Console.WriteLine(sums[i]);
                count++;
            }

            int inc = 0;

            for (int i = 0; i < sums.Length - 1; i++)
                if (Convert.ToInt32(sums[i + 1]) > Convert.ToInt32(sums[i]))
                    inc++;

            Console.WriteLine(inc); // Answer
        }
    }
}
