using System.Drawing;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (int, Dictionary<Point, string>) result = Solve();
            Console.WriteLine(result.Item1);
            for (int i = 0; i < result.Item2.Count; i++)
            {
                Console.Write(result.Item2.ElementAt(i).Value);
                if (i%40 == 0)
                    Console.WriteLine();
            }
        }

        public static (int, Dictionary<Point, string>) Solve()
        {
            string[] input = File.ReadAllLines("input.txt");
            int x = 1;
            int cycle = 1;
            int sum = 0;

            Dictionary<Point, string> screen = new Dictionary<Point, string>();


            foreach (string instruction in input)
            {
                string[] instructions = instruction.Split(' ');
                switch (instructions[0])
                {
                    case "addx":
                        CheckCycle(ref cycle, ref sum, x, ref screen);
                        CheckCycle(ref cycle, ref sum, x, ref screen);
                        x += int.Parse(instructions[1]);
                        break;
                    case "noop":
                        CheckCycle(ref cycle, ref sum, x, ref screen);
                        break;
                }
            }

            return (sum, screen);
        }

        public static void CheckCycle(ref int cycle, ref int sum, int x, ref Dictionary<Point, string> screen)
        {
            var screenX = (cycle - 1) % 40;
            if (screenX >= x - 1 && screenX <= x + 1)
            {
                screen[new Point(screenX, (cycle - 1) / 40)] = "#";
            }


            if (cycle % 40 == 20)
                sum += cycle * x;
            cycle++;
        }
    }
}