namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (int, int) result = Solve();
            Console.WriteLine(result.Item1);
            Console.WriteLine(result.Item2);
        }

        public static (int, int) Solve()
        {
            string[] input = File.ReadAllLines("input.txt");
            Tree[][] trees = new Tree[input.Length][];
            int count = 0;
            Tree bestTree = new Tree(0, -1, -1);

            for (int i = 0; i < trees.Length; i++)
            {
                Tree[] treeRow = new Tree[input[0].Length];
                for (int j = 0; j < treeRow.Length; j++)
                    treeRow[j] = new Tree(int.Parse(input[i].Substring(j, 1)), i, j);
                trees[i] = treeRow;
            }

            for (int i = 1; i < trees.Length - 1; i++)
            {
                for (int j = 1; j < trees[0].Length - 1; j++)
                {
                    trees[i][j].CalculateVisibility(trees);
                    trees[i][j].CalculateScenicScore(trees);
                    if (trees[i][j].Visible)
                        count++;
                    if (trees[i][j].ScenicScore > bestTree.ScenicScore)
                        bestTree = trees[i][j];
                }
            }

            count += 2 * input[0].Length + 2 * (input.Length-2);

            return (count, bestTree.ScenicScore);
        }
    }

    public class Tree
    {
        public int Height { get; set; }
        public bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ScenicScore { get; set; }
        
        public Tree(int height, int x, int y)
        {
            Height = height;
            X = x;
            Y = y;
            Visible = false;
        }

        public void CalculateScenicScore(Tree[][] trees)
        {
            ScenicScore = CheckUp(trees).Item2 * CheckDown(trees).Item2 * CheckLeft(trees).Item2 * CheckRight(trees).Item2;
        }

        public void CalculateVisibility(Tree[][] trees)
        {
            if (CheckUp(trees).Item1 || CheckDown(trees).Item1 || CheckLeft(trees).Item1 || CheckRight(trees).Item1)
                Visible = true;
        }

        public (bool, int) CheckUp(Tree[][] trees)
        {
            int counter = 0;
            for (int i = (X - 1); i >= 0; i--)
            {
                counter++;
                if (Height <= trees[i][Y].Height)
                    return (false, counter);
            }

            return (true, counter);
        }

        public (bool, int) CheckDown(Tree[][] trees)
        {
            int counter = 0;
            for (int i = (X + 1); i < trees.Length; i++)
            {
                counter++;
                if (Height <= trees[i][Y].Height)
                    return (false, counter);
            }

            return (true, counter);
        }

        public (bool, int) CheckLeft(Tree[][] trees)
        {
            int counter = 0;
            for (int i = (Y - 1); i >= 0; i--)
            {
                counter++;
                if (Height <= trees[X][i].Height)
                    return (false, counter);
            }

            return (true, counter);
        }

        public (bool, int) CheckRight(Tree[][] trees)
        {
            int counter = 0;
            for (int i = (Y + 1); i < trees[0].Length; i++)
            {
                counter++;
                if (Height <= trees[X][i].Height)
                    return (false, counter);
            }

            return (true, counter);
        }
    }
}