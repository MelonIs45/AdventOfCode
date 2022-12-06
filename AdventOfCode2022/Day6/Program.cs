namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solve(4));
            Console.WriteLine(Solve(14));
        }

        static int Solve(int markerLength)
        {
            string input = File.ReadAllText("input.txt");
            int counter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char[] packet = input.Substring(i, markerLength).ToCharArray();

                foreach (char c in packet)
                {
                    if (packet.Count(x => x == c) == 1)
                        counter++;
                    else
                        counter = 0;
                }

                if (counter == markerLength)
                    return i + markerLength;

                counter = 0;
            }

            return 0;


        }
    }
}