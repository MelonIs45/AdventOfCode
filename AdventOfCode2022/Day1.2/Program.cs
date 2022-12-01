
namespace Day1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            string[] split = string.Join(",", input).Split(",,");
            List<int> nums = new List<int>();

            foreach (string s in split)
            {
                int total = 0;

                foreach (string num in s.Split(","))
                    total += int.Parse(num);

                nums.Add(total);
            }

            nums.Sort();
            int count = nums.Count;
            Console.WriteLine(nums[count - 1] + nums[count - 2] + nums[count - 3]);
        }
    }
}