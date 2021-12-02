using System;
using System.IO;

namespace Day2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            (string, int)[] commands = new (string, int)[input.Length];

            int horizontal = 0;
            int depth = 0;

            for (int i = 0; i < input.Length; i++)
                commands[i] = (input[i].Split(" ")[0], Convert.ToInt32(input[i].Split(" ")[1]));

            for (int i = 0; i < commands.Length; i++)
            {
                switch (commands[i].Item1)
                {
                    case "up":
                        horizontal -= Convert.ToInt32(commands[i].Item2);
                        break;
                    case "down":
                        horizontal += Convert.ToInt32(commands[i].Item2);
                        break;
                    case "forward":
                        depth += Convert.ToInt32(commands[i].Item2);
                        break;
                }
            }

            Console.WriteLine(horizontal * depth); // Answer
        }
    }
}
