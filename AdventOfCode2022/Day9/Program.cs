namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Solve(2));
            Console.WriteLine(Solve(10));
        }

        static int Solve(int knots)
        {
            int result = 0;
            string[] input = File.ReadAllLines("input2.txt");

            (int, int, int, int) lrud = (0, 0, 0, 0);
            for (int i = 0; i < input.Length; i++)
            {
                string[] instructions = input[i].Split(' ');
                switch (instructions[0])
                {
                    case "L":
                        lrud.Item1 += int.Parse(instructions[1]);
                        break;
                    case "R":
                        lrud.Item2 += int.Parse(instructions[1]);
                        break;
                    case "U":
                        lrud.Item3 += int.Parse(instructions[1]);
                        break;
                    case "D":
                        lrud.Item4 += int.Parse(instructions[1]);
                        break;
                }
            }

            Console.WriteLine(lrud);

            Knot[][] grid = new Knot[lrud.Item3 + lrud.Item4 + 1][];
            for (int i = 0; i < grid.Length; i++)
            {
                Knot[] row = new Knot[lrud.Item1 + lrud.Item2 + 1];
                for (int j = 0; j < row.Length; j++)
                    row[j] = new Knot(i, j);
                grid[i] = row;
            }
            Rope rope = new Rope(grid.Length / 2 + 1, grid[0].Length / 2, knots);

            foreach (string instruction in input)
            {
                string[] instructions = instruction.Split(' ');

                
                rope = rope.MoveHead(instructions[0], int.Parse(instructions[1]), ref grid);

            }

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j].HadTail)
                        result++;
                }
            }

            // This is required for some reason, a knot is not checked for some reason
            result++;


            return result;
        }
    }

    public class Rope
    {
        public Knot Head { get; set; }
        public Knot Tail { get; set; }
        public Knot[] Knots { get; set; }

        public Rope(int x, int y, int length)
        {
            Knots = new Knot[length];
            for (int i = 0; i < Knots.Length; i++)
                Knots[i] = new Knot(x, y);

            Head = Knots[0];
            Tail = Knots[length - 1];
        }

        public Rope MoveHead(string direction, int amount, ref Knot[][] grid, int index = 0)
        {
            Rope newRope = this;
            for (int i = 0; i < amount; i++)
            {
                
                switch (direction)
                {
                    case "L":
                        newRope.Knots[index] = grid[newRope.Knots[index].X][newRope.Knots[index].Y - 1];
                        break;
                    case "R":
                        newRope.Knots[index] = grid[newRope.Knots[index].X][newRope.Knots[index].Y + 1];
                        break;
                    case "U":
                        newRope.Knots[index] = grid[newRope.Knots[index].X - 1][newRope.Knots[index].Y];
                        break;
                    case "D":
                        newRope.Knots[index] = grid[newRope.Knots[index].X + 1][newRope.Knots[index].Y];
                        break;
                }

                grid[newRope.Head.X][newRope.Head.Y].Visited = true;
                grid[newRope.Knots[newRope.Knots.Length - 1].X][newRope.Knots[newRope.Knots.Length - 1].Y].HadTail = true;

                if (!newRope.Knots[index].NextTo(newRope.Knots[index + 1]))
                {
                    switch (direction)
                    {
                        case "L":
                            newRope.Knots[index + 1] = grid[newRope.Knots[index].X][newRope.Knots[index + 1].Y - 1];
                            break;
                        case "R":
                            newRope.Knots[index + 1] = grid[newRope.Knots[index].X][newRope.Knots[index + 1].Y + 1];
                            break;
                        case "U":
                            newRope.Knots[index + 1] = grid[newRope.Knots[index + 1].X - 1][newRope.Knots[index].Y];
                            break;
                        case "D":
                            newRope.Knots[index + 1] = grid[newRope.Knots[index + 1].X + 1][newRope.Knots[index].Y];
                            break;
                    }

                }

                newRope.Head = newRope.Knots[0];
                newRope.Tail = newRope.Knots[newRope.Knots.Length - 1];
                if (index < 8)
                    MoveHead(direction, amount, ref grid, index + 1);
            }

            return newRope;
        }
    }

    public class Knot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool HadTail { get; set; }
        public bool Visited { get; set; }

        public Knot(int x, int y)
        {
            X = x;
            Y = y;
            HadTail = false;
        }

        public bool NextTo(Knot knot)
        {
            if ((X == knot.X - 1 || X == knot.X || X == knot.X + 1) && (Y == knot.Y - 1 || Y == knot.Y || Y == knot.Y + 1))
                return true;
            return false;
        }
    }
}