namespace Day2
{
    internal class Program
    {
        enum Result {Loss, Tie, Win};
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(false));
            Console.WriteLine(Solve(true));
        }

        static int Solve(bool calcEnd) 
        {
            // This is ugly ;<
            Dictionary<string, int> mapper = new Dictionary<string, int>()
            {
                {"A", 1},
                {"B", 2},
                {"C", 3},
                {"X", 1},
                {"Y", 2},
                {"Z", 3}
            };

            string[] input = calcEnd ? ReMakeInput() : File.ReadAllLines("input.txt");

            int totalScore = 0;

            foreach (string line in input)
            {
                string[] kvp = line.Split(" ");
                int scorePlayer = mapper[kvp[1]];

                totalScore += 3 * (int)RunGame(mapper[kvp[0]], scorePlayer) + scorePlayer;
            }

            return totalScore;
        }

        static string[] ReMakeInput()
        {
            string[] input = File.ReadAllLines("input.txt");
            string[] newInput = new string[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string[] kvp = input[i].Split(" ");

                newInput[i] = $"{kvp[0]} {CalcMove(kvp[0], kvp[1])}";
            }

            return newInput;
        }

        static string CalcMove(string elf, string result)
        {
            // PLEASE DONT JUDGE ME I WAS TIRED
            switch (result)
            {
                case "X":
                    switch (elf)
                    {
                        case "A":
                            return "Z";
                        case "B":
                            return "X";
                        case "C":
                            return "Y";
                    }
                    break;
                case "Y":
                    switch (elf)
                    {
                        case "A":
                            return "X";
                        case "B":
                            return "Y";
                        case "C":
                            return "Z";
                    }
                    break;
                case "Z":
                    switch (elf)
                    {
                        case "A":
                            return "Y";
                        case "B":
                            return "Z";
                        case "C":
                            return "X";
                    }
                    break;
            }

            return "";
        }

        static Result RunGame(int elf, int player)
        {
            if (elf == player)
                return Result.Tie;
            if (player == 1 && elf == 3 || player == 2 && elf == 1 || player == 3 && elf == 2)
                return Result.Win;

            return Result.Loss;
        }
    }
}