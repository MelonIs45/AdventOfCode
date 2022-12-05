namespace Day5
{
    internal class Program
    {
        private const int TallestHeight = 8;
        private const int AmountOfRows = 9;

        static void Main(string[] args)
        {
            Console.WriteLine(Solve().Item1);
            Console.WriteLine(Solve().Item2);
        }

        static (string, string) Solve()
        {
            string resPartOne = "";
            string resPartTwo = "";
            string[] input = File.ReadAllLines("input.txt");
            string[][] stacks = new string[TallestHeight][];

            Stack<string>[] stackOfStacks = new Stack<string>[AmountOfRows];
            Stack<string>[] stackOfStacksPartTwo = new Stack<string>[AmountOfRows];

            for (int i = 0; i < TallestHeight; i++)
            {
                string line = input[i];
                int length = line.Length;
                string[] stack = new string[AmountOfRows];

                for (int j = 0; j < length; j += 4)
                    if (line[j + 1] != ' ')
                        stack[j / 4] = line[j + 1].ToString();
                
                stacks[i] = stack;
            }

            for (int i = 0; i < AmountOfRows; i++)
            {
                Stack<string> stack = new Stack<string>();
                for (int j = 7; j >= 0; j--)
                    if (!string.IsNullOrEmpty(stacks[j][i]))
                        stack.Push(stacks[j][i]);
                
                stackOfStacks[i] = stack;
                string[] stackCopy = stack.ToArray();
                Array.Reverse(stackCopy);
                stackOfStacksPartTwo[i] = new Stack<string>(stackCopy);
            }

            (int, int, int)[] instructions = new (int, int, int)[input.Length - 10];
            for (int i = 0; i < input.Length- AmountOfRows - 1; i++)
                instructions[i] = (int.Parse(input[i + AmountOfRows + 1].Split(" ")[1]), int.Parse(input[i + AmountOfRows + 1].Split(" ")[3]), int.Parse(input[i + AmountOfRows + 1].Split(" ")[5]));

            foreach ((int, int, int) instruction in instructions)
            {
                Stack<string> tempStack = new Stack<string>();
                for (int i = 0; i < instruction.Item1; i++)
                {
                    stackOfStacks[instruction.Item3 - 1].Push(stackOfStacks[instruction.Item2 - 1].Pop());
                    tempStack.Push(stackOfStacksPartTwo[instruction.Item2-1].Pop());
                }

                for (int i = 0; i < instruction.Item1; i++)
                    stackOfStacksPartTwo[instruction.Item3-1].Push(tempStack.Pop());
            }

            for (int i = 0; i < stackOfStacks.Length; i++)
            {
                resPartOne += stackOfStacks[i].Peek();
                resPartTwo += stackOfStacksPartTwo[i].Peek();
            }

            return (resPartOne, resPartTwo);
        }
    }
}