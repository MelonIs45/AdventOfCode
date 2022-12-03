using Microsoft.VisualBasic;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SolveP1());
            Console.WriteLine(SolveP2());
        }

        static int SolveP2()
        {
            string[] input = File.ReadAllLines("input.txt");
            int sum = 0;
            (int, int) longest = (0, 0);
            List<int> smallerSacks = new List<int>();

            for (int i = 0; i < input.Length; i+=3)
            {
                string[] threeSacks =
                {
                    input[i],
                    input[i + 1],
                    input[i + 2]
                };

                int longestLength = threeSacks.OrderByDescending(s => s.Length).First().Length;
                for (int j = 0; j < threeSacks.Length; j++)
                {
                    if (threeSacks[j].Length < longestLength)
                        smallerSacks.Add(j);
                    else
                        longest = (longestLength, j);
                }

                if (smallerSacks.Count < 2)
                    smallerSacks.Add(IndexOf(threeSacks, threeSacks.OrderByDescending(s => s.Length).Take(1).First()));

                for (int j = 0; j < longest.Item1; j++)
                {
                    if (threeSacks[smallerSacks[0]].Contains(threeSacks[longest.Item2][j]) && threeSacks[smallerSacks[1]].Contains(threeSacks[longest.Item2][j]))
                    {
                        sum += ConvertCharToPriority(threeSacks[longest.Item2][j]);
                        break;
                    }

                }

                smallerSacks.Clear();

            }

            return sum;
        }

        static int IndexOf(string[] list, string search)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == search)
                    return i;
            }

            return -1;
        }


        static int SolveP1()
        {
            string[] input = File.ReadAllLines("input.txt");
            int sum = 0;

            foreach (string line in input)
            {
                int lineLength = line.Length;
                char[] sack1 = line.Substring(0, lineLength / 2).ToCharArray();
                char[] sack2 = line.Substring(lineLength/2, lineLength / 2).ToCharArray();

                for (int i = 0; i < sack1.Length; i++)
                {
                    if (sack2.Contains(sack1[i]))
                    {
                        sum += ConvertCharToPriority(sack1[i]);
                        break;
                    }
                        
                }
            }

            return sum;
        }

        static int ConvertCharToPriority(char c)
        {
            if (c >= 'a' && c <= 'z')
                return c - 96;
            return c - 38;
        }
    }
}