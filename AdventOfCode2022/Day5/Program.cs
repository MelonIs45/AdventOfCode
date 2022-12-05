using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace Day5
{
    internal class Program
    {
        const int TALLEST_HEIGHT = 8;

        static void Main(string[] args)
        {
            Console.WriteLine(Solve().Item1);
            Console.WriteLine(Solve().Item2);
        }

        static (string, string) Solve()
        {
            string res = "";
            string res2 = "";
            string[] input = File.ReadAllLines("input.txt");
            string[][] stacks = new string[TALLEST_HEIGHT][];

            Stack<string>[] stackOfStacks = new Stack<string>[9];
            Stack<string>[] stackOfStacksPartTwo = new Stack<string>[9];

            for (int i = 0; i < TALLEST_HEIGHT; i++)
            {
                string line = input[i];
                int length = line.Length;
                string[] stack = new string[9];

                for (int j = 0; j < length; j += 4)
                    if (line[j + 1] != ' ')
                        stack[j / 4] = line[j + 1].ToString();
                
                stacks[i] = stack;
            }

            for (int i = 0; i < 9; i++)
            {
                Stack<string> stack = new Stack<string>();
                for (int j = 7; j >= 0; j--)
                    if (stacks[j][i] != null)
                        stack.Push(stacks[j][i]);
                
                stackOfStacks[i] = stack;
                string[] stackCopy = stack.ToArray();
                Array.Reverse(stackCopy);
                stackOfStacksPartTwo[i] = new Stack<string>(stackCopy);
            }

            (int, int, int)[] instructions = new (int, int, int)[input.Length - 10];

            for (int i = 0; i < input.Length-10; i++)
            {
                instructions[i] = (int.Parse(input[i + 10].Split(" ")[1]), int.Parse(input[i + 10].Split(" ")[3]), int.Parse(input[i + 10].Split(" ")[5]));
            }

            foreach ((int, int, int) instruction in instructions)
            {
                Stack<string> tempStack = new Stack<string>();
                for (int i = 0; i < instruction.Item1; i++)
                {
                    stackOfStacks[instruction.Item3 - 1].Push(stackOfStacks[instruction.Item2 - 1].Pop());
                    tempStack.Push(stackOfStacksPartTwo[instruction.Item2-1].Pop());
                }

                for (int i = 0; i < instruction.Item1; i++)
                {
                    stackOfStacksPartTwo[instruction.Item3-1].Push(tempStack.Pop());
                }
                    
            }

            for (int i = 0; i < stackOfStacks.Length; i++)
            {
                res += stackOfStacks[i].Peek();
                res2 += stackOfStacksPartTwo[i].Peek();
            }

            return (res, res2);
        }
    }
}